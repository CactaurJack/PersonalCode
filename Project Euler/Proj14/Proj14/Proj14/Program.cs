using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj14
{
    class Program
    {
        public static long count = 0;

        static void Main(string[] args)
        {
            long hold = 113383;
            long counttop = 0;
            long counttemp = 0;
            long basic = hold;
            long biggest = 0;
            long num = 0;
            bool oddcheck = true;


            //while (hold < 1000000)
            //{
                if (!oddcheck)
                {
                    hold = even(hold);
                    oddcheck = true;
                }
                if (oddcheck)
                {
                    hold = odd(hold);
                    oddcheck = false;
                }

                counttemp = count;

                if (counttemp > counttop)
                {
                    counttop = counttemp;
                    biggest = basic;
                    num = basic;
                }

                basic++;
                hold = basic;
                count = 0;
            //}

            Console.WriteLine("Number of steps: " + counttop);
            Console.WriteLine("Number with most steps: " + biggest);
            Console.ReadLine();

        }

        static long even(long x)
        {
            count++;
            if (x == 1)
            {
                return 1;
            }
            long n = x/2;
            
            if (n % 2 == 0)
            {
                even(n);
            }
            if (n % 2 != 0)
            {
                odd(n);
            }
            return n;
        }

        static long odd(long x)
        {
            count++;
            if (x == 1)
            {
                return 1;
            }
            long n = (3 * x) + 1;
            if (n == 1)
            {
                return n;
            }
            if (n % 2 == 0)
            {
                even(n);
            }
            if (n % 2 != 0)
            {
                odd(n);
            }
            return n;
        }
    }
}
