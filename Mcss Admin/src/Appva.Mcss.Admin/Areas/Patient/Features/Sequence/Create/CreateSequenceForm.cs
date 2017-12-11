// <copyright file="CreateSequenceForm.cs" company="Appva AB">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Appva.Cqrs;
using Appva.Domain;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;
using Appva.Mvc;
using DataAnnotationsExtensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateSequenceForm : IRequest<DetailsSchedule>
    {
        #region Variables.
        /*
        private static readonly List<SelectListItem> AllIntervals = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Varje dag",        Value = "1" },
                new SelectListItem { Text = "Varannan dag",     Value = "2" },
                new SelectListItem { Text = "Var 3:e dag",      Value = "3" },
                new SelectListItem { Text = "Var 4:e dag",      Value = "4" },
                new SelectListItem { Text = "Var 5:e dag",      Value = "5" },
                new SelectListItem { Text = "Var 6:e dag",      Value = "6" },
                new SelectListItem { Text = "Varje vecka",      Value = "7" },
                new SelectListItem { Text = "Varannan vecka",   Value = "14" },
                new SelectListItem { Text = "Var tredje vecka", Value = "21" },
                new SelectListItem { Text = "Var fjärde vecka", Value = "28" },
                new SelectListItem { Text = "Var femte vecka",  Value = "35" },
                new SelectListItem { Text = "Var åttonde vecka",  Value = "56" },
                new SelectListItem { Text = "Var tolfte vecka",  Value = "84" },
                new SelectListItem { Text = "Annan ...",        Value = "0" }
            };
        */
        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrUpdateSequence"/> class.
        /// </summary>
        public CreateSequenceForm()
        {
            //Intervals = AllIntervals;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The Patient
        /// </summary>
        public Patient Patient 
        {
            get; 
            set;
        }

        /// <summary>
        /// The Schedule
        /// </summary>
        public Schedule Schedule 
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
        /// If sequence needs delegation, the delegation
        /// </summary>
        /*[DisplayName("Kräver delegering för")]*/
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
        /// If a new inventory should be created for this sequence
        /// </summary>
        public bool CreateNewInventory
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory
        /// </summary>
        [RequiredIf(Target = "CreateNewInventory", Value = false, ErrorMessage = "Saldo måste väljas")]
        public Guid? InventoryId
        {
            get;
            set;
        }

        /// <summary>
        /// The repeat interval
        /// </summary>
        [DisplayName("Skall ges")]
        public int? Interval 
        { 
            get;
            set; 
        }

        /// <summary>
        /// The startdate, if scheduled
        /// </summary>
        //[Date]
        [DisplayName("Fr.o.m.")]
        public Date? StartDate 
        { 
            get; 
            set;
        }

        public bool IsPeriodWithTimeOfDay
        {
            get;
            set;
        }
        public int? StartHour
        {
            get;
            set;
        }

        public int? StartMinute
        {
            get;
            set;
        }

        /// <summary>
        /// The enddate, if scheduled
        /// </summary>
        //[Date]
        [DisplayName("T.o.m.")]
        public Date? EndDate 
        {
            get;
            set;
        }

        public int? EndHour
        {
            get;
            set;
        }

        public int? EndMinute
        {
            get;
            set;
        }

        /// <summary>
        /// If occures on specific dates, the dates
        /// </summary>
        public IList<Date> Dates 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The hours which the sequence shall occur
        /// </summary>
        [DisplayName("Klockslag")]
        public IEnumerable<CheckBoxViewModel> Times 
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

        /// <summary>
        /// If the sequence is needs-based
        /// </summary>
        public bool IsNeedBased 
        { 
            get; 
            set; 
        }

        /*
        /// <summary>
        /// The startdate, if needs-based
        /// </summary>
        [RequiredIf(Target = "OnNeedBasis", Value = true, ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateLessThan(Target = "OnNeedBasisEndDate", ErrorMessage = "Startdatum måste vara ett tidigare datum än slutdatum.")]
        [DisplayName("Fr.o.m.")]
        public DateTime? OnNeedBasisStartDate 
        {
            get;
            set; 
        }

        /// <summary>
        /// The enddate, if needs-based
        /// </summary>
        [RequiredIf(Target = "OnNeedBasis", Value = true, ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateGreaterThan(Target = "OnNeedBasisStartDate", ErrorMessage = "Slutdatum måste vara ett senare datum än startdatum.")]
        [DisplayName("T.o.m.")]
        public DateTime? OnNeedBasisEndDate
        {
            get;
            set;
        }
        */
        

        /// <summary>
        /// All availabel inventories
        /// </summary>
        public IEnumerable<SelectListItem> Inventories
        {
            get;
            set;
        }

        /// <summary>
        /// All available intervals
        /// </summary>
        public IEnumerable<SelectListItem> Intervals
        { 
            get; 
            set; 
        }

        public SequenceType Type
        {
            get;
            set;
        }

        public InventoryState InventoryType
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

        public IEnumerable<TimeOfDay> SelectedDaysOfWeek
        {
            get;
            set;
        }

        #endregion
    }

    public enum SequenceType
    {
        NeedBased,
        Scheduled,
        DateRange
    }

    public enum InventoryState
    {
        New,
        Use
    }

    public enum Repetition
    {
        Daily,
        Weekly
    }
}