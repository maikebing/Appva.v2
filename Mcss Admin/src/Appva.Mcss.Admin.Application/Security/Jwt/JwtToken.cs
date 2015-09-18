// <copyright file="JwtToken.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Jwt
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using Appva.Core.Extensions;
    using Microsoft.Owin.Security.DataHandler.Encoder;
    using Thinktecture.IdentityModel.Tokens;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class JwtToken
    {
        /// <summary>
        /// Creates a new JWT token.
        /// </summary>
        /// <param name="base64SymmetricKey">The base64 symmetric key</param>
        /// <param name="issuer">The issuer</param>
        /// <param name="audience">The audience</param>
        /// <param name="expireAt">The token lifetime</param>
        /// <param name="claims">The list of claims</param>
        /// <returns>A new <see cref="JwtSecurityToken"/> instance</returns>
        public static JwtSecurityToken CreateNew(string base64SymmetricKey, string issuer, string audience, DateTime expireAt, IEnumerable<Claim> claims)
        {
            var symmetricKey = TextEncodings.Base64Url.Decode(base64SymmetricKey);
            return new JwtSecurityToken(
                issuer,
                audience,
                claims,
                DateTime.Now.ToUniversalTime(),
                expireAt.ToUniversalTime(),
                new HmacSigningCredentials(symmetricKey));
        }

        /// <summary>
        /// Creates a new symmetric key.
        /// </summary>
        /// <param name="tenantKey">The partial tenant key</param>
        /// <param name="userKey">The partial user key</param>
        /// <returns>A symmetric 64 bit byte array key</returns>
        public static byte[] CreateSymmetricKey(string tenantKey, string userKey)
        {
            var key1 = tenantKey.FromBase64();
            var key2 = userKey.FromBase64();
            var result = new byte[key1.Length + key2.Length];
            Buffer.BlockCopy(key1, 0, result, 0, key1.Length);
            Buffer.BlockCopy(key2, 0, result, key1.Length, key2.Length);
            return result;
        }
    }
}