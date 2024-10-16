using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.DtosForAdmin
{
    public class UpdateCoinDtos
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public double coin { get; set; }
    }
}
