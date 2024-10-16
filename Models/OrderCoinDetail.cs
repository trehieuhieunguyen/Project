using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class OrderCoinDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("OrderHeaderId")]
        public string OrderHeaderId { get; set; }
        [Required]
        public OrderCoinHeader OrderHeader { get; set; }

        [Required]
        [ForeignKey("CoinProductsId")]
        public int CoinProductsId { get; set; }
        [Required]
        public CoinProducts CoinProducts { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
