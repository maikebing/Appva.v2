// <copyright file="ListObservations.cs" company="Appva AB">
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
    using System.Linq;
    using System.Web.Mvc;
using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListObservations
    {
        /// <summary>
        /// The patient.
        /// </summary>
        public PatientViewModel Patient
        {
            get;
            set;
        }
        
        /// <summary>
        /// The observations.
        /// </summary>
        public IList<Observation> Observations
        {
            get;
            set;
        }
    }
}