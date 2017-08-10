// <copyright file="ActivateTena.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.Net.Http;

    #endregion

    /// <summary>
    /// TODO: 
    /// </summary>
    public sealed class ActivateTena : Identity<string>
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