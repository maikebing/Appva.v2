// <copyright file="ImportPractitionerStatusModel.cs" company="Appva AB">
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
    public sealed class ImportPractitionerStatusModel : IRequest<ImportPractitionerStatusModel>
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
        /// The file name.
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// A dictionary of practitioner rows that could not be imported.
        /// </summary>
        public Dictionary<DataRow, List<string>> InvalidRows
        {
            get;
            set;
        }

        /// <summary>
        /// The number of successfully imported practitioner rows.
        /// </summary>
        public int ImportedRowsCount
        {
            get;
            set;
        }

        #endregion
    }
}