// <copyright file="UpdateTenaObservationPeriodModel.cs" company="Appva AB">
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
    public sealed class UpdateTenaObservationPeriodModel : IRequest<ListTena>
    {
        #region Properties.

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the starts at.
        /// </summary>
        /// <value>
        /// The starts at.
        /// </value>
        public DateTime StartsAt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ends at.
        /// </summary>
        /// <value>
        /// The ends at.
        /// </value>
        public DateTime EndsAt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the instruction.
        /// </summary>
        /// <value>
        /// The instruction.
        /// </value>
        public string Instruction
        {
            get;
            set;
        }

        #endregion
    }
}