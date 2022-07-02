using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Repo
{
    public interface ICustomerService
    {
        CustomerCreationStatus AddCustomer(Customer customer);
        Customer GetCustomerDetails(int id);

    }
}
