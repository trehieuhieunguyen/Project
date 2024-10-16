using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.DtosForAdmin
{
    public class UpdateUserDtos
    {

        public string Id { get; set; }
        [Required]
        public string FirtsName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        [Required]
        public int coin { get; set; }
        public string PassWord { get; set; }
        public bool LockoutEnabled { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
