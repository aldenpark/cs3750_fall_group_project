using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class BillingSubmission
    {
        public string Name { get; set; }
        public string CreditCardNum { get; set; }
        public string SecurityCode { get; set; }
        [DataType(DataType.Date)]
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public decimal Amount { get; set; }
    }
}
