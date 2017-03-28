// <copyright file="InactivateLdapHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Pratitioner.Handlers
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
    internal sealed class InactivateLdapHandler : RequestHandler<InactivateLdap, GetSynchronizedAccount>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateLdapHandler"/> class.
        /// </summary>
        public InactivateLdapHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override GetSynchronizedAccount Handle(InactivateLdap message)
        {
            var account = this.accountService.Find(message.Id);
            if (account != null)
            {
                account.IsSynchronized = false;
                this.accountService.Update(account, null);

                return new GetSynchronizedAccount { Id = message.Id };
            }

            throw new ArgumentException("Could not find account with id {0}", message.Id.ToString());
        }

        #endregion
    }
}