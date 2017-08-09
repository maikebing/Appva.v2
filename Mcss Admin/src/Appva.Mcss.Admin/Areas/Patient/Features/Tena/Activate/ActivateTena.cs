// <copyright file="ListTena.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region imports

    using Appva.Cqrs;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: 
    /// </summary>

    public sealed class ActivateTena : Identity<Task<JsonResult>>
    {
        /// <summary>
        /// The external tena id.
        /// </summary>
        public string ExternalId { get; set; }
    }
}