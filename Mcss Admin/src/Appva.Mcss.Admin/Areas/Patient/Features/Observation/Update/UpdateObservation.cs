// <copyright file="UpdateObservation.cs" company="Appva AB">
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
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// Class UpdateObservation.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Models.Identity{Appva.Mcss.Admin.Models.UpdateObservationModel}" />
    public class UpdateObservation : Identity<UpdateObservationModel>
    {
        #region Properties.

        /// <summary>
        /// The Measurement Observation Id.
        /// </summary>
        public Guid ObservationId
        {
            get;
            set;
        }

        #endregion
    }
}