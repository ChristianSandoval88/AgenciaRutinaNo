using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class KueskyResponse
    {
        public string payment_id { get; set; }
        public string order_id { get; set; }
        public int amount { get; set; }
        public string status { get; set; }
        public string status_reason { get; set; }
        public bool sandbox { get; set; }
    }
}
