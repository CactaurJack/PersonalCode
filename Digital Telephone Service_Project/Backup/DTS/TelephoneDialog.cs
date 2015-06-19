using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DTS
{
    public partial class TelephoneDialog : Form
    {
        public TelephoneDialog()
        {
            InitializeComponent();
        }
        public string[] TelephoneNumber
        {
            get
            {
                string[] telephoneNumber = new string[3];
                telephoneNumber[0] = tbAreaCode.Text.Trim();
                telephoneNumber[1] = tbNum1.Text.Trim();
                telephoneNumber[2] = tbNum2.Text.Trim();
                return telephoneNumber;
            }
        }
    }
}