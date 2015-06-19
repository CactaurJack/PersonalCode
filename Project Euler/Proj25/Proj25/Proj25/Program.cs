using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.IO;

namespace Proj25
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger[] Master = new BigInteger[100000];
            Master[0] = new BigInteger(1);
            Master[1] = new BigInteger(2);
            int count = 1;
            for (int i = 2; i < 100000; i++)
            {
                Master[i] = Master[count] + Master[count - 1];
                count++;
            }


            int test = 0;
            for (int i = 0; i < 100000; i++)
            {
                if (Master[i].ToString().Length == 1000)
                {
                    test = Master[i].ToString().Length;
                    StreamWriter sr = new StreamWriter("out.txt");
                    sr.Write(Master[i].ToString());
                    sr.Close();
                    Console.WriteLine(Master[i]);
                    Console.ReadLine();
                    test = i;
                    break;
                }
            }

            test = test;

        }
    }
}
