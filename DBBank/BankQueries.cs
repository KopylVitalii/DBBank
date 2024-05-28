using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBank
{
    internal class BankQueries
    {
        public static string CreateBankQuery = "INSERT INTO Bank Values(@bank_accountID, @customerID)";

        public static string GetBankQuery = "SELECT ID, BankAccountID, CustomerID FROM Bank";

        public static string GetBankById = "SELECT ID, BankAccountID, CustomerID FROM Bank WHERE ID = @bank_id";

        public static string DeleteBankById = "DELETE FROM Bank WHERE ID = @bank_id";

        public static string UpdateBankById = @"UPDATE Bank Set BankAccountID = @bank_accountID, CustomerID = @customerID WHERE ID = @bank_id";
    }
}
