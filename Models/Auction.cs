using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Auction
    {
        public string Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price_Start { get; set; }
        [Required]
        public double Price_End { get; set; }
        public string Feature_Img { get; set; }
        [Required]
        public string Product_Content { get; set; }
        [Required]
        public bool Product_New { get; set; }
        [Required]
        public bool Product_Status { get; set; }


        public bool Product_StatusBuy { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public AuctionCategory Category { get; set; }
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string AuctionImgFolder { get; set; }
        [Required]
        public DateTime Time_Start { get; set; }
        [Required]
        public DateTime Time_End { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
