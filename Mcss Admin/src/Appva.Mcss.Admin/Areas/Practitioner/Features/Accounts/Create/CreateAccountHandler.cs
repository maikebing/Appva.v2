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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateAccountHandler : RequestHandler<Parameterless<CreateAccountModel>, CreateAccountModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ICacheService"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

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
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        /// <param name="roleService">The <see cref="IRoleService"/></param>
        public CreateAccountHandler(IRuntimeMemoryCache cache, ISettingsService settingsService, ITaxonomyService taxonomyService, IRoleService roleService)
        {
            this.cache           = cache;
            this.settingsService = settingsService;
            this.taxonomyService = taxonomyService;
            this.roleService     = roleService;
        }

        #endregion

        #region RequestHandler overrides.

        public override CreateAccountModel Handle(Parameterless<CreateAccountModel> message)
        {
            return new CreateAccountModel 
            {
                IsHsaIdFieldVisible                = this.settingsService.IsSithsAuthorizationEnabled(),
                IsMobileDevicePasswordEditable     = this.settingsService.Find<bool>(ApplicationSettings.AutogeneratePasswordForMobileDevice) == false,
                IsMobileDevicePasswordFieldVisible = this.settingsService.Find<bool>(ApplicationSettings.AutogeneratePasswordForMobileDevice) == false,
                IsUsernameVisible                  = this.settingsService.Find<bool>(ApplicationSettings.IsUsernameVisible),
                Taxons                             = TaxonomyHelper.SelectList(this.taxonomyService.List(TaxonomicSchema.Organization)),
                Titles                             = TitleHelper.SelectList(this.roleService.ListVisible())
            };
        }

        #endregion
    }
}