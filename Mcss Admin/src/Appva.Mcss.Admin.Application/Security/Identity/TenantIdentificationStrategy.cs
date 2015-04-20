// <copyright file="TenantIdentificationStrategy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Identity
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Tenant.Identity;
    using Microsoft.Owin;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantIdentificationStrategy"/> class.
        /// </summary>
        public TenantIdentificationStrategy()
        {
        }

        #endregion

        #region ITenantIdentificationStrategy Members.

        /// <inheritdoc />
        public bool TryIdentifyTenant(out ITenantIdentifier identifier)
        {
            if (false)
            {
                var certificate = HttpContext.Current.Request.ClientCertificate;
                identifier = certificate.IsPresent && certificate.IsValid ? new TenantIdentifier(certificate.SerialNumber) : null;
                return identifier != null;
            }
            identifier = new TenantIdentifier("TreStiftelser");
            return true;
        }

        #endregion
    }
}