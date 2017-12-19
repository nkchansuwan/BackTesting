using System;

namespace GF.BackTesting
{
    public class CandleStickReader
    {
        public int Timeframe { get; }
        public PriceReader PriceReader { get; }

        public event EventHandler<NewCandleStickEventArgs> NewCandleStick;

        public CandleStickReader(int timeframe, PriceReader PriceReader)
        {
            Timeframe = timeframe;
            this.PriceReader = PriceReader ?? throw new AggregateException();

            PriceReader.NewPrice += PriceReader_NewPrice;
        }

        private void PriceReader_NewPrice(object sender, NewPriceEventArgs e)
        {
            var item = new CandleStickItme
            {
                Open = e.Last,
                High = e.Last,
                Close = e.Last,
                Low = e.Last,
                Color = CandleStickColor.Green
           
            };

            var e2 = new NewCandleStickEventArgs(item);
            NewCandleStick?.Invoke(this, e2);
            //throw new NotImplementedException();
        }

        public void Start()
        {
            //throw new NotImplementedException();
            PriceReader.Start();
        }
    }
}