// <copyright file="CreateAccountPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Cryptography;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Mvc.Messaging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateAccountPublisher : RequestHandler<CreateAccountModel, bool>
    {
        #region Variables.

        /// <summary>
        /// The password format.
        /// </summary>
        private static readonly IDictionary<char[], int> PasswordFormat = new Dictionary<char[], int>
            {
                { "0123456789".ToCharArray(), 4 }
            };

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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountPublisher"/> class.
        /// </summary>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="roleService">The <see cref="IRoleService"/></param>
        /// <param name="taxonomies">The <see cref="ITaxonomyService"/></param>
        /// <param name="permissions">The <see cref="IPermissionService"/></param>
        /// <param name="mailService">The <see cref="IRazorMailService"/></param>
        /// <param name="jwtSecureDataFormat">The <see cref="JwtSecureDataFormat"/></param>
        /// <param name="context">The <see cref="HttpContextBase"/></param>
        public CreateAccountPublisher(
            IAccountService accountService,
            ISettingsService settings,
            IRoleService roleService,
            ITaxonomyService taxonomies,
            IPermissionService permissions,
            IRazorMailService mailService,
            JwtSecureDataFormat jwtSecureDataFormat,
            HttpContextBase context)
        {
            this.accountService = accountService;
            this.settings = settings;
            this.roleService = roleService;
            this.taxonomies = taxonomies;
            this.permissions = permissions;
            this.mailService = mailService;
            this.jwtSecureDataFormat = jwtSecureDataFormat;
            this.context = context;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(CreateAccountModel message)
        {
            var taxonId = message.Taxon.IsNotEmpty() ? message.Taxon.ToGuid() : this.taxonomies.Roots(TaxonomicSchema.Organization).Single().Id;
            var roles   = message.TitleRole.IsNotEmpty() ? new List<Role>
            {
                this.roleService.Find(message.TitleRole.ToGuid())
            } : new List<Role>();
            var address  = this.taxonomies.Get(taxonId);
            var account  = new Account();
            account.FirstName              = message.FirstName.Trim().FirstToUpper();
            account.LastName               = message.LastName.Trim().FirstToUpper();
            account.FullName               = string.Format("{0} {1}", account.FirstName, account.LastName);
            account.PersonalIdentityNumber = message.PersonalIdentityNumber;
            account.EmailAddress           = message.Email;
            account.DevicePassword         = message.DevicePassword;
            account.Taxon                  = address;
            account.Roles                  = roles;
            account.HsaId                  = message.HsaId;
            account.SymmetricKey           = Hash.Random().ToBase64();
            account.UserName               = this.accountService.CreateUniqueUserNameFor(account);
            var permissions = this.permissions.ListByRoles(roles);
            if (permissions.Any(x => x.Resource.Equals(Permissions.Device.Login.Value)))
            {
                account.DevicePassword = this.settings.AutogeneratePasswordForMobileDevice() ? Password.Random(4, PasswordFormat) : message.DevicePassword;
            }
            this.accountService.Save(account);
            bool isAccountUpgradedForAdminAccess;
            bool isAccountUpgradedForDeviceAccess;
            this.accountService.UpdateRoles(account, roles, out isAccountUpgradedForAdminAccess, out isAccountUpgradedForDeviceAccess);
            var configuration = this.settings.MailMessagingConfiguration();
            this.SendRegistrationMail(account, configuration, permissions);
            this.SendRegistrationMailForDevice(account, configuration, permissions);
            return true;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Sends a registration mail.
        /// </summary>
        /// <param name="account">The account to send to</param>
        /// <param name="configuration">The mailer configuration</param>
        /// <param name="permissions">The permissions</param>
        private void SendRegistrationMail(Account account, SecurityMailerConfiguration configuration, IList<Permission> permissions)
        {
            if (! configuration.IsRegistrationMailEnabled)
            {
                return;
            }
            if (! permissions.Any(x => x.Resource.Equals(Permissions.Admin.Login.Value)))
            {
                return;
            }
            //// Calculate the mcss link.
            var helper     = new UrlHelper(this.context.Request.RequestContext);
            var tenantLink = helper.Action("Index", "Home", new RouteValueDictionary { { "Area", string.Empty } }, this.context.Request.Url.Scheme);
            //// If siths is enabled then there is no need to send an e-mail.
            //// If HSA ID is set then the account MUST log in via siths, sort of...
            if (this.settings.IsSithsAuthorizationEnabled() || account.HsaId.IsNotEmpty())
            {
                this.mailService.Send(MailMessage.CreateNew()
                    .Template("RegisterSithsUserEmail")
                    .Model<RegistrationSithsEmail>(new RegistrationSithsEmail
                    {
                        Name = account.FullName,
                        TenantLink = tenantLink
                    })
                    .To(account.EmailAddress)
                    .Subject("Ny MCSS behörighet")
                    .Build());
                return;
            }
            //// Generate a token and token link.
            var token      = this.jwtSecureDataFormat.CreateNewRegistrationToken(account.Id, account.SymmetricKey);
            var tokenLink  = helper.Action("Register", "Account", new RouteValueDictionary { { "Area", string.Empty }, { "token", token } }, this.context.Request.Url.Scheme);
            this.mailService.Send(MailMessage.CreateNew()
                .Template("RegisterUserEmail")
                .Model<RegistrationEmail>(new RegistrationEmail
                    {
                        Name       = account.FullName,
                        UserName   = account.UserName,
                        TokenLink  = tokenLink,
                        TenantLink = tenantLink
                    })
                .To(account.EmailAddress)
                .Subject("Ny MCSS behörighet")
                .Build());
        }

        /// <summary>
        /// Sends a registration mail for device.
        /// </summary>
        /// <param name="account">The account to send to</param>
        /// <param name="configuration">The mailer configuration</param>
        /// <param name="permissions">The permissions</param>
        private void SendRegistrationMailForDevice(Account account, SecurityMailerConfiguration configuration, IList<Permission> permissions)
        {
            if (! configuration.IsMobileDeviceRegistrationMailEnabled)
            {
                return;
            }
            if (! permissions.Any(x => x.Resource.Equals(Permissions.Device.Login.Value)))
            {
                return;
            }
            this.mailService.Send(MailMessage.CreateNew()
                .Template("RegisterUserMobileDeviceEmail")
                .Model<RegistrationForDeviceEmail>(new RegistrationForDeviceEmail
                    {
                        Name = account.FullName,
                        Password = account.DevicePassword
                    })
                .To(account.EmailAddress)
                .Subject("Ny MCSS behörighet")
                .Build());
            
        }

        #endregion
    }
}