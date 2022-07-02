using BankUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Controllers
{
    public class CustomerController : Controller
    {
        Uri baseAddress;
        HttpClient client;
        IConfiguration _config;
        public CustomerController(IConfiguration config)
        {
            _config = config;
        }
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerMenu()
        {
            return View();
        }
        //post
        public IActionResult TransactionStatusView()
        {
            ViewBag.message = TempData["status"];
            return View();

        }
        //
        public IActionResult StatusFailView()
        {
            return View();

        }
        public IActionResult GetAccountByCustId()
        { 
            return View();

        }
        public IActionResult GetAllAccountByCustId()
        {
            return View();

        }

        public IActionResult DepositView()
        {
            return View();
        }

        public IActionResult GetAllStatementView()
        {
            return View();
        }
        
        public IActionResult EnterId()
        {
            return View();
        }

        public IActionResult WithdrawView()
        {
            ViewBag.message = TempData["status"];
            return View();
        }

        public IActionResult TransferView()
        {
            ViewBag.message = TempData["status"];
            return View();
        }
        public IActionResult ViewBalanceView()
        {
            return View();
        }

       
        public IActionResult GetAccountByCustIdProcess(CustomerDto obj)
        {
            List<Account> mylist = new List<Account>();
            using (client = new HttpClient())
            {
                baseAddress = new Uri(_config["Links:AccountApi"]);
                client = new HttpClient();
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/GetCustomerAccounts/" + obj.CustId ).Result;
                if (response.IsSuccessStatusCode)
                {

                    string data = response.Content.ReadAsStringAsync().Result;

                    mylist = JsonConvert.DeserializeObject<List<Account>>(data);
                }
            }
            return View("GetAllAccountByCustId", mylist);
        }

        [HttpPost]
        public IActionResult DepositViewProcess(AccountDto acc)
        {

            try
            {
                baseAddress = new Uri(_config["Links:TransactionApi"]);
                client = new HttpClient();
                client.BaseAddress = baseAddress;
                string data = JsonConvert.SerializeObject(acc);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Deposit/"+ acc.AccountId + "/" + acc.Amount, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string d = response.Content.ReadAsStringAsync().Result;

                    string result = JsonConvert.DeserializeObject<TransactionStatus>(d).Message;
                    if(string.Equals(result,"success"))
                    {
                        TempData["status"] = "Success";
                        return RedirectToAction("TransactionStatusView", "Customer");

                    }
                   
                }

            }
            catch (Exception e)
            {
                TempData["status"] = "Exception";
                return RedirectToAction("StatusFailView", "Customer");
            }

            TempData["status"] = "Failed";
            return RedirectToAction("StatusFailView", "Customer");

        }
        
        [HttpPost]
        public IActionResult StatementView(GetStatementDto obj)
        {
            List<StatementDto> mylist = new List<StatementDto>();
            using (client = new HttpClient())
            {
                baseAddress = new Uri(_config["Links:AccountApi"]);
                client = new HttpClient();
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/GetAccountStatements/" + obj.Accid + "/" + obj.Fromdate.ToString("O") + "/" + obj.Todate.ToString("O")).Result;
                if (response.IsSuccessStatusCode)
                {

                    string data = response.Content.ReadAsStringAsync().Result;

                    mylist = JsonConvert.DeserializeObject<List<StatementDto>>(data);
                }
            }
            return View("GetAllStatementView", mylist);
        }


       
        public IActionResult WithdrawViewProcess(AccountDto acc)
        {
            try
            {
                baseAddress = new Uri(_config["Links:TransactionApi"]);
                client = new HttpClient();
                client.BaseAddress = baseAddress;
                string data = JsonConvert.SerializeObject(acc);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/WithDraw/"+acc.AccountId+"/"+acc.Amount, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string d = response.Content.ReadAsStringAsync().Result;

                    string result = JsonConvert.DeserializeObject<TransactionStatus>(d).Message;
                    if (string.Equals(result, "success"))
                    {
                        TempData["status"] = "Success";
                        return RedirectToAction("TransactionStatusView", "Customer");

                    }
                   
                }

            }
            catch (Exception e)
            {
                TempData["status"] = "Exception";
                return RedirectToAction("StatusFailView", "Customer");
            }

            TempData["status"] = "Failed";
            return RedirectToAction("StatusFailView", "Customer");
        }

       
        public IActionResult TransferViewProcess(TransferDto acc)
        {
            try
            {
                baseAddress = new Uri(_config["Links:TransactionApi"]);
                client = new HttpClient();
                client.BaseAddress = baseAddress;
                string data = JsonConvert.SerializeObject(acc);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Transfer/"+acc.source_accid+"/"+acc.destination_accid+"/"+acc.amount, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string d = response.Content.ReadAsStringAsync().Result;

                    string result = JsonConvert.DeserializeObject<TransactionStatus>(d).Message;
                    if (string.Equals(result, "success"))
                    {
                        TempData["status"] = "Success";
                        return RedirectToAction("TransactionStatusView", "Customer");

                    }
                   
                }

            }
            catch (Exception e)
            {
                TempData["status"] = "Exception";
                return RedirectToAction("StatusFailView", "Customer");
            }

            TempData["status"] = "Failed";
            return RedirectToAction("StatusFailView", "Customer");
        }
        /*
        [HttpPost]
        public IActionResult CreateCustomer()
        {
            IEnumerable<CustomerDto> mylist = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" http://localhost:9898/api/Token");
                var responseTask = client.GetAsync("Customers");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readjob = result.Content.ReadAsAsync<IList<CustomerDto>>();
                    readjob.Wait();
                    mylist = readjob.Result;
                }
                else
                {
                    mylist = Enumerable.Empty<CustomerDto>();
                    ModelState.AddModelError(string.Empty, "Server Error Occurred");
                }
            }
            return View(mylist);
        }
        */

    }
}
