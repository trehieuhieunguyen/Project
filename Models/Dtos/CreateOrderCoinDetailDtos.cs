using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class CreateOrderCoinDetailDtos
    {
        [Required]
        public string OrderHeaderId { get; set; }
        [Required]
        public int CoinProductsId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
