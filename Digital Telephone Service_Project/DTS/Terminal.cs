using System;
using System.Collections.Generic;
using System.Text;

namespace DTS
{
    public class Terminal
    {
        //varible declaration
        private Controller controller;

        //Constructor
        public Terminal(Controller _controller)
        {
            controller = _controller;
        }

        //Activate meathod
        public void Activate()
        {
            //Declaration of dialog object
            PasswordDialog pDialog = new PasswordDialog();
            //Displays Dialog
            pDialog.ShowDialog();

            //Password check
            if (controller.PassCheck(pDialog.Password))
            {
               MainMenuDialog mDialog = new MainMenuDialog(controller);
               mDialog.ShowDialog();
            }

        }
    }
}
