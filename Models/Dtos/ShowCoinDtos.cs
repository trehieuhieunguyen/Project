using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class ShowCoinDtos
    {
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public double coin { get; set; }
        public string Image { get; set; }
    }
}
