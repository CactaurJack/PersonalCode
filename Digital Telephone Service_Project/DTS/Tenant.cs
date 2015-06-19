using System;
using System.Collections.Generic;
using System.Text;

namespace DTS
{
    [Serializable]
    public class Tenant
    {
        //Variable declaration
        private string AccessCode;
        private List<Bar> barList = new List<Bar>();
        private List<Call> callList = new List<Call>();
        private string FirstName;
        private string LastName;

        //Tenant Constructor
        public Tenant(string _FisrtName, string _LastName, string _AccessCode)
        {
            FirstName = _FisrtName;
            LastName = _LastName;
            AccessCode = _AccessCode;
        }

        //Add call, uses list function
        public void AddCall(Call call)
        {
            callList.Add(call);
        }

        //Add bar AC/TN, uses list functio
        public void AddBarList(Bar bar)
        {
            barList.Add(bar);
        }

        //Clear list with list function
        public void ClearCall()
        {
            callList.Clear();
        }

        //Specific removal of barred Area Code
        public void RemoveBarredAC(string _Code)
        {
            foreach (Bar x in barList)
            {
                if (x.Equals(_Code))
                {
                    barList.Remove(x);
                    break;
                }
            }
        }

        //Specific removal of barred Telephone Number
        public void RemoveBarredTN(string[] _Number)
        {
            foreach (Bar x in barList)
            {
                if (x.Equals(_Number))
                {
                    barList.Remove(x);
                    break;
                }
            }
        }

        //Checks to see if number is barred
        public bool IsBar(string[] _Number)
        {
            foreach (Bar x in barList)
            {
                if (x.IsBar(_Number))
                {
                    return true;
                }
            }
            return false;
        }

        //Checker for Access Codes
        public bool Check(string _AccessCode)
        {
            if (AccessCode == _AccessCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Check for name
        public bool Check(string _FirstName, string _LastName)
        {
            if (FirstName == _FirstName && LastName == _LastName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Array calls are ussed to make the display print properly
        public Bar[] GetArrayBar()
        {
            return barList.ToArray();
        }

        public Call[] GetArrayCall()
        {
            return callList.ToArray();
        }

        //Very annoying piece of code that took me a while to figure out
        //the print methods within the display boxes use the ToString to print
        //this override stops it from printing out "DTS.Teanent"
        public override string ToString()
        {
            string temp = FirstName + " " + LastName + " : " + AccessCode;
            return temp;
        }

    }
}
