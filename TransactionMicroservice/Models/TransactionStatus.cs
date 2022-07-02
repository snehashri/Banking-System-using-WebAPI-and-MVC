using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionMicroservice.Models
{
    public class TransactionStatus
    {
        public double SourceBalance { get; set; }
        public double DestinationBalance { get; set; }
        public string Message { get; set; }
    }
}
