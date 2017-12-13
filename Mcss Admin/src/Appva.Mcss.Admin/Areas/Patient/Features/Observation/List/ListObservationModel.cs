﻿// <copyright file="ListObservationModel.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// Class ListObservationModel.
    /// </summary>
    public class ListObservationModel
    {
        #region Properties.

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the measurement identifier.
        /// </summary>
        public Guid ObservationId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the patient view model.
        /// </summary>
        public PatientViewModel PatientViewModel
        {
            get;
            set;
        }

        /// <summary>
        /// The measurement observation list.
        /// </summary>
        public IList<Observation> ObservationList
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the measurement value list.
        /// </summary>
        public IList<ObservationItem> ObservationItemList
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the measurement unit.
        /// </summary>
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the measurement long scale.
        /// </summary>
        public string LongScale
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the measurement scale.
        /// </summary>
        public string Scale
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the delegation.
        /// </summary>
        public Taxon Delegation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the measurement.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the measurement instructions.
        /// </summary>
        public string Instructions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime? EndDate
        {
            get;
            set;
        }

        #endregion
    }
}