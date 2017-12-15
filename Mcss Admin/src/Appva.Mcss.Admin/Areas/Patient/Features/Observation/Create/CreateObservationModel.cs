// <copyright file="CreateObservationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Enum Observations
    /// </summary>
    public enum Scale
    {
        Bristol,
        Feces,
        Weight
    }

    /// <summary>
    /// Class CreateObservationModel.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Models.Identity{Appva.Mcss.Admin.Models.ListObservation}" />
    public class CreateObservationModel : Identity<ListObservation>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateObservationModel"/> class.
        /// </summary>
        public CreateObservationModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateObservationModel"/> class.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="selectScaleList">The select scale list.</param>
        /// <param name="selectDelegationList">The select delegation list.</param>
        public CreateObservationModel(Guid patientId, IEnumerable<SelectListItem> selectDelegationList)
        {
            this.PatientId = patientId;
            this.SelectDelegationList = selectDelegationList;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Patient Id
        /// </summary>
        public Guid PatientId
        {
            get; set;
        }

        /// <summary>
        /// The name.
        /// </summary>
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The Scale SelectList.
        /// </summary>
        [DisplayName("Skala")]
        public IEnumerable<SelectListItem> SelectScaleList
        {
            get
            {
                return Enum.GetValues(typeof(Scale)).Cast<Scale>().Select(x => new SelectListItem { Text = ToText(x), Value = x.ToString() });
            }
        }

        /// <summary>
        /// The delegation select list.
        /// </summary>
        [DisplayName("Kräver delegering för")]
        public IEnumerable<SelectListItem> SelectDelegationList
        {
            get;
            set;
        }

        /// <summary>
        /// The selected delegation.
        /// </summary>
        public string SelectedDelegation
        {
            get;
            set;
        }

        /// <summary>
        /// The selected scale.
        /// </summary>
        [Required]
        public string SelectedScale
        {
            get;
            set;
        }

        #endregion

        #region Private Helpers.

        private string ToText(Scale value)
        {
            switch (value)
            {
                case Scale.Bristol: return "Bristol (Typ 1-7)";
                case Scale.Feces:   return "Generisk avföringsskala (Typ AAA-k)";
                case Scale.Weight:  return "Vikt (kg)";
                default: return null;
            }
        }

        #endregion
    }
}