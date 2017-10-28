using Amazon.Lambda.TestUtilities;
using SupportWheelOfFate.Core;
using System.Collections.Generic;
using Xunit;

namespace SupportWheelOfFate.Lambda.AssignedEngineers.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void Test_GET_AssignedEngineers()
        {
            // Arrange
            var request = new Models.EngineerRequest { SupportDate = "2017-10-27" };

            // Invoke the lambda function and confirm a list of Engineers is returned
            var function = new Function();
            var context = new TestLambdaContext();
            var engineers = function.FunctionHandler(request, context);

            Assert.NotNull(engineers);
            Assert.IsType<List<Engineer>>(engineers);
            Assert.Equal(engineers.Count, 2);
        }
    }
}
