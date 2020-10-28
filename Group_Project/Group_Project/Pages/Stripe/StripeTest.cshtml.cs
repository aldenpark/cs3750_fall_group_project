using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace Group_Project.Pages.Stripe
{
    public class StripeTestModel : PageModel
    {
        [ViewData]
        public Token stripeToken { get; set; }
        public void OnGet()
        {
            StripeHelper stripeHelper = new StripeHelper();
            stripeToken = stripeHelper.SendStripeTokenRequest();
        }


        public void OnPost()
        {

        }
    }
}
