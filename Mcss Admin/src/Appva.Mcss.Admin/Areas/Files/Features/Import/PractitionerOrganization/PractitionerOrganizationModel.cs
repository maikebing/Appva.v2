// <copyright file="PractitionerOrganizationModel.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PractitionerOrganizationModel
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
        /// Indicates if the file is not null.
        /// </summary>
        public bool IsNotNull
        {
            get;
            set;
        }

        /// <summary>
        /// A collection of unique organization nodes.
        /// </summary>
        public IEnumerable<string> UniqueNodes
        {
            get;
            set;
        }

        #endregion
    }
}