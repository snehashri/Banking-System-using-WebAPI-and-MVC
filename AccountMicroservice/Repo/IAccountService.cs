using AccountMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Repo
{
    public interface IAccountService
    {
       AccountCreationStatus createAccount(int CustomerID);

       IEnumerable<Account> getCustomerAccounts(int CustomerID);
       Account getAccountById(int AccountID);
        List<Statement> getAccountStatement(int AccountID,DateTime fromDate ,DateTime toDate);
       TransactionStatus Deposite(int AccountID, float Amount);
       TransactionStatus Withdraw(int AccountId, float Amount);

       float GetBalance(int AccountId);
       List<Account> GetAllAccounts();
    }
}
