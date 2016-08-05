// <copyright file="SynchronizeLdapHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class SynchronizeLdapHandler : RequestHandler<SynchronizeLdap, GetSynchronizedAccount>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ILdapService"/>.
        /// </summary>
        private readonly ILdapService ldapService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizeLdapHandler"/> class.
        /// </summary>
        public SynchronizeLdapHandler(IAccountService accountService, ILdapService ldapService)
        {
            this.accountService = accountService;
            this.ldapService    = ldapService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override GetSynchronizedAccount Handle(SynchronizeLdap message)
        {
            this.ldapService.SynchronizeLdapAccount(message.Id);

            return new GetSynchronizedAccount { Id = message.Id };
        }

        #endregion
    }
}