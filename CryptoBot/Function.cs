using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CryptoBot
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public LexResponse FunctionHandler(LexEvent lexEvent, ILambdaContext context)
        {
            var date = lexEvent.CurrentIntent.Slots["Date"];
            var price_close = CryptoPriceProcessor.GetBitCoinPrice(date);

            return new LexResponse
            {
                SessionAttributes = lexEvent.SessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "Close",
                    FulfillmentState = "Fulfilled",
                    Message = new LexResponse.LexMessage
                    {
                        ContentType = "PlainText",
                        Content = String.Format("The price for bitcoin on date {0} was {1}", date, price_close)
                    }

                }
            };

            
        }
    }
}
