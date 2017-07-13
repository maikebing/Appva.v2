// <copyright file="CreateAccountViewHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Caching.Providers;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Web;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateAccountHandler : RequestHandler<Parameterless<CreateAccountModel>, CreateAccountModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountViewHandler"/> class.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        /// <param name="roleService">The <see cref="IRoleService"/></param>
        public CreateAccountHandler(IIdentityService identityService, IAccountService accountService, ISettingsService settingsService, ITaxonomyService taxonomyService, IRoleService roleService)
        {
            this.identityService = identityService;
            this.accountService  = accountService;
            this.settingsService = settingsService;
            this.taxonomyService = taxonomyService;
            this.roleService     = roleService;
        }

        #endregion

        #region RequestHandler overrides.

        public override CreateAccountModel Handle(Parameterless<CreateAccountModel> message)
        {
            var id      = this.identityService.PrincipalId;
            var user    = this.accountService.Find(id);
            return new CreateAccountModel 
            {
                IsHsaIdFieldVisible                = this.settingsService.IsSithsAuthorizationEnabled() || this.settingsService.Find(ApplicationSettings.IsHsaIdVisible),
                IsMobileDevicePasswordEditable     = this.settingsService.Find<bool>(ApplicationSettings.AutogeneratePasswordForMobileDevice) == false,
                IsMobileDevicePasswordFieldVisible = this.settingsService.Find<bool>(ApplicationSettings.AutogeneratePasswordForMobileDevice) == false,
                IsUsernameVisible                  = this.settingsService.Find<bool>(ApplicationSettings.IsUsernameVisible),
                Taxons                             = TaxonomyHelper.CreateItems(user, null, this.taxonomyService.List(TaxonomicSchema.Organization)),
                Titles                             = TitleHelper.SelectList(user.GetRoleAccess())
            };
        }

        #endregion
    }
}