using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.IO;

namespace Proj13
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("Input.txt");
            BigInteger[] Master = new BigInteger[100];
            BigInteger Final = new BigInteger();
            string temp;

            for (int i = 0; i < 100; i++)
            {
                temp = sr.ReadLine();
                Master[i] = BigInteger.Parse(temp);
            }

            for (int i = 0; i < Master.Length; i++)
            {
                Final += Master[i];
            }

            string check = Final.ToString();
            string last = "";
            for (int i = 0; i < 10; i++)
            {
                last += check[i];
            }

            Console.WriteLine(last);
            Console.ReadLine();
        }
    }
}
