// <copyright file="Validity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using System;
    using System.Globalization;
    using Appva.Core.Extensions;
    using Validation;

    #endregion

    /// <summary>
    /// <para>
    /// The certificate validity period is the time interval during which the CA 
    /// warrants that it will maintain information about the status of the certificate.  
    /// The field is represented as a SEQUENCE of two dates:
    /// the date on which the certificate validity period begins (notBefore) and the 
    /// date on which the certificate validity period ends (notAfter). Both notBefore 
    /// and notAfter may be encoded as UTCTime or GeneralizedTime.
    /// </para>
    /// <para>
    /// CAs conforming to this profile MUST always encode certificate validity dates 
    /// through the year 2049 as UTCTime; certificate validity dates in 2050 or later 
    /// MUST be encoded as GeneralizedTime. 
    /// Conforming applications MUST be able to process validity dates that are encoded 
    /// in either UTCTime or GeneralizedTime.
    /// </para>
    /// <para>
    /// The validity period for a certificate is the period of time from notBefore 
    /// through  notAfter, inclusive.
    /// </para>
    /// <para>
    /// In some situations, devices are given certificates for which no good expiration 
    /// date can be assigned.  For example, a device could be issued a certificate that 
    /// binds its model and serial number to its public key; such a certificate is 
    /// intended to be used for the entire lifetime of the device.
    /// </para>
    /// <para>
    /// To indicate that a certificate has no well-defined expiration date, the notAfter 
    /// SHOULD be assigned the GeneralizedTime value of 99991231235959Z.
    /// </para>
    /// <para>
    /// When the issuer will not be able to maintain status information until the 
    /// notAfter  date (including when the notAfter date is 99991231235959Z), the issuer 
    /// MUST ensure that no valid certification path exists for the certificate after 
    /// maintenance of status information is terminated.  This may be accomplished by 
    /// expiration or revocation of all CA certificates containing the public key used 
    /// to verify the signature on the certificate and discontinuing use of the public 
    /// key used to verify the signature on the certificate as a trust anchor.
    /// </para>
    /// <externalLink>
    ///     <linkText>Validity</linkText>
    ///     <linkUri>http://tools.ietf.org/html/rfc5280#section-4.1.2.5</linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class Validity
    {
        #region Variables.

        /// <summary>
        /// The date on which the certificate validity period begins.
        /// </summary>
        /// <remarks>May be encoded as UTCTime or GeneralizedTime</remarks>
        private readonly DateTime notBefore;

        /// <summary>
        /// The date on which the certificate validity period ends.
        /// </summary>
        /// <remarks>May be encoded as UTCTime or GeneralizedTime</remarks>
        private readonly DateTime notAfter;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Validity"/> class.
        /// </summary>
        /// <param name="notBefore">The date on which the certificate validity period begins</param>
        /// <param name="notAfter">The date on which the certificate validity period ends</param>
        /// <param name="defaultValidity">Default validity in years for notAfter</param>
        internal Validity(DateTime? notBefore, DateTime? notAfter, int defaultValidity)
        {
            this.notBefore = notBefore.HasValue ? notBefore.Value : DateTime.UtcNow;
            this.notAfter = notAfter.HasValue ? notAfter.Value : DateTime.UtcNow.AddYears(defaultValidity);
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Gets the not before.
        /// </summary>
        public DateTime NotBefore
        {
            get
            {
                return this.notBefore;
            }
        }

        /// <summary>
        /// Gets the not after.
        /// </summary>
        public DateTime NotAfter
        {
            get
            {
                return this.notAfter;
            }
        }

        #endregion
    }
}
