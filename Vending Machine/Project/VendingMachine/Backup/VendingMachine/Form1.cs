//////////////////////////////////////////////////////////////////////
//      Vending Machine (Form1.cs)                                  //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008             //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VendingMachine
{
    public partial class Form1 : Form
    {
        // Static Constants (for initialization)
        public const int NUMCANTYPES = 4;
        public const int NUMCOINTYPES = 3;
        public static readonly int[] NUMCANS = {4,4,4,4};
        public static readonly int[] CANPRICES = { 120, 110, 130, 110 };
        public static readonly int[] COINVALUES = { 10, 50, 100 };
        public static readonly int[] NUMCOINS = { 15, 10, 5 }; 
                // 10Yen, 50Yen, 100Yen
        public static readonly int[] THRESHOLDS = { 4, 1, 0 };

        // Boundary Classes
        private AmountDisplay amountDisplay;
        private DebugDisplay displayPrice0, displayPrice1, displayPrice2, displayPrice3;
        private DebugDisplay displayNum10Yen, displayNum50Yen, displayNum100Yen;
        private DebugDisplay displayThreshold10Yen, displayThreshold50Yen, displayThreshold100Yen; 
        private DebugDisplay displayNumCans0, displayNumCans1, displayNumCans2, displayNumCans3;
        private Light soldOutLight0, soldOutLight1, soldOutLight2, soldOutLight3;
        private Light noChangeLight;
        private Light purchasableLight0, purchasableLight1, purchasableLight2, purchasableLight3;
        private CoinDispenser coinDispenser10Yen, coinDispenser50Yen, coinDispenser100Yen;
        private CanDispenser canDispenser;
        private CoinInserterLock coinInserterLock;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            amountDisplay = new AmountDisplay(txtAmount);
            displayNum10Yen = new DebugDisplay(txtNum10Yen);
            displayNum50Yen = new DebugDisplay(txtNum50Yen);
            displayNum100Yen = new DebugDisplay(txtNum100Yen);
            displayThreshold10Yen = new DebugDisplay(txtThreshold10Yen);
            displayThreshold50Yen = new DebugDisplay(txtThreshold50Yen);
            displayThreshold100Yen = new DebugDisplay(txtThreshold100Yen);
            displayPrice0 = new DebugDisplay(txtPrice0);
            displayPrice1 = new DebugDisplay(txtPrice1);
            displayPrice2 = new DebugDisplay(txtPrice2);
            displayPrice3 = new DebugDisplay(txtPrice3);
            displayNumCans0 = new DebugDisplay(txtNumCan0);
            displayNumCans1 = new DebugDisplay(txtNumCan1);
            displayNumCans2 = new DebugDisplay(txtNumCan2);
            displayNumCans3 = new DebugDisplay(txtNumCan3);
            soldOutLight0 = new Light(cbxSOLight0);
            soldOutLight1 = new Light(cbxSOLight1);
            soldOutLight2 = new Light(cbxSOLight2);
            soldOutLight3 = new Light(cbxSOLight3);
            noChangeLight = new Light(cbxNoChange);
            purchasableLight0 = new Light(cbxPurLight0);
            purchasableLight1 = new Light(cbxPurLight1);
            purchasableLight2 = new Light(cbxPurLight2);
            purchasableLight3 = new Light(cbxPurLight3);
            coinDispenser10Yen = new CoinDispenser(txtChange10Yen);
            coinDispenser50Yen = new CoinDispenser(txtChange50Yen);
            coinDispenser100Yen = new CoinDispenser(txtChange100Yen);
            canDispenser = new CanDispenser(txtCanDispenser);
            coinInserterLock = new CoinInserterLock(btnCoinInserter100Yen, btnCoinInserter50Yen,
                btnCoinInserter10Yen);

            // Initialization

        }

        private void btnCoinInserter10Yen_Click(object sender, EventArgs e)
        {
         
        }

        private void btnCoinInserter50Yen_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCoinInserter100Yen_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPurButtn0_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPurButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnPurButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void btnPurButton3_Click(object sender, EventArgs e)
        {

        }

        private void btnChangePickedUp_Click(object sender, EventArgs e)
        {

        }

        private void btnCanPickedUp_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
           
        }
    }
}