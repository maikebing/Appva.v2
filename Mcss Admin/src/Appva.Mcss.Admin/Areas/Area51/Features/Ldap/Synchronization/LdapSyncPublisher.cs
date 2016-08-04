// <copyright file="LdapSyncPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Ldap.Synchronization
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class LdapSyncPublisher : RequestHandler<LdapSyncModel, LdapSync>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ILdapService"/>
        /// </summary>
        private readonly ILdapService ldapService;
        
        /// <summary>
        /// The <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapSyncPublisher"/> class.
        /// </summary>
        public LdapSyncPublisher(ILdapService ldapService, IAccountService accountService)
        {
            this.ldapService    = ldapService;
            this.accountService = accountService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override LdapSync Handle(LdapSyncModel message)
        {
            foreach (var a in message.ReadyToSync.Where(x => x.IsSelected))
            {
                var account = this.accountService.Find(a.Id);
                account.IsSynchronized = true;
                this.ldapService.SynchronizeLdapAccount(account);
            }
            return new LdapSync();
        }

        #endregion
    }
}