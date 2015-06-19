using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DTS
{
    public partial class MainMenuDialog : Form
    {
        //You can add some fileds
        private Controller controller;

        public MainMenuDialog()
        {
            InitializeComponent();
        }

        //Overload constructor
        public MainMenuDialog(Controller _controller) : this()
        {
            controller = _controller;
        }

        //Provided code
        private void btnDone_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        //Add button makes new Teant object from input and adds it to the list
        private void btnAddTenant_Click(object sender, EventArgs e)
        {
            InputTenantDialog tDialog = new InputTenantDialog();
            tDialog.ShowDialog();
            string FirstName = tDialog.FirstName;
            string LastName = tDialog.LastName;
            string AccessCode = tDialog.AccessCode;
            Tenant tenant = new Tenant(FirstName, LastName, AccessCode);
            controller.AddTenant(tenant);
        }

        //Same as add but removes
        private void btnDeleteTenant_Click(object sender, EventArgs e)
        {
            LocateTenantDialog ltDialog = new LocateTenantDialog();
            ltDialog.ShowDialog();
            string FirstName = ltDialog.FirstName;
            string LastName = ltDialog.LastName;
            controller.RemoveTenant(FirstName, LastName);
        }

        //Gets Teant array and sends it to be outputted
        private void btnListTenants_Click(object sender, EventArgs e)
        {
            ListDialog lDialog = new ListDialog();
            Tenant[] TenantArray = controller.GetArray();
            lDialog.AddDisplayItems(TenantArray);
            lDialog.ShowDialog();
        }

        //Checks name and then sends user to Tenant menu
        private void btnWorkTenant_Click(object sender, EventArgs e)
        {
            LocateTenantDialog ltDialog = new LocateTenantDialog();
            ltDialog.ShowDialog();
            string FirstName = ltDialog.FirstName;
            string LastName = ltDialog.LastName;
            Tenant tenant = controller.FindTenant(FirstName, LastName);
            if (tenant != null)
            {
                TenantMenuDialog tmDialog = new TenantMenuDialog(tenant);
                tmDialog.ShowDialog();
            }

        }

        //Saves to "Data.stn"
        private void btnSave_Click(object sender, EventArgs e)
        {
            controller.Save();
        }

        //Restores from "Data.stn"
        private void btnRestore_Click(object sender, EventArgs e)
        {
            controller.Load();
        }
    }
}