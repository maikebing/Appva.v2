// <copyright file="TenantIdentityPersistenceContextAwareResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Application.Persistence
{
    #region Imports.

    using System;
    using System.Web;
    using Core.Logging;
    using ResourceServer.Application.Authorization;
    using Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<TenantIdentificationStrategy>();

        #endregion

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
            identifier = null;
            try
            {
                identifier = new TenantIdentifier(HttpContext.Current.User.Identity.Tenant().ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return identifier != null;
        }

        #endregion
    }
}