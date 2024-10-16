using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class ApplicationUserDtos : IdentityUser
    {
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        [Required]
        public double coin { get; set; }

        public string Image { get; set; }
        public string PassWord { get; set; }

        public string Role { get; set; }
    }
}
