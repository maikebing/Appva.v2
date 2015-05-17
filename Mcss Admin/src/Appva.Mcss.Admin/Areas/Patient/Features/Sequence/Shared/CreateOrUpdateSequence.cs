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
                new SelectListItem { Text = "Varje dag", Value = "1" },
                new SelectListItem { Text = "Varannan dag", Value = "2" },
                new SelectListItem { Text = "Var 3:e dag", Value = "3" },
                new SelectListItem { Text = "Var 4:e dag", Value = "4" },
                new SelectListItem { Text = "Var 5:e dag", Value = "5" },
                new SelectListItem { Text = "Varje vecka", Value = "7" },
                new SelectListItem { Text = "Varannan vecka", Value = "14" },
                new SelectListItem { Text = "Annan ...", Value = "0" }
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

        [Required]
        [DisplayName("Ordination")]
        public virtual string Name { get; set; }

        [DisplayName("Instruktion")]
        public virtual string Description { get; set; }

        [DisplayName("Kräver delegering för")]
        public virtual Guid? Delegation { get; set; }

        public virtual IEnumerable<SelectListItem> Delegations { get; set; }

        [DisplayName("Skall ges")]
        public virtual int? Interval { get; set; }

        public virtual IEnumerable<SelectListItem> Intervals { get; set; }

        [Date]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Från")]
        public virtual DateTime? StartDate { get; set; }

        [Date]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Till")]
        public virtual DateTime? EndDate { get; set; }

        public virtual string Dates { get; set; }

        [DisplayName("Klockslag")]
        public IEnumerable<CheckBoxViewModel> Times { get; set; }

        public string Hour { get; set; }

        public string Minute { get; set; }

        [Range(0, 99999, ErrorMessage = "Måste vara mellan 0-99999")]
        public virtual int RangeInMinutesBefore { get; set; }

        [Range(0, 99999, ErrorMessage = "Måste vara mellan 0-99999")]
        public virtual int RangeInMinutesAfter { get; set; }

        public virtual bool OnNeedBasis { get; set; }

        [DisplayName("Lägg till påminnelse")]
        public bool Reminder { get; set; }

        [Range(0, 120, ErrorMessage = "Måste vara mellan 0-120")]
        [RequiredIf(Target = "Reminder", Value = true, ErrorMessage = "Påminnelse måste väljas.")]
        [DisplayName("Tid för påminnelse")]
        public int ReminderInMinutesBefore { get; set; }

        [RequiredIf(Target = "OnNeedBasis", Value = true, ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateLessThan(Target = "OnNeedBasisEndDate", ErrorMessage = "Startdatum måste vara ett tidigare datum är slutdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Från")]
        public virtual DateTime? OnNeedBasisStartDate { get; set; }

        [RequiredIf(Target = "OnNeedBasis", Value = true, ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateGreaterThan(Target = "OnNeedBasisStartDate", ErrorMessage = "Slutdatum måste vara ett senare datum är startdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Till")]
        public virtual DateTime? OnNeedBasisEndDate { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Schedule Schedule { get; set; }

        [DisplayName("Får endast ges av legitimerad sjuksköterska")]
        public bool Nurse { get; set;
        }

        #endregion
    }

}