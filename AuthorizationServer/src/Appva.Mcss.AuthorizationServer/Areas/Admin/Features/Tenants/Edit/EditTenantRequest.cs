// <copyright file="EditTenantRequest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class EditTenantRequest : CommonTenant
    {
        /// <summary>
        /// The tenant logotype if any.
        /// </summary>
        public Logotype Logotype
        {
            get;
            set;
        }
    }
}