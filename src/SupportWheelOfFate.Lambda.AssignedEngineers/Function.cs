
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using SupportWheelOfFate.Core;
using SupportWheelOfFate.Lambda.AssignedEngineers.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SupportWheelOfFate.Lambda.AssignedEngineers
{
    public class Function
    {       
        /// <summary>
        /// A simple function that handles an EngineerRequest object
        /// </summary>
        /// <param name="input">The EngineerRequest object</param>
        /// <param name="context">The Lambda Execution Context</param>
        /// <returns>A List of assigned Engineers</returns>
        public List<Engineer> FunctionHandler(EngineerRequest input, ILambdaContext context)
        {
            var engineerPool = new List<Engineer>();

            var url = "https://fj7w0figk9.execute-api.us-east-1.amazonaws.com/prod/engineers/"; // Obtain complete pool of Engineers

            using (var client = new HttpClient())
            {
                var apiResponse = client.GetAsync(url).Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = apiResponse.Content;

                    // by calling .Result you are synchronously reading the result
                    string json = responseContent.ReadAsStringAsync().Result;

                    engineerPool = JsonConvert.DeserializeObject<List<Engineer>>(json);                    
                }
            }

            // Invoke the Assign Engineers Service to retrieve/assigned engineers
            return Core.Services.AssignEngineersService.GetAssignedEngineers(engineerPool, Convert.ToDateTime(input.SupportDate));            
        }
    }
}
