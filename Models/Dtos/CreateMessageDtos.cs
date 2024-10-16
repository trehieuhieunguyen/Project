using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class CreateMessageDtos
    {
        public string RecipientProductId { get; set; }
        public double Content { get; set; }
    }
}
