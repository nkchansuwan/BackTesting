using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.BackTesting
{
    public abstract class PriceReader
    {
        private List<PriceItem> prices;
        private DateTime startDateTime = new DateTime(2017, 5, 5, 10, 0, 0);
        private int timeframe = 5;

        public PriceReader()
        {
            prices = new List<PriceItem>();
        }

        public event EventHandler<NewPriceEventArgs> NewPrice;

        public void AddSeedPrice(decimal last)
        {
            //throw new NotImplementedException();
            //prices.Add(last);
            AddSeedPrice(DateTime.Now, last, 0m, 0m);
        }

        protected void RaiseNewPrice(PriceItem item)
        {
            NewPrice?.Invoke(this, new NewPriceEventArgs(item));
        }

        public void AddSeedPrice(DateTime date, decimal last, decimal bid, decimal offer)
        {
            var item = new PriceItem
            {
                Date = date,
                Last = last,
                Bid = bid,
                Offer = offer
            };
            prices.Add(item);
        }

        public virtual void Start()
        {
            RaiseSeedPrices();
            RaiseStopper();
        }

        protected void RaiseSeedPrices()
        {
            foreach (var p in prices)
            {
                //var e = new NewPriceEventArgs(p);
                //NewPrice?.Invoke(this, e);
                RaiseNewPrice(p);
            }
        }

        protected void RaiseStopper()
        {
            var stopper = new NewPriceEventArgs(null);
            NewPrice?.Invoke(this, stopper);
        }
    }
}
