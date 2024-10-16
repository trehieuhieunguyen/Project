using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Extensions
{
    public static class CreateGuildNameExtensions
    {
        public static string NameFileNewGuild()
        {
            var guid = Guid.NewGuid().ToString();
            return guid;
        }
    }
}
