// <copyright file="CreateAccountPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
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
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Cryptography;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc.Messaging;
    using Appva.Persistence;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateAccountPublisher : RequestHandler<CreateAccountModel, bool>
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
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

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
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="roleService">The <see cref="IRoleService"/></param>
        /// <param name="taxonomies">The <see cref="ITaxonomyService"/></param>
        /// <param name="permissions">The <see cref="IPermissionService"/></param>
        /// <param name="mailService">The <see cref="IRazorMailService"/></param>
        /// <param name="auditing">The <see cref="IAuditService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        /// <param name="jwtSecureDataFormat">The <see cref="JwtSecureDataFormat"/></param>
        /// <param name="context">The <see cref="HttpContextBase"/></param>
        public CreateAccountPublisher(
            ISettingsService settings, 
            IRoleService roleService, 
            ITaxonomyService taxonomies,
            IPermissionService permissions,
            IRazorMailService mailService,
            IAuditService auditing,
            IPersistenceContext persistence,
            JwtSecureDataFormat jwtSecureDataFormat,
            HttpContextBase context)
        {
            this.settings    = settings;
            this.roleService = roleService;
            this.taxonomies  = taxonomies;
            this.permissions = permissions;
            this.mailService = mailService;
            this.auditing    = auditing;
            this.persistence = persistence;
            this.jwtSecureDataFormat = jwtSecureDataFormat;
            this.context = context;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(CreateAccountModel message)
        {
            var roleId  = message.TitleRole.ToGuid();
            var taxonId = message.Taxon.IsNotEmpty() ? message.Taxon.ToGuid() : this.taxonomies.Roots(TaxonomicSchema.Organization).Single().Id;
            var roles = new List<Role>
            {
                this.roleService.Find(roleId),
                this.roleService.Find(RoleTypes.Device)
            };
            var address  = this.taxonomies.Get(taxonId);
            var password = this.settings.AutogeneratePasswordForMobileDevice() ? Password.Random(4, PasswordFormat) : message.DevicePassword;
            var account  = new Account();
            account.FirstName              = message.FirstName.Trim().FirstToUpper();
            account.LastName               = message.LastName.Trim().FirstToUpper();
            account.FullName               = string.Format("{0} {1}", account.FirstName, account.LastName);
            account.UserName               = this.CreateUserName(account.FirstName, account.LastName, this.ListAllUserNames());
            account.PersonalIdentityNumber = message.PersonalIdentityNumber;
            account.EmailAddress           = message.Email;
            account.DevicePassword         = password;
            account.Taxon                  = address;
            account.Roles                  = roles;
            account.SymmetricKey           = Hash.Random().ToBase64();
            this.persistence.Save<Account>(account);
            this.auditing.Create("skapade ett konto för {0} (REF: {1}).", account.FullName, account.Id);
            //// If siths is enabled then no need to send an e-mail.
            if (this.settings.IsSithsAuthorizationEnabled())
            {
                return true;
            }
            var token      = this.jwtSecureDataFormat.CreateNewRegistrationToken(account.Id, account.SymmetricKey);
            var helper     = new UrlHelper(this.context.Request.RequestContext);
            var tokenLink  = helper.Action("Register", "Account", new RouteValueDictionary { { "Area",  string.Empty }, { "token", token } }, this.context.Request.Url.Scheme);
            var tenantLink = helper.Action("Index", "Home", new RouteValueDictionary { { "Area", string.Empty } }, this.context.Request.Url.Scheme);
            var configuration = this.settings.MailMessagingConfiguration();
            var permissions   = this.permissions.ListByRoles(roles);
            if (configuration.IsRegistrationMailEnabled 
                && permissions.Any(x => x.Resource.Equals(Permissions.Admin.Login.Value)))
            {
                this.mailService.Send(MailMessage.CreateNew()
                    .Template("RegisterUserEmail").Model<RegistrationEmail>(new RegistrationEmail
                    {
                        Name       = account.FullName,
                        UserName   = account.UserName,
                        TokenLink  = tokenLink,
                        TenantLink = tenantLink
                    })
                    .To(account.EmailAddress).Subject("Registreringsbekräftelse: Nytt MCSS konto skapat").Build());
            }
            if (configuration.IsMobileDeviceRegistrationMailEnabled 
                && permissions.Any(x => x.Resource.Equals(Permissions.Device.Login.Value)))
            {
                this.mailService.Send(MailMessage.CreateNew()
                    .Template("RegisterUserMobileDeviceEmail").Model<RegistrationForDeviceEmail>(new RegistrationForDeviceEmail
                    {
                        Name     = account.FullName,
                        Password = account.DevicePassword
                    })
                    .To(account.EmailAddress).Subject("Lösenord: Nytt MCSS konto för mobil enhet skapat").Build());
            }
            return true;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Returns all user names.
        /// </summary>
        /// <returns>A list of user names</returns>
        private IList<string> ListAllUserNames()
        {
            return this.persistence.Session.CreateCriteria<Account>()
                .SetProjection(Projections.ProjectionList()
                .Add(Projections.Property("UserName")))
                .List<string>();
        }

        /// <summary>
        /// Creates a unique username from firstname and lastname
        /// </summary>
        /// <param name="firstname">The first name</param>
        /// <param name="lastname">The last name</param>
        /// <param name="usernames">The user name list</param>
        /// <returns>A unique user name</returns>
        private string CreateUserName(string firstname, string lastname, IList<string> usernames)
        {
            var firstPart  = firstname.ToNullSafeLower().ToUrlFriendly();
            var secondPart = lastname.ToNullSafeLower().ToUrlFriendly();
            var username   = string.Format(
                "{0}{1}",
                (firstPart.Length  > 3) ? firstPart.Substring(0, 3)  : firstPart,
                (secondPart.Length > 3) ? secondPart.Substring(0, 3) : secondPart);
            if (! usernames.Contains(username))
            {
                return username;
            }
            var counter = 1;
            while (usernames.Contains(username + counter))
            {
                counter++;
            }
            return username + counter;
        }

        #endregion
    }
}