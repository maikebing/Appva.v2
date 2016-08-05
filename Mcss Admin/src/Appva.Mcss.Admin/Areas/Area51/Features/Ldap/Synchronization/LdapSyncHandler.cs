// <copyright file="ListLdapHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Ldap;
    using Appva.Ldap.Configuration;
    using Appva.Ldap.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class LdapSyncHandler : RequestHandler<LdapSync, LdapSyncModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="ILdapService"/>.
        /// </summary>
        private readonly ILdapService ldapService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapSyncHandler"/> class.
        /// </summary>
        public LdapSyncHandler(ISettingsService settings, ILdapService ldapService, IAccountService accountService)
        {
            this.settings       = settings;
            this.ldapService    = ldapService;
            this.accountService = accountService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override LdapSyncModel Handle(LdapSync message)
        {
            if (this.settings.Find<bool>(ApplicationSettings.IsLdapConnectionEnabled))
            {
                var accounts = this.accountService.List();
                var users = this.ldapService.List();

                var notInLdap       = new List<Account>();
                var readyToSync     = new List<Tickable>();
                var notReadyForSync = new List<Account>();
                var synchronized    = new List<Account>();

                foreach (var account in accounts)
                {
                    if (account.IsSynchronized)
                    {
                        synchronized.Add(account);
                    }
                    else
                    {
                        var user = users.Where(x => x.UniqueIdentifier == account.PersonalIdentityNumber.Value).FirstOrDefault();
                        if (user != null)
                        {
                            if (ReadForSync(account, user))
                            {
                                readyToSync.Add(new Tickable { Id = account.Id, IsSelected = false, Label = account.FullName });
                            }
                            else
                            {
                                notReadyForSync.Add(account);
                            }
                        }
                        else
                        {
                            notInLdap.Add(account);
                        }
                    }
                    
                }

                return new LdapSyncModel
                {
                    NotInLdap       = notInLdap,
                    ReadyToSync     = readyToSync,
                    NotReadyForSync = notReadyForSync,
                    Synchronized    = synchronized
                };
            }
            return null;

        }

        #endregion

        #region Private members

        /// <summary>
        /// Checks if an account is safe to activate synchronization. 
        /// Eg. Compares if nessecary data matches
        /// </summary>
        /// <param name="account"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool ReadForSync(Account account, User user)
        {
            if (account.UserName.IsNotEmpty() && user.Username.IsNotEmpty() && account.UserName != user.Username)
            {
                return false;
            }
            if (user.Pin.IsNotEmpty() && account.DevicePassword != user.Pin)
            {
                return false;
            }
            
            return true;
        }

        #endregion
    }
}