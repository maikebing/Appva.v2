// <copyright file="ListTenaHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Persistence;
    using Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListTenaHandler : RequestHandler<Parameterless<ListTenaModel>, ListTenaModel>
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
        /// Initializes a new instance of the <see cref="ListTenaHandler"/> class.
        /// </summary>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/>.</param>
        public ListTenaHandler(ISettingsService settingsService, IPersistenceContext persistenceContext)
        {
            this.settingsService = settingsService;
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region RequestHandlers Overrides.

        /// <inheritdoc />
        public override ListTenaModel Handle(Parameterless<ListTenaModel> message)
        {
            var tenaSettings = this.settingsService.Find(ApplicationSettings.TenaSettings);
            this.settingsService.Upsert(ApplicationSettings.TenaSettings, Domain.VO.TenaConfiguration.CreateNew(null, null, true));

            return new ListTenaModel
            {
                IsInstalled = tenaSettings.IsInstalled
            };
        }

        #endregion
    }
}