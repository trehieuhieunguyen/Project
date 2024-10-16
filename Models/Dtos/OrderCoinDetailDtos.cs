using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class OrderCoinDetailDtos
    {
        public int Id { get; set; }
        [Required]
        public string OrderHeaderId { get; set; }
        public virtual OrderCoinHeader OrderHeader { get; set; }
        [Required]
        public int CoinProductsId { get; set; }
        public virtual CoinProducts CoinProducts { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
