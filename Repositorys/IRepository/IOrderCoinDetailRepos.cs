using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys.IRepository
{
    public interface IOrderCoinDetailRepos
    {
        Task<ICollection<OrderCoinDetail>> GetOrderHearInOrderDe(string hearId, string id);
        Task<ICollection<OrderCoinHeader>> GetHearDetais(string id);
        Task<ICollection<GroupOrderDetails>> GetDetails(string id);
        Task<ICollection<GroupOrderDetails>> GetOrderCoinForAdminDetails();
        bool OderDetailsIdExist(int id);
        bool CreateOderDetails(OrderCoinDetail orderDetails);
        bool UpdateOderDetails(OrderCoinDetail orderDetails);
        bool DeleteOderDetails(OrderCoinDetail orderDetails);
        bool Save();
    }
}
