// <copyright file="CertificateTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Cryptography
{
    #region Import.

    using System.Security.Cryptography;
    using Appva.Cryptography;
    using Appva.Cryptography.HashAlgoritms;
    using Appva.Cryptography.X509;
    using Xunit;
    using Xunit.Extensions;

    #endregion

    /// <summary>
    /// Certificate tests.
    /// If testing to write to disk then:
    /// 1.  Create folder to write to, with proper permissions.
    /// 2.  After creating CA certificate, then install it in Trusted with exportable 
    ///     private key (important)
    /// 3.  Then run client certificates tests.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    public class CertificateTests
    {
        #region Tests.

        #region RSA.

        /// <summary>
        /// Creates a new X.509 root CA certificate with default RSA.
        /// </summary>
        [Fact]
        public void Certificate_RSA_CACertificateIsCreated_IsTrue()
        {
            var subject = "CA_RSA_TEST";
            var ca = Certificate.CertificateAuthority().Subject(subject).CreateNew();
            Assert.Equal("CN=" + subject, ca.Subject);
        }

        /// <summary>
        /// Creates a new X.509 client certificate with default RSA.
        /// </summary>
        [Fact]
        public void Certificate_RSA_ClientCertificateIsCreated_IsTrue()
        {
            var issuer = "CA_RSA_TEST";
            var subject = "CLIENTCERT_RSA_TEST";
            var ca = Certificate.CertificateAuthority().Subject(issuer).CreateNew();
            var cc = Certificate.Client(ca).Subject(subject).CreateNew();
            Assert.Equal("CN=" + issuer, cc.Issuer);
            Assert.Equal("CN=" + subject, cc.Subject);
        }

        #endregion

        #region ECDSA.

        /// <summary>
        /// Creates a new X.509 root certificate with ECDH and ECDSA signing.
        /// </summary>
        [Fact(Skip="This throws an exception and must be fixed or removed")]
        public void Certificate_ECDSA_CACertificateIsCreated_IsTrue()
        {
            var subject = "CA_ECDH_TEST";
            var ca = Certificate.CertificateAuthority().Subject(subject)
                .Use(Cipher.Ecdh(Curve.P256)).Signature(Signature.Sha256WithEcdsa).CreateNew();
            Assert.Equal("CN=" + subject, ca.Subject);
        }

        /// <summary>
        /// Creates a new X.509 client certificate with ECDH and SHA 384 ECDSA signing.
        /// </summary>
        [Fact(Skip = "This throws an exception and must be fixed or removed")]
        public void Certificate_ECDSA_ClientCertificateIsCreated_IsTrue()
        {
            var issuer = "CA_ECDH_TEST";
            var subject = "CLIENTCERT_ECDH_TEST";
            var ca = Certificate.CertificateAuthority().Subject(issuer).CreateNew();
            var cc = Certificate.Client(ca).Subject(subject)
                .Use(Cipher.Ecdh(Curve.P256)).Signature(Signature.Sha256WithEcdsa).CreateNew();
            Assert.Equal("CN=" + issuer, cc.Issuer);
            Assert.Equal("CN=" + subject, cc.Subject);
        }
        
        #endregion

        #endregion
    }
}
