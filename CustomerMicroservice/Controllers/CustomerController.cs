using CustomerMicroservice.Models;
using CustomerMicroservice.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerController));
        private readonly ICustomerService _repo;

        public CustomerController(ICustomerService repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            _log4net.Info("CustomerController-GetCustomerById Method IN");
            try
            {
                var customer = _repo.GetCustomerDetails(id);
                if(customer==null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer(Customer customer)
        {
            _log4net.Info("CustomerController-AddCustomer method IN");
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _repo.AddCustomer(customer);
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }


    }
}
