// <copyright file="AuthifyWtfTokenizer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths.Security
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// The WTF token generation taken from <c>AuthifyClientRest.cs</c>.
    /// </summary>
    /// <author>
    ///     Authify Team
    /// </author>
    public sealed class AuthifyWtfTokenizer : ITokenizer
    {
        #region ITokenizer Members.

        /// <inheritdoc />
        public string Generate()
        {
            var token = string.Empty;
            while (token.Length < 60)
            {
                token += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }
            return token.Substring(0, 60);
        }

        #endregion
    }
}