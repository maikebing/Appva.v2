﻿// <copyright file="AddMedicationToSequence.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AddMedicationToSequence : IAsyncRequest<DetailsMedicationRequest>
    {
        #region Properties

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
        /// Gets or sets the ordination identifier.
        /// </summary>
        /// <value>
        /// The ordination identifier.
        /// </value>
        public long OrdinationId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sequence identifier.
        /// </summary>
        /// <value>
        /// The sequence identifier.
        /// </value>
        public Guid SequenceId
        {
            get;
            set;
        }

        #endregion
    }
}