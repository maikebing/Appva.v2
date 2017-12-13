// <copyright file="AddObservationValue.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Class AddObservationValue.
    /// </summary>
    public class AddObservationValue : Identity<AddObservationValueModel>
    {
        #region Properties.

        /// <summary>
        /// The measurement observation id
        /// </summary>
        public Guid ObservationId { get; set; }

        #endregion
    }
}