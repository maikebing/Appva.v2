// <copyright file="AbstractTaskService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Domain.Entities;
    using Extensions;
    using Pdf.Prescriptions;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class AbstractTaskService
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTaskService"/> class.
        /// </summary>
        protected AbstractTaskService()
        {
        }

        #endregion

        #region Protected Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="patient"></param>
        /// <param name="tasks"></param>
        /// <param name="symbols"></param>
        /// <returns></returns>
        protected IList<PrescriptionList> FindTasksWithinTimeSpan(string title, DateTime start, DateTime end, Patient patient, IList<Task> tasks, IList<Taxon> symbols)
        {
            if (tasks == null || tasks.Count == 0)
            {
                return this.ReturnEmpty(title, patient, start, end).Item1;
            }
            var result = new List<PrescriptionList>();
            var periods = this.CreatePeriod(start, end);
            foreach (var period in periods)
            {
                var subset = tasks.Where(x => x.Scheduled >= period.Start && x.Scheduled <= period.End).ToList();
                var precriptions = this.CreatePrescriptions(subset);
                if (precriptions.Item1.Count > 0)
                {
                    //// Sort so need based (with null date time) is at the end, and the rest in time order.
                    precriptions.Item1.Sort((x, y) => DateTimeOffset.Compare(x.Time ?? DateTime.MaxValue, y.Time ?? DateTime.MaxValue));
                    result.Add(new PrescriptionList(
                        title,
                        period,
                        new PatientInformation(patient.FullName, patient.PersonalIdentityNumber),
                        precriptions.Item1,
                        CreateSymbols(symbols, false),
                        precriptions.Item2));
                }
            }
            return result;
        }

        protected IList<PrescriptionList> ConvertPreparedTasks(string title, DateTime start, DateTime end, Patient patient, IList<PreparedTask> tasks)
        {
            if (tasks == null || tasks.Count == 0)
            {
                return this.ReturnEmpty(title, patient, start, end).Item1;
            }
            var result = new List<PrescriptionList>();
            var periods = this.CreatePeriod(start, end);
            foreach (var period in periods)
            {
                var subset = tasks.Where(x => x.Date >= period.Start && x.Date <= period.End).ToList();
                var precriptions = this.CreatePrescriptions(subset);
                if (precriptions.Item1.Count > 0)
                {
                    //// Sort so need based (with null date time) is at the end, and the rest in time order.
                    precriptions.Item1.Sort((x, y) => DateTimeOffset.Compare(x.Time ?? DateTime.MaxValue, y.Time ?? DateTime.MaxValue));
                    result.Add(new PrescriptionList(
                        title,
                        period,
                        new PatientInformation(patient.FullName, patient.PersonalIdentityNumber),
                        precriptions.Item1,
                        null,
                        precriptions.Item2));
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="patient"></param>
        /// <param name="sequences"></param>
        /// <param name="symbols"></param>
        /// <param name="showInstructionsOnSeparatePage"></param>
        /// <returns></returns>
        protected Tuple<IList<PrescriptionList>, IList<References>> FindSequencesWithinTimeSpan(string title, DateTime start, DateTime end, Patient patient, IList<Sequence> sequences, IList<Taxon> symbols, bool showInstructionsOnSeparatePage)
        {
            if (sequences == null || sequences.Count == 0)
            {
                return this.ReturnEmpty(title, patient, start, end);
            }
            //// Each month will become a new list, so calculate the months so we can iterate
            //// through the days month-wise per sequence.
            var periods = this.CreatePeriod(start, end);
            var result = new List<PrescriptionList>();
            var references = new Dictionary<Guid, References>();
            foreach (var period in periods)
            {
                var items = new List<Prescription>();
                foreach (var sequence in sequences)
                {
                    var days = new List<int>();
                    var daysInMonth = period.Start.LocalDateTime.DaysInMonth();
                    for (var i = 0; i < daysInMonth; i++)
                    {
                        var current = period.Start.LocalDateTime.AddDays(i);
                        if (DateTimeUtils.IsDateOccurringWithinSpan(
                            current,
                            sequence.StartDate.Date,
                            sequence.EndDate,
                            sequence.Interval,
                            sequence.IntervalFactor,
                            DateTimeUtils.FromStringToDateTime(sequence.Dates)))
                        {
                            days.Add(current.Day);
                        }
                    }
                    if (days.Count == 0)
                    {
                        continue;
                    }
                    //// Calculate the times only for the sequence.
                    foreach (var executing in ExecutingTimes(period.Start.LocalDateTime, sequence))
                    {
                        var dateTime = sequence.OnNeedBasis ? (DateTime?) null : executing;
                        References reference = null;
                        if (showInstructionsOnSeparatePage)
                        {
                            if (!string.IsNullOrWhiteSpace(sequence.Description))
                            {
                                if (references != null && ! references.ContainsKey(sequence.Id))
                                {
                                    references.Add(sequence.Id, References.CreateNew(references.Count, sequence.Description));
                                }
                                reference = references[sequence.Id];
                            }
                        }
                        items.Add(Prescription.CreateNew(sequence.Name, reference, dateTime, days));
                    }
                }
                if (items.Count > 0)
                {
                    //// Sort so need based (with null date time) is at the end, and the rest in time order.
                    items.Sort((x, y) => DateTimeOffset.Compare(x.Time ?? DateTime.MaxValue, y.Time ?? DateTime.MaxValue));
                    result.Add(new PrescriptionList(
                        title,
                        period,
                        new PatientInformation(patient.FullName, patient.PersonalIdentityNumber),
                        items,
                        CreateSymbols(symbols),
                        CreateEmptySignaturesRows(4)));
                }
            }
            return new Tuple<IList<PrescriptionList>, IList<References>>(result, references.Values.ToList());
        }

        /// <summary>
        /// Extracts executing times from a <see cref="Sequence"/>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="sequence"></param>
        protected IList<DateTime> ExecutingTimes(DateTime date, Sequence sequence)
        {
            if (sequence.OnNeedBasis)
            {
                return new List<DateTime> { date };
            }
            int hour, minute;
            var result = new List<DateTime>();
            if (!string.IsNullOrEmpty(sequence.Times))
            {
                foreach (var hourString in sequence.Times.Split(','))
                {
                    if (int.TryParse(hourString, out hour))
                    {
                        hour = (hour == 24) ? 0 : hour;
                        result.Add(date.Date.AddHours(hour));
                    }
                }
            }
            if (!string.IsNullOrEmpty(sequence.Hour) && !string.IsNullOrEmpty(sequence.Minute))
            {
                if (int.TryParse(sequence.Hour, out hour) && int.TryParse(sequence.Minute, out minute))
                {
                    result.Add(date.Date.AddHours(hour).AddMinutes(minute));
                }
            }
            return result;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private Tuple<IList<PrescriptionList>, IList<References>> ReturnEmpty(string title, Patient patient, DateTime start, DateTime end)
        {
            return new Tuple<IList<PrescriptionList>, IList<References>>(new List<PrescriptionList>
                {
                    new PrescriptionList(
                        title,
                        new Period(start, end), 
                        new PatientInformation(patient.FullName, patient.PersonalIdentityNumber))
                }, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private IList<Period> CreatePeriod(DateTime start, DateTime end)
        {
            var size = ((start.Year - end.Year) * 12) + (start.Month - end.Month);
            size = size < 0 ? -size : size;
            size = size == 0 ? 1 : size + 1;
            var periods = new List<Period>();
            for (var i = 0; i < size; i++)
            {
                var current = start.AddMonths(i);
                periods.Add(new Period(current.FirstOfMonth(), current.LastOfMonth().LastInstantOfDay()));
            }
            return periods;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        private Tuple<List<Prescription>, IList<Signature>> CreatePrescriptions(IList<Task> tasks)
        {
            var signatures = new List<Signature>();
            var bucket = new Dictionary<Guid, string>();
            var temp = new Dictionary<string, Prescription>();
            foreach (var task in tasks)
            {
                var key = string.Format("{0:HH:mm}:{1}", task.Scheduled, task.Sequence.Id);
                var time = task.OnNeedBasis ? (DateTime?)null : new DateTime(task.Scheduled.Year, task.Scheduled.Month,  1, task.Scheduled.Hour, task.Scheduled.Minute, task.Scheduled.Second, 0, DateTimeKind.Local);
                if (!temp.ContainsKey(key))
                {
                    temp.Add(key, Prescription.CreateNew(task.Name, null, time));
                }
                var precription = temp[key];
                precription.Days.Add(task.Scheduled.Day);
                if (task.CompletedBy != null)
                {
                    if (!precription.Symbols.ContainsKey(task.Scheduled.Day))
                    {
                        var initials = this.CreateUniqueInitials(
                            bucket,
                            task.CompletedBy.Id,
                            (task.CompletedBy.FirstName.Substring(0, 1) + task.CompletedBy.LastName.Substring(0, 1)).ToUpper());
                        var signature = new Signature(task.CompletedBy.FullName, initials);
                        if (!signatures.Contains(signature))
                        {
                            signatures.Add(signature);
                        }
                        if (task.StatusTaxon != null && task.StatusTaxon.Weight > 1)
                        {
                            initials += "/" + task.StatusTaxon.Name.Substring(0, 1);
                        }
                        else if (task.Status > 0)
                        {
                            switch (task.Status)
                            {
                                case 2:
                                    initials += "/De";
                                    break;
                                case 3:
                                    initials += "/Ej";
                                    break;
                                case 4:
                                    initials += "/Ka";
                                    break;
                                case 5:
                                    initials += "/Me";
                                    break;
                            }
                        }
                        precription.Symbols.Add(task.Scheduled.Day, initials);
                    }
                }
            }
            return new Tuple<List<Prescription>, IList<Signature>>(temp.Values.ToList(), signatures);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        private Tuple<List<Prescription>, IList<Signature>> CreatePrescriptions(IList<PreparedTask> tasks)
        {
            var signatures = new List<Signature>();
            var bucket = new Dictionary<Guid, string>();
            var temp = new Dictionary<string, Prescription>();
            foreach (var task in tasks)
            {
                var key = string.Format("{0:HH:mm}:{1}", task.Date, task.PreparedSequence.Id);
                if (!temp.ContainsKey(key))
                {
                    temp.Add(key, Prescription.CreateNew(task.PreparedSequence.Name, null, task.Date));
                }
                var precription = temp[key];
                precription.Days.Add(task.Date.Day);
                if (task.PreparedBy != null)
                {
                    if (!precription.Symbols.ContainsKey(task.Date.Day))
                    {
                        var initials = this.CreateUniqueInitials(
                            bucket,
                            task.PreparedBy.Id,
                            (task.PreparedBy.FirstName.Substring(0, 1) + task.PreparedBy.LastName.Substring(0, 1)).ToUpper());
                        var signature = new Signature(task.PreparedBy.FullName, initials);
                        if (!signatures.Contains(signature))
                        {
                            signatures.Add(signature);
                        }
                        precription.Symbols.Add(task.Date.Day, initials);
                    }
                }
            }
            return new Tuple<List<Prescription>, IList<Signature>>(temp.Values.ToList(), signatures);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="id"></param>
        /// <param name="initials"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private string CreateUniqueInitials(IDictionary<Guid, string> bucket, Guid id, string initials, int count = 0)
        {
            if (bucket.ContainsKey(id))
            {
                return bucket[id];
            }
            var key = count == 0 ? initials : initials + count;
            if (bucket.Values.Contains(key))
            {
                return this.CreateUniqueInitials(bucket, id, initials, count + 1);
            }
            bucket.Add(id, key);
            return key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        private static IList<Symbol> CreateSymbols(IList<Taxon> symbols, bool showAllSymbols = true)
        {
            if (symbols == null)
            {
                return null;
            }
            var result = new List<Symbol>();
            foreach (var symbol in symbols)
            {
                if (showAllSymbols)
                {
                    result.Add(new Symbol(symbol.Name.Substring(0, 1), symbol.Name));
                }
                else
                {
                    if (symbol.Weight > 1)
                    {
                        result.Add(new Symbol(symbol.Name.Substring(0, 1), symbol.Name));
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        private static IList<Signature> CreateEmptySignaturesRows(int rows)
        {
            Requires.Range(rows > 0, "amount", "Amount must not be zero or below.");
            var result = new List<Signature>();
            var size = rows * 4;
            for (var i = 0; i < size; i++)
            {
                result.Add(new Signature(string.Empty, string.Empty));
            }
            return result;
        }

        #endregion
    }
}