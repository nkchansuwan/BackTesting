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
            var reader = new CandleStickReader(timeframe: 15, PriceReader: priceReader);
            int count = 0;
            CandleStickItme itme = null;
            List<CandleStickItme> item = new List<CandleStickItme>();

            var dt1 = new DateTime(2017, 5, 5, 10, 5, 0);
            var dt2 = new DateTime(2017, 5, 5, 10, 10, 0);
           

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
            //Assert.Equal(dt0, itme.Date);
            Assert.Equal(CandleStickColor.Green, itme.Color);
        }

        [Fact]
        public void TwoCandlestick()
        {
            var priceReader = new InMemoryPriceReader();
            var reader = new CandleStickReader(timeframe: 15, PriceReader: priceReader);
            int count = 0;
            //CandleStickItme itme = null;
            List<CandleStickItme> items = new List<CandleStickItme>();

            var dt1 = new DateTime(2017, 5, 5, 10, 5, 0);
            var dt2 = new DateTime(2017, 5, 5, 10, 10, 0);
            var dt3 = new DateTime(2017, 5, 5, 10, 15, 0);
            var dt4 = new DateTime(2017, 5, 5, 10, 20, 0);

            priceReader.AddSeedPrice(dt1, 12m, 0, 0);
            priceReader.AddSeedPrice(dt2, 14m, 0, 0);
            priceReader.AddSeedPrice(dt3, 16m, 0, 0);
            priceReader.AddSeedPrice(dt4, 15m, 0, 0);

            reader.NewCandleStick += (sender, e) =>
            {
                count++;
                items.Add(e.CandleStick);
            };

            reader.Start();

            Assert.Equal(2, count);
            Assert.Equal(12m, items[0].Low);
            Assert.Equal(12m, items[0].Open);
            Assert.Equal(14m, items[0].High);
            Assert.Equal(14m, items[0].Close);

            Assert.Equal(16m, items[1].Open);
            Assert.Equal(15m, items[1].Low);
            Assert.Equal(16m, items[1].High);
            Assert.Equal(15m, items[1].Close);

            Assert.Equal(new DateTime(2017, 5, 5, 10, 0, 0), items[0].Date);
            Assert.Equal(new DateTime(2017, 5, 5, 10, 15, 0), items[1].Date);
        }
    }
}
