using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DTS
{
    public partial class ListDialog : Form
    {
        public ListDialog()
        {
            InitializeComponent();
        }

        public void AddDisplayItems(object[] objects)
        {
            lbList.Items.AddRange(objects);
        }
    }
}