// <copyright file="PractitionerPreviewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Data;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PractitionerPreviewModel : IRequest<Guid>
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
        /// The file title.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// The file description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The file name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The file size.
        /// </summary>
        public string Size
        {
            get;
            set;
        }

        /// <summary>
        /// Validate cells at the specified row.
        /// </summary>
        public int ValidateAtRow
        {
            get;
            set;
        }

        /// <summary>
        /// Reads data from the specified row.
        /// </summary>
        public int ReadFromRow
        {
            get;
            set;
        }

        /// <summary>
        /// Roles that doesn't require HSA id.
        /// </summary>
        public string IncludedRolesWithoutHsaId
        {
            get;
            set;
        }

        /// <summary>
        /// The excluded roles.
        /// </summary>
        public string ExcludedRoles
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

        /// <summary>
        /// Indicates if the file has importable practitioners.
        /// </summary>
        public bool IsImportable
        {
            get;
            set;
        }

        #endregion
    }
}