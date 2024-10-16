using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class SaleCoinDtos
    {
        public string Id { get; set; }
        [Required]
        public string PaypalEmail { get; set; }
        [Required]
        public double amountcoin { get; set; }
        public bool TransactionStatus { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
    }
}
