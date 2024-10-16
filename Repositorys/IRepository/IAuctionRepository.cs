using Project.Models;
using Project.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repositorys.IRepository
{
    public interface IAuctionRepository
    {
        Task<int> GetCountAuctions(int IdSp);
        Task<ICollection<Auction>> GetAuctions(int IdSp);
        Task<ICollection<Auction>> GetAllAuctionsForAdmin();
        Task<ICollection<AuctionCategory>> GetAuctionCategorys();
        Task<Auction> GetIdProduct(string Id);
        Task<ICollection<AuctionDtos>> GetAuctionsUser(string Id);
        Task<ICollection<Message>> GetMyAuctionUser(string Id);
        Task<ICollection<AuctionWinner>> GetMyAuctionWinner(string Id);
        Task<AuctionWinner> GetAuctionWinnerForAdmin(string Id);
        Task<ICollection<AuctionWinner>> GetAuctionWinnerForAdmin();
        Task<IEnumerable<Auction>> GetAuctions(string input, int? IdSp, int? TimeType, int Skip, bool order);
        Task<ICollection<Auction>> GetAuctionForAdmins();
        Task<ShowCountAndTop1> GetCountAndTop(string RecipientId);
        Task<Auction> GetAuction(string Id);
        Task<Auction> GetAuctionForAdmin(string Id);
        Task<Auction> GetAuctionByIdAsync(string id);
        bool AuctionIdExist(string Id);
        bool CreateAuction(Auction auction);
        bool CreateAuctionWinner(AuctionWinner auctionWinner);
        bool CreateAuctionCategory(AuctionCategory auctionCategory);
        bool UpdateAuction(Auction auction);
        bool UpdateAuctionWinner(AuctionWinner auctionWinner);
        bool DeleteAuction(Auction auction);
        bool AuctionIdExistWinner(string auctionId);
        
        bool Save();
        
    }
}
