using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositorys.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys
{
    public class OrderCoinHeaderRepos : IOrderCoinHeaderRepos
    {
        private readonly ApplicationDbContext _db;

        public OrderCoinHeaderRepos(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateOderHeader(OrderCoinHeader orderHeader)
        {
            _db.orderCoinHeaders.Add(orderHeader);
            return Save();
        }

        public bool DeleteOderHeader(OrderCoinHeader orderHeader)
        {
            _db.orderCoinHeaders.Remove(orderHeader);
            return Save();
        }

        public async Task<OrderCoinHeader> GetOrderHeader(string HeaderId)
        {
            return await _db.orderCoinHeaders.FirstOrDefaultAsync(find => find.Id == HeaderId);
        }

        public async Task<ICollection<OrderCoinHeader>> GetOrderHeaders()
        {
            return await _db.orderCoinHeaders.OrderBy(a => a.Id).ToListAsync();
        }

        public bool OderHeaderIdExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateOderHeader(OrderCoinHeader orderHeader)
        {
            _db.orderCoinHeaders.Update(orderHeader);
            return Save();
        }
    }
}
