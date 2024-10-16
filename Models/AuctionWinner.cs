using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class AuctionWinner
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("auctionId")]
        public string auctionId { get; set; }
        public Auction auction { get; set; }
        [ForeignKey("applicationUserId")]
        public string applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        [ForeignKey("MessageId")]
        public int MessageId { get; set; }
        public Message message { get; set; }  
        public bool DeliveryStatus { get; set; }
        public DateTime DateRequired { get; set; } = DateTime.Now;
    }
}
