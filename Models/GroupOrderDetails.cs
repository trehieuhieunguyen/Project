using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class GroupOrderDetails
    {
        public string Name { get; set; }
        public int Quatity { get; set; }
        public double total { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
