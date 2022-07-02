using AccountMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.DBContext
{
    public class AccountContext:DbContext
    {

        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }
        public DbSet<Account> accounts { get; set; }
        public DbSet<Statement> statements { get; set; }

    }
}
