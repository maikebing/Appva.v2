// <copyright file="DateTimeUtilsTest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Domain.Utils
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DateTimeUtilsTests
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeUtilsTests"/> class.
        /// </summary>
        public DateTimeUtilsTests()
        {
        }

        #endregion

        #region Tests

        /// <summary>
        /// Test:    Get earliest and latest date on a empty list of strings
        /// Expects: Returns a timespan from today to tomorrow
        /// </summary>
        [Fact]
        public void GetEarliestAndLatestFromEmptyList()
        {
            DateTime startTime;
            DateTime? endTime;
            var dates = new List<string>();

            DateTimeUtils.GetEarliestAndLatestDateFrom(dates, out startTime, out endTime);

            Assert.Equal(startTime.Date, DateTime.Now.Date);
            Assert.Equal(endTime.Value.Date, DateTime.Now.AddDays(1).Date);
        }

        /// <summary>
        /// Test:    Get earliest and latest date on a list with a signle string
        /// Expects: Start and end date is equal
        /// </summary>
        [Fact]
        public void GetEarliestAndLatestFromListWithSingleEntity()
        {
            DateTime startTime;
            DateTime? endTime;
            var dates = new List<string>() { "2017-01-01" };

            DateTimeUtils.GetEarliestAndLatestDateFrom(dates, out startTime, out endTime);

            Assert.Equal(startTime.Date, new DateTime(2017,1,1));
            Assert.Equal(startTime.Date, endTime.Value.Date);
        }

        /// <summary>
        /// Test:    Get earliest and latest date on an unordered list of date strings
        /// Expects: Returns a timespan from the smallest date in list to the greatest
        /// </summary>
        [Fact]
        public void GetEarliestAndLatestFromUnorderedList()
        {
            DateTime startTime;
            DateTime? endTime;
            var dates = new List<string>() { "2017-01-30", "2017-01-04", "2017-01-08", "2017-01-05", "2017-01-25", "2017-01-15", "2017-01-12", "2017-01-10", "2017-01-02", "2017-01-01" };

            DateTimeUtils.GetEarliestAndLatestDateFrom(dates, out startTime, out endTime);

            Assert.Equal(startTime.Date, new DateTime(2017, 1, 1));
            Assert.Equal(endTime.Value.Date, new DateTime(2017, 1, 30));
        }

        /// <summary>
        /// Test:    Get earliest and latest date on an unordered list of date strings
        /// Expects: Returns a timespan from the smallest date in list to the greatest
        /// </summary>
        [Fact]
        public void GetEarliestAndLatestFromOrderedList()
        {
            DateTime startTime;
            DateTime? endTime;
            var dates = new List<string>() { "2017-01-01", "2017-01-02", "2017-01-04", "2017-01-05", "2017-01-08", "2017-01-10", "2017-01-12", "2017-01-15", "2017-01-25", "2017-01-30" };

            DateTimeUtils.GetEarliestAndLatestDateFrom(dates, out startTime, out endTime);

            Assert.Equal(startTime.Date, new DateTime(2017, 1, 1));
            Assert.Equal(endTime.Value.Date, new DateTime(2017, 1, 30));
        }


        #endregion
    }
}