// <copyright file="SecurityMailerConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using Appva.Common.Domain;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SecurityMailerConfiguration : ValueObject<SecurityMailerConfiguration>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityMailerConfiguration"/> class.
        /// </summary>
        /// <param name="isSecurityEventMailEnabled">
        /// If security event mail is enabled, then any alterations on the account will
        /// be reported by an e-mail.
        /// </param>
        /// <param name="isRegistrationMailEnabled">
        /// If registration mail is enabled, then an e-mail will be sent when a new user
        /// account has been registered.
        /// </param>
        /// <param name="isResetPasswordMailEnabled">
        /// If reset password is enabled, then an e-mail will be sent when a current user
        /// account has requested for a password reset.
        /// </param>
        /// <param name="isMobileDeviceRegistrationMailEnabled">
        /// If mobile device registration mail is enabled, then an e-mail will be sent when a 
        /// new user account has been registered.
        /// </param>
        /// <param name="isMailSigningEnabled">
        /// If mail signing is enabled, then the e-mail will be signed by the certificate
        /// specified by the <c>CertificateThumbPrint</c>.
        /// </param>
        /// <param name="certificateThumbPrint">
        /// The certificate thumb print for accessing e-mail signing certificate.
        /// </param>
        [JsonConstructor]
        private SecurityMailerConfiguration(
            bool isSecurityEventMailEnabled, 
            bool isRegistrationMailEnabled, 
            bool isResetPasswordMailEnabled,
            bool isMobileDeviceRegistrationMailEnabled,
            bool isMailSigningEnabled,
            string certificateThumbPrint)
        {
            this.IsSecurityEventMailEnabled            = isSecurityEventMailEnabled;
            this.IsRegistrationMailEnabled             = isRegistrationMailEnabled;
            this.IsResetPasswordMailEnabled            = isResetPasswordMailEnabled;
            this.IsMobileDeviceRegistrationMailEnabled = isMobileDeviceRegistrationMailEnabled;
            this.IsMailSigningEnabled                  = isMailSigningEnabled;
            this.CertificateThumbPrint                 = certificateThumbPrint;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// If security event mail is enabled, then any alterations on the account will
        /// be reported by an e-mail.
        /// </summary>
        [JsonProperty]
        public bool IsSecurityEventMailEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// If registration mail is enabled, then an e-mail will be sent when a new user
        /// account has been registered.
        /// </summary>
        [JsonProperty]
        public bool IsRegistrationMailEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// If reset password is enabled, then an e-mail will be sent when a current user
        /// account has requested for a password reset.
        /// </summary>
        [JsonProperty]
        public bool IsResetPasswordMailEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// If mail signing is enabled, then the e-mail will be signed by the certificate
        /// specified by the <c>CertificateThumbPrint</c>.
        /// </summary>
        [JsonProperty]
        public bool IsMailSigningEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// If mobile device registration mail is enabled, then an e-mail will be sent when a 
        /// new user account has been registered.
        /// </summary>
        [JsonProperty]
        public bool IsMobileDeviceRegistrationMailEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// The certificate thumb print for accessing e-mail signing certificate.
        /// </summary>
        [JsonProperty]
        public string CertificateThumbPrint
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="SecurityMailerConfiguration"/> class.
        /// </summary>
        /// <param name="isSecurityEventMailEnabled">
        /// If security event mail is enabled, then any alterations on the account will
        /// be reported by an e-mail.
        /// </param>
        /// <param name="isRegistrationMailEnabled">
        /// If registration mail is enabled, then an e-mail will be sent when a new user
        /// account has been registered.
        /// </param>
        /// <param name="isResetPasswordMailEnabled">
        /// If reset password is enabled, then an e-mail will be sent when a current user
        /// account has requested for a password reset.
        /// </param>
        /// <param name="isMobileDeviceRegistrationMailEnabled">
        /// If mobile device registration mail is enabled, then an e-mail will be sent when a 
        /// new user account has been registered.
        /// </param>
        /// <param name="isMailSigningEnabled">
        /// If mail signing is enabled, then the e-mail will be signed by the certificate
        /// specified by the <c>CertificateThumbPrint</c>.
        /// </param>
        /// <param name="certificateThumbPrint">
        /// The certificate thumb print for accessing e-mail signing certificate.
        /// </param>
        /// <returns>A new <see cref="SecurityMailerConfiguration"/> instance</returns>
        public static SecurityMailerConfiguration CreateNew(
            bool isSecurityEventMailEnabled            = true,
            bool isRegistrationMailEnabled             = true,
            bool isResetPasswordMailEnabled            = true,
            bool isMobileDeviceRegistrationMailEnabled = true,
            bool isMailSigningEnabled                  = false,
            string certificateThumbPrint               = null)
        {
            return new SecurityMailerConfiguration(isSecurityEventMailEnabled, isRegistrationMailEnabled, isResetPasswordMailEnabled, isMobileDeviceRegistrationMailEnabled, isMailSigningEnabled, certificateThumbPrint);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return
                this.IsSecurityEventMailEnabled.GetHashCode() +
                this.IsRegistrationMailEnabled.GetHashCode() +
                this.IsResetPasswordMailEnabled.GetHashCode() +
                this.IsMobileDeviceRegistrationMailEnabled.GetHashCode() +
                this.IsMailSigningEnabled.GetHashCode() +
                this.CertificateThumbPrint == null ? 0 : this.CertificateThumbPrint.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(SecurityMailerConfiguration other)
        {
            return other != null
                && this.IsSecurityEventMailEnabled.Equals(other.IsSecurityEventMailEnabled)
                && this.IsRegistrationMailEnabled.Equals(other.IsRegistrationMailEnabled)
                && this.IsResetPasswordMailEnabled.Equals(other.IsResetPasswordMailEnabled)
                && this.IsMailSigningEnabled.Equals(other.IsMailSigningEnabled)
                && this.IsMobileDeviceRegistrationMailEnabled.Equals(other.IsMobileDeviceRegistrationMailEnabled)
                && this.CertificateThumbPrint.Equals(other.CertificateThumbPrint);
        }

        #endregion
    }
}