using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DTS
{
    public partial class DTS : Form
    {
        //Variable
        private Terminal terminal;
        private Telephone telephone;
        private Admin admin;
        private Controller controller;

        
        public DTS()
        {
            InitializeComponent();
        }

        //Cascading constructing of objects
        //Each constructor takes on a progressively more complex object
        private void DTS_Load(object sender, EventArgs e)
        {
            admin = new Admin("ksu");
            controller = new Controller(admin);
            terminal = new Terminal(controller);
            telephone = new Telephone(controller);
        }

        //termianl starter
        private void btnTerminal_Click(object sender, EventArgs e)
        {
            terminal.Activate();
        }

        //telephone starter
        private void btnTelephone_Click(object sender, EventArgs e)
        {
            telephone.Activate();
        }

        //quit button
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}