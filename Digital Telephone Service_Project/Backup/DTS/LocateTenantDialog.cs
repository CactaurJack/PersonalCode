using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DTS
{
    public partial class LocateTenantDialog : Form
    {
        public LocateTenantDialog()
        {
            InitializeComponent();
        }
        public string FirstName
        {
            get
            {
                return tbFirstName.Text.Trim();
            }
        }
        public string LastName
        {
            get
            {
                return tbLastName.Text.Trim();
            }
        }
    }
}