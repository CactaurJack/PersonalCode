using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj10
{
    class Program
    {
        static void Main(string[] args)
        {
            long sum = 0;
            for (int i = 2; i < 2000000; i++)
            {
                if (i > 3)
                {
                    i++;
                }
                if (PrimeTest(i))
                {
                    sum += i;
                }
                
            }
            Console.WriteLine(sum);
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
