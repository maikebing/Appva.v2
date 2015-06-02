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
            /*identifier = null;
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
            return identifier != null;*/

            identifier = null;
            var context = HttpContext.Current;

            try
            {
                if (context == null || context.Request == null)
                {
                    return false;
                }
                var certStr = HttpContext.Current.Request.Headers["Client-SerialNumber"];
                byte[] bytes = new byte[certStr.Length * sizeof(char)];
                System.Buffer.BlockCopy(certStr.ToCharArray(), 0, bytes, 0, bytes.Length);
                var cert = new X509Certificate2(bytes);
                var id = cert.SerialNumber.ToLower() ?? "";
                List<string> key = new List<string>();
                for (var i = 2; i <= id.Length; i = i + 2)
                {
                    key.Add(id.Substring(i - 2, 2));
                }

                identifier = new TenantIdentifier(string.Join("-", key.ToArray()));
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