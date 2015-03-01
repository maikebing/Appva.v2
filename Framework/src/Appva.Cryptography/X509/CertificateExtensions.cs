// <copyright file="CertificateExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Core.Extensions;
    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.X509;
    using MSX509 = System.Security.Cryptography.X509Certificates;

    #endregion

    /// <summary>
    /// Extension helpers for X.509 certificate generation.
    /// </summary>
    public static class CertificateExtensions
    {
        /// <summary>
        /// Creates the X.509 certificate and writes to disk as a .pfx file.
        /// </summary>
        /// <param name="certificate">The current <c>X509Certificate2</c></param>
        /// <param name="outputFile">The file path, e.g. C:\cert.pfx</param>
        /// <param name="password">The .pfx password, defaults to null - no password</param>
        public static void WriteToDisk(this MSX509.X509Certificate2 certificate, string outputFile, string password = null)
        {
            var bytes = certificate.Export(MSX509.X509ContentType.Pfx, password);
            File.WriteAllBytes(outputFile, bytes);
        }

        /// <summary>
        /// Returns the subject as a <see cref="X509Name"/>.
        /// </summary>
        /// <param name="certificate">The current <c>X509Certificate2</c></param>
        /// <returns>A subject <see cref="X509Name"/></returns>
        public static X509Name GetSubject(this MSX509.X509Certificate2 certificate)
        {
            return new X509Name(certificate.Subject);
        }

        /// <summary>
        /// Returns the friendly name.
        /// </summary>
        /// <param name="certificate">The current <see cref="X509Certificate"/></param>
        /// <returns>The common name</returns>
        public static string FriendlyName(this X509Certificate certificate)
        {
            return certificate.SubjectDN.GetValues(X509Name.CN)[0] as string;
        }

        /// <summary>
        /// Sets the issuer DN.
        /// </summary>
        /// <param name="builder">The current <see cref="X509V3CertificateGenerator"/></param>
        /// <param name="issuerName">The issuer name</param>
        public static void SetIssuerDn(this X509V3CertificateGenerator builder, string issuerName)
        {
            builder.SetIssuerDN(issuerName.Contains("=") ? new X509Name(issuerName) : new X509Name("CN=" + issuerName));
        }

        /// <summary>
        /// Sets the subject DN.
        /// </summary>
        /// <param name="builder">The current <see cref="X509V3CertificateGenerator"/></param>
        /// <param name="subjectName">The subject name</param>
        public static void SetSubjectDn(this X509V3CertificateGenerator builder, string subjectName)
        {
            builder.SetSubjectDN(subjectName.Contains("=") ? new X509Name(subjectName) : new X509Name("CN=" + subjectName));
        }

        /// <summary>
        /// Adds the proper extensions for the <see cref="Usage"/>.
        /// </summary>
        /// <param name="builder">The current <see cref="X509V3CertificateGenerator"/></param>
        /// <param name="usage">The <see cref="Usage"/></param>
        public static void AddExtensions(this X509V3CertificateGenerator builder, Usage usage)
        {
            switch (usage)
            {
                case Usage.CertificateAuthority:
                    builder.AddRootAuthorityCertificateExtensions();
                    break;
                case Usage.Server:
                    builder.AddServerCertificateExtensions();
                    break;
                case Usage.Client:
                    builder.AddClientCertificateExtensions();
                    break;
                case Usage.Code:
                    builder.AddCodeCertificateExtensions();
                    break;
                default:
                    throw new InvalidOperationException("Unknown usage value {0}"
                        .FormatWith(usage.ToString()));
            }
        }

        /// <summary>
        /// Adds the root authority certificate extensions.
        /// </summary>
        /// <param name="builder">The current <see cref="X509V3CertificateGenerator"/></param>
        public static void AddRootAuthorityCertificateExtensions(this X509V3CertificateGenerator builder)
        {
            builder.AddExtension(X509Extensions.BasicConstraints, true, new BasicConstraints(true));
            builder.AddExtension(X509Extensions.KeyUsage, true, new KeyUsage(KeyUsage.KeyCertSign | KeyUsage.CrlSign));
            builder.AddExtension(
                X509Extensions.ExtendedKeyUsage, 
                true, 
                new ExtendedKeyUsage( 
                    KeyPurposeID.IdKPServerAuth, 
                    KeyPurposeID.IdKPClientAuth, 
                    KeyPurposeID.IdKPCodeSigning, 
                    KeyPurposeID.IdKPEmailProtection));
        }

        /// <summary>
        /// Adds the server certificate extensions.
        /// </summary>
        /// <param name="builder">The current <see cref="X509V3CertificateGenerator"/></param>
        public static void AddServerCertificateExtensions(this X509V3CertificateGenerator builder)
        {
            builder.AddExtension(X509Extensions.BasicConstraints, true, new BasicConstraints(false));
            builder.AddExtension(X509Extensions.KeyUsage, true, new KeyUsage(KeyUsage.DigitalSignature | KeyUsage.NonRepudiation | KeyUsage.KeyEncipherment));
            builder.AddExtension(X509Extensions.ExtendedKeyUsage, true, new ExtendedKeyUsage(KeyPurposeID.IdKPClientAuth, KeyPurposeID.IdKPServerAuth));
        }

        /// <summary>
        /// Adds the client certificate extensions.
        /// </summary>
        /// <param name="builder">The current <see cref="X509V3CertificateGenerator"/></param>
        public static void AddClientCertificateExtensions(this X509V3CertificateGenerator builder)
        {
            builder.AddExtension(X509Extensions.BasicConstraints, true, new BasicConstraints(false));
            builder.AddExtension(X509Extensions.KeyUsage, true, new KeyUsage(KeyUsage.DigitalSignature | KeyUsage.NonRepudiation | KeyUsage.KeyEncipherment));
            builder.AddExtension(X509Extensions.ExtendedKeyUsage, true, new ExtendedKeyUsage(KeyPurposeID.IdKPClientAuth));
        }

        /// <summary>
        /// Adds the code certificate extensions.
        /// </summary>
        /// <param name="builder">The current <see cref="X509V3CertificateGenerator"/></param>
        public static void AddCodeCertificateExtensions(this X509V3CertificateGenerator builder)
        {
            builder.AddExtension(X509Extensions.BasicConstraints, true, new BasicConstraints(false));
            builder.AddExtension(X509Extensions.KeyUsage, true, new KeyUsage(KeyUsage.DigitalSignature | KeyUsage.NonRepudiation | KeyUsage.KeyEncipherment));
            builder.AddExtension(X509Extensions.ExtendedKeyUsage, true, new ExtendedKeyUsage(KeyPurposeID.IdKPCodeSigning));
        }

        /// <summary>
        /// Add the "Subject Alternative Names" extension.
        /// </summary>
        /// <param name="builder">The current <see cref="X509V3CertificateGenerator"/></param>
        /// <param name="subjectAlternativeNames">A collection of alternative names</param>
        public static void AddSubjectAlternativeNames(this X509V3CertificateGenerator builder, IEnumerable<string> subjectAlternativeNames)
        {
            if (subjectAlternativeNames == null)
            {
                return;
            }
            var extension = new DerSequence(subjectAlternativeNames.Select(name => new GeneralName(GeneralName.DnsName, name)).ToArray<Asn1Encodable>());
            builder.AddExtension(X509Extensions.SubjectAlternativeName.Id, false, extension);
        }

        /// <summary>
        /// Add the Authority Key Identifier.
        /// </summary>
        /// <param name="builder">The current <see cref="X509V3CertificateGenerator"/></param>
        /// <param name="issuerDn">Issuer distinguished name (DN)</param>
        /// <param name="issuerKeyPair">Issuer asymmetric key pair</param>
        /// <param name="issuerSerialNumber">Issuer serial number</param>
        public static void AddAuthorityKeyIdentifier(this X509V3CertificateGenerator builder, X509Name issuerDn, AsymmetricCipherKeyPair issuerKeyPair, BigInteger issuerSerialNumber)
        {
            var authorityKeyIdentifierExtension = new AuthorityKeyIdentifier(
                    SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(issuerKeyPair.Public),
                    new GeneralNames(new GeneralName(issuerDn)),
                    issuerSerialNumber);
            builder.AddExtension(X509Extensions.AuthorityKeyIdentifier.Id, false, authorityKeyIdentifierExtension);
        }
    }
}
