// <copyright file="EditTenantResponse.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System.Web;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class EditTenantResponse : CommonTenant<Id<DetailsTenant>>
    {
        /// <summary>
        /// The tenant logotype if any.
        /// </summary>
        public HttpPostedFileBase Logotype
        {
            get;
            set;
        }
    }
}