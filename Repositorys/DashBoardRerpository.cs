using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Models.DtosForAdmin;
using Project.Models.DtosForAdmin.ViewModels;
using Project.Repositorys.IRepository;
using Project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys
{
    public class DashBoardRerpository : IDashBoardRespository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _managerUser;
        public DashBoardRerpository(ApplicationDbContext applicationDb, UserManager<IdentityUser> userManager)
        {
            _db = applicationDb;
            _managerUser = userManager;
        }


        public async Task<int> GetCountProduct()
        {
            return await _db.Auctions.CountAsync();
        }

        public async Task<int> GetCountUser()
        {
            var result = 0;
            var users = await _managerUser.Users.ToListAsync();

            foreach (var user in users)
            {
                var role = await _managerUser.GetRolesAsync(user);
                if (role.Contains(UserRole.Customers))
                {
                    result = await _db.applicationUser.CountAsync();
                }
            }
            return result;
        }

        public async Task<int> GetCountUserOneMonth(int i)
        {
            var currYear = DateTime.Now.Year;

            var firstOfThisMonth = new DateTime(currYear, i, 1);
            var firstOfNextMonth = firstOfThisMonth.AddMonths(1);

            //totalSMS =await (from x in _db.Auctions
            //                where x.Time_End >= firstOfThisMonth && x.Time_Start < firstOfNextMonth
            //                select x).CountAsync();
            var totalSMS = await _db.Auctions.Where(x => x.DateCreated >= firstOfThisMonth && x.DateCreated < firstOfNextMonth).CountAsync();

            return totalSMS;
        }
        public async Task<double> GetCountCoinOneMonth(int i)
        {
            var currYear = DateTime.Now.Year;

            var firstOfThisMonth = new DateTime(currYear, i, 1);
            var firstOfNextMonth = firstOfThisMonth.AddMonths(1);
            var result = await (from p in _db.orderCoinHeaders
                                join p2 in _db.orderCoinDetails on p.Id equals p2.OrderHeaderId
                                where p.Date >= firstOfThisMonth && p.Date < firstOfNextMonth
                                select p2.Price
                         ).SumAsync();


            return result;
        }
        public async Task<double> GetTurnover()
        {
            var result = await (from p in _db.AuctionWinners
                                join p2 in _db.Messages on p.auctionId equals p2.RecipientId
                                where p2.MoneyBack == false
                                select p2.Content).SumAsync();
            return result * 0.05;
        }
        public async Task<double> GetCountCoint()
        {
            return await (from p in _db.orderCoinDetails
                          select p.Price).SumAsync();

        }

        public async Task<int> GetProductStatusPrepare()
        {
            return await (from p in _db.Auctions
                          where p.DateCreated <= p.Time_Start && p.Product_Status==true && p.Product_StatusBuy == false
                          select p.Id).CountAsync();
        }

        public async Task<int> GetProductStatusStart()
        {
            return await (from p in _db.Auctions
                          where p.Time_End> p.DateCreated && p.DateCreated > p.Time_Start && p.Product_Status == true && p.Product_StatusBuy == false
                          select p.Id).CountAsync();
        }

        public async Task<int> GetProductStatusEnd()
        {
            return await (from p in _db.Auctions
                          where  p.Product_Status == true && p.Product_StatusBuy == true
                          select p.Id).CountAsync();
        }

        public async Task<int> GetProductStatusAccept()
        {
            return await (from p in _db.Auctions
                          where  p.Product_Status == false
                          select p.Id).CountAsync();
        }

        public async Task<ICollection<ViewTopVM>> GetTopUser()
        {
            List<ApplicationUser> user = new List<ApplicationUser>();
            List<IdentityUser> identities = new List<IdentityUser>();
            var users = await _managerUser.Users.ToListAsync();
            foreach (var use in users)
            {
                var roles = await _managerUser.GetRolesAsync(use);

                if (!roles.Contains(UserRole.Admin))
                {
                    var uuu = await _managerUser.FindByIdAsync(use.Id);
                    
                    identities.Add(uuu);
                }
            }
            
            List<ViewTop> cs = new List<ViewTop>();
    
            foreach (var us in identities)
            {
                
                cs.Add(new ViewTop {
                  Id= us.Id, CountWinner = await _db.AuctionWinners.Where(y=> y.applicationUserId == us.Id).CountAsync()
                })
                    ;

                //var userTop = await _db.applicationUser.FirstOrDefaultAsync(find => find.Id == us.Id);
                //applicationUsers.Add(userTop);              
            }
            ICollection<ViewTop> test = cs.OrderByDescending(x => x.CountWinner).Take(10).ToList();

            List<ViewTopVM> viewTopVMs = new List<ViewTopVM>();
            foreach (var ok in test)
            {
                
                viewTopVMs.Add(new ViewTopVM
                {
                    applicationUser = await _db.applicationUser.Where(find => find.Id == ok.Id).FirstOrDefaultAsync(),
                    CountTop = ok.CountWinner
                });
            }

   
            return viewTopVMs;

        }
    }
}
