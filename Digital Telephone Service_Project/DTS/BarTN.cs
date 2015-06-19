using System;
using System.Collections.Generic;
using System.Text;

namespace DTS
{
    //Marks class for serilization
    [Serializable]
    public class BarTN : Bar
    {
        //Variable
        private string[] Number;

        //Constructor
        public BarTN(string[] _Number)
        {
            Number = _Number;
        }

        //Start of interface meathods 
        #region Bar Members

        //Checks if number is barred
        public bool IsBar(string[] inNumber)
        {
            if (Number[0] == inNumber[0] && Number[1] == inNumber[1] && Number[2] == inNumber[2])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Blank meathod from interface, used in BarAC
        public bool Check(string areaCode)
        {
            return false;
        }

        //Checks if number is barred
        public bool Check(string[] inNumber)
        {
            return IsBar(inNumber);
        }

        #endregion
    }
}
