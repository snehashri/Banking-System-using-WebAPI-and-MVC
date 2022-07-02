using CustomerMicroservice.DBContext;
using CustomerMicroservice.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMicroservice.Repo
{
    public class CustomerService : ICustomerService
    {

        private readonly CustomerContext _context;
        public CustomerService(CustomerContext context)
        {
            _context = context;
       
        }
        public CustomerCreationStatus AddCustomer(Customer customer)
        {
            CustomerCreationStatus result = new CustomerCreationStatus();
            try
            {
                var id =_context.customers.Add(customer);
                _context.SaveChanges();
                
                int custid = id.Entity.CustId;
                if(custid !=0)
                {
                    string msg = CallCreateAccount(customer);
                    result.CustomerId = id.Entity.CustId;
                    result.Message = msg;
                    return result;
                }
                else
                {
                    result.CustomerId = id.Entity.CustId;
                    result.Message = "Failed";
                }

            }
            catch(Exception ex)
            {
                result.Message = "Error : "+ ex.Message;
                result.CustomerId = 0;
            }
            return result;

        }

        public Customer GetCustomerDetails(int id)
        {
            Customer customer;
            try
            {
                customer = _context.customers.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            return customer;
        }

        #region Private Methods

        private string CallCreateAccount(Customer customer)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri baseAddress = new Uri("http://localhost:53159/api/Account"); 
                client.BaseAddress = baseAddress;
                string data = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/CreateAccount/" +customer.CustId,content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return "Customer Account created Successfully";
                }
                return "failed";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        #endregion
    }
}
