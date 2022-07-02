using BankUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankUI.Controllers
{
    public class LoginController : Controller
    {
        Uri baseAddress;
        HttpClient client;
        IConfiguration _config;
        string loginstatus = null;

        public LoginController(IConfiguration config)
        {
            _config = config;
            baseAddress = new Uri(_config["Links:AuthApi"]);
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        
        public IActionResult SaveTokenCustomer(Token token)
        {
            return RedirectToAction("CustomerMenu", "Customer");
            
        }
        public IActionResult depositeservice()
        {
            
            return RedirectToAction("DepositeView", "Customer");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }

        public async Task<IActionResult> Login(User user)
        {
            try
            {
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(client.BaseAddress + "/gettoken", content);
                //client.
                Token token = new Token();
                token.LoginToken = await response.Content.ReadAsStringAsync();
                ViewBag.Token = await response.Content.ReadAsStringAsync();

                string[] tempo = token.LoginToken.Split(".");
                string payload = tempo[1];
                string inputStr = Encoding.UTF8.GetString(Convert.FromBase64String(payload));

                token.LoginToken = inputStr;
                //var newtoken = JsonConvert.DeserializeObject(inputStr);
                var jo = JObject.Parse(inputStr);
                var id = jo["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].ToString();
                if (id == "Customer")
                {
                    loginstatus = "success";
                    return RedirectToAction("CustomerMenu", "Customer");
                }

                //var role=newtoken[]
                loginstatus = "fail";
                return RedirectToAction("EmployeeMenu", "Employee"); ;
            }
            catch (Exception e)
            {
                loginstatus = "Exception";
                return View("Index");
            }
        }
    }
}
