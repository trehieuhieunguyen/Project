using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class UpdateSaleCoinDtos
    {
        public string Id { get; set; }
        public string PaypalEmail { get; set; }
        public double amountcoin { get; set; }
        public bool TransactionStatus { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
