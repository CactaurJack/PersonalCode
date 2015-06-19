using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj7
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            int test = 2;
            while (count != 10001)
            {
                if (PrimeTest(test))
                {
                    count++;
                }
                test++;
            }
            Console.WriteLine(test - 1);
            Console.ReadLine();
        }

        static bool PrimeTest(int x)
        {
            int sum = 0;
            bool test = true;
            if (x > 3)
            {
                for (int i = 2; i < x; i++)
                {
                    if ((x % i) == 0)
                    {
                        test = false;
                        break;
                    }
                }
            }

            return test;
        }
    }
}
