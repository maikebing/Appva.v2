// <copyright file="UpdateAdministrationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;

    #endregion

    /// <summary>
    /// The update administration handler.
    /// </summary>
    public class UpdateAdministrationHandler : RequestHandler<UpdateAdministration, UpdateAdministrationModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAdministrationHandler"/> class.
        /// </summary>
        /// <param name="settingsService">The<see cref="ISettingsService"/>.</param>
        public UpdateAdministrationHandler(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override UpdateAdministrationModel Handle(UpdateAdministration message)
        {
            var administration = this.settingsService.Find<List<AdministrationValueModel>>(ApplicationSettings.AdministrationUnitsWithAmounts)
                .SingleOrDefault(x => x.Id == message.Id);
            return UpdateAdministrationModel.New(administration);
        }

        #endregion
    }
}