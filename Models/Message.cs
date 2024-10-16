using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Message
    {
        public Message()
        {
        }

        [Key]
        public int Id { get; set; }
        [ForeignKey("SenderId")]
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        public ApplicationUser Sender { get; set; }
        [ForeignKey("RecipientId")]
        public string RecipientId { get; set; }
        public Auction Recipient { get; set; }
        public double Content { get; set; }
        public bool MoneyBack { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.Now;
    }
}
