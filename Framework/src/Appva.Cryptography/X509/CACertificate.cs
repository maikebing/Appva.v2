// <copyright file="CACertificate.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using System.Security.Cryptography.X509Certificates;
    using Org.BouncyCastle.Asn1.X509;

    #endregion

    /// <summary>
    /// CA certificate generation.
    /// <para>
    /// Is equal to the makecert command:
    /// <code>
    ///     makecert.exe -n "CN=CAcert" -r -pe -a sha256 -len 2048 -cy authority 
    ///     -sv CAcert.pvk CAcert.cer
    /// </code>
    /// </para>
    /// <externalLink>
    ///     <linkText>Makecert.exe (Certificate Creation Tool)</linkText>
    ///     <linkUri>https://msdn.microsoft.com/en-us/library/bfsktky3%28v=vs.110%29.aspx</linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class CACertificate : SelfSignedCertificate
    {
        #region SelfSignedCertificate Overrides.

        /// <inheritdoc />
        protected override X509Certificate2 CreateCertificate()
        {
            return CertificateUtils.CreateCertificate(
                Usage.CertificateAuthority,
                new X509Name(this.Subject),
                new Validity(this.NotBefore, this.NotAfter, 2),
                null,
                this.Cipher,
                this.Signature);
        }

        #endregion
    }
}
