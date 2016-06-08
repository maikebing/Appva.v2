// <copyright file="TempTenantIdentificationStrategy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Identity
{
    #region Imports.

    using Appva.Tenant.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TempTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TempTenantIdentificationStrategy"/> class.
        /// </summary>
        public TempTenantIdentificationStrategy()
        {
        }

        #endregion

        #region ITenantIdentificationStrategy Members.

        /// <inheritdoc />
        public bool TryIdentifyTenant(out ITenantIdentifier identifier)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IValidateTenantIdentificationResult Validate(ITenantIdentity identity, Uri uri)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}