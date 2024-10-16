using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class SaleCoin
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string PaypalEmail { get; set; }
        [Required]
        public double amountcoin { get; set; }
        [Required]
        public bool TransactionStatus { get; set; }
        [Required]
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        [Required]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
    }
}
