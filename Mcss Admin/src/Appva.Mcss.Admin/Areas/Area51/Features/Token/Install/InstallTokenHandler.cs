// <copyright file="InstallTokenHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InstallTokenHandler : NotificationHandler<InstallToken>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenantService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallTokenHandler"/> class.
        /// </summary>
        /// <param name="tenantService">The <see cref="ITenantService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        public InstallTokenHandler(ITenantService tenantService, ISettingsService settings)
        {
            this.tenantService = tenantService;
            this.settings = settings;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override void Handle(InstallToken notification)
        {
            if (this.settings.IsSecurityTokenConfigurationInstalled())
            {
                return;
            }
            this.CreateTokenConfiguration();
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Created the token configuration.
        /// </summary>
        private void CreateTokenConfiguration()
        {
            ITenantIdentity identity;
            if (this.tenantService.TryIdentifyTenant(out identity))
            {
                var issuer   = string.Format("https://schemas.appva.se/jwt/issuer/{0}/{1}/{2}", identity.Id, Guid.NewGuid(), DateTime.Now.Ticks);
                var audience = string.Format("https://schemas.appva.se/jwt/audience/{0}", identity.Id);
                this.settings.Upsert(ApplicationSettings.TokenConfiguration, SecurityTokenConfiguration.CreateNew(issuer, audience));
            }
        }

        #endregion
    }
}