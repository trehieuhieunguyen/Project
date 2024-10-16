using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Extensions;
using Project.Models;
using Project.Models.Dtos;
using Project.Models.ViewModels;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize(Roles = UserRole.Customers)]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;
        public HomeController(IUserRepository userRepository,IAuctionRepository auctionRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            List<int> counts = new List<int>();
            int result;
            for (int i = 1; i <= 6; i++)
            {
                result = await _auctionRepository.GetCountAuctions(i);
                counts.Add(result);
            }

            List<IEnumerable<Auction>> myList = new List<IEnumerable<Auction>>();
            IEnumerable<Auction> test;
            for (int i = 1; i <= 6; i++)
            {
                test = await _auctionRepository.GetAuctions(i);
                myList.Add(test);
            };
            

            IEnumerable<Auction> npList = await _auctionRepository.GetAuctions(1); ;

            ShowAuctionVM viewModel = new ShowAuctionVM()
            {
                ListCount = counts,
                auction = myList

            };
            return await Task.Run(() => View(viewModel));
        }

        [HttpGet]
        //[Route("[controller]/")]
        [Route("[controller]/GetCoins")]
        public async Task<IActionResult> GetCoins()
        {
            var category = await _userRepository.getUserByEmailAsync(User.GetEmail());
            if (category == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ShowCoinDtos>(category);
            return await Task.Run(() => Json(objDto));
        }
        [AllowAnonymous]
        public IActionResult chitiet()
        {
            return View();
        }

    }
}
