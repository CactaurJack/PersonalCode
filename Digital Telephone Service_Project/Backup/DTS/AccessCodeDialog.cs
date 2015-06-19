using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DTS
{
    public partial class AccessCodeDialog : Form
    {
        public AccessCodeDialog()
        {
            InitializeComponent();
        }

        public string AccessCode
        {
            get
            {
                return tbAccessCode.Text.Trim();
            }
        }
    }
}