using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBank
{
    internal class CustomerService
    {
        public CustomerService() { }
        public async Task<bool> AddCustomer(Customer newCustomer)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    var cmd = new SqlCommand(CustomerQueries.CreateCustomerQuery, connection, transaction);
                    cmd.Parameters.Add("@firstName", System.Data.SqlDbType.VarChar, 255).Value = newCustomer.FirstName;
                    cmd.Parameters.Add("@lastName", System.Data.SqlDbType.VarChar, 255).Value = newCustomer.LastName;
                    cmd.Parameters.Add("@bank_account", System.Data.SqlDbType.VarChar, 255).Value = newCustomer.BankAccount;

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
        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(CustomerQueries.GetCustomerQuery, connection);

                var customerList = new List<Customer>();

                connection.Open();

                using (var sqlReader = cmd.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        var customer_id = Convert.ToInt32(sqlReader["ID"]);
                        var firstName = sqlReader["FirstName"].ToString();
                        var lastName = sqlReader["LastName"].ToString();
                        var bank_account = sqlReader["BankAccount"].ToString();
                        
                        customerList.Add(new Customer(customer_id, firstName, lastName, bank_account));
                    }
                }

                connection.Close();

                return customerList;
            }
        }
        public async Task<Customer> GetCustomer(int id)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(CustomerQueries.GetCustomerById, connection);

                Customer customer = null;
                cmd.Parameters.Add("@customer_id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();

                using (var sqlReader = cmd.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        var customer_id = Convert.ToInt32(sqlReader["ID"]);
                        var firstName = sqlReader["FirstName"].ToString();
                        var lastName = sqlReader["LastName"].ToString();
                        var bank_account = sqlReader["BankAccount"].ToString();

                        customer = new Customer(customer_id, firstName, lastName, bank_account);
                    }
                }

                connection.Close();

                return customer;
            }
        }
        public async Task<bool> UpdateCustomer(Customer updatedCustomer)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(CustomerQueries.UpdateCustomerById, connection);
                cmd.Parameters.Add("@customer_id", System.Data.SqlDbType.Int).Value = updatedCustomer.ID;
                cmd.Parameters.Add("@first_name", System.Data.SqlDbType.VarChar, 255).Value = updatedCustomer.FirstName;
                cmd.Parameters.Add("@last_name", System.Data.SqlDbType.VarChar, 255).Value = updatedCustomer.LastName;
                cmd.Parameters.Add("@bank_account", System.Data.SqlDbType.VarChar, 255).Value = updatedCustomer.BankAccount;

                connection.Open();
                var affectedRows = cmd.ExecuteNonQuery();
                connection.Close();

                return affectedRows == 1;
            }
        }
        public async Task<bool> DeleteCustomer(int id)
        {
            using (var connection = new SqlConnection(ApplicationSettings.ConnectionString))
            {
                var cmd = new SqlCommand(CustomerQueries.DeleteCustomerById, connection);
                cmd.Parameters.Add("@customer_id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                var affectedRows = cmd.ExecuteNonQuery();
                connection.Close();

                return affectedRows == 1;
            }
        }
    }
}
