using Alexa.NET.Request;
using Amazon.Lambda.TestUtilities;
using Newtonsoft.Json;
using System.IO;
using Xunit;

namespace SupportWheelOfFate.Lambda.AlexaHandler.Tests
{
    public class LaunchRequestTest
    {
        /// <summary>
        /// Basic Unit Test for Skill LaunchRequest
        /// </summary>
        [Fact]
        public void Skill_LaunchRequest()
        {
            // Arrange
            var fileStream = new FileStream(Directory.GetCurrentDirectory() + "/RequestObjects/LaunchRequest.json", FileMode.Open);
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
            }
        }
    }
}
