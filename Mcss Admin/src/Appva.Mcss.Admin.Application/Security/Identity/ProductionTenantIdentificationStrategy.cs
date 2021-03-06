﻿// <copyright file="ProductionTenantIdentificationStrategy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
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
    public sealed class ProductionTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        #region Variables.

        /// <summary>
        /// The production host domain.
        /// </summary>
        private const string Host = "appvamcss.com";

        /// <summary>
        /// The certificate issuer.
        /// </summary>
        private const string Issuer = "CN=Appva MCSS Self-signed Root CA";

        /// <summary>
        /// The client certificate header key.
        /// </summary>
        private const string ClientCertificateHeader = "Client-SerialNumber";

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
                    Log.Error("Context is null");
                    return false;
                }
                if (context.Request.Headers[ClientCertificateHeader] == null)
                {
                    Log.Error("Client certificate header empty");
                    return false;
                }
                var content = context.Request.Headers[ClientCertificateHeader];
                var certificate = new X509Certificate2(content.ToUtf8Bytes());
                if (!this.IsValidCertificate(certificate))
                {
                    Log.Warn(
                        "The certificate is invalid! SerialNumber: [{0}], Issuer: [{1}], NotBefore: [{2}], NotAfter: [{3}]",
                        certificate.SerialNumber,
                        certificate.Issuer,
                        certificate.NotBefore,
                        certificate.NotAfter);
                    return false;
                }
                //// The serial number without dashes.
                var serialNumber = certificate.SerialNumber.ToLower();
                //// Push dashed on every other character, e.g. d6-80-bb-0a...
                //// This is not the fastest way of doing this but the easiest to read.
                for (int i = 2; i < serialNumber.Length; i += 3)
                {
                    serialNumber = serialNumber.Insert(i, "-");
                }
                identifier = new TenantIdentifier(serialNumber);
            }
            catch (Exception ex)
            {
                Log.Error("Client certificate header: {0}", HttpContext.Current.Request.Headers[ClientCertificateHeader]);
                Log.Error("Exception when try identify tenant:{0}", ex.Message);
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

        #region Private Methods.

        /// <summary>
        /// Validates the certificate.
        /// </summary>
        /// <param name="certificate">The certificate to be validated</param>
        /// <returns>True if the certificate is valid; otherwise false</returns>
        private bool IsValidCertificate(X509Certificate2 certificate)
        {
            if (DateTime.Now.IsGreaterThan(certificate.NotAfter) || DateTime.Now.IsLessThan(certificate.NotBefore))
            {
                return false;
            }
            if (certificate.SerialNumber.IsEmpty())
            {
                return false;
            }
            if (! certificate.Issuer.Equals(Issuer))
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}