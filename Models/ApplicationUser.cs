using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirtsName { get; set; }
        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string district { get; set; }
        [Required]
        public string ward { get; set; }
        [Required]
        public double coin { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Image { get; set; }
    }
}
