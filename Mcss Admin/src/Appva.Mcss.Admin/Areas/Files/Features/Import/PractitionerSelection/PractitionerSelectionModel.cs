// <copyright file="PractitionerSelectionModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Data;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PractitionerSelectionModel
    {
        #region Properties.

        /// <summary>
        /// The file id.
        /// </summary>
        public Guid FileId
        {
            get;
            set;
        }

        /// <summary>
        /// The excel data.
        /// </summary>
        public DataTable Data
        {
            get;
            set;
        }

        #endregion
    }
}