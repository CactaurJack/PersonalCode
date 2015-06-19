using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Proj11
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("Input.txt");
            string[] RawIn = new string[20];
            int[,] ParsedIn = new int[20,20];
            string temp = "";
            int count = 0;

            for (int i = 0; i < 20; i++)
            {
                RawIn[i] = sr.ReadLine();
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < RawIn[i].Length; j++)
                {
                    temp += RawIn[i][j];
                    temp += RawIn[i][j+1];
                    j += 2;
                    ParsedIn[i, count] = (Convert.ToInt32(temp));
                    count++;
                    temp = "";
                }
                count = 0;
            }
            int x = AddUpDown(ParsedIn);
            int y = DiagLeft(ParsedIn);

        }

        static int AddUpDown(int[,] x)
        {
            int product = 1;
            int highest = 0;
            int count = 0;
            int test = 0;
            while (count < 17)
            {
                for (int i = 0; i < 20; i++)
                {
                    for (int j = count; j < count + 4; j++)
                    {
                        product *= x[j, i];
                    }

                    if (product > highest)
                    {
                        highest = product;
                    }
                    product = 1;

                }
                count++;
            }

            return highest;
        }

        static int DiagLeft(int[,] z)
        {
            int product = 1;
            int highest = 0;
            int count = 0;
            int test;
            int x = 0;
            int y = 3;
            int temp = y;
            for (int k = 0; k < 17; k++)
            {
                
                for (int i = 0; i < 17; i++)
                {
                    for (int j = i; j < (i + 4); j++)
                    {
                        test = z[temp, j];
                        product *= z[temp, j];
                        temp--;
                    }
                    temp = y;
                    if (product > highest)
                    {
                        highest = product;
                    }
                    product = 1;
                }
                y++;
                temp = y;
            }
            return highest;
        }

        static int DiagRight(int[,] x)
        {
            return 0;
        }
    }
}
