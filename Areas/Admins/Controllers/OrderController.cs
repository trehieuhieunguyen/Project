using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using Project.Models;
using Project.Models.Dtos;
using Project.Repositorys;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Areas.Admins.Controllers
{
    [Area("Admins")]
    [Authorize(Roles = UserRole.Admin + "," + UserRole.Moderate)]
    public class OrderController : Controller
    {
        private readonly IOrderCoinDetailRepos _orderCoinDetailRepos;
        private readonly IUserRepository _userRepository;
        private readonly ISaleCoinRepository _saleCoinRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderCoinDetailRepos orderCoinDetailRepos, IUserRepository userRepository, ISaleCoinRepository saleCoinRepository, IMapper mapper)
        {
            _orderCoinDetailRepos = orderCoinDetailRepos;
            _userRepository = userRepository;
            _saleCoinRepository = saleCoinRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<GroupOrderDetails> model = await _orderCoinDetailRepos.GetOrderCoinForAdminDetails();
            var result = _mapper.Map<List<GroupOrderDetailsDtos>>(model);
            return await Task.Run(() => View(result));
        }

        public async Task<IActionResult> OrderSaleCoins()
{
            IEnumerable<SaleCoin> salecoin = await _saleCoinRepository.GetSaleCoinsAdmin();

            var objDto = _mapper.Map<List<SaleCoinDtos>>(salecoin);

            return await Task.Run(() => View(objDto));
        }

        [HttpPost, ActionName("OrderSaleCoins")]
        public async Task<IActionResult> CofirmUpdateSaleCoin(UpdateSaleCoinDtos updateSaleCoin)
        {
            SaleCoin salecoin = await _saleCoinRepository.GetSaleCoinForAdmin(updateSaleCoin.Id);
            updateSaleCoin.DateCreate = salecoin.DateCreate;
            updateSaleCoin.amountcoin = salecoin.amountcoin;
            updateSaleCoin.PaypalEmail = salecoin.PaypalEmail;
            updateSaleCoin.ApplicationUserId = salecoin.ApplicationUserId;
            updateSaleCoin.TransactionStatus = true;
            if (updateSaleCoin == null)
            {
                return BadRequest(ModelState);
            }

            var SaleCoinObj = _mapper.Map<SaleCoin>(updateSaleCoin);

            if (!_saleCoinRepository.UpdateSaleCoin(SaleCoinObj))
            {
                ModelState.AddModelError("", $"Something went wrong when Updating the record");
                return StatusCode(500, ModelState);
            }
            return await Task.Run(() => RedirectToAction(nameof(OrderSaleCoins)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteSaleCoin(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_saleCoinRepository.SaleCoinIdExist(id))
            {
                return NotFound();
            }

            SaleCoin SaleCoinObj = await _saleCoinRepository.GetSaleCoinForAdmin(id);
            if (!_saleCoinRepository.DeleteSaleCoin(SaleCoinObj))
            {
                ModelState.AddModelError("", $"Something went wrong when Remove the record {SaleCoinObj.Id}");
                return StatusCode(500, ModelState);
            }

            ApplicationUser userupdate = await _userRepository.getUserByIdAsync(SaleCoinObj.ApplicationUserId);
            userupdate.coin += SaleCoinObj.amountcoin;
            _userRepository.UpdateAppUser(userupdate);

            return await Task.Run(() => RedirectToAction(nameof(OrderSaleCoins)));
        }
    }
}
