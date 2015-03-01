// <copyright file="SelfSignedCertificate.cs" company="Appva AB">
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

    //// Interface constraints in order to make the fluent interface follow
    //// explicitly.

    #region Constraints.

    /// <summary>
    /// The self signed certificate constraint base.
    /// </summary>
    public interface ISelfSignedCertificate
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
        ISelfSignedCertificate Subject(string subject);

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
        ISelfSignedCertificate NotBefore(DateTime date);

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
        ISelfSignedCertificate NotAfter(DateTime date);

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
        /// <param name="signature">The algorithm to use for signing</param>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        ISelfSignedCertificate Signature(Signature signature);

        /// <summary>
        /// Sets a specific asymmetric key pair generator to use.
        /// </summary>
        /// <param name="cipher">The key pair generator to use</param>
        /// <returns><see cref="ISelfSignedCertificate"/></returns>
        ISelfSignedCertificate Use(ICipher cipher);

        /// <summary>
        /// Creates a new self signed X.509 certificate and returns the 
        /// <see cref="X509Certificate2"/> instance.
        /// </summary>
        /// <returns>An instance of <see cref="X509Certificate2"/></returns>
        X509Certificate2 CreateNew();

        /// <summary>
        /// Creates a new self signed X.509 certificate and Writes to disk as a .pfx file.
        /// </summary>
        /// <param name="outputFile">The output file, e.g. C:\cert.pfx</param>
        /// <param name="password">The .pfx password, defaults to null - no password</param>
        void WriteToDisk(string outputFile, string password = null);
    }

    #endregion

    /// <summary>
    /// Base class for self signed certificates.
    /// </summary>
    public abstract class SelfSignedCertificate : 
        ISelfSignedCertificate
    {
        #region Properties.

        /// <summary>
        /// Gets the subject field.
        /// </summary>
        protected string Subject
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the date on which the certificate validity period begins.
        /// </summary>
        protected DateTime? NotBefore
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the date on which the certificate validity period ends.
        /// </summary>
        protected DateTime? NotAfter
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the algorithm identifier for the algorithm used by the CA to 
        /// sign the certificate.
        /// </summary>
        protected Signature Signature
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the certificate asymmetric cipher generator.
        /// </summary>
        protected ICipher Cipher
        {
            get;
            private set;
        }

        #endregion

        #region ISelfSignedCertificate Members.

        /// <inheritdoc />
        ISelfSignedCertificate ISelfSignedCertificate.Subject(string subject)
        {
            this.Subject = subject.Contains("=") ? subject : "CN=" + subject;
            return this;
        }

        /// <inheritdoc />
        ISelfSignedCertificate ISelfSignedCertificate.NotBefore(DateTime date)
        {
            this.NotBefore = date;
            return this;
        }

        /// <inheritdoc />
        ISelfSignedCertificate ISelfSignedCertificate.NotAfter(DateTime date)
        {
            this.NotAfter = date;
            return this;
        }

        /// <inheritdoc />
        ISelfSignedCertificate ISelfSignedCertificate.Signature(Signature signature)
        {
            this.Signature = signature;
            return this;
        }

        /// <inheritdoc />
        ISelfSignedCertificate ISelfSignedCertificate.Use(ICipher cipher)
        {
            this.Cipher = cipher;
            return this;
        }

        /// <inheritdoc />
        X509Certificate2 ISelfSignedCertificate.CreateNew()
        {
            return this.CreateCertificate();
        }

        /// <inheritdoc />
        void ISelfSignedCertificate.WriteToDisk(string outputFile, string password)
        {
            var certificate = this.CreateCertificate();
            certificate.WriteToDisk(outputFile, password);
        }

        #endregion

        #region Abstract Members.

        /// <summary>
        /// Creates the X.509 certificate and returns an instance
        /// of <see cref="X509Certificate2"/>
        /// </summary>
        /// <returns>An instance of <see cref="X509Certificate2"/></returns>
        protected abstract X509Certificate2 CreateCertificate();

        #endregion
    }
}