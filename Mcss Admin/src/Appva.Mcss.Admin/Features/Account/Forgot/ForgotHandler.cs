// <copyright file="ForgotHandler.cs" company="Appva AB">
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
    using System.Web.Routing;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Core.Messaging.RazorMail;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ForgotHandler : RequestHandler<Forgot, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="JwtSecureDataFormat"/>.
        /// </summary>
        private readonly JwtSecureDataFormat jwtSecureDataFormat;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

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
        /// Initializes a new instance of the <see cref="ForgotHandler"/> class.
		/// </summary>
        /// <param name="jwtSecureDataFormat">The <see cref="JwtSecureDataFormat"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="mailService">The <see cref="IRazorMailService"/></param>
        /// <param name="context">The <see cref="HttpContextBase"/></param>
        public ForgotHandler(JwtSecureDataFormat jwtSecureDataFormat, IAccountService accountService, IRazorMailService mailService, HttpContextBase context)
		{
            this.jwtSecureDataFormat = jwtSecureDataFormat;
            this.accountService = accountService;
            this.mailService = mailService;
            this.context = context;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(Forgot message)
        {
            var account = this.accountService.FindByPersonalIdentityNumber(message.PersonalIdentityNumber);
            if (account == null || account.IsPaused || !account.IsActive || ! account.EmailAddress.Equals(message.EmailAddress, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            if (! this.accountService.IsInRoles(account, RoleTypes.Backend) &&
                ! this.accountService.HasPermissions(account, Permissions.Admin.Login.Value))
            {
                return false;
            }
            var token  = this.jwtSecureDataFormat.CreateNewResetPasswordToken(account.Id, account.SymmetricKey);
            var helper = new UrlHelper(this.context.Request.RequestContext);
            var link   = helper.Action("Reset", "Account", new RouteValueDictionary
            {
                { "token", token }
            }, this.context.Request.Url.Scheme);
            this.mailService.Send(MailMessage.CreateNew().Template("ResetPasswordEmail").Model(new ForgotEmail
            {
                Name      = account.FullName,
                TokenLink = link
            }).To(account.EmailAddress).Subject("Återställningslänk till Appva MCSS").Build());
            return true;
        }

        #endregion
    }
}