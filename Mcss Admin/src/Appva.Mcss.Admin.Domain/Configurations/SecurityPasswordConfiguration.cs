// <copyright file="SecurityPasswordConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SecurityPasswordConfiguration : ValueObject<SecurityPasswordConfiguration>
    {
        #region Variables.

        /// <summary>
        /// The default password expiration life time.
        /// </summary>
        /// <remarks>Defaults to 90 days</remarks>
        private static readonly TimeSpan DefaultPasswordExpirationLifetime = TimeSpan.FromDays(90);

        /// <summary>
        /// The default password expiration enabled/disabled status.
        /// </summary>
        /// <remarks>Defaults to disabled (false)</remarks>
        private static readonly bool DefaultIsPasswordExpirationEnabled = false;

        #endregion
        
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityPasswordConfiguration"/> class.
        /// </summary>
        /// <param name="isPasswordExpirationEnabled">Whether or not password expiration is enabled or not</param>
        /// <param name="passwordExpirationLifetime">The password expiration life time in days</param>
        [JsonConstructor]
        private SecurityPasswordConfiguration(bool isPasswordExpirationEnabled, TimeSpan passwordExpirationLifetime)
        {
            this.IsPasswordExpirationEnabled = isPasswordExpirationEnabled;
            this.PasswordExpirationLifetime = passwordExpirationLifetime;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not password expiration is enabled or not.
        /// </summary>
        [JsonProperty]
        public bool IsPasswordExpirationEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// The password expiration life time in days.
        /// </summary>
        [JsonProperty]
        public TimeSpan PasswordExpirationLifetime
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityPasswordConfiguration"/> class.
        /// </summary>
        /// <param name="isPasswordExpirationEnabled">Whether or not password expiration is enabled or not</param>
        /// <param name="passwordExpirationLifetime">The password expiration life time in days</param>
        /// <returns>A new <see cref="SecurityPasswordConfiguration"/> instance</returns>
        public static SecurityPasswordConfiguration New(bool isPasswordExpirationEnabled, TimeSpan passwordExpirationLifetime)
        {
            return new SecurityPasswordConfiguration(isPasswordExpirationEnabled, passwordExpirationLifetime);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SecurityPasswordConfiguration"/> class.
        /// </summary>
        /// <returns>A new <see cref="SecurityPasswordConfiguration"/> instance</returns>
        public static SecurityPasswordConfiguration NewDefault()
        {
            return New(DefaultIsPasswordExpirationEnabled, DefaultPasswordExpirationLifetime);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.IsPasswordExpirationEnabled;
            yield return this.PasswordExpirationLifetime;
        }

        #endregion
    }
}