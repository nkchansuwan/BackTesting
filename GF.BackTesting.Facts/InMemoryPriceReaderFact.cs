using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GF.BackTesting.Facts
{
    public class InMemoryPriceReaderFact
    {
        [Fact]
        public void NoSeedPrice()
        {
            var reader = new InMemoryPriceReader();
            
            decimal price = 0m;
            int count = 0;

            reader.NewPrice += (sender, e) =>
            {
                price = e.Last;
                count++;
            };

            reader.Start();

            Assert.Equal(0, count);
            Assert.Equal(0m, price);
        }

        [Fact]
        public void SinglePrice()
        {
            var reader = new InMemoryPriceReader();
            decimal price = 0m;

            reader.AddSeedPrice(last: 15.0m);
            reader.NewPrice += (sender, e) =>
            {
                price = e.Last;
            };

            reader.Start();

            Assert.Equal(15.0m, price);
        }

        [Fact]
        public void ThreePrices()
        {
            var reader = new InMemoryPriceReader();
            decimal price = 0m;
            int count = 0;

            reader.AddSeedPrice(last: 15.0m);
            reader.AddSeedPrice(last: 16.0m);
            reader.AddSeedPrice(last: 14.0m);

            reader.NewPrice += (sender, e) =>
            {
                price = e.Last;
                count++;
            };

            reader.Start();

            Assert.Equal(3, count);
            Assert.Equal(14.0m, price);
        }
    }
}
