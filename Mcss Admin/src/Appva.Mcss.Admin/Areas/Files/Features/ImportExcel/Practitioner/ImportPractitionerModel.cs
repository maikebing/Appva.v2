// <copyright file="ImportPractitionerModel.cs" company="Appva AB">
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
    public sealed class ImportPractitionerModel : IRequest<bool>
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
        /// The valid column data.
        /// </summary>
        public string[] ValidColumns
        {
            get
            {
                return new string[]
                {
                    "Personnummer",
                    "Förnamn",
                    "Efternamn",
                    "E-post",
                    "Roll",
                    "Organisationstillhörighet",
                    "HSA-id"
                };
            }
        }

        /// <summary>
        /// Validate cells at the specified row.
        /// </summary>
        public int ValidateAtRow
        {
            get { return 2; }
        }

        /// <summary>
        /// Reads data from the specified row.
        /// </summary>
        public int ReadFromRow
        {
            get { return 4; }
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

        #endregion
    }
}