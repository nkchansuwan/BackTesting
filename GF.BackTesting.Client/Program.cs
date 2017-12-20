using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.BackTesting.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var priceReader = new CsvPriceReader(args[0]);
            var reader = new CandleStickReader(timeframe: 5, PriceReader: priceReader);


            reader.NewCandleStick += C_NewCandleStick;
            reader.Start();
        }
        private static void C_NewCandleStick(object sender, NewCandleStickEventArgs e)
        {
            Console.WriteLine(e.CandleStick);
        }
    }
}
