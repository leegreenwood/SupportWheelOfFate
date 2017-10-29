using Amazon.Lambda.TestUtilities;
using SupportWheelOfFate.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace SupportWheelOfFate.Lambda.AssignedEngineers.Tests
{
    public class FunctionTests
    {
        [Fact]
        public void Test_AssignedEngineers_NextShift()
        {
            // Arrange
            var workday = NextWorkday();
            var request = new Models.EngineerRequest { SupportDate = workday.ToString("yyyy-MM-dd") };

            // Invoke the lambda function and confirm a list of Engineers is returned
            var function = new Function();
            var context = new TestLambdaContext();
            var engineers = function.FunctionHandler(request, context);

            Assert.NotNull(engineers);
            Assert.IsType<List<Engineer>>(engineers);
            Assert.Equal(engineers.Count, 2);

            foreach (var engineer in engineers)
            {
                Assert.NotNull(engineer);
                Assert.Equal(Convert.ToDateTime(engineer.DateLastShift), workday);
            }
        }

        [Fact]
        public void Test_AssignedEngineers_MostRecentWorkday()
        {
            // Arrange
            var workday = MostRecentWorkday();
            var request = new Models.EngineerRequest { SupportDate = workday.ToString("yyyy-MM-dd") };

            // Invoke the lambda function and confirm a list of Engineers is returned
            var function = new Function();
            var context = new TestLambdaContext();
            var engineers = function.FunctionHandler(request, context);

            Assert.NotNull(engineers);
            Assert.IsType<List<Engineer>>(engineers);
            Assert.Equal(engineers.Count, 2);

            foreach(var engineer in engineers)
            {
                Assert.NotNull(engineer);
                Assert.Equal(Convert.ToDateTime(engineer.DateLastShift), workday);
            }
        }

        #region Private Functions

        private DateTime NextWorkday()
        {
            DateTime workday;
            var today = DateTime.Now.Date;

            switch (today.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    workday = today.AddDays(2).Date;
                    break;

                case DayOfWeek.Sunday:
                    workday = today.AddDays(1).Date;
                    break;

                default:
                    workday = today;
                    break;
            }

            return workday;
        }

        private DateTime MostRecentWorkday()
        {
            DateTime workday;
            var today = DateTime.Now.Date;

            switch (today.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    workday = today.AddDays(-1).Date;
                    break;

                case DayOfWeek.Sunday:
                    workday = today.AddDays(-2).Date;
                    break;

                default:
                    workday = today;
                    break;
            }

            return workday;
        }

        #endregion
    }
}
