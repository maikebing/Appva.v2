// <copyright file="ResetPasswordToken.cs" company="Appva AB">
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
    using System.Linq;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ResetPasswordToken : Token
    {
        #region Variables.

        /// <summary>
        /// The symmetric key.
        /// </summary>
        [JsonProperty]
        private readonly string key;

        /// <summary>
        /// The lifetime.
        /// </summary>
        [JsonProperty]
        private readonly TimeSpan lifetime;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordToken"/> class.
        /// </summary>
        /// <param name="key">The partial token signing key</param>
        /// <param name="issuer">The issuer</param>
        /// <param name="audience">The audience</param>
        /// <param name="lifetime">The token life time</param>
        [JsonConstructor]
        private ResetPasswordToken(string key, string issuer, string audience, TimeSpan lifetime)
            : base(issuer, audience)
        {
            this.key = key;
            this.lifetime = lifetime;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ResetPasswordToken"/> class.
        /// </summary>
        /// <param name="key">The partial token signing key</param>
        /// <param name="issuer">The issuer</param>
        /// <param name="audience">The audience</param>
        /// <param name="lifetime">The token life time</param>
        /// <returns>A new <see cref="ResetPasswordToken"/> instance</returns>
        public static ResetPasswordToken CreateNew(string key, string issuer, string audience, TimeSpan lifetime)
        {
            return new ResetPasswordToken(key, issuer, audience, lifetime);
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the issuer.
        /// </summary>
        [JsonIgnore]
        public string Key
        {
            get
            {
                return this.key;
            }
        }

        /// <summary>
        /// Returns the audience.
        /// </summary>
        [JsonIgnore]
        public TimeSpan Lifetime
        {
            get
            {
                return this.lifetime;
            }
        }

        #endregion
    }
}