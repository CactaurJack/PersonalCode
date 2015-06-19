using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj2
{
    class Program
    {
        static void Main(string[] args)
        {
            int previous = -1; int next = 1;
            int position = 0;
            int count = 0;
            int sum = 0;
            int[] Master = new int[35];
            //Console.WriteLine("Enter the position");
            //position = int.Parse(Console.ReadLine());

            while (sum < 3800000)
            {
                sum = next + previous;
                Master[position] = sum;
                previous = next;
                next = sum;
                //Console.WriteLine(next);
                position++;
            }

            sum = 0;
            for (int i = 0; i < Master.Length - 1; i++)
            {
                if (Master[i] % 2 == 0)
                {
                    sum += Master[i];
                }
            }

            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
