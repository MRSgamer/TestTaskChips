using System;
using System.Collections.Generic;
using System.Linq;


namespace TestChips
{
    internal class Program
    {
        static void Main()
        {

            int[] chips = InputArray();

            // Пример: 4 игрока (сумма = 20, среднее = 5)
            int minMoves = MinMovesCircular(chips);
            Console.WriteLine($"Минимальное количество ходов: {minMoves}");
        }

        static int MinMovesCircular(int[] chips)
        {
            int n = chips.Length;
            int total = chips.Sum();

            if (total % n != 0)
                throw new ArgumentException("Невозможно равномерно распределить фишки.");

            int average = total / n;
            int[] difference = new int[n];
            int[] cumulative = new int[n];
            int minMoves = int.MaxValue;

            for (int i = 0; i < n; i++)
                difference[i] = chips[i] - average;

            // Перебираем все возможные начальные точки
            for (int start = 0; start < n; start++)
            {
                int currentMoves = 0;
                int currentSum = 0;

                for (int i = 0; i < n - 1; i++)
                {
                    int idx = (start + i) % n;
                    currentSum += difference[idx];
                    currentMoves += Math.Abs(currentSum);
                }

                if (currentMoves < minMoves)
                    minMoves = currentMoves;
            }

            return minMoves;
        }

        static int[] InputArray()
        {
            List<int> temp = new List<int>();

            while (true)
            {
                Console.Write("Введите число (Enter для завершения): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) break;

                if (int.TryParse(input, out int num))
                    temp.Add(num);
                else
                    Console.WriteLine("Ошибка: введено не число!");
            }

            if (temp.Count == 0) temp.Add(0);

            return temp.ToArray();
        }

    }
}