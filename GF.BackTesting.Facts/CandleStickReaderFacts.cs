using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GF.BackTesting.Facts
{
    public class CandleStickReaderFacts
    {
        [Fact]
        public void NoPrice()
        {
            var priceReader = new InMemoryPriceReader();
            var reader = new CandleStickReader(timeframe: 5, PriceReader: priceReader);
            int count = 0;

            reader.NewCandleStick += (sender, e) =>
            {
                count++;
            };

            reader.Start();

            Assert.Equal(0,count);

        }

        [Fact]
        public void SinglePrice()
        {
            var priceReader = new InMemoryPriceReader();
            var reader = new CandleStickReader(timeframe: 5, PriceReader: priceReader);
            int count = 0;
            CandleStickItme itme = null;

            priceReader.AddSeedPrice(12m);

            reader.NewCandleStick += (sender, e) =>
            {
                count++;
                itme = e.CandleStick;
            };

            reader.Start();

            Assert.Equal(1, count);
            Assert.Equal(12m, itme.Open);
            Assert.Equal(12m, itme.High);
            Assert.Equal(12m, itme.Close);
            Assert.Equal(12m, itme.Low);
            Assert.Equal(CandleStickColor.Green, itme.Color);

        }

        [Fact]
        public void TwoPrices()
        {
            var priceReader = new InMemoryPriceReader();
            var reader = new CandleStickReader(timeframe: 5, PriceReader: priceReader);
            int count = 0;
            CandleStickItme itme = null;
            var dt1 = new DateTime(2017, 5, 5, 10, 1, 0);
            var dt2 = new DateTime(2017, 5, 5, 10, 2, 0);

            priceReader.AddSeedPrice(dt1,12m,0,0);
            priceReader.AddSeedPrice(dt2,14m,0,0);

            reader.NewCandleStick += (sender, e) =>
            {
                count++;
                itme = e.CandleStick;
            };

            reader.Start();

            Assert.Equal(1, count);
            Assert.Equal(12m, itme.Open);
            Assert.Equal(12m, itme.Low);
            Assert.Equal(14m, itme.High);
            Assert.Equal(14m, itme.Close);
            var dt0 = new DateTime(2017, 5, 5, 10, 0, 0);
            Assert.Equal(dt0, itme.Date);
            Assert.Equal(CandleStickColor.Green, itme.Color);

        }
    }
}
