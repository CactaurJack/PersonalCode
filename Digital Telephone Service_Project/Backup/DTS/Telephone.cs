using System;
using System.Collections.Generic;
using System.Text;

namespace DTS
{
    public class Telephone
    {
        //Varible declaration
        private Call call;
        private Controller controller;
        private Tenant tenant;

        //Constructor
        public Telephone(Controller _controller)
        {
            controller = _controller;
        }

        //Activate meathod
        public void Activate()
        {
            //Dialog declaration and display
            AccessCodeDialog acDialog = new AccessCodeDialog();
            acDialog.ShowDialog();
            string AccessCode = acDialog.AccessCode;
            tenant = controller.FindTenant(AccessCode);

            //error check
            if (tenant != null)
            {
                TelephoneDialog tDialog = new TelephoneDialog();
                tDialog.ShowDialog();
                string[] Number = tDialog.TelephoneNumber;
                //check if number is barred
                if (!tenant.IsBar(Number))
                {
                    //Call object made
                    call = new Call(Number, DateTime.Now);
                    new ConnectedDialog().ShowDialog();
                    call._FinishTime = DateTime.Now;
                    //Call added to list
                    tenant.AddCall(call);
                    //Local varibles reset
                    call = null;
                    tenant = null;
                }
            }

        }

    }
}
