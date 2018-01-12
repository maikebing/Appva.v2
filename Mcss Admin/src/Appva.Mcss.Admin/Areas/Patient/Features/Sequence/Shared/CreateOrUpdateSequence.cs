// <copyright file="CreateOrUpdateSequence.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Domain;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;

    #endregion

    public enum SequenceType
    {
        Scheduled = 0,
        DateRange = 1,
        NeedBased = 2
    }

    public enum InventoryState
    {
        New = 0,
        Use = 1
    }

    public enum Repetition
    {
        Daily = 0,
        Weekly = 1
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateOrUpdateSequence
    {
        ////////////////////////////////////////////////////////////////////
        //// FIXME: Should we have article stuff here??? -> see history (?) 
        ////////////////////////////////////////////////////////////////////

        /*
        /// <summary>
        /// If the sequence is orderable
        /// </summary>
        public bool IsOrderable
        {
            get;
            set;
        }

        /// <summary>
        /// If the article order option is enabled.
        /// </summary>
        public bool IsOrderableArticleEnabled
        {
            get;
            set;
        }
        */

        /// <summary>
        /// The form collection.
        /// </summary>
        public FormCollection Collection
        {
            get;
            set;
        }

        /// <summary>
        /// The patient view model.
        /// </summary>
        public PatientViewModel Patient
        {
            get;
            set;
        }

        public Guid PatientId
        {
            get;
            set;
        }

        public Guid ScheduleId
        {
            get;
            set;
        }

        /// <summary>
        /// The Sequence name.
        /// </summary>
        [Required(ErrorMessage="Insats måste anges")]
        public string Name 
        { 
            get;
            set;
        }

        /// <summary>
        /// The sequence 'description'.
        /// </summary>
        public string Instruction 
        { 
            get;
            set; 
        }

        /// <summary>
        /// If sequence needs delegation, the delegation
        /// </summary>
        public Guid? DelegationId
        {
            get;
            set;
        }

        /// <summary>
        /// All available delegations
        /// </summary>
        public IEnumerable<SelectListItem> Delegations
        {
            get;
            set;
        }

        /// <summary>
        /// If the sequence only can be completed by a specific role.
        /// </summary>
        public bool IsRequiredRole
        {
            get;
            set;
        }

        /// <summary>
        /// The required role text, eg. the sequence can only be completed by a nurse.
        /// </summary>
        public string RequiredRoleText
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory type, either create new or use current.
        /// </summary>
        public InventoryState InventoryType
        {
            get;
            set;
        }

        /// <summary>
        /// The selected inventory ID.
        /// </summary>
        [RequiredIf(Target = "InventoryType", Value = InventoryState.Use, ErrorMessage = "Saldo måste väljas")]
        public Guid? InventoryId
        {
            get;
            set;
        }

        /// <summary>
        /// All available inventories.
        /// </summary>
        public IEnumerable<SelectListItem> Inventories
        {
            get;
            set;
        }

        /// <summary>
        /// The sequence type.
        /// </summary>
        public SequenceType Type
        {
            get;
            set;
        }

        /// <summary>
        /// The start date, if <see cref="SequenceType.Scheduled"/>.
        /// </summary>
        public Date? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The end date, if <see cref="SequenceType.Scheduled"/>.
        /// </summary>
        public Date? EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// If to set start hour and minute, as well as end hour and minute.
        /// </summary>
        public bool IsPeriodWithTimeOfDay
        {
            get;
            set;
        }

        /// <summary>
        /// The start hour.
        /// </summary>
        public int? StartHour
        {
            get;
            set;
        }

        /// <summary>
        /// The start minute.
        /// </summary>
        public int? StartMinute
        {
            get;
            set;
        }

        /// <summary>
        /// The end hour.
        /// </summary>
        public int? EndHour
        {
            get;
            set;
        }

        /// <summary>
        /// The end minute.
        /// </summary>
        public int? EndMinute
        {
            get;
            set;
        }

        /// <summary>
        /// If occures on specific dates, the collection of dates.
        /// </summary>
        public IList<Date> Dates
        {
            get;
            set;
        }

        /// <summary>
        /// The type of repeat.
        /// </summary>
        public Repetition Repetition
        {
            get;
            set;
        }

        /// <summary>
        /// The period or frequency when <see cref="Repetition.Daily"/>.
        /// </summary>
        public int? EverydayFrequency
        {
            get;
            set;
        }

        /// <summary>
        /// The period or frequency when <see cref="Repetition.Weekly"/>.
        /// </summary>
        public int? WeeklyFrequency
        {
            get;
            set;
        }

        /// <summary>
        /// The days of week to choose from.
        /// </summary>
        public IList<DaysOfWeekModel> DaysOfWeek
        {
            get;
            set;
        }

        /// <summary>
        /// The hours which the sequence shall occur
        /// </summary>
        public IList<TimeModel> Times 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The timespan before its ok to complete.
        /// In minutes.
        /// </summary>
        [Range(0, 99999, ErrorMessage = "Måste vara mellan 0-99999")]
        public int RangeInMinutesBefore 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The timespan after its ok to complete.
        /// In minutes.
        /// </summary>
        [Range(0, 99999, ErrorMessage = "Måste vara mellan 0-99999")]
        public int RangeInMinutesAfter 
        { 
            get; 
            set;
        }

        public Frequency GetSelectedFrequency()
        {
            return Frequency.New(
                this.Repetition == Models.Repetition.Daily ? this.EverydayFrequency.GetValueOrDefault(0) : this.WeeklyFrequency.GetValueOrDefault(0), 
                this.Repetition == Models.Repetition.Daily ? UnitOfTime.Day : UnitOfTime.Week);
        }

        public Period GetSelectedPeriod()
        {
            Date start = this.StartDate ?? Date.Today;
            Date? end  = this.EndDate;
            DateTime startDateTime = start == Date.Today ? 
                start.ToDateTime(TimeOfDay.New(this.StartHour.GetValueOrDefault(TimeOfDay.Now.Hour), this.StartMinute.GetValueOrDefault(TimeOfDay.Now.Minute))) :
                start.ToDateTime(TimeOfDay.New(this.StartHour.GetValueOrDefault(0), this.StartMinute.GetValueOrDefault(0)));
            DateTime? endDateTime = end == null ? 
                (DateTime?) null : 
                end.Value.ToDateTime(TimeOfDay.New(this.EndHour.GetValueOrDefault(23), this.EndMinute.GetValueOrDefault(59)));
            return Period.New(startDateTime, endDateTime);
        }

        public List<Date> GetSelectedDates()
        {
            return this.Collection
                    .AllKeys
                    .Where (x => x.StartsWith("id-choose-date-value-list-panel"))
                    .Select(x => Date.Parse(x))
                    .ToList();
        }

        public List<TimeOfDay> GetSelectedTimesOfDay()
        {
            return this.Times
                .Where (x => x.IsChecked == true)
                .Select(x => new TimeOfDay(x.Hour, x.Minute))
                .ToList();
        }

        public List<Appva.Domain.DayOfWeek> GetSelectedDaysOfWeek()
        {
            return this.DaysOfWeek
                .Where(x => x.IsChecked == true)
                .Select(x => new Appva.Domain.DayOfWeek(x.Code))
                .ToList();
        }

        public Offset GetSelectedOffset()
        {
            return Offset.New(this.RangeInMinutesBefore, this.RangeInMinutesAfter);
        }

        public TimingSchedule GetTypeOfSchedule()
        {
            switch (this.Type)
            {
                case SequenceType.Scheduled: return TimingSchedule.New(this.GetSelectedPeriod(), this.GetSelectedFrequency(), this.GetSelectedTimesOfDay(), this.GetSelectedDaysOfWeek(), this.GetSelectedOffset());
                case SequenceType.DateRange: return TimingSchedule.New(this.GetSelectedDates(), this.GetSelectedTimesOfDay(), this.GetSelectedOffset());
                case SequenceType.NeedBased: return TimingSchedule.New(this.GetSelectedPeriod(), this.GetSelectedFrequency(), this.GetSelectedTimesOfDay(), this.GetSelectedDaysOfWeek(), this.GetSelectedOffset(), true);
            }
            throw new ValidationException("Type is not one of scheduled, date-range or need-based");
        }
    }
}