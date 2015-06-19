using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DTS
{
    public partial class TenantMenuDialog : Form
    {
       // You can add some fileds
        Tenant tenant;

        public TenantMenuDialog()
        {
            InitializeComponent();
        }

        public TenantMenuDialog(Tenant _tenant) : this()
        {
            tenant = _tenant;
        }
        //public TenantMenuDialog(XXX if you want to add arguments, use this form):this()
        //{
        //  
        //}

        private void btnDone_Click(object sender, EventArgs e)
        {
           this. Visible = false;
        }

        private void btnBarAreaCode_Click(object sender, EventArgs e)
        {
            AreaCodeDialog acDialog = new AreaCodeDialog();
            acDialog.ShowDialog();
            BarAC inBar = new BarAC(acDialog.AreaCode);
            tenant.AddBarList(inBar);
        }

        private void btnBarTelNumber_Click(object sender, EventArgs e)
        {
            TelephoneDialog tDialog = new TelephoneDialog();
            tDialog.ShowDialog();
            BarTN inBar = new BarTN(tDialog.TelephoneNumber);
            tenant.AddBarList(inBar);
        }

        private void btnUnBarAreaCode_Click(object sender, EventArgs e)
        {
            AreaCodeDialog acDialog = new AreaCodeDialog();
            acDialog.ShowDialog();
            tenant.RemoveBarredAC(acDialog.AreaCode);
        }

        private void btnUnBarTelNumber_Click(object sender, EventArgs e)
        {
            TelephoneDialog tDialog = new TelephoneDialog();
            tDialog.ShowDialog();
            tenant.RemoveBarredTN(tDialog.TelephoneNumber);
        }

        private void btnListBarNumbers_Click(object sender, EventArgs e)
        {
            Bar[] BarArray = tenant.GetArrayBar();
            ListDialog lDialog = new ListDialog();
            lDialog.AddDisplayItems(BarArray);
            lDialog.ShowDialog();
        }

        private void btnListCalls_Click(object sender, EventArgs e)
        {
            Call[] CallArray = tenant.GetArrayCall();
            ListDialog lDialog = new ListDialog();
            lDialog.AddDisplayItems(CallArray);
            lDialog.ShowDialog();
        }

        private void btnClearCalls_Click(object sender, EventArgs e)
        {
            tenant.ClearCall();
        }
    }
}