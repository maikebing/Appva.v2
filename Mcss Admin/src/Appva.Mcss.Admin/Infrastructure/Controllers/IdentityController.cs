// <copyright file="IdentityController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal abstract class IdentityController : AbstractMediatorController
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identities;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        /// <param name="identities">The <see cref="IIdentityService"/></param>
        /// <param name="accounts">The <see cref="IAccountService"/></param>
        protected IdentityController(IMediator mediator, IIdentityService identities, IAccountService accounts)
            : base (mediator)
        {
            this.identities = identities;
            this.accounts = accounts;
        }

        #endregion

        #region Protected Methods.

        protected Account Identity()
        {
            //// TODO : Add caching here .Item
            return this.accounts.Find(this.identities.PrincipalId);
        }

        #endregion
    }
}