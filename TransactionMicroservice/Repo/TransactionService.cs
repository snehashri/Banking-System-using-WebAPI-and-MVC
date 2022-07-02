using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TransactionMicroservice.DBContext;
using TransactionMicroservice.Models;

namespace TransactionMicroservice.Repo
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionContext _context;
        public TransactionService(TransactionContext context)
        {
            _context = context;
        }
        
        public TransactionStatus deposit(int AccId, float Amount)
        {
            TransactionStatus result = new TransactionStatus();
            try
            {
                HttpClient client = new HttpClient();
                Uri baseAddress = new Uri("http://localhost:53159/api/Account");
                client.BaseAddress = baseAddress;
                string data = JsonConvert.SerializeObject(AccId);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Deposit/" + AccId+"/"+Amount,content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string d = response.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<TransactionStatus>(d);
                    if(string.Equals(result.Message,"success"))
                    {
                        TransactionHistory tHist=new TransactionHistory()
                        {
                            Account_ID = AccId,
                            Amount_of_Transaction = Amount,
                            Date_of_Transaction = DateTime.Now,
                            Description = "Deposited"

                        };
                        _context.transactionHistories.Add(tHist);
                        _context.SaveChanges();
                    }

                }
                return result;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public TransactionStatus withdraw(int AccId, float Amount)
        {
            TransactionStatus result = new TransactionStatus();
            try
            {
                HttpClient client = new HttpClient();
                Uri baseAddress = new Uri("http://localhost:53159/api/Account");
                client.BaseAddress = baseAddress;
                string data = JsonConvert.SerializeObject(AccId);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                
                float balance = getBalance(AccId);
                float balanceAfterWithdraw = balance - Amount;
                var permission = checkMinBalance(balanceAfterWithdraw);
                if (permission == "allowed")
                {
                    HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Withdraw/" + AccId + "/" + Amount, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string d = response.Content.ReadAsStringAsync().Result;

                        result = JsonConvert.DeserializeObject<TransactionStatus>(d);
                        if (string.Equals(result.Message, "success"))
                        {
                            TransactionHistory tHist = new TransactionHistory()
                            {
                                Account_ID = AccId,
                                Amount_of_Transaction = Amount,
                                Date_of_Transaction = DateTime.Now,
                                Description = "Withdrawn"

                            };
                            _context.transactionHistories.Add(tHist);
                            _context.SaveChanges();
                        }

                    }
                }
                else
                {
                    result.Message = "failed";
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TransactionHistory> getTransactions(int Accid)
        {
            List<TransactionHistory> tranList=new List<TransactionHistory>();
            try
            {
                tranList = _context.transactionHistories.Where(c => c.Account_ID == Accid).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return tranList;
        }

        public TransactionStatus transfer(int SrcAccountId, int TarAccountId, float transactionamount)
        {
            TransactionStatus result = new TransactionStatus();
            try
            {
                float balance = getBalance(SrcAccountId);
                float balanceAfterWithdraw = balance - transactionamount;
                var permission = checkMinBalance(balanceAfterWithdraw);
                if(permission =="allowed")
                {
                    if(string.Equals(withdraw(SrcAccountId,transactionamount).Message,"success"))
                    {
                        if(string.Equals(deposit(TarAccountId,transactionamount).Message,"success"))
                        {
                            result.SourceBalance = getBalance(SrcAccountId);
                            result.Message = "success";
                            return result;
                        }
                        else
                        {
                            deposit(SrcAccountId, transactionamount);
                            
                        }
                    }

                }
               
                    result.Message = "failed";
                    return result;
                
            }
            catch (Exception e)
            {
                result.Message = "Error : "+e.Message;
                return result;
            }
        }


        public string checkMinBalance(float balance)
        {
            string result = null;
            try
            {
                HttpClient client = new HttpClient();
                Uri baseAddress = new Uri("http://localhost:61833/api/Rules");
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/EvaluateMinimumBalance/" + balance).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    result=JsonConvert.DeserializeObject<RuleStatusDto>(data).status;
                    
                }
                return result;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public float getBalance(int AccId)
        {
            float balance=0;
            try
            {
                HttpClient client = new HttpClient();
                Uri baseAddress = new Uri("http://localhost:53159/api/Account");
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/GetBalance/" + AccId).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    balance = JsonConvert.DeserializeObject<float>(data);
                }
                return balance;
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
