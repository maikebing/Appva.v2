// <copyright file="SequenceMedicationCompareModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SequenceMedicationCompareModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceMedicationCompareModel"/> class.
        /// </summary>
        public SequenceMedicationCompareModel()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public Sequence Sequence
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        /// <value>
        /// The history.
        /// </value>
        public IList<Sequence> History
        {
            get;
            set;
        }

        #endregion
    }
}