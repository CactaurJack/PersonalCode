using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;


namespace DTS
{
    public class Controller
    {
        //Varible declaration
        private Admin admin;
        private List<Tenant> TenantList = new List<Tenant>();

        //Constructor
        public Controller(Admin _admin)
        {
            admin = _admin;
        }

        //Adds tenant
        public void AddTenant(Tenant _tenant)
        {
            TenantList.Add(_tenant);
        }

        //Finds tenant and then removes it
        public void RemoveTenant(string _FirstName, string _LastName)
        {
            foreach (Tenant x in TenantList)
            {
                if (x.Check(_FirstName, _LastName))
                {
                    TenantList.Remove(x);
                    break;
                }
            }
        }

        //Finds tenant
        public Tenant FindTenant(string _AccessCode)
        {
            foreach (Tenant x in TenantList)
            {
                if (x.Check(_AccessCode))
                {
                    return x;
                }
            }
            return null;
        }

        //Overload find meathod
        public Tenant FindTenant(string _FirstName, string _LastName)
        {
            foreach (Tenant x in TenantList)
            {
                if (x.Check(_FirstName, _LastName))
                {
                    return x;
                }
            }
            return null;
        }

        //Checks password
        public bool PassCheck(string _password)
        {
            if (admin.PassCheck(_password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //GetArray meathod for output
        public Tenant[] GetArray()
        {
            return TenantList.ToArray();
        }

        //Load meathod (Binary)
        public void Load()
        {
            FileStream serialStream = new FileStream("data.stn", FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter format = new BinaryFormatter();
            TenantList = (List<Tenant>)format.Deserialize(serialStream);
            serialStream.Close();
        }

        //Save meathod (Binary)
        public void Save()
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream serialStream = new FileStream("data.stn", FileMode.Create);
            format.Serialize(serialStream, TenantList);
            serialStream.Close();
        }

    }
}
