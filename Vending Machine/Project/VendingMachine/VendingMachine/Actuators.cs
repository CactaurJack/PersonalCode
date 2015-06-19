//////////////////////////////////////////////////////////////////////
//      Vending Machine (Actuators.cs)                              //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008             //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace VendingMachine
{
    public class AmountDisplay
    {
        TextBox tbx;
        public AmountDisplay(TextBox tbx)
        {
            this.tbx = tbx;
        }

        public void DisplayAmount(int amount)
        {
            this.tbx.Text = "\\" + amount;
        }
    }

    public class DebugDisplay
    {
        TextBox tbx;
        public DebugDisplay(TextBox tbx)
        {
            this.tbx = tbx;
        }

        public void Display(int amount)
        {
            this.tbx.Text = "" + amount;
        }

        public void Display(string st)
        {
            this.tbx.Text = st;
        }
    }

    public class Light
    {
        CheckBox cb;
        public Light(CheckBox cb)
        {
            this.cb = cb;
        }
        public void TurnOn()
        {
            cb.Checked = true;
        }
        public void TurnOff()
        {
            cb.Checked = false;
        }

        public Boolean IsOn()
        {
            return cb.Checked;
        }
    }

    public class CoinDispenser
    {
        TextBox txbChange;
        public CoinDispenser(TextBox txbChange)
        {
            this.txbChange = txbChange;
        }

        public void Actuate(int numCoins)
        {
            txbChange.Text = "" + numCoins;
        }

        public void Clear()
        {
            txbChange.Text = "";
        }
    }

    public class CanDispenser
    {
        TextBox txbCan;
        public CanDispenser(TextBox txbCan)
        {
            this.txbCan = txbCan;
        }

        public void Actuate(string canType)
        {
            this.txbCan.Text = canType;
        }

        public void Clear()
        {
            this.txbCan.Text = "";
        }
    }

    public class CoinInserterLock
    {
        Button btn100Yen, btn50Yen, btn10Yen;
        public CoinInserterLock(Button btn100Yen, Button btn50Yen, Button btn10Yen)
        {
            this.btn100Yen = btn100Yen;
            this.btn50Yen = btn50Yen;
            this.btn10Yen = btn10Yen;
        }

        public void Lock()
        {
            btn100Yen.Enabled = btn50Yen.Enabled = btn10Yen.Enabled = false;
        }

        public void UnLock()
        {
            btn100Yen.Enabled = btn50Yen.Enabled = btn10Yen.Enabled = true;
        }
    }
}
