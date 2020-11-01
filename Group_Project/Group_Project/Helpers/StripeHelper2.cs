
using Group_Project.HelperModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Group_Project.Helpers
{
    public class StripeHelper2
    {

        private readonly string BaseURL = "https://api.stripe.com";
        //POST
        private readonly string CreateURL = "/v1/tokens";
        private readonly string ChargeURL = "/v1/charges";
        //GET
        private readonly string GetURL = "/v1/tokens/:id";
        private readonly string APIKey = "sk_test_51HUauxGGhia5bBAKB5Pjb3fkp5OxNTzEZcHDtu2LPSXODAryxlez2qEv7OcOhGAcPzRRpqoOv7XdhKj44aFhWh8L00cdl9rLaO";
        private string cardToken;
        private HttpClient httpClient;
        private HttpContent httpContent;
        private StripeTokenResponse stripeToken;

        public StripeHelper2()
        {
            //Initialize the HttpClient
            httpClient = new HttpClient();
            //Initialize the Response object
            stripeToken = new StripeTokenResponse();
            //Initialize the cardToken
            cardToken = "";
        }

        public async Task<StripeTokenResponse> CreateCard()
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

            //Add authorization header
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", APIKey);

            //Serialize the request object
            var jsonObject = JsonConvert.SerializeObject(options);


            var cardValues = new Dictionary<string, string>
            {
                { "card[number]", options.Card.Number },
                { "card[exp_month]", options.Card.ExpMonth.ToString()},
                { "card[exp_year]", options.Card.ExpYear.ToString() },
                { "card[cvc]", options.Card.Cvc}
            };


            var formUrlEncodedContent = new FormUrlEncodedContent(cardValues);


            //Post request to Stripe
            var response = await httpClient.PostAsync(BaseURL + CreateURL, formUrlEncodedContent);
            //Parse the json
            var responsejson = await response.Content.ReadAsStringAsync();
            //Parse the token id
            string token = JObject.Parse(responsejson).SelectToken("id").ToString();


            var chargeValues = new Dictionary<string, string>
            {
                { "amount", (10000).ToString() },
                { "currency", "usd"},
                { "description", "Test Charge Through Learning Management System" },
                { "source", token }
            };
            var formUrlEncodedContent2 = new FormUrlEncodedContent(chargeValues);


            var response2 = await httpClient.PostAsync(BaseURL + ChargeURL, formUrlEncodedContent2);
            var responsejson2 = await response.Content.ReadAsStringAsync();
            if(response2.StatusCode != System.Net.HttpStatusCode.OK)
            {

            }
            string token2 = JObject.Parse(responsejson).SelectToken("id").ToString();

            return stripeToken;

            /*
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
            */
        }

        /*
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

        */
    }




    public class StripeTokenRequest
    {

    }

    public class Metadata
    {
    }

    public class TokenCreateOptions
    {
        public TokenCardOptions Card { get; set; }
    }

    public class TokenCardOptions
    {
        public string Number { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Cvc { get; set; }
    }

    public class ChargeCreateOptions
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
    }


  




}
