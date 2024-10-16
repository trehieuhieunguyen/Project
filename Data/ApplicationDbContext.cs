using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Groups;
using System;
using System.Collections.Generic;
using System.Text;
using Project.Models.DtosForAdmin;

namespace Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> applicationUser { get; set; }

        public DbSet<CoinProducts> coinProducts { get; set; }
        public DbSet<OrderCoinHeader> orderCoinHeaders { get; set; }
        public DbSet<OrderCoinDetail> orderCoinDetails { get; set; }
        public DbSet<SaleCoin> saleCoins { get; set; }

        public DbSet<AuctionCategory> AuctionCategories { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionWinner> AuctionWinners { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<ChatApp> chatApps { get; set; }
    }
}
