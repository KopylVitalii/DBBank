using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DBBank
{
    internal class BankAccountService
    {
        public BankAccountService() { }
        public async Task<bool> AddBankAccount(BankAccount newBankAccount)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    var cmd = new SqlCommand(BankAccountQueries.CreateBankAccountQuery, connection, transaction);
                    cmd.Parameters.Add("@number_account", System.Data.SqlDbType.VarChar, 255).Value = newBankAccount.NumberAccount;
                    cmd.Parameters.Add("@rate", System.Data.SqlDbType.Decimal, 10).Value = newBankAccount.Rate;

                    var affectedRows = cmd.ExecuteNonQuery();

                    transaction.Commit();

                    return affectedRows == 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return false;
                    }

                    return false;
                }
            }
        }
        public async Task<IEnumerable<BankAccount>> GetBankAccount()
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(BankAccountQueries.GetBankAccountQuery, connection);

                var bankAccountList = new List<BankAccount>();

                connection.Open();

                using (var sqlReader = cmd.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        var bank_account_id = Convert.ToInt32(sqlReader["ID"]);
                        var number_account = sqlReader["NumberAccount"].ToString();
                        var rate = Convert.ToDecimal(sqlReader["Rate"]);

                        bankAccountList.Add(new BankAccount(bank_account_id, number_account, rate));
                    }
                }

                connection.Close();

                return bankAccountList;
            }
        }
        public async Task<BankAccount> GetBankAccount(int id)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(BankAccountQueries.GetBankAccountById, connection);

                BankAccount bankAccount = null;
                cmd.Parameters.Add("@bank_account_id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();

                using (var sqlReader = cmd.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        var bank_account_id = Convert.ToInt32(sqlReader["ID"]);
                        var number_account = sqlReader["NumberAccount"].ToString();
                        var rate = Convert.ToDecimal(sqlReader["Rate"]);

                        bankAccount = new BankAccount(bank_account_id, number_account, rate);
                    }
                }

                connection.Close();

                return bankAccount;
            }
        }
        public async Task<bool> UpdateBankAccount(BankAccount updatedBankAccount)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(BankAccountQueries.UpdateBankAccountById, connection);
                cmd.Parameters.Add("@bank_account_id", System.Data.SqlDbType.Int).Value = updatedBankAccount.ID;
                cmd.Parameters.Add("@number_account", System.Data.SqlDbType.VarChar, 255).Value = updatedBankAccount.NumberAccount;
                cmd.Parameters.Add("@rate", System.Data.SqlDbType.Decimal, 10).Value = updatedBankAccount.Rate;

                connection.Open();
                var affectedRows = cmd.ExecuteNonQuery();
                connection.Close();

                return affectedRows == 1;
            }
        }
        public async Task<bool> DeleteBankAccount(int id)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(BankAccountQueries.DeleteBankAccountById, connection);
                cmd.Parameters.Add("@bank_account_id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                var affectedRows = cmd.ExecuteNonQuery();
                connection.Close();

                return affectedRows == 1;
            }
        }

    }
}
