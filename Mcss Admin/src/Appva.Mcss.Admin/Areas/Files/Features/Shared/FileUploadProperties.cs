// <copyright file="FileUploadProperties.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class FileUploadProperties
    {
        #region Properties.

        /// <summary>
        /// If the file is an importable excel file.
        /// </summary>
        public bool IsImportableExcelFile
        {
            get;
            set;
        }

        #endregion
    }
}