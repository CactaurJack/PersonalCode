using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj5
{
    class Program
    {
        static void Main(string[] args)
        {
            int test = 1;
            int checker = 1;
            while (checker != 0)
            {
                checker = Check(test);
                test++;
            }
            Console.WriteLine(test - 1);
            Console.ReadLine();

        }

        static int Check (int x)
        {
            int sum = 0;
            for (int i = 1; i < 21; i++)
            {
                sum += x % i;
            }
            return sum;
        }
    }
}
