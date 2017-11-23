// <copyright file="ILdapService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Core.Messaging.RazorMail;
    using Appva.Ldap;
    using Appva.Ldap.Configuration;
    using Appva.Ldap.Models;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;


    #endregion

    public interface ILdapService : IService
    {
        /// <summary>
        /// Gets a user from the LDAP directory
        /// </summary>
        /// <param name="id">The identifer</param>
        /// <returns>The <see cref="User"/></returns>
        User Find(Guid accountId);

        /// <summary>
        /// Gets a user from the LDAP directory
        /// </summary>
        /// <param name="id">The identifer</param>
        /// <returns>The <see cref="User"/></returns>
        User Find(Account account);

        /// <summary>
        /// Gets a user from the LDAP directory
        /// </summary>
        /// <param name="id">The identifer</param>
        /// <returns>The <see cref="User"/></returns>
        User Find(PersonalIdentityNumber id);

        /// <summary>
        /// Gets a user from the LDAP directory
        /// </summary>
        /// <param name="id">The identifer</param>
        /// <returns>The <see cref="User"/></returns>
        User Find(string id);

        /// <summary>
        /// Lists users in the ldap directory
        /// </summary>
        /// <returns></returns>
        IList<User> List();

        /// <summary>
        /// Synchronizes an account with the ldap directory
        /// </summary>
        /// <param name="accountId">The account id</param>
        /// <returns>If the synchronization was successfull or not</returns>
        bool SynchronizeLdapAccount(Guid accountId);

        /// <summary>
        /// Synchronizes an account with the ldap directory
        /// </summary>
        /// <param name="account">The account</param>
        /// <returns>If the synchronization was successfull or not</returns>
        bool SynchronizeLdapAccount(Account account);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class LdapService : ILdapService
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService audit;

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IRazorMailService"/>.
        /// </summary>
        private readonly IRazorMailService mailer;

        /// <summary>
        /// The <see cref="ILadpClient"/>
        /// </summary>
        private ILdapClient ldap;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ILdapService"/> class.
        /// </summary>
        public LdapService(IAccountService accountService, IAuditService audit, ISettingsService settings, IRazorMailService mailer)
        {
            this.accountService = accountService;
            this.settings       = settings;
            this.audit          = audit;
            this.mailer         = mailer;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The <see cref="ILdapClient"/>.
        /// </summary>
        private ILdapClient Ldap
        {
            get
            {
                if (this.settings.Find<bool>(ApplicationSettings.IsLdapConnectionEnabled))
                {
                    if (this.ldap == null)
                    {
                        this.ldap = new LdapClient(this.settings.Find<LdapConfiguration>(ApplicationSettings.LdapConfiguration));
                    }
                    return this.ldap;
                }
                throw new LdapNotActivatedException("The LDAP connection is disabled");
            }
        }

        #endregion

        #region ILdapService members

        /// <inheritdoc />
        public User Find(Guid accountId)
        {
            return this.Find(this.accountService.Find(accountId));
        }

        /// <inheritdoc />
        public User Find(Account account)
        {
            this.audit.Read("hämtade användare {0} från ldap-katalog", account.Id);
            return this.Find(account.PersonalIdentityNumber);
        }

        /// <inheritdoc />
        public User Find(PersonalIdentityNumber id)
        {
            return this.Ldap.Find(id.Value);
        }

        /// <inheritdoc />
        public User Find(string id)
        {
            return this.Ldap.Find(id);
        }

        /// <inheritdoc />
        public IList<User> List()
        {
            return this.Ldap.List();
        }

        /// <inheritdoc />
        public bool SynchronizeLdapAccount(Guid accountId)
        {
            var account = this.accountService.Find(accountId);
            return this.SynchronizeLdapAccount(account);
        }

        /// <inheritdoc />
        public bool SynchronizeLdapAccount(Account account)
        {
            if (account != null && account.IsSynchronized)
            {
                var user = this.Find(account.PersonalIdentityNumber);
                if (user != null)
                {
                    bool usernameUpdated;
                    bool pinUpdated;
                    account.UserName = GetStringProperty(account.UserName, user.Username, out usernameUpdated);
                    account.DevicePassword = GetStringProperty(account.DevicePassword, user.Pin, out pinUpdated);
                    account.EmailAddress = GetStringProperty(account.EmailAddress, user.Mail);
                    account.FirstName = GetStringProperty(account.FirstName, user.FirstName);
                    account.LastName = GetStringProperty(account.LastName, user.LastName);
                    account.HsaId = GetStringProperty(account.HsaId, user.HsaId);
                    account.FullName = GetStringProperty(account.FullName, string.Format("{0} {1}", user.FirstName, user.LastName));
                    account.LastSynchronized = DateTime.Now;

                    this.audit.Update("synkroniserade användare {0} med ldap-katalog", account.Id);
                    this.accountService.Update(account);

                    SendSynchronizationMail(account, usernameUpdated, pinUpdated);
                }
            }
            return false;
        }

        #endregion

        #region Private members

        /// <summary>
        /// Compares two string properties and returns the updated string
        /// </summary>
        /// <param name="account"></param>
        /// <param name="user"></param>
        /// <param name="isUpdated"></param>
        /// <returns></returns>
        private static string GetStringProperty(string account, string user, out bool isUpdated)
        {
            if (user.IsNotEmpty() && account != user)
            {
                isUpdated = true;
                return user;
            }
            isUpdated = false;
            return account;
        }

        /// <summary>
        /// Compares two string properties and returns the updated string
        /// </summary>
        /// <param name="account"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string GetStringProperty(string account, string user)
        {
            bool update;
            return GetStringProperty(account, user, out update);
        }

        /// <summary>
        /// If there is an update which the user needs to be aware of,
        /// send an email.
        /// </summary>
        /// <param name="account">The updated account</param>
        /// <param name="usernameUpdated">If the username is changed</param>
        /// <param name="pinUpdated">If the pin is changed</param>
        private void SendSynchronizationMail(Account account, bool usernameUpdated, bool pinUpdated)
        {
            var updates = new Dictionary<string, string>();
            if (pinUpdated)
            {
                updates.Add("Kod för inloggning på mobil enhet:", account.DevicePassword);
            }
            if (usernameUpdated && this.accountService.HasAnyPermissions(account, Permissions.Admin.Login.Value))
            {
                updates.Add("Användarnamn för inloggning i admin:", account.UserName);
            }
            if (updates.Count > 0)
            {
                this.mailer.Send(MailMessage.CreateNew()
                    .Template("SynchronizationEmail")
                    .Model<SynchronizationEmailData>(new SynchronizationEmailData
                    {
                        Name = account.FullName,
                        Updates = updates
                    })
                    .To(account.EmailAddress)
                    .Subject("Ditt MCSS konto har uppdaterats")
                    .Build());
            }
        }

        #endregion
    }

    public class LdapNotActivatedException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapNotActivatedException"/> class.
        /// </summary>
        public LdapNotActivatedException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapNotActivatedException"/> class.
        /// </summary>
        public LdapNotActivatedException(string message)
            :base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapNotActivatedException"/> class.
        /// </summary>
        public LdapNotActivatedException(string message, params string[] parameters)
            :base(string.Format(message, parameters))
        {

        }

        #endregion
    }
}