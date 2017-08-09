// <copyright file="UpdateTenaHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateTenaHandler : RequestHandler<ListTenaModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTenaHandler"/> class.
        /// </summary>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/>.</param>
        public UpdateTenaHandler(ISettingsService settingsService, IPersistenceContext persistenceContext)
        {
            this.settingsService = settingsService;
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region RequestHandlers overrides.

        /// <inheritdoc />
        public override bool Handle(ListTenaModel message)
        {
            var settings = this.settingsService.Find(ApplicationSettings.TenaSettings);
            var clientId = string.IsNullOrEmpty(message.ClientId) ? settings.ClientId : message.ClientId;
            var clientSecret = string.IsNullOrEmpty(message.ClientSecret) ? settings.ClientSecret : message.ClientSecret;

            this.settingsService.Upsert(ApplicationSettings.TenaSettings, Domain.VO.TenaConfiguration.CreateNew(clientId, clientSecret, settings.IsInstalled));

            return true;
        }

        #endregion
    }
}