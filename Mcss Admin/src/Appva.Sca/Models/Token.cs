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
        #region Fields.

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

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        internal Token()
        {
            this.Value = string.Empty;
            this.Expires = DateTimeOffset.UtcNow.AddHours(-1);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        internal Token(string value)
        {
            this.Value = value;
            this.Expires = DateTimeOffset.UtcNow;
        }

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

        #region Members.

        /// <summary>
        /// Sets the Expires.
        /// </summary>
        /// <param name="expires">Exprires.</param>
        internal void SetExpires(DateTimeOffset expires)
        {
            this.Expires = expires;
        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        internal void SetValues(string value, DateTimeOffset expires)
        {
            this.Value = value;
            this.Expires = expires;
        }

        #endregion
    }
}
