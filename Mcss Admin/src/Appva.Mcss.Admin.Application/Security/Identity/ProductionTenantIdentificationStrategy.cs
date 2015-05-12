// <copyright file="ProductionTenantIdentificationStrategy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Identity
{
    #region Imports.

    using System.Web;
    using Appva.Core.Logging;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ProductionTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<ProductionTenantIdentificationStrategy>();

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
                var cert = context.Request.ClientCertificate;
                if (! cert.IsPresent || ! cert.IsValid)
                {
                    return false;
                }
                identifier = new TenantIdentifier(cert.SerialNumber);
            }
            catch (HttpException ex)
            {
                Log.Error(ex);
            }
            return identifier != null;
        }

        #endregion
    }
}