using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dtos
{
    public class MessageDtos
    {
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string RecipientId { get; set; }
        public double Content { get; set; }
        public bool MoneyBack { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
