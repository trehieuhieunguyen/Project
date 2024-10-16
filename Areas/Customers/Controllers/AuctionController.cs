using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Models.Dtos;
using Project.Models.ViewModels;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Extensions;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Org.BouncyCastle.Cms;
using Project.Repositorys;

namespace Project.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize(Roles = UserRole.Customers)]
    [Route("[controller]/")]
    public class AuctionController : Controller
    {
        [BindProperty]
        public List<string> ImageList { get; set; }
        private readonly IAuctionRepository _auctionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public AuctionController(IAuctionRepository auctionRepository,IUserRepository userRepository,IMessageRepository messageRepository, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [Route("GetAuctions")]
        public async Task<JsonResult> GetAuctions([FromQuery(Name = ("input"))] string input, [FromQuery(Name = ("loai"))] int? ProductType, [FromQuery(Name = ("time"))] int time, [FromQuery (Name = ("skip"))] int skip, [FromQuery(Name = ("order"))] bool order)
        {
            List<ListAuctionAndCountTop> list = new List<ListAuctionAndCountTop>();
            IEnumerable<Auction> model = await _auctionRepository.GetAuctions(input, ProductType, time, skip,order);
            ShowCountAndTop1 CountAndTop;

            foreach (var item in model)
            {
                CountAndTop = await _auctionRepository.GetCountAndTop(item.Id);
                list.Add(new ListAuctionAndCountTop { auction = item, Count = CountAndTop.Count, Top1 = CountAndTop.Top1 });
            }

            ShowFindAuctionVM npmlist = new ShowFindAuctionVM()
            {
                list = list
            };
            
            return await Task.Run(() => Json(npmlist));
        }

        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> Index([FromQuery(Name = ("input"))] string input, [FromQuery(Name = ("loai"))] int? ProductType, [FromQuery(Name = ("time"))] int? time, [FromQuery(Name = ("order"))] bool order)
        {
            List<ListAuctionAndCountTop> list = new List<ListAuctionAndCountTop>();
            IEnumerable<Auction> model = await _auctionRepository.GetAuctions(input, ProductType, time, 0, order);
            ShowCountAndTop1 CountAndTop;
            
            foreach (var item in model)
            {
                CountAndTop = await _auctionRepository.GetCountAndTop(item.Id);
                list.Add(new ListAuctionAndCountTop { auction = item, Count = CountAndTop.Count, Top1 = CountAndTop.Top1 });
            }

            ShowFindAuctionVM npmlist = new ShowFindAuctionVM()
            {
                list = list
            };

            //List<AuctionDtos> result = _mapper.Map<List<AuctionDtos>>(model);
            return await Task.Run(() => View(npmlist));
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            IEnumerable<AuctionCategory> npList = await _auctionRepository.GetAuctionCategorys();
            AuctionVM viewModel = new AuctionVM()
            {
                AuctionCateList = npList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                auction = new Auction()

            };

            return await Task.Run(() => View(viewModel));
        }
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuctionVM auctionVM)
        {
            if (!ModelState.IsValid)
                return BadRequest("Nhập dữ liệu sai");

            if(auctionVM.auction.Time_Start > auctionVM.auction.Time_End)
                return BadRequest("Ngày bắt đầu không thể nhỏ hơn ngày kết thúc");

            if (auctionVM.auction.Price_Start >= auctionVM.auction.Price_End)
                return BadRequest("Giá đầu không được nhỏ hơn giá chốt");

            var webroot = _webHostEnvironment.WebRootPath;
            var GetNameFile = CreateGuildNameExtensions.NameFileNewGuild();
            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var filePath = Path.Combine(webroot + @"\images\Product\", $"{GetNameFile}");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string destinationUserImage;
                string imgFirst = "";
                foreach (var file in files)
                {
                    destinationUserImage = Path.Combine(webroot, @$"images\Product\{GetNameFile}\", file.FileName);
                    await using (var fileStream = new FileStream(destinationUserImage, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    if (files.First() == file)
                    {
                        imgFirst = @$"\images\Product\{GetNameFile}\{file.FileName}";
                    }
                }
                auctionVM.auction.Feature_Img = imgFirst;
                auctionVM.auction.AuctionImgFolder = GetNameFile;
            }

            var GetNameAuction = CreateGuildNameExtensions.NameFileNewGuild();
            CreateAuctionDtos createAuctionDtos = new CreateAuctionDtos(){};

            auctionVM.auction.Id = GetNameAuction;
            auctionVM.auction.Product_Status = false;
            auctionVM.auction.ApplicationUserId = User.GetUserId();
            auctionVM.auction.Product_StatusBuy = false;

            _mapper.Map(auctionVM.auction, createAuctionDtos);

            var CreateAuctionObj = _mapper.Map<Auction>(createAuctionDtos);
            if (!_auctionRepository.CreateAuction(CreateAuctionObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {CreateAuctionObj.ProductName}");
                return StatusCode(500, ModelState);
            }
            return await Task.Run(() => RedirectToAction("Details", "Auction", new { area = "Customers", id = GetNameAuction }));
        }
        [AllowAnonymous]
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return RedirectToAction("Index","Home");

            Auction model = await _auctionRepository.GetAuction(id);
            if(model == null)
                return RedirectToAction("Index", "Home");

            if (model.Product_StatusBuy == false) { 
                if(model.Time_End < DateTime.Now)
                {
                    Message message = await _messageRepository.GetWinner(model.Id);
                    
                    if(message != null)
                    {

                        if (!_auctionRepository.AuctionIdExistWinner(message.RecipientId)) { 
                        
                            model.Product_StatusBuy = true;
                            model.Time_End = DateTime.Now;
                            _auctionRepository.UpdateAuction(model);


                            CreateAuctionWinnerDtos auctionWinnerDtos = new CreateAuctionWinnerDtos()
                            {
                                auctionId = message.RecipientId,
                                applicationUserId = message.SenderId,
                                MessageId = message.Id,
                                DeliveryStatus = false
                            };


                            var result1 = _mapper.Map<AuctionWinner>(auctionWinnerDtos);

                            _auctionRepository.CreateAuctionWinner(result1);
                            ApplicationUser backcoin = await _userRepository.getUserByIdAsync(model.ApplicationUserId);
                            backcoin.coin += (message.Content * 0.95);
                            _userRepository.UpdateAppUser(backcoin);
                        }
                    }

                }
            }

            if (model == null)
                return RedirectToAction("Index", "Home");

            if (model.Product_Status == false)
            {
                if(model.ApplicationUserId != User.GetUserId())
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            var root = _webHostEnvironment.WebRootPath;
            var provider = new PhysicalFileProvider(root);
            var contents = provider.GetDirectoryContents(Path.Combine("images/Product", model.AuctionImgFolder));
            var objFiles = contents.OrderBy(m => m.LastModified);

            ImageList = new List<string>();
            foreach (var item in objFiles.ToList())
            {
                ImageList.Add(item.Name);
            }
            ViewBag.Showimage = ImageList;
            var result = _mapper.Map<AuctionDtos>(model);
            return await Task.Run(() => View(result));
        }
       
        [Route("GetAutionUser")]
        public async Task<IActionResult> GetAutionUser() {
            IEnumerable<AuctionDtos> auction = await _auctionRepository.GetAuctionsUser(User.GetUserId());
            
            //var result = _mapper.Map<List<AuctionDtos>>(auction);
            return await Task.Run(() => View(auction));
            
        }
        [Route("GetMyAuctionUser")]
        public async Task<IActionResult> GetMyAuctionUser()
        {
            IEnumerable<Message> messageDtos = await _auctionRepository.GetMyAuctionUser(User.GetUserId());
            var result =  _mapper.Map<List<MessageMyAuctionDtos>>(messageDtos);
            return await Task.Run(() => View(result));
            
        }
        [Route("GetMyAuctionWinner")]
        public async Task<IActionResult> GetMyAuctionWinner()
        {
            IEnumerable<AuctionWinner> auctionWinners = await _auctionRepository.GetMyAuctionWinner(User.GetUserId());
            var result= _mapper.Map<List<AuctionWinnerDtos>>(auctionWinners);
            
            return await Task.Run(() => View(result));
        }
        [Route("Edit/{id?}")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IEnumerable<AuctionCategory> npList = await _auctionRepository.GetAuctionCategorys();
            Auction auction = await _auctionRepository.GetAuction(id);

            if (auction.ApplicationUserId != User.GetUserId())
            {
                return RedirectToAction("Index", "Home");
            }

            DateTime TimeNow = DateTime.Now;

            if (auction.Time_Start < TimeNow)
            {
                return BadRequest("không thể cập nhập , bởi vì phiên đấu giá đã bắt đầu!");
            }

            if (auction == null)
            {
                return NotFound();
            }


            AuctionVM viewModel = new AuctionVM()
            {
                AuctionCateList = npList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                auction = auction

            };

            var root = _webHostEnvironment.WebRootPath;
            var provider = new PhysicalFileProvider(root);
            var contents = provider.GetDirectoryContents(Path.Combine("images/Product", viewModel.auction.AuctionImgFolder));
            var objFiles = contents.OrderBy(m => m.LastModified);
            var NameFouldImg = viewModel.auction.AuctionImgFolder;
            ImageList = new List<string>();
            foreach (var item in objFiles.ToList())
            {
                ImageList.Add("/images/Product/" + NameFouldImg + "/" + item.Name);
            }
            ViewBag.Showimage = ImageList;
            return await Task.Run(() => View(viewModel));
        }

        [Route("Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuctionVM auctionVM)
        {
            if (!ModelState.IsValid) { 
                TempData["Status"] = "Cập nhập thất bại do điền sai dữ liệu!";
                return BadRequest();
            }

            Auction auction = await _auctionRepository.GetAuction(auctionVM.auction.Id);
            DateTime TimeNow = DateTime.Now;
            if (auction.ApplicationUserId != User.GetUserId())
                return RedirectToAction("Index", "Home");

            if (auction.Time_End < TimeNow)
                return BadRequest("không thể cập nhập , bởi vì phiên đấu giá đã bắt đầu!");

            if (auctionVM.auction.Time_Start > auctionVM.auction.Time_End)
                return BadRequest("Ngày bắt đầu không thể nhỏ hơn ngày kết thúc");

            if (auctionVM.auction == null)
                return BadRequest(ModelState);

            if (!_auctionRepository.AuctionIdExist(auctionVM.auction.Id))
            {
                ModelState.AddModelError("", "Auction not Exists!");
                return StatusCode(404, ModelState);
            }

            var webroot = _webHostEnvironment.WebRootPath;
            var FileName = auction.AuctionImgFolder;
            var files = Request.Form.Files;

            if (files.Count > 0)
            {
                //string extension = Path.GetExtension(files[0].FileName);
                var filePath = Path.Combine(webroot + @"\images\Product\", $"{FileName}");
                DirectoryInfo di = new DirectoryInfo(filePath);
                if (Directory.Exists(filePath))
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                }
                string destinationUserImage;
                string imgFirst = "";
                foreach (var file in files)
                {
                    destinationUserImage = Path.Combine(webroot, @$"images\Product\{FileName}\", file.FileName);
                    await using (var fileStream = new FileStream(destinationUserImage, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    if (files.First() == file)
                    {
                        imgFirst = @$"\images\Product\{FileName}\{file.FileName}";
                    }
                }
                auctionVM.auction.Feature_Img = imgFirst;
            }
            else
            {
                auctionVM.auction.Feature_Img = auction.Feature_Img;
            }

            UpdateAuctionDtos updateAuctionDtos = new UpdateAuctionDtos(){};
            auctionVM.auction.Product_StatusBuy = false;

            _mapper.Map(auctionVM.auction, updateAuctionDtos);

            updateAuctionDtos.Product_Status = false;
            updateAuctionDtos.Id = auction.Id;
            updateAuctionDtos.ApplicationUserId = auction.ApplicationUserId;
            updateAuctionDtos.AuctionImgFolder = auction.AuctionImgFolder;


            _mapper.Map(updateAuctionDtos, auction);


            if (!_auctionRepository.UpdateAuction(auction))
            {
                ModelState.AddModelError("", $"Something went wrong when Updating the record {auction.ProductName}");
                return StatusCode(500, ModelState);
            }
            TempData["Status"] = "Cập nhập thành công!";
            return await Task.Run(() => RedirectToAction("Edit", "Auction", new { area = "Customers", id = auctionVM.auction.Id }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return await Task.Run(() => View());

            if (!_auctionRepository.AuctionIdExist(id))
            {
                return NotFound();
            }

            Auction AuctionObj = await _auctionRepository.GetAuction(id);

            if (AuctionObj.ApplicationUserId != User.GetUserId())
            {
                return BadRequest(ModelState);
            }

            if (AuctionObj.Time_Start < DateTime.Now)
            {
                return BadRequest(ModelState);
            }

            if (!_auctionRepository.DeleteAuction(AuctionObj))
            {
                ModelState.AddModelError("", $"Something went wrong when Remove the record {AuctionObj.ProductName}");
                return StatusCode(500, ModelState);
            }
            var webroot = _webHostEnvironment.WebRootPath;
            var FileName = AuctionObj.AuctionImgFolder;
            var filePath = Path.Combine(webroot + @"\images\Product\", $"{FileName}");
            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath, true);
            }

            return await Task.Run(() => RedirectToAction("Index", "Home", new { area = "Customers" }));
        }
    }
}
