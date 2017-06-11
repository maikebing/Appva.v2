﻿// <copyright file="UserCredentialsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Accounts.ShowCredentials
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UserCredentialsHandler : RequestHandler<Identity<UserCredentialsModel>, UserCredentialsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IAuditingService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCredentialsHandler"/> class.
        /// </summary>
        public UserCredentialsHandler(
            IAccountService accountService,
            IAuditService auditing)
        {
            this.accountService = accountService;
            this.auditing       = auditing;
        }

        #endregion

        #region RequestHandler Members.

        /// <inheritdoc />
        public override UserCredentialsModel Handle(Identity<UserCredentialsModel> message)
        {
            var account = this.accountService.Find(message.Id);
            this.auditing.Read("did read user credentials for practitioner {0}", account.Id);
            return new UserCredentialsModel { 
                Id           = account.Id,
                UserFullName = account.FullName,
                UserId       = account.PersonalIdentityNumber != null ? account.PersonalIdentityNumber.Value : string.Empty,
                Password     = account.DevicePassword
            };
        }

        #endregion
    }
}