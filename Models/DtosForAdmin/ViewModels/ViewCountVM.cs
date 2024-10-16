using Project.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.DtosForAdmin.ViewModels
{
    public class ViewCountVM
    {
        public int CountUser { get; set; }
        public int CountProduct { get; set; }
        public double CountEmail { get; set; }
        public double CountOrder { get; set; }
        public double productStatusPrepare { get; set; }
        public double productStatusStart { get; set; }
        public double productStatusEnd { get; set; }
        public double productStatusAccept { get; set; }
        public ICollection<int> productChartDtos { get; set; }
        public ICollection<double> CoinChartDtos { get; set; }
        public ICollection<ViewTopVM> applicationUsers{ get; set; }

    }
}
