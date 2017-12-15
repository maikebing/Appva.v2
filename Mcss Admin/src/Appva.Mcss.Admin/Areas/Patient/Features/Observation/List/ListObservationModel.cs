// <copyright file="ListObservationModel.cs" company="Appva AB">
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
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListObservationModel"/> class.
        /// </summary>
        public ListObservationModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListObservationModel"/> class.
        /// </summary>
        /// <param name="patientViewModel">The patient view model.</param>
        /// <param name="observationList">The observation list.</param>
        public ListObservationModel(PatientViewModel patientViewModel, IList<Observation> observationList)
        {
            this.PatientViewModel = patientViewModel;
            this.ObservationList  = observationList;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListObservationModel"/> class.
        /// </summary>
        /// <param name="observation">The observation.</param>
        /// <param name="patientViewModel">The patient view model.</param>
        /// <param name="observationList">The observation list.</param>
        /// <param name="observationItemList">The observation item list.</param>
        public ListObservationModel(Observation observation, PatientViewModel patientViewModel, IList<Observation> observationList, IList<ObservationItem> observationItemList)
        {
            this.Id                  = observation.Patient.Id;
            this.ObservationId       = observation.Id;
            this.Name                = observation.Name;
            this.Instructions        = observation.Description;
            this.Delegation          = observation.Delegation;
            this.PatientViewModel    = patientViewModel;
            this.ObservationList     = observationList;
            this.ObservationItemList = observationItemList;
        }

        #endregion

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