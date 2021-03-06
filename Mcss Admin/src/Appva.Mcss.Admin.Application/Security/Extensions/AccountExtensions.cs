﻿// <copyright file="AccountExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security.Extensions
{
    #region Imports.

    using System;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class AccountExtensions
    {
        /// <summary>
        /// Returns whether or not the account is inactive.
        /// </summary>
        /// <param name="account">The current account</param>
        /// <returns>True if the account is inactive</returns>
        public static bool IsInactive(this Account account)
        {
            return !account.IsActive;
        }

        /// <summary>
        /// Returns whether or not the account is in lock out mode.
        /// </summary>
        /// <param name="account">The current account</param>
        /// <returns>True if the account is locked out</returns>
        public static bool IsLockout(this Account account)
        {
            return account.LockoutUntilDate.HasValue && account.LockoutUntilDate.Value > DateTime.Now;
        }

        /// <summary>
        /// Returns whether or not the account password is NOT equal to the password submitted.
        /// </summary>
        /// <param name="account">The current account</param>
        /// <param name="password">The password</param>
        /// <returns>True if the password is incorrect</returns>
        public static bool IsIncorrectPassword(this Account account, string password)
        {
            if (account.AdminPassword.IsEmpty() || account.Salt.IsEmpty())
            {
                return true;
            }
            return !account.AdminPassword.Equals(EncryptionUtils.Hash(password, account.Salt));
        }
    }
}