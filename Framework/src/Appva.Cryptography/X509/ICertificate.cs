// <copyright file="ICertificate.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using System;
    using System.Security.Cryptography.X509Certificates;

    #endregion

    /// <summary>
    /// The fluent certificate constraint base.
    /// </summary>
    public interface ICertificate
    {
        /// <summary>
        /// The subject field identifies the entity associated with the public key stored 
        /// in the subject public key field. The subject name MAY be carried in the 
        /// subject field and/or the subjectAltName extension.
        /// <externalLink>
        ///     <linkText>Subject</linkText>
        ///     <linkUri>http://tools.ietf.org/html/rfc5280#section-4.1.2.6</linkUri>
        /// </externalLink>
        /// </summary>
        /// <param name="subject">The subject</param>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        ICertificate Subject(string subject);

        /// <summary>
        /// Sets the date on which the certificate validity period begins. Both notBefore 
        /// and notAfter may be encoded as UTCTime or GeneralizedTime.
        /// <externalLink>
        ///     <linkText>Validity</linkText>
        ///     <linkUri>http://tools.ietf.org/html/rfc5280#section-4.1.2.5</linkUri>
        /// </externalLink>
        /// </summary>
        /// <param name="date">The date which the validity period begins</param>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        ICertificate NotBefore(DateTime date);

        /// <summary>
        /// Sets the date on which the certificate validity period ends. Both notBefore 
        /// and notAfter may be encoded as UTCTime or GeneralizedTime.
        /// <externalLink>
        ///     <linkText>Validity</linkText>
        ///     <linkUri>http://tools.ietf.org/html/rfc5280#section-4.1.2.5</linkUri>
        /// </externalLink>
        /// </summary>
        /// <param name="date">The date on which the validity period ends.</param>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        ICertificate NotAfter(DateTime date);

        /// <summary>
        /// <para>
        /// This field contains the algorithm identifier for the algorithm used by the CA to 
        /// sign the certificate.
        /// </para>
        /// <para>
        /// This field MUST contain the same algorithm identifier as the signatureAlgorithm 
        /// field in the sequence Certificate (Section 4.1.1.2). The contents of the 
        /// optional parameters field will vary according to the algorithm identified. 
        /// [RFC3279], [RFC4055], and [RFC4491] list supported signature algorithms, but other 
        /// signature algorithms MAY also be supported.
        /// </para>
        /// <externalLink>
        ///     <linkText>Signature</linkText>
        ///     <linkUri>http://tools.ietf.org/html/rfc5280#section-4.1.2.3</linkUri>
        /// </externalLink>
        /// </summary>
        /// <param name="algorithm">The algorithm to use for signing</param>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        ICertificate Signature(Signature algorithm);

        /// <summary>
        /// Sets a specific asymmetric key pair generator to use.
        /// </summary>
        /// <param name="cipher">The key pair generator to use</param>
        /// <returns><see cref="ICertificate"/></returns>
        ICertificate Use(ICipher cipher);

        /// <summary>
        /// Creates the X.509 certificate.
        /// </summary>
        /// <param name="password">The password for the generated pfx, null if nothing</param>
        /// <returns>An instance of <see cref="X509Certificate2"/></returns>
        X509Certificate2 Create(string password = null);
    }
}
