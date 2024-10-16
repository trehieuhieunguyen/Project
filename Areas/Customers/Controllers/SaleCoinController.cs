using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Extensions;
using Project.Models;
using Project.Models.Dtos;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize(Roles = UserRole.Customers)]
    [Route("[controller]/")]
    public class SaleCoinController : Controller
    {
        private readonly ISaleCoinRepository _saleCoinRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public SaleCoinController(ISaleCoinRepository saleCoinRepository, IUserRepository userRepository, IMapper mapper)
        {
            _saleCoinRepository = saleCoinRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            return await Task.Run(() => View());
        }
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleCoinDtos saleCoinDtos)
        {
            if (!ModelState.IsValid)
                return await Task.Run(() => View());

            if (saleCoinDtos == null)
            {
                return BadRequest(ModelState);
            }
            var username = await _userRepository.getUserByIdAsync(User.GetUserId());

            if(saleCoinDtos.amountcoin < 0 || saleCoinDtos.amountcoin > username.coin) {
                return BadRequest();
            }

            var SaleCoinOrderId = Guid.NewGuid();

            SaleCoinDtos model = new SaleCoinDtos
            {
                Id = SaleCoinOrderId.ToString(),
                PaypalEmail = saleCoinDtos.PaypalEmail,
                amountcoin = saleCoinDtos.amountcoin,
                TransactionStatus = false,
                ApplicationUserId = User.GetUserId()
            };

            var SaleCoinObj = _mapper.Map<SaleCoin>(model);

            if (!_saleCoinRepository.CreateSaleCoin(SaleCoinObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record");
                return StatusCode(500, ModelState);
            }

            username.coin -= Convert.ToInt32(model.amountcoin);

            if (!_userRepository.UpdateAppUser(username))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record");
                return StatusCode(500, ModelState);
            }

            return await Task.Run(() => RedirectToAction("Details",new { id = SaleCoinOrderId}));
        }
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(string id)
        {
            var model = await _saleCoinRepository.GetSaleCoin(id, User.GetUserId());

            var result = _mapper.Map<SaleCoinDtos>(model);
            return await Task.Run(() => View(result));
        }
        [Route("History")]
        public async Task<IActionResult> History()
        {
            var model = await _saleCoinRepository.GetSaleCoins(User.GetUserId());

            var result = _mapper.Map<List<SaleCoinDtos>>(model);
            return await Task.Run(() => View(result));
        }
    }
}
