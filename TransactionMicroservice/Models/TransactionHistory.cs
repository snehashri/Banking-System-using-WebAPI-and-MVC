using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionMicroservice.Models
{
    public class TransactionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Transaction_ID { get; set; }
        public int Account_ID { get; set; }
        public int? transaction_status_code { get; set; }
        public DateTime? Date_of_Transaction { get; set; }
        public float? Amount_of_Transaction { get; set; }
        public string? Description { get; set; }


        
    }
}
