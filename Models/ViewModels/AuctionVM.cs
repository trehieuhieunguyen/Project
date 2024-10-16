using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class AuctionVM
    {
        public IEnumerable<SelectListItem> AuctionCateList { get; set; }
        public Auction auction { get; set; }
    }
}
