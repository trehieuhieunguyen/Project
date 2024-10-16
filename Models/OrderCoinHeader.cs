using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class OrderCoinHeader
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string PaymentType { get; set; }
        [Required]
        public bool PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        [Required]
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        [Required]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
