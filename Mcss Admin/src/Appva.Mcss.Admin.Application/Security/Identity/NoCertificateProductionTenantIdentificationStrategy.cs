// <copyright file="NoCertificateProductionTenantIdentificationStrategy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.com">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Identity
{
    #region Imports.

    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Web;
    using Appva.Core.Extensions;
    using Appva.Core.Logging;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NoCertificateProductionTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Variables.

        /// <summary>
        /// The production host domain.
        /// </summary>
        private const string Host = "appva.jp";

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<NoCertificateProductionTenantIdentificationStrategy>();

        #endregion

        #region ITenantIdentificationStrategy Members.

        /// <inheritdoc />
        public bool TryIdentifyTenant(out ITenantIdentifier identifier)
        {
            identifier = null;
            try
            {
                var context = HttpContext.Current;
                if (context == null || context.Request == null)
                {
                    return false;
                }

                var domainNodes = context.Request.Url.Host.Split('.');
                if (domainNodes.Length != 3)
                {
                    return false;
                }
                identifier = new TenantIdentifier(domainNodes[0]);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return identifier != null;
        }

        /// <inheritdoc />
        public IValidateTenantIdentificationResult Validate(ITenantIdentity identity, Uri uri)
        {
            if (identity.IsNull())
            {
                return ValidateTenantIdentificationResult.NotFound;
            }
            if (identity.HostName.IsEmpty())
            {
                return ValidateTenantIdentificationResult.Valid;
            }
            var expected = "{0}.{1}".FormatWith(identity.HostName, Host);
            return expected.Equals(uri.Host) ? ValidateTenantIdentificationResult.Valid : ValidateTenantIdentificationResult.Invalid;
        }

        #endregion
    }
}