
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using SupportWheelOfFate.Lambda.AssignedEngineers.Models;
using System.Collections.Generic;
using System.Net.Http;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SupportWheelOfFate.Lambda.AssignedEngineers
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<Engineer> FunctionHandler(EngineerRequest input, ILambdaContext context)
        {
            var engineers = new List<Engineer>();

            var url = "https://fj7w0figk9.execute-api.us-east-1.amazonaws.com/prod/engineers/"; // Obtain list of all Engineers

            using (var client = new HttpClient())
            {
                var apiResponse = client.GetAsync(url).Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = apiResponse.Content;

                    // by calling .Result you are synchronously reading the result
                    string json = responseContent.ReadAsStringAsync().Result;

                    engineers = JsonConvert.DeserializeObject<List<Engineer>>(json);                    
                }
            }

            // TODO: Need to correctly process the Engineers List

            return engineers;
        }
    }
}
