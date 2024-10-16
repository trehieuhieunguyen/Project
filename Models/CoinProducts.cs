using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class CoinProducts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string CoinName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string Photo { get; set; }
    }
}
