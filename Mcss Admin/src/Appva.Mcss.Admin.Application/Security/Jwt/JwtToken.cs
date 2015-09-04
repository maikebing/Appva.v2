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
        /// 
        /// </summary>
        /// <param name="base64SymmetricKey"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="expireAt"></param>
        /// <param name="claims"></param>
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
        /// 
        /// </summary>
        /// <param name="tenantKey"></param>
        /// <param name="userKey"></param>
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