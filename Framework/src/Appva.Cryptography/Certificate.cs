// <copyright file="Certificate.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Generators;
    using Org.BouncyCastle.Crypto.Prng;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.Security;
    using Org.BouncyCastle.Utilities;
    using Org.BouncyCastle.X509;
    using X509Certificate2 = System.Security.Cryptography.X509Certificates.X509Certificate2;
    using X509KeyStorageFlags = System.Security.Cryptography.X509Certificates.X509KeyStorageFlags;
    using X509ContentType = System.Security.Cryptography.X509Certificates.X509ContentType;
    using X509Store = System.Security.Cryptography.X509Certificates.X509Store;
    using X509Certificate2Collection = System.Security.Cryptography.X509Certificates.X509Certificate2Collection;
    using X509FindType = System.Security.Cryptography.X509Certificates.X509FindType;
    using OpenFlags = System.Security.Cryptography.X509Certificates.OpenFlags;
    using StoreName = System.Security.Cryptography.X509Certificates.StoreName;
    using Appva.Cryptography.X509;
    using System.Security.Cryptography.X509Certificates;

    #endregion
    
    /// <summary>
    /// X.509 v3 Certificate helper.
    /// <externalLink>
    ///     <linkText>RFC 5280</linkText>
    ///     <linkUri>http://tools.ietf.org/html/rfc5280</linkUri>
    /// </externalLink>
    /// </summary>
    /// <example>
    /// Create CA Certificate:
    /// Certificate.CertificateAuthority().Subject("MyTrustedCARoot")
    ///     .Use(Cipher.Ecdh(Curve.P384)).Signature(Signature.Sha512WithEcdsa)
    ///     .WriteToDisk("C:\\cacert.pfx", "password")
    /// </example>
    /// <example>
    /// Create Client Certificate:
    /// Certificate.Client("MyTrustedCARoot").Subject("MyClientCertificate")
    ///     .Use(Cipher.Ecdh(Curve.P384)).Signature(Signature.Sha512WithEcdsa)
    ///     .WriteToDisk("C:\\clientcert.pfx", "password")
    /// </example>
    public static class Certificate
    {
        /// <summary>
        /// Creates a self signed CA certificate.
        /// </summary>
        /// <example>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        public static ISelfSignedCertificate CertificateAuthority()
        {
            return new CACertificate();
        }

        /// <summary>
        /// Creates a self signed server certificate signed by a CA certificate.
        /// </summary>
        /// <param name="signer">The client certificate CA signer</param>
        public static ISelfSignedCertificate Server(X509Certificate2 signer)
        {
            return new ServerCertificate(signer);
        }

        /// <summary>
        /// Creates a self signed server certificate signed by a CA certificate.
        /// </summary>
        /// <param name="subjectName">The root cert distinguished subject name, e.g. CN=CARootTest</param>
        /// <param name="location">The location of the X.509 certificate store</param>
        /// <param name="store">The name of the X.509 certificate store to open</param>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        public static ISelfSignedCertificate Server(
            string subjectName,
            StoreLocation location = StoreLocation.CurrentUser,
            StoreName store = StoreName.Root)
        {
            return new ServerCertificate(subjectName, location, store);
        }

        /// <summary>
        /// Creates a self signed client certificate signed by a CA certificate.
        /// </summary>
        /// <param name="signer">The client certificate CA signer</param>
        public static ISelfSignedCertificate Client(X509Certificate2 signer)
        {
            return new ClientCertificate(signer);
        }

        /// <summary>
        /// Creates a self signed client certificate signed by a CA certificate.
        /// </summary>
        /// <param name="subjectName">The root cert distinguished subject name, e.g. CN=CARootTest</param>
        /// <param name="location">The location of the X.509 certificate store</param>
        /// <param name="store">The name of the X.509 certificate store to open</param>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        public static ISelfSignedCertificate Client(
            string subjectName,
            StoreLocation location = StoreLocation.CurrentUser,
            StoreName store = StoreName.Root)
        {
            return new ClientCertificate(subjectName, location, store);
        }

        /// <summary>
        /// Creates a self signed code certificate signed by a CA certificate.
        /// </summary>
        /// <param name="signer">The client certificate CA signer</param>
        public static ISelfSignedCertificate Code(X509Certificate2 signer)
        {
            return new CodeCertificate(signer);
        }

        /// <summary>
        /// Creates a self signed code certificate signed by a CA certificate.
        /// </summary>
        /// <param name="subjectName">The root cert distinguished subject name, e.g. CN=CARootTest</param>
        /// <param name="location">The location of the X.509 certificate store</param>
        /// <param name="store">The name of the X.509 certificate store to open</param>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        public static ISelfSignedCertificate Code(
            string subjectName,
            StoreLocation location = StoreLocation.CurrentUser,
            StoreName store = StoreName.Root)
        {
            return new ClientCertificate(subjectName, location, store);
        }

        /// <summary>
        /// Finds a certificate by subject distinguished name.
        /// </summary>
        /// <param name="subjectDistinguishedName">The subject distinguished name to find</param>
        /// <param name="location">The location of the X.509 certificate store</param>
        /// <param name="store">The name of the X.509 certificate store to open</param>
        /// <returns>A instance of <see cref="X509Certificate2"/> or null if not found</returns>
        public static X509Certificate2 FindBySubjectDistinguishedName(
            object subjectDistinguishedName,
            StoreName store = StoreName.Root,
            StoreLocation location = StoreLocation.CurrentUser)
        {
            return CertificateUtils.LoadCertificate(
                X509FindType.FindBySubjectDistinguishedName, subjectDistinguishedName, store, location);
        }

        /// <summary>
        /// Finds a certificate by thumbprint.
        /// </summary>
        /// <param name="thumbprint">The thumbprint to find</param>
        /// <param name="location">The location of the X.509 certificate store</param>
        /// <param name="store">The name of the X.509 certificate store to open</param>
        /// <returns>A instance of <see cref="X509Certificate2"/> or null if not found</returns>
        public static X509Certificate2 FindByThumbprint(
            object thumbprint,
            StoreName store = StoreName.Root,
            StoreLocation location = StoreLocation.CurrentUser)
        {
            return CertificateUtils.LoadCertificate(
                X509FindType.FindByThumbprint, thumbprint, store, location);
        }

        /// <summary>
        /// Finds a certificate by serial number.
        /// </summary>
        /// <param name="serialNumber">The serial number to find</param>
        /// <param name="location">The location of the X.509 certificate store</param>
        /// <param name="store">The name of the X.509 certificate store to open</param>
        /// <returns>A instance of <see cref="X509Certificate2"/> or null if not found</returns>
        public static X509Certificate2 FindBySerialNumber(
            object serialNumber, 
            StoreName store = StoreName.Root, 
            StoreLocation location = StoreLocation.CurrentUser)
        {
            return CertificateUtils.LoadCertificate(
                X509FindType.FindBySerialNumber, serialNumber, store, location);
        }
    }
}
