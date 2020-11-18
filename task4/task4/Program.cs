using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
       static List<interval> _intervals = new List<interval>();
        static List<interval> _needIntervals = new List<interval>();
        static void Main(string[] args)
        {
            _intervals.Add(new interval() { start = 8.15, end = 8.40 });
            _intervals.Add(new interval() { start = 8, end = 8.10 });
            _intervals.Add(new interval() { start = 8.30, end = 8.40 });
            _intervals.Add(new interval() { start = 8.40, end = 8.50 });
            _intervals.Add(new interval() { start = 8.52, end = 9 });
            _intervals.Sort();
            int countInts = 1;
            foreach (var interval in _intervals)
            {
                if (interval.start < _intervals[countInts - 1].end)
                {
                    _needIntervals.Add(new interval() {start = _intervals[countInts-1].start, end = interval.end});
                }
            }
            Console.ReadKey();
        }
    }
    class interval
    {
        public double start;
        public  double end;
    }
}
