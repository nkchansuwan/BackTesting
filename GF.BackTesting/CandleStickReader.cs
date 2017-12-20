using System;
using System.Collections.Generic;

namespace GF.BackTesting
{
    public class CandleStickReader
    {
        public int Timeframe { get; }
        public PriceReader PriceReader { get; }
        private double previousCandleIndex = -1;


        public event EventHandler<NewCandleStickEventArgs> NewCandleStick;

        public CandleStickReader(int timeframe, PriceReader PriceReader)
        {
            Timeframe = timeframe;
            this.PriceReader = PriceReader ?? throw new AggregateException();

            PriceReader.NewPrice += PriceReader_NewPrice;

            item = null;

        }

        private CandleStickItme item;

        private void PriceReader_NewPrice(object sender, NewPriceEventArgs e)
        {
            //stop
            if (e.NewPrice == null)
            {
                if (item != null)
                {
                    var e2 = new NewCandleStickEventArgs(item);
                    NewCandleStick?.Invoke(this, e2);
                }
                return;
            }
            double candleIndex = Math.Floor(e.NewPrice.Date.Minute / (double)Timeframe);
            //new timeframe block?
            if (candleIndex != previousCandleIndex)
            {
                if (item != null)
                {
                    //raise new candle stick
                    var e2 = new NewCandleStickEventArgs(item);
                    NewCandleStick?.Invoke(this, e2);
                }

                //create candle stick
                item = new CandleStickItme();
                item.Open = item.Close = 0m;
                item.Close = 0m;
                item.High = decimal.MinValue;
                item.Low = decimal.MaxValue;

                item.Date = (e.NewPrice.Date).Date.AddHours(e.NewPrice.Date.Hour).AddMinutes(candleIndex * Timeframe);
            }

            //adjust candle stick

            if (item.Open == 0)
                item.Open = e.NewPrice.Last;
            if (e.NewPrice.Last > item.High)
                item.High = e.NewPrice.Last;
            if (e.NewPrice.Last < item.Low)
                item.Low = e.NewPrice.Last;
            item.Close = e.NewPrice.Last;

            previousCandleIndex = candleIndex;
        }

        public void Start()
        {
            PriceReader.Start();
        }

    }
}