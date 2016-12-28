// <copyright file="VerifyUniqueAccountHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using NHibernate;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class VerifyUniqueAccountHandler : RequestHandler<VerifyUniqueAccount, bool>
    {
        /// <summary>
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountService accounts;

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyUniqueAccountHandler"/> class.
        /// </summary>
        public VerifyUniqueAccountHandler(IAccountService accounts)
        {
            this.accounts = accounts;
        }

        #endregion

        #region RequestHandler overrides.

        public override bool Handle(VerifyUniqueAccount message)
        {
            try
            {
                var account = this.accounts.FindByPersonalIdentityNumber(message.PersonalIdentityNumber);
                if (!message.Id.HasValue || account == null)
                {
                    return account == null;
                }
                return account.Id.Equals(message.Id.Value);
            }
            catch (NonUniqueResultException e)
            {
                //// Todo: Log and notify techs about exception
                return false;
            }
               
        }

        #endregion
    }
}