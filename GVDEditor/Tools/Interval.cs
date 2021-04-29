using System;

namespace GVDEditor.Tools
{
    internal class Interval
    {
        public Interval(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateTime From { get; }
        public DateTime To { get; }

        public bool Overlaps(Interval interval2)
        {
            return From <= interval2.From ? To >= interval2.From : interval2.To >= From;
        }
    }
}