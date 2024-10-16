using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Cms;
using Project.Data;
using Project.Models;
using Project.Models.Dtos;
using Project.Repositorys.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Project.Repositorys
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ApplicationDbContext _db;

        public AuctionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AuctionIdExist(string Id)
        {
            return _db.Auctions.Any(find => find.Id == Id);
        }

        public async Task<ShowCountAndTop1> GetCountAndTop(string RecipientId)
        {
            ShowCountAndTop1 top = new ShowCountAndTop1();
            int getcount = await _db.Messages.Where(u => u.RecipientId == RecipientId).CountAsync();
            Message getTop1 = await _db.Messages.Where(u => u.RecipientId == RecipientId).OrderByDescending(m => m.Content).FirstOrDefaultAsync();
            double top1;
            if(getTop1 == null)
            {
                top1 = 0;
            }
            else
            {
                top1 = getTop1.Content;
            }
            top.Count = getcount;
            top.Top1 = top1;

            return top;
        }

        public bool CreateAuction(Auction auction)
        {
            _db.Auctions.Add(auction);
            return Save();
        }

        public bool CreateAuctionCategory(AuctionCategory auctionCategory)
        {
            _db.AuctionCategories.Add(auctionCategory);
            return Save();
        }


        public bool DeleteAuction(Auction auction)
        {
            _db.Auctions.Remove(auction);
            return Save();
        }


        public async Task<Auction> GetAuction(string Id)
        {
            return await _db.Auctions.Include(c => c.Category)
                .Include(u => u.ApplicationUser)
                .AsNoTracking().FirstOrDefaultAsync(find => find.Id == Id);
        }

        public async Task<Auction> GetAuctionByIdAsync(string id)
        {
            return await _db.Auctions.Include(c => c.Category)
                .Include(u => u.ApplicationUser)
                .AsNoTracking().FirstOrDefaultAsync(find => find.Id == id);
        }

        public async Task<ICollection<AuctionCategory>> GetAuctionCategorys()
        {
            return await _db.AuctionCategories.ToListAsync();
        }

        public async Task<Auction> GetAuctionForAdmin(string Id)
        {
            return await _db.Auctions.Include(c => c.Category).Include(c => c.ApplicationUser).AsNoTracking().FirstOrDefaultAsync(find => find.Id == Id);
        }

        public async Task<ICollection<Auction>> GetAuctionForAdmins()
        {
            /*throw new NotImplementedException();*/
            return await _db.Auctions.Where(a => a.Product_Status == false).ToListAsync();
        }

        public async Task<ICollection<Auction>> GetAuctions(int IdSp)
        {
            return await _db.Auctions.Where(sp => sp.CategoryId == IdSp && sp.Product_Status == true).Include(u => u.ApplicationUser).OrderByDescending(x => x.DateCreated).Take(4).ToListAsync();
        }

        public async Task<IEnumerable<Auction>> GetAuctions(string input, int? IdSp, int? TimeType, int Skip, bool order)
        {
            IEnumerable<Auction> sp = await _db.Auctions.Where(sp => sp.Product_Status == true).Include(u => u.ApplicationUser).ToListAsync();
            switch (TimeType)
            {
                case 1:
                    if(IdSp != null && (input == null || input == "")) {
                        sp = sp.Where(sp => sp.CategoryId == IdSp  && (sp.Time_Start > DateTime.Now));
                    }else if(IdSp == null && input != null)
                    {
                        sp = sp.Where(sp => sp.ProductName.Contains(input, StringComparison.OrdinalIgnoreCase) && (sp.Time_Start > DateTime.Now));
                    }
                    else if(IdSp != null && input != null)
                    {
                        sp = sp.Where(sp => sp.CategoryId == IdSp && sp.ProductName.Contains(input, StringComparison.OrdinalIgnoreCase) && (sp.Time_Start > DateTime.Now));
                    }
                    else
                    {
                        sp = sp.Where(sp => sp.Time_Start > DateTime.Now);
                    }

                    if(order is false)
                    {
                        sp = sp.OrderByDescending(t => t.DateCreated);
                    }
                    else
                    {
                        sp = sp.OrderBy(t => t.DateCreated);
                    }

                    sp = sp.Skip(Skip).Take(8);
                    break;
                case 2:
                    if (IdSp != null && (input == null || input == ""))
                    {
                        sp = sp.Where(sp => sp.CategoryId == IdSp && ((sp.Time_End > DateTime.Now) && (DateTime.Now > sp.Time_Start)));
                    }
                    else if (IdSp == null && input != null)
                    {
                        sp = sp.Where(sp => sp.ProductName.Contains(input, StringComparison.OrdinalIgnoreCase) && ((sp.Time_End > DateTime.Now) && (DateTime.Now > sp.Time_Start)));
                    }
                    else if (IdSp != null && input != null)
                    {
                        sp = sp.Where(sp => sp.CategoryId == IdSp && sp.ProductName.Contains(input, StringComparison.OrdinalIgnoreCase) && ((sp.Time_End > DateTime.Now) && (DateTime.Now > sp.Time_Start)));
                    }
                    else
                    {
                        sp = sp.Where(sp => (sp.Time_End > DateTime.Now) && (DateTime.Now > sp.Time_Start));
                    }

                    if (order is false)
                    {
                        sp = sp.OrderByDescending(t => t.DateCreated);
                    }
                    else
                    {
                        sp = sp.OrderBy(t => t.DateCreated);
                    }

                    sp = sp.Skip(Skip).Take(8);
                    break;
                case 3:
                    if (IdSp != null && (input == null || input == ""))
                    {
                        sp = sp.Where(sp => sp.CategoryId == IdSp && (sp.Time_End < DateTime.Now));
                    }
                    else if (IdSp == null && input != null)
                    {
                        sp = sp.Where(sp => sp.ProductName.Contains(input, StringComparison.OrdinalIgnoreCase) && (sp.Time_End < DateTime.Now));
                    }
                    else if (IdSp != null && input != null)
                    {
                        sp = sp.Where(sp => sp.CategoryId == IdSp && sp.ProductName.Contains(input, StringComparison.OrdinalIgnoreCase) && (sp.Time_End < DateTime.Now));
                    }
                    else
                    {
                        sp = sp.Where(sp => sp.Time_End < DateTime.Now);
                    }

                    if (order is false)
                    {
                        sp = sp.OrderByDescending(t => t.DateCreated);
                    }
                    else
                    {
                        sp = sp.OrderBy(t => t.DateCreated);
                    }

                    sp = sp.Skip(Skip).Take(8);
                    break;
                default:
                    if (IdSp != null && (input == null))
                    {
                        sp = sp.Where(sp => sp.CategoryId == IdSp);
                    }
                    else if (IdSp == null && input != null)
                    {
                        sp = sp.Where(sp => sp.ProductName.Contains(input, StringComparison.OrdinalIgnoreCase));
                    }
                    else if (IdSp != null && input != null)
                    {
                        sp = sp.Where(sp => sp.CategoryId == IdSp && sp.ProductName.Contains(input, StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        sp = sp;
                    }

                    if (order is false)
                    {
                        sp = sp.OrderByDescending(t => t.DateCreated);
                    }
                    else
                    {
                        sp = sp.OrderBy(t => t.DateCreated);
                    }

                    sp = sp.Skip(Skip).Take(8);
                    break;
            }

            return sp.AsQueryable();
        }

        public async Task<int> GetCountAuctions(int IdSp)
        {
            return await _db.Auctions.Where(x => x.CategoryId == IdSp && x.Product_Status == true).CountAsync();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool UpdateAuction(Auction auction)
        {
            _db.Auctions.Update(auction);
            return Save();
        }
        public bool UpdateAuctionWinner(AuctionWinner auctionWinner)
        {
            _db.AuctionWinners.Update(auctionWinner);
            return Save();
        }
        public async Task<ICollection<AuctionDtos>> GetAuctionsUser(string Id)
        {
            ICollection<Auction> auctions = await _db.Auctions.Where(find => find.ApplicationUserId == Id).Include(x => x.ApplicationUser).ToListAsync();
            List<AuctionDtos> auctionDtos = new List<AuctionDtos>();
            foreach (var user in auctions)
            {
                auctionDtos.Add(new AuctionDtos
                {
                    Id = user.Id,
                    Category = user.Category,
                    ApplicationUser = user.ApplicationUser,
                    Feature_Img = user.Feature_Img,
                    Price_End = user.Price_End,
                    ProductName = user.ProductName,
                    Time_End = user.Time_End,
                    Product_Status = user.Product_Status

                });
            }
            return auctionDtos;
            
        }

        public bool CreateAuctionWinner(AuctionWinner auctionWinner)
        {
            _db.AuctionWinners.Add(auctionWinner);
            return Save();
        }

        public bool AuctionIdExistWinner(string auctionId)
        {
            return _db.AuctionWinners.Any(c => c.auctionId == auctionId);
        }

        public async Task<ICollection<Message>> GetMyAuctionUser(string Id)
        {
            List<Message> messages = await _db.Messages.Where(x => x.SenderId == Id).Include(y => y.Recipient).ToListAsync();

            return messages;

        }

        public async Task<ICollection<AuctionWinner>> GetMyAuctionWinner(string Id)
        {
           List<AuctionWinner> messages = await _db.AuctionWinners.Where(x=>x.applicationUserId == Id).Include(y=> y.auction).ToListAsync();
            return messages;
        }

        public async Task<ICollection<AuctionWinner>> GetAuctionWinnerForAdmin()
        {
            List<AuctionWinner> auctionWinners = await _db.AuctionWinners
                                                        .Include(y => y.auction)
                                                        .Include(z => z.applicationUser)
                                                        .Include(x => x.message)
                                                        .ToListAsync();

            
            return auctionWinners;
                                                            
                                                       
        }

        public async Task<AuctionWinner> GetAuctionWinnerForAdmin(string Id)
        {
            return await _db.AuctionWinners.Include(x=>x.applicationUser).AsNoTracking().FirstOrDefaultAsync(find => find.auctionId == Id);
        }

        public async Task<ICollection<Auction>> GetAllAuctionsForAdmin()
        {
            return await _db.Auctions.ToListAsync();
        }

        public async Task<Auction> GetIdProduct(string Id)
        {
            return await _db.Auctions.FirstOrDefaultAsync(find=> find.Id == Id);
        }
    }
}
