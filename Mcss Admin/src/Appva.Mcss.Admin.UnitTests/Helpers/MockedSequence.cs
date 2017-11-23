using Appva.Domain;
using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Appva.Mcss.Admin.UnitTests.Helpers
{
    public class MockedSequence
    {

        private const int Weekly = 7;

        private const int Monthly = 31;

        private const int Yearly = 365;

        public Patient Patient { get; }
        public Schedule Schedule { get; }
        public Sequence Sequence { get; }
        public Repeat Repeat { get; }

        // Mocked Calender Event
        public MockedSequence(Date startAt, Date endAt, int? period, UnitOfTime periodUnit, int? duration, UnitOfTime durationUnit, List<TimeOfDay> timesOfDay, List<Appva.Domain.DayOfWeek> daysOfWeek, List<Flag> flags, List<Date> boundsRange)
        {
            this.Repeat = new Repeat(startAt, endAt, period, periodUnit, duration, durationUnit, 0,0, false, timesOfDay, daysOfWeek, flags, boundsRange);
            this.Patient = new Patient { FirstName = "John", LastName = "Doe" };
            this.Schedule = new Schedule { Name = "medication", Patient = Patient };
            this.Sequence = new Sequence(Schedule, "Experimental Medicine", "This is an experiment", Repeat);
        }

        // Mocked Interval Event
        public MockedSequence(DateTime startAt, DateTime? endAt, int interval, int intervalFactor, List<TimeOfDay> timesOfDay, int offsetBefore = 0, int offsetAfter = 0, List<Date> boundsRange = null, bool isNeedBased = false, bool isIntervalDate = false, bool isAllDay = false )
        {
            this.Repeat = new Repeat(startAt, endAt, interval, intervalFactor, offsetBefore, offsetAfter, timesOfDay, boundsRange, isNeedBased, isIntervalDate, isAllDay);
            this.Patient = new Patient { FirstName = "John", LastName = "Doe" };
            this.Schedule = new Schedule { Name = "medication", Patient = Patient };
            this.Sequence = new Sequence(Schedule, "Experimental Medicine", "This is an experiment", Repeat);
        }

        public DateTime OldSequenceGetNextMethod(DateTime date)
        {

            var factor = this.Repeat.IntervalFactor < 1 ? 1 : this.Repeat.IntervalFactor;

            if (this.Repeat.Interval.Equals(Weekly))
            {
                var days = factor * 7;
                return date.AddDays(days);
            }

            if (this.Repeat.Interval.Equals(Yearly))
            {
                return date.AddYears(1);
            }

            if (this.Repeat.Interval.Equals(Monthly))
            {
                //// Repeat on given date (eg. 10 every month)
                if (this.Repeat.IsIntervalDate)
                {
                    return date.AddMonths(factor);
                }
                //// Repeat on given day (eg. 2 monday every month)
                else
                {
                    var newDate = date.AddMonths(factor);

                    while (newDate.Day % 7 != 0)
                    {
                        if (newDate.DayOfWeek == date.DayOfWeek)
                        {
                            return newDate;
                        }
                        newDate = newDate.AddDays(1);
                    }
                    if (newDate.DayOfWeek == date.DayOfWeek)
                    {
                        return newDate;
                    }
                    newDate = newDate.AddDays(-1);
                    while (newDate.Day % 7 != 0)
                    {
                        if (newDate.DayOfWeek == date.DayOfWeek)
                        {
                            return newDate;
                        }
                        newDate = newDate.AddDays(-1);
                    }

                    return newDate;
                }
            }

            //// Interval is specified in dates
            return date.AddDays(this.Repeat.Interval);
        }
    }
}
