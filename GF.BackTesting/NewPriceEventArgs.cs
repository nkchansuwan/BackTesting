using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.BackTesting
{
    public class NewPriceEventArgs : EventArgs
    {
        public NewPriceEventArgs(PriceItem item)
        {
            Date = item.Date;
            Last = item.Last;
            Bid = item.Bid;
            Offer = item.Offer;
        }

        public DateTime Date { get; }
        public decimal Last { get; }
        public decimal Bid { get; }//ราคาเสนอขาย
        public decimal Offer { get; }//ราคาเสนอซื้อ

    }
}
