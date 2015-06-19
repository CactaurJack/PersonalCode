using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DTS
{
    public partial class AreaCodeDialog : Form
    {
        public AreaCodeDialog()
        {
            InitializeComponent();
        }

        public string AreaCode
        {
            get
            {
                return tbAreaCode.Text.Trim();
            }
        }
    }
}