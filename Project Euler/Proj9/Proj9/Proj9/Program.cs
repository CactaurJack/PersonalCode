using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj9
{
    class Program
    {
        static void Main(string[] args)
        {
            bool master = false;
            for(int a = 0; a < 1000; a++)
            {
                for (int b = 0; b < 1000; b++)
                {
                    for (int c = 0; c < 1000; c++)
                    {
                        if ((a + b + c) == 1000)
                        {
                            master = PyTest(a, b, c);
                            if (master)
                            {
                                Console.WriteLine(a + " " + b + " " + c);
                                Console.WriteLine(a * b * c);
                                Console.ReadLine();
                            }
                        }
                    }
                }
            }
        }

        static bool PyTest(int a, int b, int c)
        {
            int test = (a * a) + (b * b);
            int check = (c * c);
            if (test == check)
            {
                return true;
            }
            return false;
        }
    }
}
