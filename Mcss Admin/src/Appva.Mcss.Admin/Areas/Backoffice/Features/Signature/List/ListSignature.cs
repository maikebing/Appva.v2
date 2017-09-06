// <copyright file="ListSignature.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListSignature
    {
        /// <summary>
        /// The signing id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The signing name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The signing image path.
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Returns true if the signing option is in use.
        /// </summary>
        public bool IsUsedByList
        {
            get;
            set;
        }
    }
}