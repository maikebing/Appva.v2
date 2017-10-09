// <copyright file="CreateMeasurementHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Application.Common;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateMeasurementHandler : RequestHandler<CreateMeasurement, CreateMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The service
        /// </summary>
        private readonly IMeasurementService service;

        /// <summary>
        /// The settings
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The delegations
        /// </summary>
        private readonly IDelegationService delegations;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMeasurementHandler"/> class.
        /// </summary>
        /// <param name="service">The Measurement Service<see cref="IMeasurementService"/>.</param>
        /// <param name="settings">The Settings Service<see cref="ISettingsService"/>.</param>
        /// <param name="delegations">The Delegation Service<see cref="IDelegationService"/>.</param>
        public CreateMeasurementHandler(IMeasurementService service, ISettingsService settings, IDelegationService delegations)
        {
            this.service = service;
            this.settings = settings;
            this.delegations = delegations;
        }

        #endregion

        #region Members

        /// <inheritdoc />
        public override CreateMeasurementModel Handle(CreateMeasurement message)
        {
            return new CreateMeasurementModel
            {
                PatientId = message.Id,
                SelectUnitList = this.settings.Find(ApplicationSettings.InventoryUnitsWithAmounts)
                .Where(x => x.Field == InventoryDefaults.Feature.measurement.ToString())
                .Select(x => new SelectListItem {
                    Text = string.Format("{0} ({1})", x.Name, x.Unit),
                    Value = x.Id.ToString()
                }),
                SelectDelegationList = this.service.GetDelegationsList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
        }

        #endregion
    }
}