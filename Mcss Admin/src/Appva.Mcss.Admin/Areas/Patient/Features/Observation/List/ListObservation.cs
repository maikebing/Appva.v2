// <copyright file="ListObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// Class ListObservation.
    /// </summary>
    /// <seealso cref="Appva.Cqrs.IRequest{Appva.Mcss.Admin.Models.ListObservationModel}" />
    public class ListObservation : IRequest<ListObservationModel>
    {
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
    }
}