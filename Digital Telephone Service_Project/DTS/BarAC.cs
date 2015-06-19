using System;
using System.Collections.Generic;
using System.Text;

namespace DTS
{
    //Marks class for serialization
    [Serializable]
    public class BarAC : Bar
    {
        //Variables
        private string Code;

        //Constructor
        public BarAC(string inCode)
        {
            Code = inCode;
        }

        //Interface Meathods
        #region Bar Members

        //checks number
        public bool IsBar(string[] inNumber)
        {
            if (inNumber[0] == Code)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Checks area code
        public bool Check(string areaCode)
        {
            return (Code == areaCode);
        }

        //Blank meathod from interface
        public bool Check(string[] inNumber)
        {
            return false;
        }

        #endregion

        
    }
}
