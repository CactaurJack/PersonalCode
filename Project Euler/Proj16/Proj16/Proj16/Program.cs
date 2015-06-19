using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.IO;

namespace Proj16
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger master = new BigInteger(1);
            for (int i = 0; i < 1000; i++)
            {
                master *= 2;
            }

            string temp = master.ToString();
            long sum = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                sum += (Convert.ToInt32(temp[i]) - 48);
            }



        }
    }
}
