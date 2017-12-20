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
            NewPrice = item;
        }

        
        public PriceItem NewPrice { get; }
    }
}
