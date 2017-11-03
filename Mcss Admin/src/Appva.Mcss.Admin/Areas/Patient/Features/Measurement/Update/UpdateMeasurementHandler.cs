﻿// <copyright file="UpdateMeasurementHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports

    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Application.Common;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Application.Models;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateMeasurementHandler : RequestHandler<UpdateMeasurement, UpdateMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The MeasurementService
        /// </summary>
        private readonly IMeasurementService service;

        /// <summary>
        /// The SettingsService
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The DelegationService
        /// </summary>
        private readonly IDelegationService delegations;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMeasurementHandler"/> class.
        /// </summary>
        /// <param name="service">The Measurement Service<see cref="IMeasurementService"/>.</param>
        /// <param name="settings">The Settings Service<see cref="ISettingsService"/>.</param>
        /// <param name="delegations">The Delegation Service<see cref="IDelegationService"/>.</param>
        public UpdateMeasurementHandler(IMeasurementService service, ISettingsService settings, IDelegationService delegations)
        {
            this.service = service;
            this.settings = settings;
            this.delegations = delegations;
        }

        #endregion

        #region Members

        /// <inheritdoc />
        public override UpdateMeasurementModel Handle(UpdateMeasurement message)
        {
            var observation = this.service.Get(message.MeasurementId);
            //var scale = JsonConvert.DeserializeObject<MeasurementScaleModel>(observation.Scale);

            var model = new UpdateMeasurementModel
            {
                MeasurementId = observation.Id,
                Name = observation.Name,
                Instruction = observation.Description,
                SelectedScale = MeasurementScale.GetNameForScale((MeasurementScale.Scale) Enum.Parse(typeof(MeasurementScale.Scale), observation.Scale)),
                SelectedUnit = MeasurementScale.GetUnitForScale(observation.Scale),
                SelectDelegationList = this.service.GetDelegationsList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return model;
        }

        #endregion
    }
}