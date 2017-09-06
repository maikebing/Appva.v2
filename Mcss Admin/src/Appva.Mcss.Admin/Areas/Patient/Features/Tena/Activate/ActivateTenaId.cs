// <copyright file="ActivateTenaId.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ActivateTenaId : Identity<ListTena>
    {
        /// <summary>
        /// The external tena id.
        /// </summary>
        public string ExternalId
        {
            get;
            set;
        }
    }
}