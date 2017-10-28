
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SupportWheelOfFate.Lambda.AlexaHandler
{
    public class Function
    {
        /// <summary>
        /// The main Alexa Skill Handler for Support Wheel Of Fate
        /// </summary>
        /// <param name="input">The SkillRequest object from Alexa</param>
        /// <param name="context">The LambdaContext</param>
        /// <returns>A populated SkillResponse</returns>
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            SkillResponse response = new SkillResponse { Response = new ResponseBody { ShouldEndSession = false } };
            IOutputSpeech innerResponse = null;
            var log = context.Logger;
            log.LogLine("Skill Request Object:");
            log.LogLine(JsonConvert.SerializeObject(input));

            if (input.GetRequestType() == typeof(LaunchRequest))
            {
                log.LogLine("Default LaunchRequest made: 'Alexa, open Support Wheel Of Fate'");
                innerResponse = new PlainTextOutputSpeech();
                ((PlainTextOutputSpeech)innerResponse).Text = "Welcome to Support Wheel Of Fate";

            }
            else if (input.GetRequestType() == typeof(IntentRequest))
            {
                var intentRequest = (IntentRequest)input.Request;

                string deviceId;

                try
                {
                    deviceId = input.Context.System.Device.DeviceID;
                    if (!string.IsNullOrEmpty(deviceId))
                    {
                        log.LogLine("Device ID: " + deviceId);
                    }
                }
                catch
                {
                    deviceId = null;
                }

                switch (intentRequest.Intent.Name)
                {
                    case "AssignedEngineers":
                        innerResponse = new PlainTextOutputSpeech();
                        ((PlainTextOutputSpeech)innerResponse).Text = "I'm afraid I don't have that information at present.";
                        response.Response.ShouldEndSession = true;
                        break;

                    case "AMAZON.CancelIntent":
                        log.LogLine("AMAZON.CancelIntent: send StopMessage");
                        innerResponse = new PlainTextOutputSpeech();
                        ((PlainTextOutputSpeech)innerResponse).Text = "Thank you. Goodbye!";
                        response.Response.ShouldEndSession = true;
                        break;

                    case "AMAZON.StopIntent":
                        log.LogLine("AMAZON.StopIntent: send StopMessage");
                        innerResponse = new PlainTextOutputSpeech();
                        ((PlainTextOutputSpeech)innerResponse).Text = "Thank you. Goodbye!";
                        response.Response.ShouldEndSession = true;
                        break;

                    case "AMAZON.HelpIntent":
                        log.LogLine("AMAZON.HelpIntent: send HelpMessage");
                        innerResponse = new PlainTextOutputSpeech();
                        ((PlainTextOutputSpeech)innerResponse).Text = "You can say... Tell me about Support Wheel Of Fate";
                        break;
                }

            }

            response.Response.OutputSpeech = innerResponse;
            response.Version = "1.0";
            log.LogLine("Skill Response Object...");
            log.LogLine(JsonConvert.SerializeObject(response));
            return response;
        }
    }
}
