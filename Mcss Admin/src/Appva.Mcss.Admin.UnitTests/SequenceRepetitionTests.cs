using Appva.Domain;
using Appva.Mcss.Admin.UnitTests.Helpers;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Appva.Mcss.Admin.UnitTests
{
    public class SequenceRepetitionTests
    {
        // duration 2
        [Theory(DisplayName = "Event Duration")]
        [InlineData("2018-03-28,2018-04-28,2020-02-28,2020-03-28,2021-02-28,2021-03-28", 4)]
        public void Event_Keep_Duration(string startAtValues, int expectedDurationInDays)
        {
            // Arrange            
            var result = startAtValues.Split(',').Select(x => Date.Parse(x)).ToList();

            // Act
            foreach (var startAtValue in result)
            {
                var startDate = startAtValue;
                var endDate = startAtValue.AddDays(expectedDurationInDays);
                var duration = (endDate - startDate).Days;
                var durationUnit = UnitOfTime.Day;
                var period = 1;
                var periodUnit = UnitOfTime.Month;
                var timeOfDay = new List<TimeOfDay> { new TimeOfDay(13, 00) };

                var mockedSequence = new MockedSequence(startDate, endDate, period, periodUnit, duration, durationUnit, timeOfDay, null, null, null);
                var newStartDate = (Date)mockedSequence.Repeat.Next(startDate);
                var newEndDate = newStartDate.Add(durationUnit, duration);

                var actualDurationInDays = (newEndDate - newStartDate).Days;

                Assert.Equal<int>(expectedDurationInDays, actualDurationInDays);
            }
        }
       
        // last friday every month, the 5th friday each month
        [Theory(DisplayName = "Fifth/Last Friday")]
        [InlineData("2018-03-30", "2018-04-27")]
        [InlineData("2018-04-27", "2018-05-25")]
        [InlineData("2018-05-25", "2018-06-29")]
        [InlineData("2018-06-29", "2018-07-27")]
        [InlineData("2019-01-25", "2019-02-22")]
        [InlineData("2020-01-31", "2020-02-28")]
        public void Occuring_Fifth_Or_Last_Friday_In_Month(string dateToTest, string expectedDate)
        {
            // Arrange
            var startDate = Date.Parse("2018-03-30");
            var endDate = Date.Parse("2018-04-01");
            var duration = 2;
            var durationUnit = UnitOfTime.Day;
            var period = 1;
            var periodUnit = UnitOfTime.Month;
            var daysOfWeek = new List<Appva.Domain.DayOfWeek> { Appva.Domain.DayOfWeek.Friday };
            var timeOfDay = new List<TimeOfDay> { new TimeOfDay(13, 00) };
            var flags = new List<Flag> { new Flag("N/5") };

            var mockedSequence = new MockedSequence(startDate, endDate, period, periodUnit, duration, durationUnit, timeOfDay, daysOfWeek, flags, null);

            // Act
            var actualDate = mockedSequence.Repeat.Next(Date.Parse(dateToTest));

            // Assert
            Assert.Equal<Date>(Date.Parse(expectedDate), (Date)actualDate);
        }

        // gliding dates
        [Theory(DisplayName = "Last Day in Month")]
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

            var startDate = startAt;
            var endDate = Date.MaxValue;
            var period = 1;
            var periodUnit = UnitOfTime.Month;
            var duration = 0;
            var durationUnit = UnitOfTime.Day;
            var timeOfDay = new List<TimeOfDay> { new TimeOfDay(13, 00) };
            List<Appva.Domain.DayOfWeek> dayOfWeek = new List<Appva.Domain.DayOfWeek> { startAt.DayOfWeek };
            var flags = new List<Flag> { new Flag("N/L") };
            var mockedSequence = new MockedSequence(startDate, endDate, period, periodUnit, duration, durationUnit, timeOfDay, dayOfWeek, flags, null);

            foreach (var expected in result)
            {
                var actual = mockedSequence.Repeat.Next(nextAt);
                Assert.Equal(expected, actual);
                nextAt = (Date)actual;
            }
        }
    }
}
