// <copyright file="GetSynchronizedAccountHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Ldap;
    using Appva.Ldap.Configuration;
    using Appva.Ldap.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class GetSynchronizedAccountHandler : RequestHandler<GetSynchronizedAccount, GetSynchronizedAccountModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IAccountTransformer"/>.
        /// </summary>
        private readonly IAccountTransformer accountTransformer;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion 

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSynchronizedAccountHandler"/> class.
        /// </summary>
        public GetSynchronizedAccountHandler(ISettingsService settings, IAccountService accountService, IAccountTransformer accountTransformer)
        {
            this.accountService = accountService;
            this.accountTransformer = accountTransformer;
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides
        
        /// <inheritdoc />
        public override GetSynchronizedAccountModel Handle(GetSynchronizedAccount message)
        {
            var account = this.accountService.Find(message.Id);
            if (this.settings.Find<bool>(ApplicationSettings.IsLdapConnectionEnabled))
            {
                var ldapClient = new LdapClient(this.settings.Find<LdapConfiguration>(ApplicationSettings.LdapConfiguration));
                var user = ldapClient.Find(account.PersonalIdentityNumber.Value);

                var unsyncedProperties = CheckSynchronization(account, user);

                return new GetSynchronizedAccountModel
                {
                    Account                     = this.accountTransformer.ToAccount(account),
                    LocalAccount                = account,
                    LdapUser                    = user,
                    SynchronizationAvailable    = true,
                    AccountSynchronized         = unsyncedProperties == 0,
                    SynchronizationErrorCount   = unsyncedProperties
                };
            }
            return new GetSynchronizedAccountModel
            {
                SynchronizationAvailable = false,
                Account                  = this.accountTransformer.ToAccount(account)
            };

        }

        private int CheckSynchronization(Account account, User user)
        {
            if (!account.IsSynchronized)
            {
                return -1;
            }
            var username  = user.Username.IsNotEmpty()  ? user.Username.Equals(account.UserName) : true;
            var mail      = user.Mail.IsNotEmpty()      ? user.Mail.Equals(account.EmailAddress) : true;
            var firstName = user.FirstName.IsNotEmpty() ? user.Mail.Equals(account.FirstName) : true;
            var lastName  = user.LastName.IsNotEmpty()  ? user.Mail.Equals(account.LastName) : true;
            var pin       = user.Pin.IsNotEmpty()       ? user.Pin.Equals(account.DevicePassword) : true;
            var hsa       = user.HsaId.IsNotEmpty()     ? user.HsaId.Equals(account.HsaId) : true;

            return (new bool[] {username, mail, firstName, lastName, pin, hsa}).Count(x => !x);
        }

        #endregion
    }
}