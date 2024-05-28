using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBank
{
    internal class BankAccount
    {
        public int ID { get; private set; }

        public string NumberAccount { get; private set; }

        public decimal Rate { get; private set; }
        public BankAccount(string numberAccount, decimal rate)
        {
            NumberAccount = numberAccount;
            Rate = rate;
        }
        public BankAccount(int id, string numberAccount, decimal rate)
        {
            ID = id;
            NumberAccount = numberAccount;
            Rate = rate;
        }
    }
}
