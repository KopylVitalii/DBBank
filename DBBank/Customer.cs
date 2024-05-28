using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBank
{
    internal class Customer
    {
        public int ID { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string BankAccount { get; private set; }

        public Customer(string firstName, string lastName, string bankAccount)
        {
            FirstName = firstName;
            LastName = lastName;
            BankAccount = bankAccount;
        }

        public Customer(int id, string firstName, string lastName, string bankAccount) 
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            BankAccount = bankAccount;
        }
    }
}
