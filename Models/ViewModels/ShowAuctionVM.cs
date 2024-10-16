using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
    public class ShowAuctionVM
    {
        public List<int> ListCount { get; set; }
        public List<IEnumerable<Auction>> auction { get; set; }
    }
}
