// <copyright file="Token.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Sca.Models
{
    #region Imports.

    using System;

    #endregion


    /// <summary>
    /// Token.
    /// </summary>
    internal class Token
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="expires">Expires.</param>
        internal Token(string value, DateTimeOffset expires)
        {
            this.Value = value;
            this.Expires = expires;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Value.
        /// </summary>
        internal string Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Expires.
        /// </summary>
        internal DateTimeOffset Expires
        {
            get;
            private set;
        }

        /// <summary>
        /// IsValid.
        /// </summary>
        internal bool IsValid
        {
            get
            {
                return this.Expires > DateTime.UtcNow;
            }
        }

        #endregion

        #region Members

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value;
        }

        #endregion

        #region Static Members.

        /// <summary>
        /// Creates the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="expires">The expires.</param>
        /// <returns></returns>
        public static Token Create(string token, DateTimeOffset expires)
        {
            return new Token(token, expires);
        }

        #endregion
    }
}
