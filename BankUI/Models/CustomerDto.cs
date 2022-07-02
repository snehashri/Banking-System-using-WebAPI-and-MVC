using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankUI.Models
{
    public class CustomerDto
    {
        public int CustId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public DateTime? DOB { get; set; }
        public int PANno { get; set; }
    }
}
