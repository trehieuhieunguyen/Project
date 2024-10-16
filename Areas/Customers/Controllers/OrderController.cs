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
    public class OrderController : Controller
    {
        private readonly IOrderCoinDetailRepos _orderDetailRepos;
        private readonly IMapper _mapper;
        public OrderController(IOrderCoinDetailRepos orderDetailRepos, IMapper mapper)
        {
            _orderDetailRepos = orderDetailRepos;
            _mapper = mapper;
        }
        [Route("OrderHistory")]
        public async Task<IActionResult> OrderHistory()
        {
            ICollection<GroupOrderDetails> model = await _orderDetailRepos.GetDetails(User.GetUserId());

            var result = _mapper.Map<List<GroupOrderDetailsDtos>>(model);

            return await Task.Run(() => View(result));
        }
        [Route("OrderDetails/{id?}")]
        public async Task<IActionResult> OrderDetails(string id)
        {
            ICollection<OrderCoinDetail> model = await _orderDetailRepos.GetOrderHearInOrderDe(id, User.GetUserId());
            if (model.Count == 0)
            {
                return RedirectToAction("OrderHistory");
            }

            List<OrderCoinDetailDtos> ReverseMapModel = new List<OrderCoinDetailDtos>();
            foreach (var obj in model)
            {
                ReverseMapModel.Add(_mapper.Map<OrderCoinDetailDtos>(obj));
            }
            return await Task.Run(() => View(ReverseMapModel));
        }

    }
}
