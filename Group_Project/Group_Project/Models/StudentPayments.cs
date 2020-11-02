using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class StudentPayments
    {
        public int ID { get; set; }
        public int StudentId { get; set; }
        [DataType(DataType.Currency)]
        public decimal Payment { get; set; }
        public string StripeTokenId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
