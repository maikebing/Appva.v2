using Xunit;
using System.Linq;
using Appva.Domain;
using System.Collections.Generic;
using Appva.Mcss.Admin.UnitTests.Helpers;

namespace Appva.Mcss.Admin.UnitTests
{
    public class OldSequenceRepetitionTests
    {
        [Theory(DisplayName = "Old Event Duration")]
        [InlineData("2018-03-28,2018-04-28,2020-02-28,2020-03-28,2021-02-28,2021-03-28", 4)]
        public void Event_Keep_Duration(string startAtValues, int expectedDurationInDays)
        {
            // Arrange            
            var result = startAtValues.Split(',').Select(x => Date.Parse(x)).ToList();

            foreach (var startAt in result)
            {
                var startDate = startAt;
                var endDate = startDate.AddDays(expectedDurationInDays);
                var interval = 31;
                var intervalFactor = 1;
                var timeOfDay = new List<TimeOfDay> { new TimeOfDay(13, 00) };

                var mockedSequence = new MockedSequence(startDate, endDate, interval, intervalFactor, timeOfDay, isIntervalDate: true);

                // Act
                var nextStartAt = (Date)mockedSequence.OldSequenceGetNextMethod(startDate);
                var nextEndAt = (Date)mockedSequence.OldSequenceGetNextMethod(endDate);

                var actualDurationInDays = (nextEndAt - nextStartAt).Days;

                // Assert
                Assert.Equal<int>(expectedDurationInDays, actualDurationInDays);
            }
        }

        // last friday every month, the 5th friday each month
        [Theory(DisplayName = "Old Fifth/Last Friday")]
        [InlineData("2018-03-30", "2018-04-27")]
        public void Occuring_Fifth_Or_Last_Friday_In_Month(string dateToTest, string expectedDate)
        {
            var startDate = Date.Parse(dateToTest);
            var endDate = Date.MaxValue;
            var interval = 31;
            var intervalFactor = 1;
            var timeOfDay = new List<TimeOfDay> { new TimeOfDay(13, 00) };

            var mockedSequence = new MockedSequence(startDate, endDate, interval, intervalFactor, timeOfDay, isIntervalDate: false);
            // Act
            var actualDate = mockedSequence.OldSequenceGetNextMethod(startDate);

            // Assert
            Assert.Equal<Date>(Date.Parse(expectedDate), (Date)actualDate);
        }

        // gliding dates
        [Theory(DisplayName = "Old Last Day in Month")]
        [InlineData("2083-01-31", "2083-02-28,2083-03-31,2083-04-30,2083-05-31")]
        [InlineData("2084-01-31", "2084-02-29,2084-03-31,2084-04-30,2084-05-31")]
        [InlineData("2085-01-31", "2085-02-28,2085-03-31,2085-04-30,2085-05-31")]
        public void Occuring_Last_Day_In_Month(string startAtValue, string expectedValues)
        {
            Date startAt, nextAt;
            startAt = nextAt = Date.Parse(startAtValue);

            var result = expectedValues.Split(',')
                .Select(x => Date.Parse(x))
                .ToList();

            var startDate = Date.Parse(startAtValue);
            var endDate = Date.MaxValue;
            var interval = 31;
            var intervalFactor = 1;
            var timeOfDay = new List<TimeOfDay> { new TimeOfDay(13, 00) };

            var mockedSequence = new MockedSequence(startDate, endDate, interval, intervalFactor, timeOfDay, isIntervalDate: true);

            foreach (var expected in result)
            {
                var actual = (Date)mockedSequence.OldSequenceGetNextMethod(nextAt);
                Assert.Equal(expected, actual);
                nextAt = (Date)actual;
            }
        }
    }
}
