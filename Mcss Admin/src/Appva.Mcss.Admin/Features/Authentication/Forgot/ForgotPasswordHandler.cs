// <copyright file="ForgotPasswordHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mvc.Messaging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ForgotPasswordHandler : RequestHandler<ForgotPassword, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IRazorMailService"/>.
        /// </summary>
        private readonly IRazorMailService mailService;

        /// <summary>
        /// The <see cref="HttpContextBase"/>.
        /// </summary>
        private readonly HttpContextBase context;

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordHandler"/> class.
		/// </summary>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        /// <param name="mailService">The <see cref="IRazorMailService"/></param>
        /// <param name="context">The <see cref="HttpContextBase"/></param>
        public ForgotPasswordHandler(IAccountService accountService, ISettingsService settingsService, IRazorMailService mailService, HttpContextBase context)
		{
            this.accountService = accountService;
            this.settingsService = settingsService;
            this.mailService = mailService;
            this.context = context;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(ForgotPassword message)
        {
            var account = this.accountService.FindByPersonalIdentityNumber(message.PersonalIdentityNumber);
            if (account == null || account.IsPaused || account.IsInactive() || account.EmailAddress != message.Email)
            {
                return false;
            }
            if (! this.accountService.IsInRoles(account, RoleTypes.Backend) &&
                ! this.accountService.HasPermissions(account, Permissions.Admin.Login.Value))
            {
                return false;
            }
            var config = this.settingsService.ResetPasswordTokenConfiguration();
            var token = account.CreateNewResetPasswordTicket(config.Key, config.Issuer, config.Audience, DateTime.Now.Add(config.Lifetime));
            var helper = new UrlHelper(this.context.Request.RequestContext);
            var link = helper.Action("ResetPassword", "Authentication", null, this.context.Request.Url.Scheme) + "?token=" + token;
            this.mailService.Send(MailMessage.CreateNew().Template("ResetPasswordEmail").Model(new ForgotPasswordEmail
            {
                HumanName = account.FullName,
                TokenLink = link
            }).To(account.EmailAddress).Subject("Återställningslänk till Appva MCSS").Build());
            return true;
        }

        #endregion
    }
}