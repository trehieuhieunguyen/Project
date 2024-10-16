using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class AuctionWinnerDtos
    {
        int Id { get; set; }
        public string auctionId { get; set; }
        public Auction auction { get; set; }
        
        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public int MessageId { get; set; }
        public Message message { get; set; }
        public bool DeliveryStatus { get; set; }
        public DateTime DateRequired { get; set; } = DateTime.Now;
    }
}
