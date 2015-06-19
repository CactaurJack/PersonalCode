using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj15
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 20;
            int moves = (Factorial(2 * size)) / (2 * Factorial(size));
            Console.WriteLine(moves);
            Console.ReadLine();
        }

        static int Factorial(int x)
        {
            int temp = 1;
            for (int i = 1; i <= x; i++)
            {
                temp *= i;
            }
            return temp;
        }
    }
}
