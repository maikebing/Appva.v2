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
        public PatientViewModel Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The view measurement model.
        /// </summary>
        public ViewMeasurementModel ViewMeasurementModel
        {
            get;
            set;
        }

        /// <summary>
        /// The measurement observation list.
        /// </summary>
        public IList<Domain.Entities.MeasurementObservation> MeasurementList
        {
            get;
            set;
        }

        #endregion
    }
}