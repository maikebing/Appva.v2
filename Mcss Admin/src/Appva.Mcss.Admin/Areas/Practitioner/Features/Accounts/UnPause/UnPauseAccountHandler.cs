﻿// <copyright file="UnPauseAccountHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Accounts.UnPause
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UnPauseAccountHandler : RequestHandler<UnPauseAccountModel, bool>
    {
        #region Private fields.

        /// <summary>
        /// The <see cref="IAccountService"/> implementation
        /// </summary>
        private readonly IAccountService accounts;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UnPauseAccountHandler"/> class.
        /// </summary>
        public UnPauseAccountHandler(IAccountService accounts)
        {
            this.accounts = accounts;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(UnPauseAccountModel message)
        {
            this.accounts.UnPause(this.accounts.Find(message.AccountId));

            return true;
        }

        #endregion
    }
}