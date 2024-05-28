using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBank
{
    internal class Banks
    {
        public int ID { get; private set; }
        public int BankAccountID { get; private set; }
        public int CustomerID { get; private set; }
        public Banks (int id, int bank_accountID, int customerID) 
        {
            ID = id;
            BankAccountID = bank_accountID;
            CustomerID = customerID;
        }
        public Banks Update(int bank_accountID, int customerID)
        {
            return new Banks(ID, bank_accountID, customerID);
        }

    }
}
