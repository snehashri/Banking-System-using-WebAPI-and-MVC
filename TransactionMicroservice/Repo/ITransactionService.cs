using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionMicroservice.Models;

namespace TransactionMicroservice.Repo
{
   public  interface ITransactionService
    {
        TransactionStatus deposit(int AccId,float Amount);
        TransactionStatus withdraw(int AccId, float Amount);
        TransactionStatus transfer(int SrcAccountId,int TarAccountId, float amount);
        List<TransactionHistory> getTransactions(int Accid);
        string checkMinBalance(float balance);

        float getBalance(int AccId);
    }
}
