using AccountMicroservice.DBContext;
using AccountMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Repo
{
    public class AccountService : IAccountService
    {
        private readonly AccountContext _context;
        public AccountService(AccountContext context)
        {
            _context = context;
        }


        public AccountCreationStatus createAccount(int CustomerID)
        {
            AccountCreationStatus result = new AccountCreationStatus();
            try
            {
                Account account = new Account();
                account.CustomerID = CustomerID;
                account.AccountBalance = 0;

                var accountId = _context.Add(account);
                _context.SaveChanges();
                result.AccountId= accountId.Entity.AccountID;
                result.Message = "Account created successfully!";
            
            }
            catch (Exception ex)
            {
                result.Message = "Error : " + ex.Message;
                result.AccountId = 0;
            }
            return result;
        }

        public Account getAccountById(int AccountID)
        {
            Account account;
            try
            {
                account = _context.accounts.Find(AccountID);
            }
            catch (Exception)
            {
                throw;
            }
            return account;
        }

        public List<Statement> getAccountStatement(int AccId, DateTime fromDate, DateTime toDate)
        {
            List<Statement> statement=new List<Statement>();
            DateTime start = fromDate;
            DateTime end = toDate;
            try
            {
                statement = _context.statements.Where(c => c.AccountID==AccId && c.Date>=start && c.Date<=end ).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return statement;
        }

        public float GetBalance(int AccountId)
        {
            float mybalance = 0;
            try
            {
                Account myaccount = new Account();
                myaccount = _context.accounts.Find(AccountId);
                mybalance = myaccount.AccountBalance;
            }
            catch (Exception)
            {
                throw;
            }
            return mybalance;
        }

        public IEnumerable<Account> getCustomerAccounts(int CustID)
        {
            IEnumerable<Account> accountList;
            try
            {
               accountList= _context.accounts.Where(c => c.CustomerID == CustID);
            }
            catch (Exception)
            {
                throw;
            }
            return accountList;
        }
        public TransactionStatus Deposite(int AccountID, float Amount)
        {
            TransactionStatus result = new TransactionStatus();
            
            Account account = _context.accounts.Find(AccountID);
            if (account != null)
            {
                using var transaction = _context.Database.BeginTransaction();

                try
                {
                    //transaction.CreateSavepoint("BeforeDeposite");
                    
                    account.AccountBalance += Amount;
                    _context.Update(account);
                    _context.SaveChanges();

                    result.DestinationBalance = account.AccountBalance;
                    result.Message = "success";

                    Statement statement = new Statement();
                    statement.AccountID = AccountID;
                    statement.Date = DateTime.Now;
                    statement.Deposite = Amount;
                    statement.Withdrawal = 0;
                    statement.ClosingBalance = account.AccountBalance;
                    statement.Ref = 0;
                    statement.Description = "Deposited";

                    _context.statements.Add(statement);
                    _context.SaveChanges();

                    transaction.Commit();
                    return result;
                }
                catch(Exception ex)
                {
                    result.DestinationBalance = account.AccountBalance;
                    result.Message = "Error : " + ex.Message;
                    //transaction.RollbackToSavepoint("BeforeDeposite");
                }
            }
            return result;
        }

        public TransactionStatus Withdraw(int AccountId, float Amount)
        {
            TransactionStatus result = new TransactionStatus();

            Account account = _context.accounts.Find(AccountId);
            if (account != null)
            {
                using var transaction = _context.Database.BeginTransaction();

                try
                {
                    //transaction.CreateSavepoint("BeforeWithdraw");
                    if (account.AccountBalance - Amount >= 0)
                    {
                        account.AccountBalance -= Amount;
                        _context.Update(account);
                        _context.SaveChanges();

                        result.SourceBalance = account.AccountBalance;
                        result.Message = "success";

                        Statement statement = new Statement();
                        statement.AccountID = AccountId;
                        statement.Date = DateTime.Now;
                        statement.Deposite = 0;
                        statement.Withdrawal = Amount;
                        statement.ClosingBalance = account.AccountBalance;
                        statement.Ref = 0;
                        statement.Description = "Withdrawn";

                        _context.statements.Add(statement);
                        _context.SaveChanges();

                        transaction.Commit();
                        return result;
                    }
                    else
                    {
                        result.Message = "failed";
                    }
                }
                catch (Exception ex)
                {
                    result.SourceBalance = account.AccountBalance;
                    result.Message = "Error : " + ex.Message;
                    //transaction.RollbackToSavepoint("BeforeWithdraw");
                }
            }
            return result;
        }

        public List<Account> GetAllAccounts()
        {
            return _context.accounts.ToList();
        }
    }
}
