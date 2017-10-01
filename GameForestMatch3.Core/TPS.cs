using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForestMatch3.Core
{
    public class TPS
    {
        private Queue<double> TicksHistory;
        private int _AvergePeriod = 5;
        private const int Decimals = 2;
        private TimeSpan PreviewTime;

        public double Ticks { get; private set; }
        public int AvergePeriod
        {
            get { return _AvergePeriod; }
            set { if (value >= 1 && value <= 100) _AvergePeriod = value; }
        }

        public TPS()
        {
            Ticks = 0;
            TicksHistory = new Queue<double>();
        }

        public void Tick(TimeSpan totalTime)
        {
            TimeSpan ts = totalTime - PreviewTime;
            double ticks = 1000 / ts.TotalMilliseconds;
            PreviewTime = totalTime;
            if (ticks == Double.NaN) Ticks = 0;
            else
            if (double.IsInfinity(ticks))
                Ticks = 0;
            else
            {
                TicksHistory.Enqueue(ticks);
                if (TicksHistory.Count > AvergePeriod)
                    TicksHistory.Dequeue();
                Ticks = Math.Round(TicksHistory.Average(), Decimals);
            }
        }

        public override string ToString()
        {
            return Ticks.ToString();
        }
    }
}
