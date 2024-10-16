using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class CreateAuctionWinnerDtos
    {
        public string auctionId { get; set; }
        public string applicationUserId { get; set; }
        public bool DeliveryStatus { get; set; }
        
        public int MessageId { get; set; }
        public DateTime DateRequired { get; set; } = DateTime.Now;
    }
}
