// <copyright file="UpdateMeasurementModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateMeasurementModel : Identity<ListMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The PatientId
        /// </summary>
        public Guid PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// The MeasurementObservation.
        /// </summary>
        public MeasurementObservation MeasurementObservation
        {
            get;
            set;
        }

        /// <summary>
        /// The MeasurementObservationId
        /// </summary>
        public Guid MeasurementId
        {
            get;
            set;
        }

        /// <summary>
        /// The name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The instruction.
        /// </summary>
        public string Instruction
        {
            get;
            set;
        }

        /// <summary>
        /// The unit SelectList
        /// </summary>
        public IEnumerable<SelectListItem> SelectUnitList
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation SelectList
        /// </summary>
        public IEnumerable<SelectListItem> SelectDelegationList
        {
            get;
            set;
        }

        /// <summary>
        /// The selected unit.
        /// </summary>
        public string SelectedUnit
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

        #endregion
    }
}