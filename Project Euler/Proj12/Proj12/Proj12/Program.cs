using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proj12
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 1;
            int temp;
            int check;
            int top = 0;

            //temp = Triangle(7);
            //check = Check(temp);
            //Console.WriteLine(check);
            //Console.WriteLine(temp);
            //Console.ReadLine();

            while (true)
            {
                temp = Triangle(count);
                check = Check(temp);

                if (top < check)
                {
                    top = check;
                }
                

                if (check >= 500)
                {
                    Console.WriteLine(check);
                    Console.WriteLine(temp);
                    Console.ReadLine();
                    break;
                }
                count++;

            }
            
        }

        public static int Triangle(int x)
        {
            int temp = 0;
            for (int i = 0; i <= x; i++)
            {
                temp += i;
            }
            return temp;
        }

        public static int Check(int x)
        {
            int check = 0;
            for (int i = 1; i <= x; i++)
            {
                if ((x % i) == 0)
                {
                    check++;
                }
            }
            return check;
        }
    }
}
