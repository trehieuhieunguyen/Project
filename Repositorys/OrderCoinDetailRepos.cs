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
    public class OrderCoinDetailRepos : IOrderCoinDetailRepos
    {
        private readonly ApplicationDbContext _db;

        public OrderCoinDetailRepos(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateOderDetails(OrderCoinDetail orderDetails)
        {
            _db.orderCoinDetails.Add(orderDetails);
            return Save();
        }

        public bool DeleteOderDetails(OrderCoinDetail orderDetails)
        {
            _db.orderCoinDetails.Remove(orderDetails);
            return Save();
        }

        public bool OderDetailsIdExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateOderDetails(OrderCoinDetail orderDetails)
        {
            _db.orderCoinDetails.Add(orderDetails);
            return Save();
        }

        public async Task<ICollection<OrderCoinDetail>> GetOrderHearInOrderDe(string hearId, string id)
        {
            return await _db.orderCoinDetails.Where(i => i.OrderHeader.ApplicationUser.Id == id)
                .Include(c => c.CoinProducts).Include(c => c.OrderHeader.ApplicationUser)
                .Where(c => c.OrderHeaderId == hearId).OrderBy(c => c.CoinProductsId).ToListAsync();
        }

        public async Task<ICollection<OrderCoinHeader>> GetHearDetais(string id)
        {
            return await _db.orderCoinHeaders
                .Where(i => i.ApplicationUser.Id == id)
                .Include(c => c.ApplicationUser)
                .OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<ICollection<GroupOrderDetails>> GetDetails(string id)
        {
            ICollection<OrderCoinDetail> result = await _db.orderCoinDetails
                .Where(i => i.OrderHeader.ApplicationUser.Id == id)
                .Include(c => c.CoinProducts)
                .Include(c => c.OrderHeader.ApplicationUser)
                .OrderBy(a => a.Id).ToListAsync();

            List<GroupOrderDetails> icollection = new List<GroupOrderDetails>();

            foreach (var items in result)
            {
                icollection.Add(new GroupOrderDetails { 
                    Name = items.OrderHeaderId, 
                    Quatity = items.Quantity, 
                    total = (items.Price * items.Quantity), 
                    LastName = items.OrderHeader.ApplicationUser.LastName, 
                    FirstName = items.OrderHeader.ApplicationUser.FirtsName, 
                    PaymentStatus = items.OrderHeader.PaymentStatus, 
                    DateCreate = items.OrderHeader.Date });
            }

            var NameGroupDetails = icollection.GroupBy(t => t.Name)
                           .Select(t => new
                           {
                               Name = t.Key,
                               Total = t.Sum(ta => ta.total),
                               Quatity = t.Sum(ta => ta.Quatity),
                               LastName = t.First().LastName,
                               FirstName = t.First().FirstName,
                               DateTime = t.First().DateCreate,
                               PaymentStatus = t.First().PaymentStatus
                           }).OrderByDescending(o => o.DateTime).ToList();
            icollection.Clear();
            foreach (var items in NameGroupDetails)
            {
                icollection.Add(new GroupOrderDetails { Name = items.Name, 
                    Quatity = items.Quatity, 
                    total = items.Total, 
                    LastName = items.LastName, 
                    FirstName = items.FirstName, 
                    PaymentStatus = items.PaymentStatus, 
                    DateCreate = items.DateTime });
            }

            return icollection;
        }

        public async Task<ICollection<GroupOrderDetails>> GetOrderCoinForAdminDetails()
        {
            ICollection<OrderCoinDetail> result = await _db.orderCoinDetails
                .Where(i => i.OrderHeader.PaymentStatus == true)
                .Include(c => c.CoinProducts)
                .Include(c => c.OrderHeader.ApplicationUser)
                .OrderBy(a => a.Id).ToListAsync();

            List<GroupOrderDetails> icollection = new List<GroupOrderDetails>();

            foreach (var items in result)
            {
                icollection.Add(new GroupOrderDetails
                {
                    Name = items.OrderHeaderId,
                    Quatity = items.Quantity,
                    total = (items.Price * items.Quantity),
                    LastName = items.OrderHeader.ApplicationUser.LastName,
                    FirstName = items.OrderHeader.ApplicationUser.FirtsName,
                    PaymentStatus = items.OrderHeader.PaymentStatus,
                    DateCreate = items.OrderHeader.Date
                });
            }

            var NameGroupDetails = icollection.GroupBy(t => t.Name)
                           .Select(t => new
                           {
                               Name = t.Key,
                               Total = t.Sum(ta => ta.total),
                               Quatity = t.Sum(ta => ta.Quatity),
                               LastName = t.First().LastName,
                               FirstName = t.First().FirstName,
                               DateTime = t.First().DateCreate,
                               PaymentStatus = t.First().PaymentStatus
                           }).OrderByDescending(o => o.DateTime).ToList();
            icollection.Clear();
            foreach (var items in NameGroupDetails)
            {
                icollection.Add(new GroupOrderDetails
                {
                    Name = items.Name,
                    Quatity = items.Quatity,
                    total = items.Total,
                    LastName = items.LastName,
                    FirstName = items.FirstName,
                    PaymentStatus = items.PaymentStatus,
                    DateCreate = items.DateTime
                });
            }

            return icollection;
        }
    }
}
