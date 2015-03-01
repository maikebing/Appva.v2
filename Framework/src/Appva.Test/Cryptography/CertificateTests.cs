// <copyright file="CertificateTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Cryptography
{
    #region Import.

    using Appva.Cryptography;
    using Appva.Cryptography.X509;
    using Xunit;

    #endregion

    /// <summary>
    /// Test suite for <see cref="Certificate"/>.
    /// For tests using the currently omitted writeToDisk():
    /// <list type="number">
    ///     <item>Create a folder with appropriate write permissions</item>
    ///     <item>Create the CA certificate</item>
    ///     <item>Install the CA certificate in Trusted with exportable private key</item>
    ///     <item>Run any tests with FindBySerial or other</item>
    /// </list>
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    public class CertificateTests
    {
        #region Variables.

        /// <summary>
        /// The CA issuer.
        /// </summary>
        private const string CaIssuer = "CN=CA_TEST";

        /// <summary>
        /// The Client certificate issuer.
        /// </summary>
        private const string CcIssuer = "CN=CLIENTCERT_TEST";

        #endregion

        #region Tests.

        #region RSA.

        /// <summary>
        /// Creates a new X.509 root CA certificate with default RSA.
        /// </summary>
        [Fact]
        public void CreateRsaCaCertificate_ShouldBeCreated()
        {
            var ca = Certificate.CertificateAuthority().Subject(CaIssuer).CreateNew();
            Assert.Equal(CaIssuer, ca.Subject);
        }

        /// <summary>
        /// Creates a new X.509 client certificate with default RSA.
        /// </summary>
        [Fact]
        public void CreateRsaClientCertificate_ShouldBeCreated()
        {
            var ca = Certificate.CertificateAuthority().Subject(CaIssuer).CreateNew();
            var cc = Certificate.Client(ca).Subject(CcIssuer).CreateNew();
            Assert.Equal(CaIssuer, cc.Issuer);
            Assert.Equal(CcIssuer, cc.Subject);
        }

        #endregion

        #region ECDSA.

        /// <summary>
        /// Creates a new X.509 root certificate with ECDH and ECDSA signing.
        /// </summary>
        [Fact(Skip="This throws an exception and must be fixed or removed")]
        public void CreateEcdsaCaCertificate_ShouldBeCreated()
        {
            var ca = Certificate.CertificateAuthority().Subject(CaIssuer)
                .Use(Cipher.Ecdh(Curve.P256)).Signature(Signature.Sha256WithEcdsa).CreateNew();
            Assert.Equal(CaIssuer, ca.Subject);
        }

        /// <summary>
        /// Creates a new X.509 client certificate with ECDH and SHA 384 ECDSA signing.
        /// </summary>
        [Fact(Skip = "This throws an exception and must be fixed or removed")]
        public void CreateEcdsaClientCertificate_ShouldBeCreated()
        {
            var ca = Certificate.CertificateAuthority().Subject(CaIssuer).CreateNew();
            var cc = Certificate.Client(ca).Subject(CcIssuer)
                .Use(Cipher.Ecdh(Curve.P256)).Signature(Signature.Sha256WithEcdsa).CreateNew();
            Assert.Equal(CaIssuer, cc.Issuer);
            Assert.Equal(CcIssuer, cc.Subject);
        }
        
        #endregion

        #endregion
    }
}
