using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Helpers
{
    public class StripeHelper
    {
        private TokenService tokenService;
        private ChargeService chargeService;
        private Token myCardToken;
        private Charge myChargeToken;
        /*
        public StripeHelper()
        {
            //Initialize the API Key
            StripeConfiguration.ApiKey = "sk_test_51HUauxGGhia5bBAKB5Pjb3fkp5OxNTzEZcHDtu2LPSXODAryxlez2qEv7OcOhGAcPzRRpqoOv7XdhKj44aFhWh8L00cdl9rLaO";
            //Initialize the tokenService
            tokenService = new TokenService();
            chargeService = new ChargeService();
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
            //myCardToken = tokenService.Create(options);

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

        */
    }
        
}
