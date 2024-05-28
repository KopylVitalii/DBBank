using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBank
{
    internal class CustomerQueries
    {
        public static string CreateCustomerQuery = "INSERT INTO Customer Values(@firstName, @lastName, @bank_account)";

        public static string GetCustomerQuery = "SELECT ID, FirstName, LastName, BankAccount FROM Customer";

        public static string GetCustomerById = "SELECT ID, FirstName, LastName, BankAccount FROM Customer WHERE ID = @customer_id";

        public static string DeleteCustomerById = "DELETE FROM Customer WHERE ID = @customer_id";

        public static string UpdateCustomerById = @"UPDATE Customer Set FirstName = @firstName, LastName = @lastName, BankAccount = @bank_account WHERE ID = @customer_id";
    }
}
