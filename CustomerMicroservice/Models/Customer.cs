using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustId { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }

        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Enter PANno")]
        [Range(10000, Int32.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int PANno { get; set; }
    }
}
