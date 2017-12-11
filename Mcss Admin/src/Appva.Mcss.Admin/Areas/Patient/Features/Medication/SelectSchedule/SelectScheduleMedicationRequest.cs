// <copyright file="SelectScheduleMedicationRequest.cs" company="Appva AB">
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
    public sealed class SelectScheduleMedicationRequest : IRequest<SelectScheduleMedicationModel>
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
        public string OrdinationId
        {
            get;
            set;
        }

        #endregion
    }
}