using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.BackTesting
{
    public class CandleStickItme
    {
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Close { get; set; }
        public decimal Low { get; set; }
        public CandleStickColor Color
        {
            get
            {
                if (Open <= Close)
                    return CandleStickColor.Green;
                else
                    return CandleStickColor.Red;
            }
        }

        public override string ToString()
        {
            return $"{Date:s} {Open,10:n2} {Close,10:n2} {High,10:n2} {Low,10:n2} {Color.ToString()[0]}";
        }
    }
}