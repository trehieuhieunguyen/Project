using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class OrderCoinHeaderDtos
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
        public string ApplicationUserId { get; set; }
    }
}
