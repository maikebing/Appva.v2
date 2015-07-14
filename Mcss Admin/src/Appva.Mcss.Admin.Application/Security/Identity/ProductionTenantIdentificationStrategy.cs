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
    using System.Security.Cryptography.X509Certificates;
    using System.Collections.Generic;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ProductionTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Variables.

        /// <summary>
        /// The production host domain.
        /// </summary>
        private const string Host = "appvamcss";

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
            try
            {
                var context = HttpContext.Current;
                if (context == null || context.Request == null)
                {
                    return false;
                }
                var certStr = HttpContext.Current.Request.Headers["Client-SerialNumber"];
                var bytes = new byte[certStr.Length * sizeof(char)];
                Buffer.BlockCopy(certStr.ToCharArray(), 0, bytes, 0, bytes.Length);
                var cert = new X509Certificate2(bytes);
                if (DateTime.Now > cert.NotAfter)
                {
                    return false;
                }
                var serialNumber = cert.SerialNumber.ToLower() ?? "";
                var key = new List<string>();
                for (var i = 2; i <= serialNumber.Length; i = i + 2)
                {
                    key.Add(serialNumber.Substring(i - 2, 2));
                }
                identifier = new TenantIdentifier(string.Join("-", key.ToArray()));
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