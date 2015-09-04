// <copyright file="Token.cs" company="Appva AB">
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
    using Appva.Common.Domain;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class Token : ValueObject<Token>
    {
        #region Variables.

        /// <summary>
        /// The "iss" (issuer) claim identifies the principal that issued the JWT. The 
        /// processing of this claim is generally application specific. The "iss" value is a 
        /// case-sensitive string containing a StringOrURI value. Use of this claim is 
        /// optional.
        /// </summary>
        [JsonProperty]
        private readonly string issuer;

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
        private readonly string audience;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="issuer">The issuer</param>
        /// <param name="audience">The audience</param>
        protected Token(string issuer, string audience)
        {
            this.issuer = issuer;
            this.audience = audience;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the issuer.
        /// </summary>
        [JsonIgnore]
        public string Issuer
        {
            get
            {
                return this.issuer;
            }
        }

        /// <summary>
        /// Returns the audience.
        /// </summary>
        [JsonIgnore]
        public string Audience
        {
            get
            {
                return this.issuer;
            }
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.issuer.GetHashCode() + this.audience.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(Token other)
        {
            return other != null
                && this.issuer.Equals(other.Issuer)
                && audience.Equals(other.Audience);
        }

        #endregion
    }
}