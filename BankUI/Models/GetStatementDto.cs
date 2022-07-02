using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankUI.Models
{
    public class GetStatementDto
    {
        public int Accid { get; set; }
        public DateTime Fromdate { get; set; }

        public DateTime Todate { get; set; }
    }
}
