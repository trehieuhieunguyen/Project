using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class UpdateAuctionDtos
    {
        public UpdateAuctionDtos()
        {
        }

        public string Id { get; set; }
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
        [Required]
        public string ApplicationUserId { get; set; }
        public string ApplicationUserBuyId { get; set; }

        [Required]
        public string AuctionImgFolder { get; set; }
        [Required]
        public DateTime Time_Start { get; set; }
        [Required]
        public DateTime Time_End { get; set; }
    }
}
