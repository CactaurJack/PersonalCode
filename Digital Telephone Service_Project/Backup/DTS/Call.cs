using System;
using System.Collections.Generic;
using System.Text;

namespace DTS
{
    //Marks class for serialization
    [Serializable]
    public class Call
    {
        //Varible declaration
        private DateTime Start;
        private DateTime Finish;
        private string[] Number;

        //Constructor
        public Call(string[] inNumber, DateTime inStart)
        {
            Number = inNumber;
            Start = inStart;
        }

        //Time setter
        public DateTime _FinishTime
        {
            set
            {
                Finish = value;
            }
        }

        //This override took a long time to hammer out
        //I found how to use the string.Concat meathod online
        //This was the last thing I wrote because for a long time it didn't work
        public override string ToString()
        {
            return string.Concat(new object[] { Number[0], "-", Number[1], "-", Number[2], " : ", Start, " --- ", Finish });
        }

    }
}
