
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using SupportWheelOfFate.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;

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

                        string payload;

                        var inquireDateValue = intentRequest.Intent.Slots["SupportDate"].Value;
                        var inquireDate = Convert.ToDateTime(inquireDateValue);

                        if (inquireDate > DateTime.Now.Date.AddDays(1))
                        {
                            payload = "We allocate support engineers on a daily basis, and sadly we cannot predict the future";
                        }
                        else if (inquireDate.DayOfWeek == DayOfWeek.Saturday || inquireDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            payload = "You are inquiring about a weekend day. Support outside core hours in handled by an overseas team";
                        }
                        else if ((DateTime.Now - inquireDate).TotalDays > 14)
                        {
                            payload = "We only track each engineers last shift, and our records don't cover that date";
                        }
                        else
                        {
                            var engineers = new List<Engineer>();

                            var url = "https://d1gifumbnf.execute-api.us-east-1.amazonaws.com/prod/engineers/" + inquireDateValue; // Obtain list of all Engineers

                            using (var client = new HttpClient())
                            {
                                var apiResponse = client.GetAsync(url).Result;

                                if (apiResponse.IsSuccessStatusCode)
                                {
                                    var responseContent = apiResponse.Content;

                                    string json = responseContent.ReadAsStringAsync().Result;

                                    try
                                    {
                                        engineers = JsonConvert.DeserializeObject<List<Engineer>>(json);

                                        if (engineers != null)
                                        {
                                            payload = "The engineers on support are ";

                                            var engineerCount = 0;

                                            foreach (var engineer in engineers)
                                            {
                                                payload += engineer.EngineerName;
                                                engineerCount++;

                                                if (engineerCount < engineers.Count)
                                                {
                                                    payload += " and ";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            payload = "I'm afraid I don't have that information at present.";
                                        }
                                    }
                                    catch
                                    {
                                        payload = "I'm afraid I don't have that information at present.";
                                    }
                                }
                                else
                                {
                                    payload = "I'm afraid I don't have that information at present.";
                                }
                            }
                        }

                        innerResponse = new PlainTextOutputSpeech();
                        ((PlainTextOutputSpeech)innerResponse).Text = payload + ".";
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
