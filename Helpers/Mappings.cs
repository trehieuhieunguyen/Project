using AutoMapper;
using Project.Models;
using Project.Models.Dtos;
using Project.Models.DtosForAdmin;
using Project.Models.DtosForAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Helpers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<ApplicationUser, ApplicationUserDtos>().ReverseMap();
            CreateMap<ApplicationUser, UpdateCoinUserDtos>().ReverseMap();
            CreateMap<ApplicationUser, ShowCoinDtos>().ReverseMap();
            CreateMap<CoinProducts, CoinPoductsDtos>().ReverseMap();
            CreateMap<OrderCoinHeader, OrderCoinHeaderDtos>().ReverseMap();
            CreateMap<OrderCoinDetail, CreateOrderCoinDetailDtos>().ReverseMap();
            CreateMap<OrderCoinDetail, OrderCoinDetailDtos>().ReverseMap();
            CreateMap<GroupOrderDetails, GroupOrderDetailsDtos>().ReverseMap();
            CreateMap<SaleCoin, SaleCoinDtos>().ReverseMap();
            CreateMap<SaleCoin, UpdateSaleCoinDtos>().ReverseMap();
            CreateMap<Auction, CreateAuctionDtos>().ReverseMap();
            CreateMap<Auction, UpdateAuctionDtos>().ReverseMap();
            CreateMap<Auction, AuctionDtos>().ReverseMap();
            CreateMap<AuctionWinner, CreateAuctionWinnerDtos>().ReverseMap();
            CreateMap<Message, MessageMyAuctionDtos>().ReverseMap();
            CreateMap<AuctionWinner, AuctionWinnerDtos>().ReverseMap();
            CreateMap<Message, MessageDtos>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src =>
                    src.Sender.Image));
            CreateMap<Message, UpdateMessageContentDtos>().ReverseMap();
            //Mapper Areas : Admin
            CreateMap<ApplicationUser, UpdateCoinDtos>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserDtos>().ReverseMap();
        }
    }
}
