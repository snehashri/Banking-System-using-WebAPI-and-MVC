using CustomerMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.DBContext
{
    public class CustomerContext:DbContext
    {
        public CustomerContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Customer> customers { get; set; }
    }
}
