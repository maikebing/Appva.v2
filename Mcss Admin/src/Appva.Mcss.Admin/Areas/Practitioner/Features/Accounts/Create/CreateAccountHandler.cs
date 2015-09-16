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
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomies;

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
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        public CreateAccountHandler(IRuntimeMemoryCache cache, ISettingsService settings, ITaxonomyService taxonomies, IRoleService roleService)
        {
            this.cache = cache;
            this.settings = settings;
            this.taxonomies = taxonomies;
            this.roleService = roleService;
        }

        #endregion

        #region RequestHandler overrides.

        public override CreateAccountModel Handle(Parameterless<CreateAccountModel> message)
        {
            return new CreateAccountModel 
            {
                IsHsaIdFieldVisible = this.settings.GetAdminLogin().Equals("siths"),
                IsMobileDevicePasswordEditable = this.settings.Find<bool>(ApplicationSettings.AutogeneratePasswordForMobileDevice) == false,
                IsMobileDevicePasswordFieldVisible = this.settings.Find<bool>(ApplicationSettings.AutogeneratePasswordForMobileDevice) == false,
                IsUsernameVisible = this.settings.Find<bool>(ApplicationSettings.IsUsernameVisible),
                Taxons = TaxonomyHelper.SelectList(this.taxonomies.List(TaxonomicSchema.Organization)),
                Titles = TitleHelper.SelectList(roleService.ListVisible())
            };
        }

        #endregion
    }
}