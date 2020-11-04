using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class PayBillResponse
    {
        public HttpResponseMessage responseMessage { get; set; }
        public StudentPayment studentPayments { get; set; }
    }
}
