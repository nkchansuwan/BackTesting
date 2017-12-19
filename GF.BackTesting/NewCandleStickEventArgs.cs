using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.BackTesting
{
    public class NewCandleStickEventArgs : EventArgs
    {
        public NewCandleStickEventArgs(CandleStickItme candleStick)
        {
            CandleStick = candleStick;
        }

        public CandleStickItme CandleStick { get; }
    }
}
