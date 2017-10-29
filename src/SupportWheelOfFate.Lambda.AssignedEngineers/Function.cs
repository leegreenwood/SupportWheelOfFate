
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using SupportWheelOfFate.Core;
using SupportWheelOfFate.Lambda.AssignedEngineers.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SupportWheelOfFate.Lambda.AssignedEngineers
{
    public class Function
    {       
        private const string baseApiUrl = "https://fj7w0figk9.execute-api.us-east-1.amazonaws.com/prod";

        /// <summary>
        /// A simple function that handles an EngineerRequest object
        /// </summary>
        /// <param name="input">The EngineerRequest object</param>
        /// <param name="context">The Lambda Execution Context</param>
        /// <returns>A List of assigned Engineers</returns>
        public List<Engineer> FunctionHandler(EngineerRequest input, ILambdaContext context)
        {
            var engineerPool = new List<Engineer>();            

            using (var client = new HttpClient())
            {
                var apiResponse = client.GetAsync(baseApiUrl + "/engineers/").Result;

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
            var assignedEngineers = Core.Services.AssignEngineersService.GetAssignedEngineers(engineerPool, Convert.ToDateTime(input.SupportDate));

            if (input.UpdateEngineers)
            {
                using (var client = new HttpClient())
                {
                    foreach (var engineer in assignedEngineers)
                    {                                              
                        var content = new StringContent(JsonConvert.SerializeObject(engineer), Encoding.UTF8);
                        content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");

                        var request = new HttpRequestMessage(HttpMethod.Post, baseApiUrl + "/engineers/update/" + engineer.EngineerId)
                        {
                            Content = content
                        };

                        var response = client.SendAsync(request).Result;
                    }
                }
            }

            return assignedEngineers;
        }
    }
}
