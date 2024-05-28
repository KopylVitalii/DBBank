using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBank
{
    internal class BankAccountQueries
    {
        public static string CreateBankAccountQuery = "INSERT INTO BankAccount Values(@number_account, @rate)";

        public static string GetBankAccountQuery = "SELECT ID, NumberAccount, Rate FROM BankAccount";

        public static string GetBankAccountById = "SELECT ID, NumberAccount, Rate FROM BankAccount WHERE ID = @bank_account_id";

        public static string DeleteBankAccountById = "DELETE FROM BankAccount WHERE ID = @bank_account_id";

        public static string UpdateBankAccountById = @"UPDATE BankAccount Set NumberAccount = @number_account, Rate = @rate WHERE ID = @bank_account_id";
    }
}
