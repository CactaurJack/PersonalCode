using System;
using System.Collections.Generic;
using System.Text;

namespace DTS
{
    public class Admin
    {
        //Variable
        private string password;

        //Constructor
        public Admin(string _password)
        {
            password = _password;
        }

        //Password checker
        public bool PassCheck(string _password)
        {
            if (_password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //private getter setter
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
    }
}
