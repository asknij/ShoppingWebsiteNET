using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingWebsiteNET.Models
{
    public class Comments
    {
        public string product { get; set; }
        public string user { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
    }
}
