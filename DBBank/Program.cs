using DBBank;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

var bankService = new BankService();
var bankAccountService = new BankAccountService();
var customerService = new CustomerService();

var account = new BankAccount("UA12345678973121556", 100.12m);
var customer = new Customer("Ivan","Ivanov", account.NumberAccount);
var bank = new Banks(1,1,1);
try
{
    var bankAccount_add = await bankAccountService.AddBankAccount(account);
    var customer_add = await customerService.AddCustomer(customer);
    var bank_add = await bankService.AddBank(bank);
}
catch (Exception e)
{
    Console.WriteLine(e);
}


Console.ReadKey();
