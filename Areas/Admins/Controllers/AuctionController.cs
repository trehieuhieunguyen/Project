using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Extensions;
using Project.Models;
using Project.Models.Dtos;
using Project.Repositorys;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Areas.Admins.Controllers
{
    [Area("Admins")]
    [Authorize(Roles = UserRole.Admin + "," + UserRole.Moderate)]
    public class AuctionController : Controller
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public AuctionController(IAuctionRepository auctionRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Auction> model = await _auctionRepository.GetAuctionForAdmins();
            List<AuctionDtos> result = _mapper.Map<List<AuctionDtos>>(model);

            return await Task.Run(() => View(result));
        }
        
        public async Task<IActionResult> GetAllProductAdmin()
        {
            IEnumerable<Auction> auctions = await _auctionRepository.GetAllAuctionsForAdmin();
            List<AuctionDtos> auctionDtos = _mapper.Map<List<AuctionDtos>>(auctions);
            return await Task.Run(() => View(auctionDtos));
        }
        [HttpPost, ActionName("ConfirmSp")]
        public async Task<IActionResult> CofirmSp(AuctionDtos auctionDtos)
        {
            Auction sp = await _auctionRepository.GetAuctionForAdmin(auctionDtos.Id);
            sp.Product_Status = auctionDtos.Product_Status;
            _auctionRepository.UpdateAuction(sp);
            return await Task.Run(() => RedirectToAction("Index","Auction", new { area = "Admins" }));
        }

        [HttpPost, ActionName("DeleteSp")]
        public async Task<IActionResult> DeleteSp(string id)
        {
            Auction sp = await _auctionRepository.GetAuctionForAdmin(id);
            var webroot = _webHostEnvironment.WebRootPath;
            var FileName = sp.AuctionImgFolder;
            var filePath = Path.Combine(webroot + @"\images\Product\", $"{FileName}");
            DirectoryInfo di = new DirectoryInfo(filePath);
            if (Directory.Exists(filePath))
            {
                Directory.Delete(filePath, true);
            }
            _auctionRepository.DeleteAuction(sp);
            return await Task.Run(() => RedirectToAction("Index", "Auction", new { area = "Admins" }));
        }
        public async Task<IActionResult> Edit(string id)
        {
            Auction product =await _auctionRepository.GetIdProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<UpdateAuctionDtos>(product);
            return await Task.Run(() => View(result));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAuctionDtos auctionDtos)
        {
            Auction auction = await _auctionRepository.GetIdProduct(auctionDtos.Id);
            if (auction == null)
            {
                return NotFound();
            }
            UpdateAuctionDtos updatenewDtos = new UpdateAuctionDtos();
            _mapper.Map(auction, updatenewDtos);
            updatenewDtos.Time_End = auctionDtos.Time_End;
            updatenewDtos.Time_Start= auctionDtos.Time_Start;
            _mapper.Map(updatenewDtos, auction);

            if (!_auctionRepository.UpdateAuction(auction))
            {
                ModelState.AddModelError("", $"Something went wrong when Updating the record");
                return StatusCode(500, ModelState);
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        public async Task<IActionResult> GetAuctionWinnerAdmin()
        {
            IEnumerable<AuctionWinner> auctionWinners = await _auctionRepository.GetAuctionWinnerForAdmin();
            var result = _mapper.Map<List<AuctionWinnerDtos>>(auctionWinners);
           
            return await Task.Run(() => View(result));
        }
        [HttpPost, ActionName("ConfirmOrder")]
        public async Task<IActionResult> ConfirmOrder(AuctionWinnerDtos auctionWinnerDtos)
        {
            AuctionWinner auction =await _auctionRepository.GetAuctionWinnerForAdmin(auctionWinnerDtos.auctionId);
            auction.DeliveryStatus = auctionWinnerDtos.DeliveryStatus;
            _auctionRepository.UpdateAuctionWinner(auction);
            return await Task.Run(() => RedirectToAction("GetAuctionWinnerAdmin", "Auction", new { area = "Admins" }));
        }
        [HttpPost, ActionName("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(AuctionWinnerDtos auctionWinnerDtos)
        {
            AuctionWinner auction = await _auctionRepository.GetAuctionWinnerForAdmin(auctionWinnerDtos.auctionId);
            auction.DateRequired = auctionWinnerDtos.DateRequired;
            _auctionRepository.UpdateAuctionWinner(auction);
            return await Task.Run(() => RedirectToAction("GetAuctionWinnerAdmin", "Auction", new { area = "Admins" }));
        }
    }
}
