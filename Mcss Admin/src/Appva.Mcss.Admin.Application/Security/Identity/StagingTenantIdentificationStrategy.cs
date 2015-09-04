// <copyright file="StagingTenantIdentificationStrategy.cs" company="Appva AB">
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
    using Appva.Core.Logging;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class StagingTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Variables.

        /// <summary>
        /// The HTTP header.
        /// </summary>
        private const string HostHeader = "HOST";

        /// <summary>
        /// The host domain.
        /// </summary>
        private const string Host = "dev.appvamcss";

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<StagingTenantIdentificationStrategy>();

        #endregion

        #region ITenantIdentificationStrategy Members.

        /// <inheritdoc />
        public bool TryIdentifyTenant(out ITenantIdentifier identifier)
        {
            identifier = null;
            var context = HttpContext.Current;
            try
            {
                if (context == null || context.Request == null)
                {
                    return false;
                }
                var domains = context.Request.Headers.Get(HostHeader).Split('.');
                if (domains.Length < 3)
                {
                    return false;
                }
                identifier = new TenantIdentifier(domains[0]);
            }
            catch (HttpException ex)
            {
                Log.Error(ex);
            }
            return identifier != null;
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