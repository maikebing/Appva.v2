// <copyright file="ListMeasurementModel.cs" company="Appva AB">
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
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mcss.Admin.Domain.Entities;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListMeasurementModel
    {
        #region Variables

        /// <summary>
        /// The patient viewmodel
        /// </summary>
        //public PatientViewModel Patient
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// The measurement observation list.
        /// </summary>
        public IList<MeasurementObservation> MeasurementList
        {
            get;
            set;
        }

        public Guid Id { get; set; }
        public Guid MeasurementId { get; set; }
        public PatientViewModel PatientViewModel { get; set; }
        public IList<ObservationItem> MeasurementValueList { get; set; }
        public string MeasurementUnit { get; set; }
        public string MeasurementLongScale { get; set; }
        public string MeasurementScale { get; set; }
        public Taxon Delegation { get; set; }
        public string MeasurementName { get; set; }
        public string MeasurementInstructions { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        #endregion
    }
}