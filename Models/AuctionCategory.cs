using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class AuctionCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
