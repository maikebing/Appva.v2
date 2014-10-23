// <copyright file="Token.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    //using System.IdentityModel.Protocols.WSTrust;
    //using System.ServiceModel.Security.Tokens;
    using System.Security.Cryptography;
    using System.IdentityModel.Protocols.WSTrust;
    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class Token
    {
        public static string GenerateToken(RsaKeyGenerationResult rsasKeyGenerationResult)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(rsasKeyGenerationResult.PublicAndPrivateKey);
                var jwtToken = new JwtSecurityToken(
                    issuer: "http://issuer.com",
                    audience: "http://mysite.com",
                    claims: new List<Claim>()
                    { 
                        new Claim(ClaimTypes.Name, "Andras Nemes") 
                    },
                    lifetime: new Lifetime(DateTime.UtcNow, DateTime.UtcNow.AddHours(1)),
                    signingCredentials: new SigningCredentials(
                        new RsaSecurityKey(rsa),
                            SecurityAlgorithms.RsaSha256Signature,
                            SecurityAlgorithms.Sha256Digest));
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(jwtToken);
            }
        }

        public static ClaimsPrincipal Validate(RsaKeyGenerationResult rsasKeyGenerationResult, string token)
        {
            JwtSecurityToken tokenReceived = new JwtSecurityToken(token);
            RSACryptoServiceProvider publicOnly = new RSACryptoServiceProvider();
            publicOnly.FromXmlString(rsasKeyGenerationResult.PublicKeyOnly);
            TokenValidationParameters validationParameters = new System.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidIssuer = "http://issuer.com",
                AllowedAudience = "http://mysite.com",
                SigningToken = new RsaSecurityToken(publicOnly)
            };

            JwtSecurityTokenHandler recipientTokenHandler = new JwtSecurityTokenHandler();
            return recipientTokenHandler.ValidateToken(tokenReceived, validationParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static RsaKeyGenerationResult GenerateRsaKeys()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                var publicKey = rsa.ExportParameters(true);
                return new RsaKeyGenerationResult()
                {
                    PublicAndPrivateKey = rsa.ToXmlString(true),
                    PublicKeyOnly = rsa.ToXmlString(false)
                };
            }
        }
        
    }

    public class RsaKeyGenerationResult
    {
        public string PublicKeyOnly { get; set; }
        public string PublicAndPrivateKey { get; set; }
    }
}