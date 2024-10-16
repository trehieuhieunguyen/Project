using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class AuctionDtos
    {
        public string Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price_Start { get; set; }
        [Required]
        public double Price_End { get; set; }
        [Required]
        public string Feature_Img { get; set; }
        [Required]
        public string Product_Content { get; set; }
        [Required]
        public bool Product_New { get; set; }
        [Required]
        public bool Product_Status { get; set; }

        public bool Product_StatusBuy { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public AuctionCategory Category { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser  { get; set; }

        [Required]
        public string AuctionImgFolder { get; set; }
        [Required]
        public DateTime Time_Start { get; set; }
        [Required]
        public DateTime Time_End { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
