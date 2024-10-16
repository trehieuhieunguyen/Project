using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys.IRepository
{
    public interface IOrderCoinHeaderRepos
    {
        Task<ICollection<OrderCoinHeader>> GetOrderHeaders();
        Task<OrderCoinHeader> GetOrderHeader(string HeaderId);
        bool OderHeaderIdExist(int id);
        bool CreateOderHeader(OrderCoinHeader orderHeader);
        bool UpdateOderHeader(OrderCoinHeader orderHeader);
        bool DeleteOderHeader(OrderCoinHeader orderHeader);
        bool Save();
    }
}
