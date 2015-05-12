// <copyright file="StagingTenantIdentificationStrategy.cs" company="Appva AB">
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
    public sealed class StagingTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<StagingTenantIdentificationStrategy>();

        /// <summary>
        /// The HTTP header.
        /// </summary>
        private const string Host = "HOST";

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
                var domains = context.Request.Headers.Get(Host).Split('.');
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

        #endregion
    }
}