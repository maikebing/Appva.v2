// <copyright file="CertificateUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using System.IO;
    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.Security;
    using Org.BouncyCastle.Utilities;
    using Org.BouncyCastle.X509;
    using MSX509 = System.Security.Cryptography.X509Certificates;

    #endregion

    /// <summary>
    /// Utility Helper for creating X.509 v3 certificates. 
    /// </summary>
    public static class CertificateUtils
    {
        /// <summary>
        /// Loads an X.509 certificate from the certificate store.
        /// </summary>
        /// <param name="findBy">The method to search for</param>
        /// <param name="value">The value to search for</param>
        /// <param name="name">The name of the X.509 certificate store to open</param>
        /// <param name="location">The location of the X.509 certificate store</param>
        /// <returns>An instance of <c>X509Certificate2</c> or null if not found</returns>
        public static MSX509.X509Certificate2 LoadCertificate(MSX509.X509FindType findBy, object value, MSX509.StoreName name = MSX509.StoreName.Root, MSX509.StoreLocation location = MSX509.StoreLocation.CurrentUser)
        {
            MSX509.X509Store store = null;
            try
            {
                store = new MSX509.X509Store(name, location);
                store.Open(MSX509.OpenFlags.ReadOnly);
                var certificates = store.Certificates.Find(findBy, value, true);
                if (certificates.Count != 1)
                {
                    return null;
                }
                return certificates[0];
            }
            finally
            {
                if (store != null)
                {
                    store.Close();
                }
            }
        }

        /// <summary>
        /// Loads an X.509 certificate from disk.
        /// </summary>
        /// <param name="inputFile">The input file, e.g. C:\cert.pfx</param>
        /// <param name="password">The certificate password</param>
        /// <returns>An instance of <c>X509Certificate2</c></returns>
        public static MSX509.X509Certificate2 LoadCertificateFromDisk(string inputFile, string password)
        {
            return new MSX509.X509Certificate2(inputFile, password);
        }

        #region Protected Members.

        /// <summary>
        /// Creates a self signed X.509 certificate by usage. 
        /// </summary>
        /// <param name="usage">The certificate usage</param>
        /// <param name="name">The subject name</param>
        /// <param name="validity">The validity of the certificate</param>
        /// <param name="issuer">The certificate issuer/signer</param>
        /// <param name="cipher">Optional cipher used, defaults to RSA 2048</param>
        /// <param name="signature">Optional signature algorithm used, defaults to SHA256</param>
        /// <returns>An instance of <c>X509Certificate2</c></returns>
        public static MSX509.X509Certificate2 CreateCertificate(Usage usage, X509Name name, Validity validity, MSX509.X509Certificate2 issuer = null, ICipher cipher = null, Signature signature = null)
        {
            cipher = cipher ?? Cipher.Rsa(KeySize.Bit2048);
            signature = signature ?? Signature.Sha256WithRsa;
            var serialNumber = BigIntegers.CreateRandomInRange(
                BigInteger.One,
                BigInteger.ValueOf(long.MaxValue),
                cipher.Random);
            var generator = cipher.CreateNew();
            var keys = generator.GenerateKeyPair();
            var signer = issuer == null ? keys : DotNetUtilities.GetKeyPair(issuer.PrivateKey);
            var issuerSerialNumber = issuer == null ? serialNumber : new BigInteger(issuer.GetSerialNumber());
            var issuerName = issuer == null ? name : issuer.GetSubject();
            var builder = new X509V3CertificateGenerator();
            builder.SetSerialNumber(serialNumber);
            builder.SetSubjectDN(name);
            builder.SetIssuerDN(issuerName);
            builder.SetPublicKey(keys.Public);
            builder.SetNotBefore(validity.NotBefore);
            builder.SetNotAfter(validity.NotAfter);
            builder.SetSignatureAlgorithm(signature.Algorithm);
            builder.AddExtensions(usage);
            builder.AddAuthorityKeyIdentifier(issuerName, signer, issuerSerialNumber);
            //// Sign the certificate with the issuers private key.
            var certificate = builder.Generate(signer.Private, cipher.Random);
            //// Convert from Bouncy Castle to X509Certificate2
            DotNetUtilities.ToX509Certificate(certificate);
            var store = new Pkcs12StoreBuilder().Build();
            store.SetKeyEntry(certificate.FriendlyName(), new AsymmetricKeyEntry(keys.Private), new[] { new X509CertificateEntry(certificate) });
            using (var stream = new MemoryStream())
            {
                //// Temporary set a random password. This will be overwritten when written to disk.
                var password = Password.Random();
                store.Save(stream, password.ToCharArray(), cipher.Random);
                var bytes = stream.ToArray();
                return new MSX509.X509Certificate2(bytes, password, MSX509.X509KeyStorageFlags.Exportable | MSX509.X509KeyStorageFlags.PersistKeySet);
            }
        }

        #endregion
    }
}