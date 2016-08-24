// <copyright file="Account.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Core.Extensions;
    using Appva.Cryptography;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Account : Person<Account>
    {
        #region Private fields.

        /// <summary>
        /// The title field
        /// </summary>
        private readonly string _title;

        #endregion

        #region Properties.

        /// <summary>
        /// If the account is "paused".
        /// </summary>
        public virtual bool IsPaused
        {
            get;
            set;
        }

        /// <summary>
        /// HSA Id
        /// </summary>
        public virtual string HsaId
        {
            get;
            set;
        }

        /// <summary>
        /// The user name.
        /// </summary>
        public virtual string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Primary E-mail address.
        /// </summary>
        public virtual string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Primary telephone number.
        /// </summary>
        public virtual string TelephoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The Account Title. 
        /// Note Title is readonly and can only be set by changin account roles
        /// </summary>
        public virtual string Title
        {
            get
            {
                return _title;
            }
        }

        ///<summary>
        /// The device password.
        /// </summary>
        public virtual string DevicePassword
        {
            get;
            set;
        }

        ///<summary>
        /// The hashed password, used only when logging on to the administrative system.
        /// </summary>
        public virtual string AdminPassword
        {
            get;
            set;
        }

        ///<summary>
        /// The hashed salt.
        /// </summary>
        public virtual string Salt
        {
            get;
            set;
        }

        /// <summary>
        /// The last date the password was altered. 
        /// </summary>
        public virtual DateTime? LastPasswordChangedDate
        {
            get;
            set;
        }

        /// <summary>
        /// Last login by the user.
        /// </summary>
        public virtual DateTime? LastLoginDate
        {
            get;
            set;
        }

        /// <summary>
        /// Last activity by a user. 
        /// </summary>
        public virtual DateTime? LastActivityDate
        {
            get;
            set;
        }

        /// <summary>
        /// Counter of failed authentications.
        /// </summary>
        public virtual int FailedPasswordAttemptsCount
        {
            get;
            set;
        }

        /// <summary>
        /// Lockout by date and time.
        /// </summary>
        public virtual DateTime? LockoutUntilDate
        {
            get;
            set;
        }

        /// <summary>
        /// Password reset date. 
        /// </summary>
        public virtual DateTime? PasswordResetDate
        {
            get;
            set;
        }

        /// <summary>
        /// The taxonomy <see cref="Taxon"/>.
        /// </summary>
        public virtual Taxon Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// The roles.
        /// </summary>
        public virtual IList<Role> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// The delegations.
        /// </summary>
        public virtual IList<Delegation> Delegations
        {
            get;
            set;
        }

        /// <summary>
        /// If user should be prompted with news on next admin-logon
        /// </summary>
        public virtual bool ShowAdminNewsNotice
        {
            get;
            set;
        }

        /// <summary>
        /// The symmetric key used for signing JWT tokens.
        /// </summary>
        public virtual string SymmetricKey
        {
            get;
            set;
        }

        /// <summary>
        /// If the account is synchronized with an external directory
        /// </summary>
        public virtual bool IsSynchronized
        {
            get;
            set;
        }

        /// <summary>
        /// Last time the account was synchronized with an external directory
        /// </summary>
        public virtual DateTime? LastSynchronized
        {
            get;
            set;
        }

        #endregion

        #region Public Static Method.

        public static Account CreateForTest(Guid id)
        {
            return new Account { Id = id };
        }
        #endregion

        #region Public Methods.

        /// <summary>
        /// Adds a role to the user.
        /// </summary>
        /// <param name="role"></param>
        public virtual void addRole(Role role)
        {
            Roles.Add(role);
        }

        /// <summary>
        /// Adds a delegation to the user.
        /// </summary>
        /// <param name="delegation"></param>
        public virtual void addDelegation(Delegation delegation)
        {
            Delegations.Add(delegation);
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="password">The new password</param>
        /// <param name="salt">The new password salt</param>
        public virtual void ChangePassword(string password, string salt)
        {
            this.AdminPassword = password;
            this.Salt = salt;
            this.LastPasswordChangedDate = DateTime.Now;
            this.SymmetricKey = Hash.Random().ToBase64();
        }

        public virtual void Lockout(int minutes)
        {
            this.LockoutUntilDate = DateTime.Now.AddMinutes(minutes);
            this.FailedPasswordAttemptsCount = 0;
        }

        public virtual void IncrementFailedPasswordAttempts()
        {
            this.FailedPasswordAttemptsCount++;
        }

        public virtual void ResetFailedPasswordAttempts()
        {
            this.FailedPasswordAttemptsCount = 0;
            this.LockoutUntilDate = null;
        }

        #endregion
    }
}