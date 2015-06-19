using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj6
{
    class Program
    {
        static void Main(string[] args)
        {
            int start = 100;
            int sum1;
            int sum2;
            int final;
            sum1 = SumOfSquare(start);
            sum2 = SquareOfSum(start);
            final = sum2 - sum1;
            Console.WriteLine("sum1 = " + sum1);
            Console.WriteLine("sum2 = " + sum2);
            Console.WriteLine("final = " + final);
            Console.ReadLine();

        }

        static int SumOfSquare(int limit)
        {
            int sum = 0;
            for (int i = 0; i <= limit; i++)
            {
                sum += (i * i);
            }
            return sum;
        }

        static int SquareOfSum(int limit)
        {
            int sum = 0;
            for (int i = 0; i <= limit; i++)
            {
                sum += i;
            }
            sum = sum * sum;
            return sum;
        }
    }
}
