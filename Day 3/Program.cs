using System.Diagnostics;
namespace Day_3
{
    static class Program
    {
        delegate bool CheckIfNumberIsPrime(int number);
        static async Task Main(string[] args)
        {
            int min = 0, max = 200000;

            // Task<List<int>>[] tasks = new Task<List<int>>[5];
            // tasks[0] = GetPrimeNumberAsync(min, max, IsPrimeNumberFast, 0);
            // tasks[1] = GetPrimeNumberAsync(min, max, IsPrimeNumberFast, 1);
            // tasks[2] = GetPrimeNumberAsync(min, max, IsPrimeNumberFast, 2);
            // tasks[3] = GetPrimeNumberAsync(min, max, IsPrimeNumberFast, 3);
            // tasks[4] = GetPrimeNumberAsync(min, max, IsPrimeNumberFast, 4);

            var result = await GetPrimeNumberAsync(min, max, IsPrimeNumberFast);
            Console.WriteLine($"Total numbers: {result.Count}");
            // var results = await Task.WhenAll(tasks);
            Console.WriteLine("Done!!");
        }
        static bool IsPrimeNumberFast(int number)
        {
            int i;
            var boundary = (int)Math.Floor(Math.Sqrt(number));
            if (number < 2) return false;
            for (i = 2; i <= boundary; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
        static List<int> ToPrintOutPrimeNumber(int min, int max)
        {
            var result = new List<int>();
            for (int i = min; i <= max; i++)
            {
                if (IsPrimeNumberFast(i))
                {
                    result.Add(i);
                }
            }
                return result;
        }
        static async Task<List<int>> GetPrimeNumberAsync(int min, int max, CheckIfNumberIsPrime checker, int? index = null)
        {
            var sw = new Stopwatch();
            sw.Start();

            var list = new List<int>();
            var results = await Task.Factory.StartNew(() =>
            {
                for (int i = min; i <= max; i++)
                {
                    if (checker(i))
                    {
                        list.Add(i);
                    }
                }
                return list;
            });
            Console.WriteLine($"[{index}].Total Time: [{sw.ElapsedMilliseconds}]");
            return results;
        }
        static void PrintNumbers(List<int> numbers)
        {
            foreach (var i in numbers)
            {
                Console.Write($"{i}");
            }
        }
    }
}
