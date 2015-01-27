// <copyright file="CertificateTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.Cryptography
{
    #region Import.

    using System.Security.Cryptography;
    using Appva.Cryptography;
    using Appva.Cryptography.HashAlgoritms;
    using Xunit;
    using Xunit.Extensions;

    #endregion

    /// <summary>
    /// Certificate tests.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    public class CertificateTests
    {
        #region Tests.

        #region Create New.

        /// <summary>
        /// Creates a new X.509 certificate.
        /// </summary>
        [Fact]
        public void Certificate_VerifyThatCertificateIsCorrectlyCreated_IsTrue()
        {
            var expected_subject = "CN=subject";
            var expected_issuer = "CN=issuer";
            var expected_has_private_key = true;
            var cert = Certificate.CreateNew("subject", "issuer");
            Assert.Equal(expected_subject, cert.Subject);
            Assert.Equal(expected_issuer, cert.Issuer);
            Assert.Equal(expected_has_private_key, cert.HasPrivateKey);
        }

        #endregion

        #endregion
    }
}
