// <copyright file="FindTenaId.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class GetResident : Identity<string>
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