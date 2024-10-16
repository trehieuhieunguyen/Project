using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Extensions;
using Project.Helpers;
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
    public class CoinController : Controller
    {
        private readonly ICoinRepository _coinRepos;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public CoinController(ICoinRepository coinRepos, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _coinRepos = coinRepos;
            _userManager = userManager;
            _mapper = mapper;
        }
        [Route("")]
        public async Task<IActionResult> Index()
        {
            if (SessionHelper.GetObjectFromJson<List<OrderCoinDetail>>(HttpContext.Session, "cart") == null)
            {
                return await Task.Run(() => RedirectToAction("BuyCoin", "Coin", new { area = "Customers" }));
            }

            return await Task.Run(() => View());
        }
        
        [Route("BuyCoin")]
        public async Task<IActionResult> BuyCoin()
        {
            var username = await _userManager.FindByNameAsync(User.GetUsername());
            bool TwoFactorCheck = await _userManager.GetTwoFactorEnabledAsync(username);

            //if (TwoFactorCheck is false)
            //{
            //    TempData["NotiTwo"] = "Vì lý do bảo mật, bạn cần xác minh bảo bật 2 bước mới có thể mua được xu";
            //    return await Task.Run(() => RedirectToAction("Manage", "Account", new { area = "Identity", id = "TwoFactorAuthentication" }));
            //}
            ICollection<CoinProducts> model = await _coinRepos.GetCoins();

            return await Task.Run(() => View(_mapper.Map<List<CoinPoductsDtos>>(model)));
        }
        [Route("Buy")]
        public async Task<IActionResult> Buy(int id)
        {

            if (SessionHelper.GetObjectFromJson<List<OrderCoinDetail>>(HttpContext.Session, "cart") == null)
            {
                List<OrderCoinDetail> cart = new List<OrderCoinDetail>();
                cart.Add(new OrderCoinDetail { CoinProducts = await _coinRepos.GetCoin(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<OrderCoinDetail> cart = SessionHelper.GetObjectFromJson<List<OrderCoinDetail>>(HttpContext.Session, "cart");
                int index = await isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new OrderCoinDetail { CoinProducts = await _coinRepos.GetCoin(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        [Route("BuyCoinByInput")]
        public async Task<IActionResult> BuyCoinByInput()
        {
            var username = await _userManager.FindByNameAsync(User.GetUsername());
            bool TwoFactorCheck = await _userManager.GetTwoFactorEnabledAsync(username);

            //if (TwoFactorCheck is false)
            //{
            //    TempData["NotiTwo"] = "Vì lý do bảo mật, bạn cần xác minh bảo bật 2 bước mới có thể mua được xu";
            //    return await Task.Run(() => RedirectToAction("Manage", "Account", new { area = "Identity", id = "TwoFactorAuthentication" }));
            //}
            CoinProducts model = await _coinRepos.GetCoin(7);

            return await Task.Run(() => View(_mapper.Map<CoinPoductsDtos>(model)));
        }
        [Route("BuyMul")]
        public async Task<IActionResult> BuyMul(int id, double price)
        {

            if (SessionHelper.GetObjectFromJson<List<OrderCoinDetail>>(HttpContext.Session, "cart") == null)
            {
                List<OrderCoinDetail> cart = new List<OrderCoinDetail>();
                cart.Add(new OrderCoinDetail { CoinProducts = await _coinRepos.GetCoin(id), Quantity = 1 ,Price = price });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<OrderCoinDetail> cart = SessionHelper.GetObjectFromJson<List<OrderCoinDetail>>(HttpContext.Session, "cart");
                int index = await isExist(id);
                if (index != -1)
                {
                    cart[index].Price += price;
                }
                else
                {
                    cart.Add(new OrderCoinDetail { CoinProducts = await _coinRepos.GetCoin(id), Quantity = 1, Price = price });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        [Route("Remove")]
        public async Task<IActionResult> Remove(int id)
        {
            List<OrderCoinDetail> cart = SessionHelper.GetObjectFromJson<List<OrderCoinDetail>>(HttpContext.Session, "cart");
            int index = await isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private Task<int> isExist(int id)
        {
            List<OrderCoinDetail> cart = SessionHelper.GetObjectFromJson<List<OrderCoinDetail>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].CoinProducts.Id.Equals(id))
                    return Task.FromResult(i);
            return Task.FromResult(-1);
        }
    }
}
