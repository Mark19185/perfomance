using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> fList = new List<double>(); // форматируемый лист со значениями

            /*
             * Проверяем кол-во аргументов
             * Пытаемся считать данные из пути, переданного в качестве аргумента
             * Добавляем в лист значение, если оно удовлетворяет критерию > -32768 && <=32767, можно форматировать в shortint и автоматом выбьет ограничение длины, но нужна плавающаяя запятая
             * выводим нужные данные
             */
            if (args.Length == 1)
            {
                try
                {
                    if (File.ReadAllLines(args[0]).Length <= 1000)
                    {
                        StreamReader _reader = new StreamReader(args[0]);
                        string line;
                        int errCount = 0;
                        while ((line =  _reader.ReadLine()) != null)
                        {
                            if (Convert.ToDouble(line) > -32768 && Convert.ToDouble(line) <= 32767)
                                fList.Add(Convert.ToDouble(line));
                            else
                                errCount++;
                        }
                        Console.WriteLine("{0} Значения(ий) не прошли условие",errCount);
                        fList.Sort();
                        foreach (var value in fList)
                        {
                            Console.WriteLine(value);
                        }
                        Console.WriteLine(String.Format("Минимальное значение: {0:0,0.00}", fList[0]));
                        Console.WriteLine(String.Format("Максимальное значение: {0:0,0.00}", fList[fList.Count-1]));
                        Console.WriteLine(String.Format("Медиана: {0:0,0.00}", getMedian(fList)));
                        Console.WriteLine(String.Format("Среднее арифметическое: {0:0,0.00}", getAverageValue(fList)));
                        Console.WriteLine(String.Format("90 Процентиль: {0:0,0.00}", getPercentile(fList)));
                    }
                }
                catch (Exception e)
                {
                     Console.WriteLine(e.Message);
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Ошибка считывания аргумента. Программа может принимать только один аргумент");
            }
        }
        /*
         * Проверяем на четность количество элементов массива
         * Если количество чётное, то вычисляем сумму средних значений /2
         * Если количество нечётное, то получаем индекс среднего элемента
         */
        public static double getMedian(List<double> list)
        {
            if (list.Count % 2 == 0)
            {
                int firstIndex = (list.Count / 2)-1;
                double median = (list[firstIndex] + list[firstIndex + 1]) / 2;
                return median;
            }
            else
            {
                decimal firstIndex = Math.Ceiling((decimal)(list.Count / 2));
                return (double)(list[(int)firstIndex]);
            }
        }
        /*
         * прогоняем циклом все элементы массива и получаем их сумму
         * рассчитываем среднее арифметическое по формуле сумма/колличество
         */
        public static double getAverageValue(List<double> list)
        {
            double _arrSum = 0;
            foreach (var value in list)
            {
                _arrSum += value;
            }
            return _arrSum/list.Count;
        }
        /*
         * получаем индекс числа, соответствующего 90 процентилю по формуле кол-во элементов * 0,9
         * Получаем значение этого элемента
         */
        public static double getPercentile(List<double> list)
        {
            decimal needIndex = Math.Floor((decimal)(list.Count * 90) / 100)-1;
            return list[(int)needIndex];
        }
    }
}
