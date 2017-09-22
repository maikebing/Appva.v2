// <copyright file="EditUnitsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditUnitsHandler : RequestHandler<Parameterless<EditUnitsModel>, EditUnitsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService service;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditUnitsHandler"/> class.
        /// </summary>
        /// <param name="service">The <see cref="ISettingsService"/>.</param>
        public EditUnitsHandler(ISettingsService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EditUnitsModel Handle(Parameterless<EditUnitsModel> message)
        {
            var settings = this.service.Find(ApplicationSettings.DosageConfigurationValues);
            var dosages = new List<EditUnits>();

            foreach (var item in settings.DosageScaleModelList)
            {
                var dosage = new EditUnits()
                {
                    Name = item.Name,
                    Unit = item.Unit,
                    Values = item.Values
                };

                dosages.Add(dosage);
            }

            return new EditUnitsModel
            {
                Dosages = dosages
            };
        }

        #endregion
    }
}