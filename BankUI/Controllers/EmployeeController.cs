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
    public class EmployeeController : Controller
    {
        Uri baseAddress;
        HttpClient client;
        IConfiguration _config;
        public EmployeeController(IConfiguration config)
        {
            _config = config;
         
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmployeeMenu()
        {
            return View();
        }



        public IActionResult TransactionStatusView()
        {
            ViewBag.message = TempData["status"];
            return View();

        }
        public IActionResult CreateCustomerView()
        {

            return View();
        }
        public IActionResult StatusFailView()
        {
            return View();

        }
        public IActionResult CreateCustomerViewProcess(CustomerDto dto)
        {
            try
            {
                baseAddress = new Uri(_config["Links:CustomerApi"]);
                client = new HttpClient();
                client.BaseAddress = baseAddress;
                string data = JsonConvert.SerializeObject(dto);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/AddCustomer/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["status"] = "Success";
                    return RedirectToAction("TransactionStatusView", "Employee");
                }

            }
            catch (Exception e)
            {
                TempData["status"] = "Exception";
                return RedirectToAction("StatusFailView", "Employee");
            }

            TempData["status"] = "Failed";
            return RedirectToAction("StatusFailView", "Employee");

        }

        public IActionResult CustAccDetailsView()
        {
            IEnumerable<Account> mylist = null;
            using (var httpClient = new HttpClient())
            {
                baseAddress = new Uri(_config["Links:AccountApi"]);
                client = new HttpClient();
                client.BaseAddress = baseAddress;

                HttpResponseMessage response = httpClient.GetAsync(client.BaseAddress + "/GetAllAccounts" ).Result;
                if (response.IsSuccessStatusCode)
                {

                    string data = response.Content.ReadAsStringAsync().Result;

                    mylist = JsonConvert.DeserializeObject<IList<Account>>(data);
                }

            }

            return View(mylist);
        }


    }
}
