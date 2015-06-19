using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Time_Check
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu = "";
            double time = 0.0;
            double totalTime = 0.0;
            List<TimeList> timeList = new List<TimeList>();

            while(true)
            {
                Console.Write("Enter command or time completed: ");
                menu = Console.ReadLine();
                
                if (menu.Equals("q"))
                {
                    break;
                }

                if (menu.Equals("c"))
                {
                    Console.Write("Enter tape time: ");
                    totalTime = timeFix(Console.ReadLine());
                    Console.WriteLine("Tape time in decimal minutes = " + totalTime);
                    continue;
                }

                if (menu.Equals("p"))
                {
                    foreach (TimeList x in timeList)
                    {
                        Console.WriteLine(x.printFormat());
                    }
                    continue;
                }

                if (menu.Equals("cl"))
                {
                    timeList = new List<TimeList>();
                    continue;
                }

                if (menu.Equals("start"))
                {
                    TimeList temp = new TimeList(timeList[timeList.Count - 1]);
                    temp.dateNow();
                    timeList.Add(temp);
                    
                }

                else
                {

                    time = timeFix(menu);
                    if (totalTime == 0.0)
                    {
                        Console.WriteLine("Error: Total tape time not entered");
                    }

                    else
                    {
                        timeList.Add(new TimeList(time, totalTime));
                        Console.WriteLine("Percent Complete: " + PercentCalc(time, totalTime) + "%");
                    }
                }
            }

        }

        public static double timeFix(string _input)
        {
            string first = "";
            string second = "";
            double final = 0.0;

            if (_input.Length < 4)
            {
                Console.WriteLine("Incorrect time format");
                return 0.0;
            }
            first = _input.Substring(0, _input.Length - 3);
            second = _input.Substring((_input.Length - 2), 2);
            final = Convert.ToDouble(first);

            final += (Convert.ToDouble(second) / 60);

            return final;
        }

        public static double PercentCalc (double _time, double totalTime)
        {
            return (_time / totalTime) * 100;
        }

        public static double Average(List<TimeList> _list)
        {
            double temp = 0.0;
            double final = 0.0;
            for (int i = 1; i < _list.Count; i++)
            {
               
            }

            return 0.0;
        }

    }


    [Serializable()]
    public class TimeList
    {
        double tapeTime;
        double time;
        double percent;
        public DateTime start;
        DateTime stop;

        public TimeList(double _time, double _tapeTime)
        {
            time = _time;
            tapeTime = _tapeTime;
            percent = (time / tapeTime) * 100;
            start = DateTime.Now;
        }

        public TimeList(TimeList _input)
        {
            time = _input.time;
            tapeTime = _input.tapeTime;
            percent = (time / tapeTime) * 100;
        }

        public string printFormat()
        {
            return "Completed: " + time + " || " + percent + "% || " + start;
        }

        public void dateNow()
        {
            start = DateTime.Now;
        }

        

    }
}
