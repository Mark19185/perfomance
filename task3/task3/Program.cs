using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        static DateTime time1 = new DateTime(2020, 1, 1, 0, 30, 0);
        static DateTime time2 = new DateTime(2020, 1, 1, 0, 30, 0);
        static DateTime time3 = new DateTime(2020, 1, 1, 0, 30, 0);
        static DateTime time4 = new DateTime(2020, 1, 1, 0, 30, 0);
        static DateTime time5 = new DateTime(2020, 1, 1, 0, 30, 0);
        static List<kassa> _kassa1 = new List<kassa>();
        static List<kassa> _kassa2 = new List<kassa>();
        static List<kassa> _kassa3 = new List<kassa>();
        static List<kassa> _kassa4 = new List<kassa>();
        static List<kassa> _kassa5 = new List<kassa>();
        
        static void Main(string[] args)
        {
            List<string> paths = new List<string>();
            if (args.Length == 5)
            {
                foreach (var arg in args)
                {
                    if (File.ReadAllLines(arg).Length == 16)
                    {
                        try
                        {
                            paths.Add(arg);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Документ {0} не подходит под условия",arg);
                    }
                }
                try
                {
                    loadFiles(paths);
                }
                catch (Exception ex)
                {
                   Console.WriteLine( ex.Message);
                }
            }
            
        }
        public static void loadFiles(List<string> args)
        {
            StreamReader _stream1 = new StreamReader(args[0]);
            StreamReader _stream2 = new StreamReader(args[1]);
            StreamReader _stream3 = new StreamReader(args[2]);
            StreamReader _stream4 = new StreamReader(args[3]);
            StreamReader _stream5 = new StreamReader(args[4]);

            getStreamData(_stream1, _kassa1, time1);
            getStreamData(_stream2, _kassa2, time2);
            getStreamData(_stream3, _kassa3, time3);
            getStreamData(_stream4, _kassa4, time4);
            getStreamData(_stream5, _kassa5, time5);
            decimal max1 = 0, max2 = 0, max3 = 0, max4 = 0, max5 = 0;
            /*
             * Получаем максимальные значения в каждой кассе
             * записываем время
             */
            foreach (var _kassa in _kassa1)
            {
                if (_kassa.length > max1)
                {
                    max1 = _kassa.length;
                    time1 = _kassa.time;
                }
            }
            foreach (var _kassa in _kassa2)
            {
                if (_kassa.length > max2)
                {
                    max2 = _kassa.length;
                    time2 = _kassa.time;
                }
            }
            foreach (var _kassa in _kassa3)
            {
                if (_kassa.length > max3)
                {
                    max3 = _kassa.length;
                    time3 = _kassa.time;
                }
            }
            foreach (var _kassa in _kassa4)
            {
                if (_kassa.length > max4)
                {
                    max4 = _kassa.length;
                    time4 = _kassa.time;
                }
            }
            foreach (var _kassa in _kassa5)
            {
                if (_kassa.length > max5)
                {
                    max5 = _kassa.length;
                    time5 = _kassa.time;
                }
            }
            /*
             * формаруем массив периодов из дат максимальных значений
             * сортируем по возрастанию
             * формируем период
             */
            List<DateTime> period = new List<DateTime>() {time1,time2,time3,time4,time5};
            period.Sort();
            Console.WriteLine("Самый пиковый период на кассах с {0} с момента открытия до {1} с момента открытия", period[0].ToShortTimeString(),period[period.Count-1].ToShortTimeString());
            Console.ReadKey();
        }
        public static void getStreamData(StreamReader reader, List<kassa> list, DateTime time)
        {
            while (!reader.EndOfStream)
            {
                var value = reader.ReadLine().Split('\\');
                value[0] = value[0].Replace('.', ',');
                list.Add(new kassa() { time = time, length = Convert.ToDecimal(value[0]) });
                time = time.AddMinutes(30);
            }
        }
    }
    class kassa
    {
        public DateTime time;
        public decimal length;
    }
}
