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
        public decimal amount { get; set; }
        public string status { get; set; }
        public string status_reason { get; set; }
        public bool sandbox { get; set; }

        public override string ToString()
        {
            return "payment_id:"+payment_id + "|order_id:" + order_id + "|amount:" + amount.ToString() + "|status:" + status.ToString() + "|status_reason:" + status_reason + "|sandbox:" + sandbox;
        }
    }
}
