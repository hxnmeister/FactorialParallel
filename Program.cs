using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FactorialParallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string value;
            List<uint> valuesForFactorial = new List<uint>();

            while (true)
            {
                Console.Write($"\n  Current array: ");
                valuesForFactorial.ForEach(x => Console.Write($"{x} "));

                Console.WriteLine("\n\n In the field below, enter the values to obtain their factorial,\n to shade the entry and start calculating, type \"E\"!");
                Console.Write("\n Enter field: ");

                if ((value = Console.ReadLine()).ToUpper() == "E")
                {
                    break;
                }

                Console.Clear();

                if (uint.TryParse(value, out uint convertedValue) && convertedValue > 0)
                {
                    valuesForFactorial.Add(convertedValue);
                }
                else
                {
                    Console.WriteLine($"\n Invalid input \"{value}\"!\n Try again!\n");
                }
            }

            Console.Clear();

            if (valuesForFactorial.Count == 0)
            {
                Console.WriteLine("\n No values to calculate!");
                return;
            }

            try
            {
                Console.WriteLine("\nResults:");
                Console.WriteLine("-----------------------------------");
                Parallel.ForEach<uint>(valuesForFactorial, GetFactorialInfo);
                Console.WriteLine("-----------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n  Parallel Exception: " + ex.Message);
            }
        }

        static void GetFactorialInfo(uint number)
        {
            uint numbersCount = 1;
            uint factorial = 1;
            uint sum = 1;

            for (uint i = 2; i <= number; i++)
            {
                factorial *= i;
                sum += i;
            }

            for (uint i = factorial; i / 10 > 0; i /= 10)
            {
                ++numbersCount;
            }

            Console.WriteLine($" {number}! = {factorial}");
            Console.WriteLine($" {number}! sum equals to {sum}");
            Console.WriteLine($" {number}! contains {numbersCount} numbers");
        }
    }
}
