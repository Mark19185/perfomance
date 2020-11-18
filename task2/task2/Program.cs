using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    class Program
    {
        static List<Dots> rectangle = new List<Dots>();
        static List<Dots> dots = new List<Dots>();
        static void Main(string[] args)
        {
            StreamReader _reader1 = new StreamReader(args[0]);
            StreamReader _reader2 = new StreamReader(args[1]);

            while (!_reader1.EndOfStream)
            {
                string[] value = _reader1.ReadLine().Split(' ');
                rectangle.Add(new Dots() { x = Convert.ToDouble(value[0]), y = Convert.ToDouble(value[1])});
            }
            while (!_reader2.EndOfStream)
            {
                string[] value = _reader2.ReadLine().Split(' ');
                dots.Add(new Dots() { x = Convert.ToDouble(value[0]), y = Convert.ToDouble(value[1]) });
            }
            double maxY = 0, maxX = 0, minX = 0, minY = 0, firstMiddleX = 0, lastMiddleX = 0, firstMiddleY = 0, lastMiddleY = 0;

            foreach (var peak in rectangle)
            {
                if (peak.x >= maxX)
                {
                    minX = lastMiddleX;
                    lastMiddleX = firstMiddleX;
                    firstMiddleX = maxX;
                    maxX = peak.x;
                }
                else if (peak.x <= maxX && peak.x >= firstMiddleX)
                {
                    minX = lastMiddleX;
                    lastMiddleX = firstMiddleX;
                    firstMiddleX= peak.x;
                }

                if (peak.y >= maxY)
                {
                    minY = lastMiddleY;
                    lastMiddleY = firstMiddleY;
                    firstMiddleY = maxY;
                    maxY = peak.y;
                }
                else if (peak.y <= maxY && peak.y >= firstMiddleY)
                {
                    minY = lastMiddleY;
                    lastMiddleY = firstMiddleY;
                    firstMiddleY = peak.y;
                }
            }

            double i = firstMiddleY;
            firstMiddleY = lastMiddleY;
            lastMiddleY = i;

            foreach (var dot in dots)
            {

                if (checkDotInRect(dot))
                {
                    Console.WriteLine("Четырёхугольник вмещает в себя точку ({0},{1})", dot.x, dot.y);
                }
                else
                {
                    foreach (var peak in rectangle)
                    {
                        if (dot.x == peak.x && dot.y == peak.y)
                        {
                            Console.WriteLine("Точка с координатами ({0},{1}) соответствует вершине", dot.x, dot.y);
                        }
                    }
                }
            }
            Console.ReadKey();
        }
        static public bool checkDotInRect(Dots dot)
        {
            /*
             * Прменяя математическую формулу просчитываем все элементы
             * Если знак у всех един, то четырёхугольник вмещает точку
             */
            double ab = (rectangle[0].y * rectangle[1].x) - (rectangle[0].x * rectangle[1].y);
            double bc = (rectangle[1].y * rectangle[2].x) - (rectangle[1].x * rectangle[2].y);
            double cd = (rectangle[2].y * rectangle[3].x) - (rectangle[2].x * rectangle[3].y);
            double da = (rectangle[3].y * rectangle[0].x) - (rectangle[3].x * rectangle[0].y);

            double ao = (rectangle[0].y * dot.x) - (rectangle[0].x * dot.y);
            double bo = (rectangle[1].y * dot.x) - (rectangle[1].x * dot.y);
            double co = (rectangle[2].y * dot.x) - (rectangle[2].x * dot.y);
            double DO = (rectangle[3].y * dot.x) - (rectangle[3].x * dot.y);

            double compose1 = ab * ao;
            double compose2 = bo * bc;
            double compose3 = cd * co;
            double compose4 = da * DO;
             
            if ((compose1 >= 0 && compose2 >= 0 && compose3 >= 0 && compose4 >= 0) || (compose1 <= 0 && compose2 <= 0 && compose3 <= 0 && compose4 <= 0))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    
    class Dots
    {
        public double x;
        public double y;
    }
}
