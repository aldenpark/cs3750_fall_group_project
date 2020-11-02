using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Group_Project.HelperModels;
using Group_Project.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using Stripe;

namespace Group_Project.Pages.Stripe
{
    public class StripeTestModel : PageModel
    {
        //[ViewData]
        //public Token stripeToken { get; set; }
       //public StripeTokenResponse stripeToken { get; set; }
        public async void OnGet()
        {
            //StripeHelper stripeHelper = new StripeHelper();
            //stripeToken = stripeHelper.SendStripeTokenRequest();
            StripeHelper2 stripeHelper2 = new StripeHelper2();
            //stripeToken = await stripeHelper2.CreateCard();
            await stripeHelper2.CreateCard();
        }


        public void OnPost()
        {

        }
    }
}
