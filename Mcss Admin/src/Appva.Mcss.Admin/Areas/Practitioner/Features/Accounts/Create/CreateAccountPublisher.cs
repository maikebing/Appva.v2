// <copyright file="CreateAccountPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Resources;
    using Appva.Cryptography;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateAccountPublisher : RequestHandler<CreateAccountModel, bool>
    {
        #region Private fields.

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accounts;

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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountPublisher"/> class.
        /// </summary>
        public CreateAccountPublisher(IAccountService accounts, ISettingsService settings, IRoleService roleService, ITaxonomyService taxonomies)
        {
            this.accounts = accounts;
            this.settings = settings;
            this.roleService = roleService;
            this.taxonomies = taxonomies;
        }

        #endregion

        #region RequestHandler overrides

        public override bool Handle(CreateAccountModel message)
        {
            var role = this.roleService.Find(new Guid(message.TitleRole));
            var roles = new List<Role> 
            { 
                this.roleService.Find(RoleTypes.Device),
                role
            };
            if (this.settings.Find<bool>(ApplicationSettings.AutogeneratePasswordForMobileDevice))
            {
                message.DevicePassword = Password.Random(4, new Dictionary<char[], int>
                {
                    { "0123456789".ToCharArray(), 4 }
                });;
            }
            if (message.Taxon.IsEmpty())
            {
                message.Taxon = taxonomies.Roots(TaxonomicSchema.Organization).SingleOrDefault().Id.ToString();
            }
            //// FIXME: Remove all this unnessecary stuff and calculate all this in the service in ONE function.
            if ((role.MachineName.IsNotEmpty() && role.MachineName.StartsWith(RoleTypes.Nurse)) || this.HasAccessToAdmin(role))
            {
                this.accounts.CreateBackendAccount(
                    message.FirstName, 
                    message.LastName,
                    message.PersonalIdentityNumber, 
                    message.Email, 
                    "abc123ABC", //// FIXME: Generate password OR better yet send out a password reset token, valid for e.g. 1-5 days.
                    this.taxonomies.Get(message.Taxon.ToGuid()), 
                    roles,
                    message.DevicePassword);
            }
            else
            {
                this.accounts.Create(
                    message.FirstName,
                    message.LastName,
                    message.PersonalIdentityNumber,
                    message.Email,
                    message.DevicePassword,
                    this.taxonomies.Get(message.Taxon.ToGuid()), 
                    roles);
            }
            return true;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private bool HasAccessToAdmin(Role role)
        {
            foreach (var permission in role.Permissions)
            {
                if (permission.Resource == Permissions.Admin.Login.Value)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}