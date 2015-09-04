﻿// <copyright file="ResetPasswordHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Extensions;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mvc.Messaging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ResetPasswordHandler : RequestHandler<ResetPassword, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordHandler"/> class.
		/// </summary>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="settingsService">The <see cref="IIdentityService"/></param>
        /// <param name="mailService">The <see cref="IRazorMailService"/></param>
        /// <param name="context">The <see cref="HttpContextBase"/></param>
        public ResetPasswordHandler(IAccountService accountService, IIdentityService identityService)
		{
            this.accountService = accountService;
            this.identityService = identityService;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(ResetPassword message)
        {
            var id = this.identityService.PrincipalId;
            var account = this.accountService.Find(id);
            this.accountService.ChangePassword(account, message.NewPassword);
            return true;
        }

        #endregion
    }
}