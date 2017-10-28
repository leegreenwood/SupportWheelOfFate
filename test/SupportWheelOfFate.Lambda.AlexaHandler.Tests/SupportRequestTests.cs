using Alexa.NET.Request;
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
        /// Basic Unit Test for Skill Support Requests
        /// </summary>
        [Fact]
        public void Skill_SupportToday()
        {
            // Arrange
            var fileStream = new FileStream(Directory.GetCurrentDirectory() + "/RequestObjects/SupportToday.json", FileMode.Open);
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
                //Assert.Equal(((PlainTextOutputSpeech)response.Response.OutputSpeech).Text, "I'm afraid I don't have that information at present.");
            }
        }
    }
}
