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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using DataAnnotationsExtensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateOrUpdateSequence
    {
        #region Variables.

        private static readonly List<SelectListItem> AllIntervals = new List<SelectListItem>()
            {
                new SelectListItem { Text = Resources.Language.Varje_dag,        Value = "1" },
                new SelectListItem { Text = Resources.Language.Varannan_dag,     Value = "2" },
                new SelectListItem { Text = Resources.Language.Var_3_e_dag,      Value = "3" },
                new SelectListItem { Text = Resources.Language.Var_4_e_dag,      Value = "4" },
                new SelectListItem { Text = Resources.Language.Var_5_e_dag,      Value = "5" },
                new SelectListItem { Text = Resources.Language.Var_6_e_dag,      Value = "6" },
                new SelectListItem { Text = Resources.Language.Varje_vecka,      Value = "7" },
                new SelectListItem { Text = Resources.Language.Varannan_vecka,   Value = "14" },
                new SelectListItem { Text = Resources.Language.Var_tredje_vecka, Value = "21" },
                new SelectListItem { Text = Resources.Language.Var_fjärde_vecka, Value = "28" },
                new SelectListItem { Text = Resources.Language.Var_femte_vecka,  Value = "35" },
                new SelectListItem { Text = Resources.Language.Var_åttonde_vecka, Value = "56" },
                new SelectListItem { Text = Resources.Language.Var_tolfte_vecka, Value = "84" },
                new SelectListItem { Text = Resources.Language.Annan___,         Value = "0" }
            };

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrUpdateSequence"/> class.
        /// </summary>
        public CreateOrUpdateSequence()
        {
            Intervals = AllIntervals;
        }

        #endregion

        #region Properties.

        #region Structural

        /// <summary>
        /// The patient id
        /// </summary>
        public Guid PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// The schedule id
        /// </summary>
        public Guid ScheduleId
        {
            get;
            set;
        }

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

        #endregion

        #region Form

        /// <summary>
        /// The Sequence name
        /// </summary>
        [Required(ErrorMessageResourceName="Insats_måste_anges", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Insats", ResourceType = typeof(Resources.Language))]
        public string Name 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The sequence description
        /// </summary>
        [Display(Name = "Instruktion", ResourceType = typeof(Resources.Language))]
        public string Description 
        { 
            get;
            set; 
        }

        /// <summary>
        /// If the sequence only can be completed by a nurse
        /// </summary>
        public bool Nurse 
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
        [Display(Name = "Kräver_delegering_för", ResourceType = typeof(Resources.Language))]
        public Guid? Delegation 
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
        [RequiredIf(Target = "CreateNewInventory", Value = false, ErrorMessageResourceName = "Förbrukningsjournal_måste_väljas", ErrorMessageResourceType = typeof(Resources.Language))]
        public Guid? Inventory
        {
            get;
            set;
        }

        /// <summary>
        /// The repeat interval
        /// </summary>
        [Display(Name = "Skall_ges", ResourceType = typeof(Resources.Language))]
        public int? Interval 
        { 
            get;
            set; 
        }

        /// <summary>
        /// The startdate, if scheduled
        /// </summary>
        [Date]
        [Display(Name = "Fr_o_m_", ResourceType = typeof(Resources.Language))]
        public virtual DateTime? StartDate 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The enddate, if scheduled
        /// </summary>
        [Date]
        [Display(Name ="T_o_m_", ResourceType = typeof(Resources.Language))]
        public DateTime? EndDate 
        {
            get;
            set;
        }

        /// <summary>
        /// If occures on specific dates, the dates
        /// </summary>
        public string Dates 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The hours which the sequence shall occur
        /// </summary>
        [Display(Name = "Klockslag", ResourceType = typeof(Resources.Language))]
        public IEnumerable<CheckBoxViewModel> Times 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The timespan before its ok to complete.
        /// In minutes.
        /// </summary>
        [Range(0, 99999, ErrorMessageResourceName = "Måste_vara_mellan_0_99999", ErrorMessageResourceType = typeof(Resources.Language))]
        public int RangeInMinutesBefore 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The timespan after its ok to complete.
        /// In minutes.
        /// </summary>
        [Range(0, 99999, ErrorMessageResourceName = "Måste_vara_mellan_0_99999", ErrorMessageResourceType = typeof(Resources.Language))]
        public int RangeInMinutesAfter 
        { 
            get; 
            set;
        }

        /// <summary>
        /// If the sequence is needs-based
        /// </summary>
        public bool OnNeedBasis 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// If the sequence should have a specific reminder
        /// </summary>
        [Display(Name = "Lägg_till_påminnelse", ResourceType = typeof(Resources.Language))]
        public bool Reminder 
        {
            get;
            set; 
        }

        /// <summary>
        /// When the specific reminder shall be sent
        /// </summary>
        [Range(0, 120, ErrorMessageResourceName = "Måste_vara_mellan_0-120", ErrorMessageResourceType = typeof(Resources.Language))]
        [RequiredIf(Target = "Reminder", Value = true, ErrorMessageResourceName = "Påminnelse_måste_väljas", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Tid_för_påminnelse", ResourceType = typeof(Resources.Language))]
        public int ReminderInMinutesBefore 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The startdate, if needs-based
        /// </summary>
        [RequiredIf(Target = "OnNeedBasis", Value = true, ErrorMessageResourceName = "Datum_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [Date(ErrorMessageResourceName = "Datum_måste_fyllas_i_med_åtta_siffror_och_bindestreck__t__ex__2012-12-21", ErrorMessageResourceType = typeof(Resources.Language))]
        [DateLessThan(Target = "OnNeedBasisEndDate", ErrorMessageResourceName = "Startdatum_måste_vara_ett_tidigare_datum_är_slutdatum", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Fr_o_m_", ResourceType = typeof(Resources.Language))]
        public DateTime? OnNeedBasisStartDate 
        {
            get;
            set; 
        }

        /// <summary>
        /// The enddate, if needs-based
        /// </summary>
        [RequiredIf(Target = "OnNeedBasis", Value = true, ErrorMessageResourceName = "Datum_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [Date(ErrorMessageResourceName = "Datum_måste_fyllas_i_med_åtta_siffror_och_bindestreck__t__ex__2012-12-21", ErrorMessageResourceType = typeof(Resources.Language))]
        [DateGreaterThan(Target = "OnNeedBasisStartDate", ErrorMessageResourceName = "Slutdatum_måste_vara_ett_senare_datum_är_startdatum", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "T_o_m_", ResourceType = typeof(Resources.Language))]
        public DateTime? OnNeedBasisEndDate
        {
            get;
            set;
        }

        #endregion

        #region Lists.

        /// <summary>
        /// All available delegations
        /// </summary>
        public IEnumerable<SelectListItem> Delegations 
        { 
            get;
            set;
        }

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

        #endregion


        #endregion
    }

}