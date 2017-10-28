using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.TestUtilities;
using Newtonsoft.Json;
using System.IO;
using Xunit;

namespace SupportWheelOfFate.Lambda.AlexaHandler.Tests
{
    public class SupportRequestTests
    {
        /// <summary>
        /// Test for support today
        /// </summary>
        [Fact]
        public void Skill_Support_Today()
        {
            // Arrange
            var fileStream = new FileStream(Directory.GetCurrentDirectory() + "/RequestObjects/SupportToday.json", FileMode.Open);
            using (var r = new StreamReader(fileStream))
            {
                var json = r.ReadToEnd();
                var requestObject = JsonConvert.DeserializeObject<SkillRequest>(json);

                var intentRequest = (IntentRequest)requestObject.Request;
                intentRequest.Intent.Slots["SupportDate"].Value = System.DateTime.Now.ToString("yyyy-MM-dd"); // Reset the date to Today

                // Act
                var function = new Function();
                var context = new TestLambdaContext();
                var response = function.FunctionHandler(requestObject, context);

                // Assert - Response
                Assert.NotNull(response);
                Assert.NotNull(((PlainTextOutputSpeech)response.Response.OutputSpeech).Text);
                //Assert.Equal(((PlainTextOutputSpeech)response.Response.OutputSpeech).Text, "I'm afraid I don't have that information at present.");
            }
        }

        /// <summary>
        /// Test for a support date outside the historical period
        /// </summary>
        [Fact]
        public void Skill_Support_LastMonth()
        {
            // Arrange
            var fileStream = new FileStream(Directory.GetCurrentDirectory() + "/RequestObjects/SupportLastMonth.json", FileMode.Open);
            using (var r = new StreamReader(fileStream))
            {
                var json = r.ReadToEnd();
                var request = JsonConvert.DeserializeObject<SkillRequest>(json);

                // Act
                var function = new Function();
                var context = new TestLambdaContext();
                var response = function.FunctionHandler(request, context);

                // Assert - Response
                Assert.NotNull(response);
                Assert.NotNull(((PlainTextOutputSpeech)response.Response.OutputSpeech).Text);
                Assert.Equal(((PlainTextOutputSpeech)response.Response.OutputSpeech).Text, "We only track each engineers last shift, and our records don't cover that date.");
            }
        }
    }
}
