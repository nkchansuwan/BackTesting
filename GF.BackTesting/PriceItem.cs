using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.BackTesting
{
   public class PriceItem
    {
        public DateTime Date { get; set; }
        public decimal Last { get; set; }
        public decimal Bid { get; set; }//ราคาเสนอขาย
        public decimal Offer { get; set; }//ราคาเสนอซื้อ
    }
}
