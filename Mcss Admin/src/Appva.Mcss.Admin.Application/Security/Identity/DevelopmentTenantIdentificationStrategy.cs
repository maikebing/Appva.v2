// <copyright file="DevelopmentTenantIdentificationStrategy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Identity
{
    #region Imports.

    using System;
    using System.Web;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DevelopmentTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Variables.

        /// <summary>
        /// The host domain.
        /// </summary>
        private const string Host = "locahost";

        /// <summary>
        /// The development fixed tenant identifier.
        /// </summary>
        private readonly TenantIdentifier identifier = new TenantIdentifier("development");

        #endregion

        #region ITenantIdentificationStrategy Members.

        /// <inheritdoc />
        public bool TryIdentifyTenant(out ITenantIdentifier identifier)
        {
            identifier = this.identifier;
            return true;
        }

        /// <inheritdoc />
        public IValidateTenantIdentificationResult Validate(ITenantIdentity identity, Uri uri)
        {
            if (identity == null)
            {
                return ValidateTenantIdentificationResult.NotFound;
            }
            if (! string.IsNullOrWhiteSpace(identity.HostName))
            {
                var expected = identity.HostName + "." + Host;
                if (! expected.Equals(uri.Host))
                {
                    return ValidateTenantIdentificationResult.Invalid;
                }
            }
            return ValidateTenantIdentificationResult.Valid;
        }

        #endregion
    }
}