// <copyright file="SecurityTokenConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using System;
    using Appva.Common.Domain;
    using Appva.Core.Extensions;
    using Appva.Cryptography;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SecurityTokenConfiguration : ValueObject<SecurityTokenConfiguration>
    {
        #region Variables.

        /// <summary>
        /// The default registration token life time.
        /// </summary>
        /// <remarks>Defaults to 42 days (6 weeks)</remarks>
        private static readonly TimeSpan DefaultRegistrationTokenLifetime = TimeSpan.FromDays(42);

        /// <summary>
        /// The default reset token life time.
        /// </summary>
        /// <remarks>Defaults to 60 minutes</remarks>
        private static readonly TimeSpan DefaultResetTokenLifetime = TimeSpan.FromMinutes(60);

        #endregion
        
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityTokenConfiguration"/> class.
        /// </summary>
        /// <param name="issuer">The issuer</param>
        /// <param name="audience">The audience</param>
        /// <param name="registrationTokenLifetime">The registration token life time</param>
        /// <param name="resetTokenLifetime">The reset token life time</param>
        [JsonConstructor]
        private SecurityTokenConfiguration(string issuer, string audience, TimeSpan registrationTokenLifetime, TimeSpan resetTokenLifetime)
        {
            this.SigningKey = Hash.Random().ToBase64();
            this.Issuer = issuer;
            this.Audience = audience;
            this.RegistrationTokenLifetime = registrationTokenLifetime;
            this.ResetTokenLifetime = resetTokenLifetime;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The partial symmetric signing key.
        /// </summary>
        [JsonProperty]
        public string SigningKey
        {
            get;
            private set;
        }

        /// <summary>
        /// The "iss" (issuer) claim identifies the principal that issued the JWT. The 
        /// processing of this claim is generally application specific. The "iss" value is a 
        /// case-sensitive string containing a StringOrURI value. Use of this claim is 
        /// optional.
        /// </summary>
        [JsonProperty]
        public string Issuer
        {
            get;
            private set;
        }

        /// <summary>
        /// The "aud" (audience) claim identifies the recipients that the JWT is intended 
        /// for. Each principal intended to process the JWT MUST identify itself with a 
        /// value in the audience claim.  If the principal processing the claim does not 
        /// identify itself with a value in the "aud" claim when this claim is present, then 
        /// the JWT MUST be rejected.  In the general case, the "aud" value is an array of 
        /// case-sensitive strings, each containing a StringOrURI value.  In the special 
        /// case when the JWT has one audience, the "aud" value MAY be a single 
        /// case-sensitive string containing a StringOrURI value.  The interpretation of 
        /// audience values is generally application specific. Use of this claim is optional.
        /// </summary>
        [JsonProperty]
        public string Audience
        {
            get;
            private set;
        }

        /// <summary>
        /// The user registration token life time.
        /// </summary>
        [JsonProperty]
        public TimeSpan RegistrationTokenLifetime
        {
            get;
            private set;
        }

        /// <summary>
        /// The reset token life time.
        /// </summary>
        [JsonProperty]
        public TimeSpan ResetTokenLifetime
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="SecurityTokenConfiguration"/> class.
        /// </summary>
        /// <param name="issuer">The issuer</param>
        /// <param name="audience">The audience</param>
        /// <param name="registrationTokenLifetime">The registration token life time</param>
        /// <param name="resetTokenLifetime">The reset token life time</param>
        /// <returns>A new <see cref="SecurityTokenConfiguration"/> instance</returns>
        public static SecurityTokenConfiguration CreateNew(string issuer, string audience, TimeSpan registrationTokenLifetime, TimeSpan resetTokenLifetime)
        {
            return new SecurityTokenConfiguration(issuer, audience, registrationTokenLifetime, resetTokenLifetime);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SecurityTokenConfiguration"/> class.
        /// </summary>
        /// <param name="issuer">The issuer</param>
        /// <param name="audience">The audience</param>
        /// <returns>A new <see cref="SecurityTokenConfiguration"/> instance</returns>
        public static SecurityTokenConfiguration CreateNew(string issuer, string audience)
        {
            return CreateNew(issuer, audience, DefaultRegistrationTokenLifetime, DefaultResetTokenLifetime);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return
                this.SigningKey.GetHashCode() +
                this.Issuer.GetHashCode() +
                this.Audience.GetHashCode() +
                this.RegistrationTokenLifetime.GetHashCode() +
                this.ResetTokenLifetime.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(SecurityTokenConfiguration other)
        {
            return other != null
                && this.SigningKey.Equals(other.SigningKey)
                && this.Issuer.Equals(other.Issuer)
                && this.Audience.Equals(other.Audience)
                && this.RegistrationTokenLifetime.Equals(other.RegistrationTokenLifetime)
                && this.ResetTokenLifetime.Equals(other.ResetTokenLifetime);
        }

        #endregion
    }
}