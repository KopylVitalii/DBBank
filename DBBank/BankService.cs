using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBank
{
    internal class BankService
    {
        public BankService() { }
        public async Task<bool> AddBank(Banks newBank)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    var cmd = new SqlCommand(BankQueries.CreateBankQuery, connection, transaction);
                    cmd.Parameters.Add("@bank_accountID", System.Data.SqlDbType.Int, 10).Value = newBank.BankAccountID;
                    cmd.Parameters.Add("@customerID", System.Data.SqlDbType.Int, 10).Value = newBank.CustomerID;

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
        public async Task<IEnumerable<Banks>> GetBanks()
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(BankQueries.GetBankQuery, connection);

                var bankList = new List<Banks>();

                connection.Open();

                using (var sqlReader = cmd.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        var bankId = Convert.ToInt32(sqlReader["ID"]);
                        var bank_accountID = Convert.ToInt32(sqlReader["BankAccountID"]);
                        var customerID = Convert.ToInt32(sqlReader["CustomerID"]);

                        bankList.Add(new Banks(bankId, bank_accountID, customerID));
                    }
                }

                connection.Close();

                return bankList;
            }
        }
        public async Task<Banks> GetBank(int id)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(BankQueries.GetBankById, connection);

                Banks bank = null;
                cmd.Parameters.Add("@bank_id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();

                using (var sqlReader = cmd.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        var bankId = Convert.ToInt32(sqlReader["ID"]);
                        var bank_accountID = Convert.ToInt32(sqlReader["BankAccountID"]);
                        var customerID = Convert.ToInt32(sqlReader["CustomerID"]);

                        bank = new Banks(bankId, bank_accountID, customerID);
                    }
                }

                connection.Close();

                return bank;
            }
        }
        public async Task<bool> UpdateBank(Banks updatedBank)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(BankQueries.UpdateBankById, connection);
                cmd.Parameters.Add("@bank_id", System.Data.SqlDbType.Int).Value = updatedBank.ID;
                cmd.Parameters.Add("@bank_accountID", System.Data.SqlDbType.Int, 10).Value = updatedBank.BankAccountID;
                cmd.Parameters.Add("@customerID", System.Data.SqlDbType.Int, 10).Value = updatedBank.CustomerID;

                connection.Open();
                var affectedRows = cmd.ExecuteNonQuery();
                connection.Close();

                return affectedRows == 1;
            }
        }
        public async Task<bool> DeleteBank(int id)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(BankQueries.DeleteBankById, connection);
                cmd.Parameters.Add("@bank_id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                var affectedRows = cmd.ExecuteNonQuery();
                connection.Close();

                return affectedRows == 1;
            }
        }

    }
}

