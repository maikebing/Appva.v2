// <copyright file="DemoTenantIdentificationStrategy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Identity
{
    #region Imports.

    using System;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DemoTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Variables.

        /// <summary>
        /// Static demo tenant identifier. 
        /// </summary>
        private static readonly TenantIdentifier Identifier = new TenantIdentifier("bc-14-7c-4e-4e-3c-79-b4-47-e2-c4-7e-4c-b6-dd-7a");
        
        #endregion

        #region ITenantIdentificationStrategy Members.

        /// <inheritdoc />
        public bool TryIdentifyTenant(out ITenantIdentifier identifier)
        {
            identifier = Identifier;
            return true;
        }

        /// <inheritdoc />
        public IValidateTenantIdentificationResult Validate(ITenantIdentity identity, Uri uri)
        {
            return ValidateTenantIdentificationResult.Valid;
        }

        #endregion
    }
}