// <copyright file="CreateMedicationRequest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateMedicationRequest : IAsyncRequest<CreateMedicationModel>
    {
        #region Properties.

        /// <summary>
        /// The patient id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The ordination id
        /// </summary>
        public long OrdinationId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the schedule.
        /// </summary>
        public Guid Schedule
        {
            get;
            set;
        }

        #endregion
    }
}