using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Proj8
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("Input.txt");
            string MasterIn = sr.ReadToEnd();
            int input = MasterIn.Length;
            int count = 1;
            int spacer = 0;
            string temp = "";
            int highest = 0;
            int test;
            while (count < input - 4)
            {
                for (int i = spacer; i < (spacer + 5); i++)
                {
                    temp += MasterIn[i];
                }
                spacer++;
                count++;
                test = MultNums(temp);
                if (test > highest)
                {
                    highest = test;
                }
                temp = "";

            }
            Console.WriteLine(highest);
            Console.ReadLine();
        }

        static int MultNums(string input)
        {
            int sum = 1;
            int[] temp = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                temp[i] = (Convert.ToInt32(input[i]) - 48);
            }

            for (int i = 0; i < input.Length; i++)
            {
                sum *= temp[i];
            }
            return sum;
        }
    }
}
