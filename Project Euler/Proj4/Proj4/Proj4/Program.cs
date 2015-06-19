using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj4
{
    class Program
    {
        static void Main(string[] args)
        {
            string forward = "";
            string backward = "";
            int product = 0;
            int final = 0;

            for (int i = 999; i > 100; i--)
            {
                for (int j = 999; j > 100; j--)
                {
                    product = i * j;
                    forward = product.ToString();
                    backward = Reverse(forward);
                    if (forward.Equals(backward))
                    {
                        if (final < product)
                        {
                            final = product;
                        }
                        Console.WriteLine(final);
                        Console.ReadLine();
                        break;
                    }
                }
            }

        }

        static string Reverse(string x)
        {
            string ret = "";
            for (int i = x.Length - 1; i >= 0; i--)
            {
                ret += x[i];
            }
            return ret;
        }
    }
}
