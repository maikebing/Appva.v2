// <copyright file="AccountExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Extensions
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Jwt;
    using Appva.Mcss.Admin.Domain.Entities;
    using Microsoft.Owin.Security.DataHandler.Encoder;
    using Thinktecture.IdentityModel.Tokens;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class AccountExtensions
    {
        /// <summary>
        /// Returns whether or not the account is inactive.
        /// </summary>
        /// <returns>True if the account is inactive</returns>
        public static bool IsInactive(this Account account)
        {
            return ! account.IsActive;
        }

        /// <summary>
        /// Returns whether or not the account is in lock out mode.
        /// </summary>
        /// <returns>True if the account is locked out</returns>
        public static bool IsLockout(this Account account)
        {
            return account.LockoutUntilDate.HasValue && account.LockoutUntilDate.Value > DateTime.Now;
        }

        /// <summary>
        /// Returns whether or not the account password is equal to the password submitted.
        /// </summary>
        /// <returns>True if the password is correct</returns>
        public static bool IsIncorrectPassword(this Account account, string password)
        {
            return !account.AdminPassword.Equals(EncryptionUtils.Hash(password, account.Salt));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public static string CreateNewResetPasswordTicket(this Account account, string key, string issuer, string audience, DateTime expiresAt)
        {
            var key1 = key.FromBase64();
            if (key1.Length > 32)
            {
                throw new Exception(key.Length.ToString());
            }
            var key2 = account.SymmetricKey.FromBase64();
            byte[] rv = new byte[key1.Length + key2.Length];
            System.Buffer.BlockCopy(key1, 0, rv, 0, key1.Length);
            System.Buffer.BlockCopy(key2, 0, rv, key1.Length, key2.Length);
            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(JwtToken.CreateNew(
                (rv).ToBase64(),
                issuer,
                audience,
                expiresAt,
                new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
                }));
        }
    }
}