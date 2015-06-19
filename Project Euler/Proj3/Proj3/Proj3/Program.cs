using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj3
{
    class Program
    {
        static void Main(string[] args)
        {
            long a, b;
            Console.WriteLine("Please enter your integer: ");
            a = long.Parse(Console.ReadLine());
            for (b = 2; a > 1; b++)
                if (a % b == 0)
                {
                    int x = 0;
                    while (a % b == 0)
                    {
                        a /= b;
                        x++;
                    }
                    Console.WriteLine("{0} is a prime factor {1} times!", b, x);
                }
            Console.ReadLine();

        }

        

    }
}
