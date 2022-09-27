using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interval = System.ValueTuple<int, int>;

namespace ConsoleAppKata
{
    public class ProgramKata
    {
        /// <summary>
        /// Объединение интервалов по пересечениям границ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static (int, int)[] Merge((int, int)[] input)
        {
            // Сортировка по начальным границам интвералов
            var intervals = input.OrderBy(x => x.Item1).ToList();

            for (int i = 0; i < intervals.Count() - 1; i++)
            {
                for (int j = i + 1; j < intervals.Count(); j++)
                {
                    // Пример работы:
                    // (1, 6), (1, 10) => (1, 10)
                    // (1, 6), (2, 8) => (1, 8)
                    // (1, 6), (2, 4) => (1, 6)

                    // Словесный алгоритм:
                    // Если правая граница А больше или равна правой границе В
                    // и левая граница А меньше или равна левой границы В,
                    // то создаем новый интервал (левая граница А, большая из правых границ А и В)
                    // на месте старого, чтобы не сбить сортировку по левой границе,
                    // удаляем интервалы А и В,
                    // возвращаемся во внешний цикл для проверки нового интвервала на возможность объединения

                    if ((intervals[i].Item2 >= intervals[j].Item1) && (intervals[i].Item1 <= intervals[j].Item1))
                    {
                        intervals[i] = (intervals[i].Item1, Math.Max(intervals[i].Item2, intervals[j].Item2));
                        intervals.Remove(intervals[j]);
                        i--;
                        break;
                    }
                }
            }
            return intervals.ToArray();
        }

        public static int SumIntervals((int, int)[] intervals)
        {
            int sum = 0;
            if (intervals.Length != 0)
            {
                intervals = Merge(intervals);
                for (int i = 0; i < intervals.Length; i++)
                {
                    sum += Math.Abs(intervals[i].Item2 - intervals[i].Item1);
                }
            }
            return sum;
        }

        static void Main(string[] args)
        {
            // 19 = (1, 20)
            Console.WriteLine(SumIntervals(new Interval[] { (1, 5), (10, 20), (1, 6), (16, 19), (5, 11) }));
            // 1234 = 62 + 22 + 150 = (-5400, -5338), (-7, 15), (2000, 3150)
            Console.WriteLine(SumIntervals(new Interval[] { (-7, 8), (-2, 10), (5, 15), (2000, 3150), (-5400, -5338) }));
            // 4
            Console.WriteLine(SumIntervals(new Interval[] { (1, 2), (4, 4), (6, 6), (8, 8), (10, 12) }));
            // 6
            Console.WriteLine(SumIntervals(new Interval[] { (5, 8), (3, 6), (1, 2) }));
            // 9
            Console.WriteLine(SumIntervals(new Interval[] { (1, 2), (6, 10), (11, 15) }));
            // 7
            Console.WriteLine(SumIntervals(new Interval[] { (-1, 4), (-5, -3) }));
            // 78
            Console.WriteLine(SumIntervals(new Interval[] { (-245, -218), (-194, -179), (-155, -119) }));
            // 2_000_000_000
            Console.WriteLine(SumIntervals(new Interval[] { (-1_000_000_000, 1_000_000_000) }));
        }
    }
}
