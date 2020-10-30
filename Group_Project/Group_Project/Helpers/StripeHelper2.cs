using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Group_Project.Helpers
{
    public class StripeHelper2
    {
        private TokenService tokenService;
        private ChargeService chargeService;
        private Token myCardToken;
        private Charge myChargeToken;

        private readonly string BaseURL = "https://api.stripe.com";
        private readonly string CreateURL = "/v1/tokens";
        private HttpClient httpClient;
        private HttpContent httpContent;

        public StripeHelper2()
        {
            //Initialize the API Key
            StripeConfiguration.ApiKey = "sk_test_51HUauxGGhia5bBAKB5Pjb3fkp5OxNTzEZcHDtu2LPSXODAryxlez2qEv7OcOhGAcPzRRpqoOv7XdhKj44aFhWh8L00cdl9rLaO";
            //Initialize the tokenService
            tokenService = new TokenService();
            chargeService = new ChargeService();
            httpClient = new HttpClient();
            
        }

        public Token SendStripeTokenRequest()
        {
            //Make a card
            var options = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = "4242424242424242",
                    ExpMonth = 10,
                    ExpYear = 2021,
                    Cvc = "314",
                },
            };

           

            //Make request to stripe using a card, retrieve token
            var token = 
            myCardToken = tokenService.Create(options);

            //Retrieve the Card using the token
            Token retrievedCard = GetCardById(myCardToken.Id);

            //Charge the card and retrieve the charge token
            myChargeToken = ChargeCard(myCardToken.Id);

            //Retrieve the charge record using the charge token
            Charge retrievedCharge = GetChargeById(myChargeToken.Id);

            //Return token
            return myCardToken;
        }

        public Token GetCardById(string Id)
        {
            Token result = tokenService.Get(Id);

            return result;
        }

        public Charge ChargeCard(string cardId)
        {
            var options = new ChargeCreateOptions
            {
                Amount = 2000,
                Currency = "usd",
                Source = cardId,
                Description = "My First Test Charge (created for API docs)",
            };
            Charge result = chargeService.Create(options);

            return result;
        }

        public Charge GetChargeById(string Id)
        {
            var service = new ChargeService();
            Charge result = service.Get(Id);

            return result;
        }


    }

    public class StripeToken
    {
        public string id { get; set; }
        public string _object { get; set; }
        public Card card { get; set; }
        public object client_ip { get; set; }
        public int created { get; set; }
        public bool livemode { get; set; }
        public string type { get; set; }
        public bool used { get; set; }
    }

    public class Card
    {
        public string id { get; set; }
        public string _object { get; set; }
        public object address_city { get; set; }
        public object address_country { get; set; }
        public object address_line1 { get; set; }
        public object address_line1_check { get; set; }
        public object address_line2 { get; set; }
        public object address_state { get; set; }
        public object address_zip { get; set; }
        public object address_zip_check { get; set; }
        public string brand { get; set; }
        public string country { get; set; }
        public string cvc_check { get; set; }
        public object dynamic_last4 { get; set; }
        public int exp_month { get; set; }
        public int exp_year { get; set; }
        public string fingerprint { get; set; }
        public string funding { get; set; }
        public string last4 { get; set; }
        public Metadata metadata { get; set; }
        public object name { get; set; }
        public object tokenization_method { get; set; }
    }

    public class StripeTokenRequest
    {

    }

    public class Metadata
    {
    }
}
