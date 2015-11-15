﻿// <copyright file="InActivateAccountHandler.cs" company="Appva AB">
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
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class InactivateAccountHandler : RequestHandler<InactivateAccount, bool>
    {
        #region Private fields.

        /// <summary>
        /// The <see cref="IAccountService"/> implementation
        /// </summary>
        private readonly IAccountService accounts;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateAccountHandler"/> class.
        /// </summary>
        public InactivateAccountHandler(IAccountService accounts)
        {
            this.accounts = accounts;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override bool Handle(InactivateAccount message)
        {
            this.accounts.InActivate(this.accounts.Find(message.Id));
            return true;
        }

        #endregion
    }
}