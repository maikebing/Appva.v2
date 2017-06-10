// <copyright file="UpdateRolesFormHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Persistence;
    using Appva.Core.Messaging.RazorMail;
    using Appva.Mcss.Admin.Application.Utils.i18n;
    using Appva.Mvc.Localization;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRolesFormHandler : RequestHandler<UpdateRolesForm, ListAccount>
    {
        #region Private fields.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

        /// <summary>
        /// The <see cref="IPermissionService"/>.
        /// </summary>
        private readonly IPermissionService permissions;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IRazorMailService"/>.
        /// </summary>
        private readonly IRazorMailService mailService;

        /// <summary>
        /// The <see cref="JwtSecureDataFormat"/>.
        /// </summary>
        private readonly JwtSecureDataFormat jwtSecureDataFormat;

        /// <summary>
        /// The <see cref="HttpContextBase"/>.
        /// </summary>
        private readonly HttpContextBase context;

        /// <summary>
        /// The <see cref="IHtmlLocalizer"/>.
        /// </summary>
        private readonly IHtmlLocalizer localizer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesFormHandler"/> class.
        /// </summary>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="roleService">The <see cref="IRoleService"/></param>
        /// <param name="taxonomies">The <see cref="ITaxonomyService"/></param>
        /// <param name="permissions">The <see cref="IPermissionService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        /// <param name="mailService">The <see cref="IRazorMailService"/></param>
        /// <param name="jwtSecureDataFormat">The <see cref="JwtSecureDataFormat"/></param>
        /// <param name="context">The <see cref="HttpContextBase"/></param>
        public UpdateRolesFormHandler(
            IAccountService accountService,
            ISettingsService settings,
            IRoleService roleService,
            ITaxonomyService taxonomies,
            IPermissionService permissions,
            IPersistenceContext persistence,
            IRazorMailService mailService,
            JwtSecureDataFormat jwtSecureDataFormat,
            HttpContextBase context,
            IHtmlLocalizer localizer)
        {
            this.accountService      = accountService;
            this.settings            = settings;
            this.roleService         = roleService;
            this.taxonomies          = taxonomies;
            this.permissions         = permissions;
            this.persistence         = persistence;
            this.mailService         = mailService;
            this.jwtSecureDataFormat = jwtSecureDataFormat;
            this.context             = context;
            this.localizer           = localizer;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListAccount Handle(UpdateRolesForm message)
        {
            var account = this.accountService.Find(message.Id);
            var roles = (message.SelectedRoles != null && message.SelectedRoles.Length > 0) ? this.persistence.QueryOver<Role>()
                    .AndRestrictionOn(x => x.Id)
                    .IsIn(message.SelectedRoles.Select(x => new Guid(x)).ToArray())
                    .List() : null;
            bool isAccountUpgradedForAdminAccess;
            bool isAccountUpgradedForDeviceAccess;
            this.accountService.UpdateRoles(account, roles, out isAccountUpgradedForAdminAccess, out isAccountUpgradedForDeviceAccess);
            var configuration = this.settings.MailMessagingConfiguration();
            this.SendRegistrationMail(account, configuration, isAccountUpgradedForAdminAccess);
            this.SendRegistrationMailForDevice(account, configuration, isAccountUpgradedForDeviceAccess);
            return new ListAccount();
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Sends a registration mail.
        /// </summary>
        /// <param name="account">The account to send to</param>
        /// <param name="configuration">The mailer configuration</param>
        /// <param name="hasPermission">Whether or not the account ahs proper permissions</param>
        private void SendRegistrationMail(Account account, SecurityMailerConfiguration configuration, bool hasPermission)
        {
            if (! configuration.IsRegistrationMailEnabled)
            {
                return;
            }
            if (! hasPermission)
            {
                return;
            }
            //// Calculate the mcss link.
            var helper = new UrlHelper(this.context.Request.RequestContext);
            var tenantLink = helper.Action("Index", "Home", new RouteValueDictionary { { "Area", string.Empty } }, this.context.Request.Url.Scheme);
            //// If siths is enabled then there is no need to send an e-mail.
            //// If HSA ID is set then the account MUST log in via siths, sort of...
            if (this.settings.IsSithsAuthorizationEnabled() || account.HsaId.IsNotEmpty())
            {
                this.mailService.Send(MailMessage.CreateNew()
                    .Template(I18nUtils.GetEmailTemplatePath("RegisterSithsUserEmail"))
                    .Model<RegistrationSithsEmail>(new RegistrationSithsEmail
                    {
                        Name = account.FullName
                    })
                    .To(account.EmailAddress)
                    .Subject(this.localizer["Ny_MCSS_behörighet"].Value)
                    .Build());
                return;
            }
            //// Generate a token and token link.
            var token = this.jwtSecureDataFormat.CreateNewRegistrationToken(account.Id, account.SymmetricKey);
            var tokenLink = helper.Action("Register", "Account", new RouteValueDictionary { { "Area", string.Empty }, { "token", token } }, this.context.Request.Url.Scheme);
            this.mailService.Send(MailMessage.CreateNew()
                .Template(I18nUtils.GetEmailTemplatePath("RegisterUserEmail"))
                .Model<RegistrationEmail>(new RegistrationEmail
                {
                    Name = account.FullName,
                    UserName = account.UserName,
                    TokenLink = tokenLink,
                    TenantLink = tenantLink
                })
                .To(account.EmailAddress)
                .Subject(this.localizer["Ny_MCSS_behörighet"].Value)
                .Build());
        }

        /// <summary>
        /// Sends a registration mail for device.
        /// </summary>
        /// <param name="account">The account to send to</param>
        /// <param name="configuration">The mailer configuration</param>
        /// <param name="hasPermission">Whether or not the account ahs proper permissions</param>
        private void SendRegistrationMailForDevice(Account account, SecurityMailerConfiguration configuration, bool hasPermission)
        {
            if (! configuration.IsMobileDeviceRegistrationMailEnabled)
            {
                return;
            }
            if (! hasPermission)
            {
                return;
            }
            this.mailService.Send(MailMessage.CreateNew()
                .Template(I18nUtils.GetEmailTemplatePath("RegisterUserMobileDeviceEmail"))
                .Model<RegistrationForDeviceEmail>(new RegistrationForDeviceEmail
                {
                    Name = account.FullName,
                    Password = account.DevicePassword
                })
                .To(account.EmailAddress)
                .Subject(this.localizer["Ny_MCSS_behörighet"].Value)
                .Build());
        }

        #endregion
    }
}