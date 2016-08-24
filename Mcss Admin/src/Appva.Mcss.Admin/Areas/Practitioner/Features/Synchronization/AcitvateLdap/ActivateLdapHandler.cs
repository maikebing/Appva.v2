// <copyright file="ActivateLdapHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ActivateLdapHandler : RequestHandler<ActivateLdap, GetSynchronizedAccount>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="GetSynchronizedAccount"/>
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateLdapHandler"/> class.
        /// </summary>
        public ActivateLdapHandler(IAccountService accountService, ISettingsService settings)
        {
            this.accountService = accountService;
            this.settings       = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override GetSynchronizedAccount Handle(ActivateLdap message)
        {
            if(this.settings.Find<bool>(ApplicationSettings.IsLdapConnectionEnabled))
            {
                var account = this.accountService.Find(message.Id);
                if(account != null)
                {
                    account.IsSynchronized = true;
                    this.accountService.Update(account);

                    return new GetSynchronizedAccount { Id = message.Id };
                }

                throw new ArgumentException("Could not find account with id {0}", message.Id.ToString());
            }
            throw new Exception("Couldn activate synchronization on account when ldap connection is disabled");
        }

        #endregion
    }
}