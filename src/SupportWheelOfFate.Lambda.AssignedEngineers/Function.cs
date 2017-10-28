
using Amazon.Lambda.Core;
using SupportWheelOfFate.Lambda.AssignedEngineers.Models;
using System.Collections.Generic;

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

            engineers.Add(new Engineer
            {
                EngineerName = "Rich Purnell",
                EngineerHandle = "Steely Eyed Rocket Man",
                EngineerId = "6632b5d5-e656-4b27-9ea1-1046de9eb4c8",
                DateLastShift = System.DateTime.UtcNow.ToString("yyyy-MM-dd"),
                TimeLastShift = "AM"
            });

            return engineers;
        }
    }
}
