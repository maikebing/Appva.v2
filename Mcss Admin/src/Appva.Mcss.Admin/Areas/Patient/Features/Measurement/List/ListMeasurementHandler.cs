// <copyright file="ListMeasurementHandler.cs" company="Appva AB">
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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListMeasurementHandler : RequestHandler<ListMeasurement, ListMeasurementModel>
    {
        #region Variables

        /// <summary>
        /// The settings service
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The measurement service
        /// </summary>
        private readonly IMeasurementService service;

        /// <summary>
        /// The patient transformer
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMeasurementHandler"/> class.
        /// </summary>
        /// <param name="settings">The settings service<see cref="ISettingsService"/>.</param>
        /// <param name="service">The measurement service<see cref="IMeasurementService"/>.</param>
        /// <param name="patientTransformer">The patient transformer<see cref="IPatientTransformer"/>.</param>
        public ListMeasurementHandler(ISettingsService settings, IMeasurementService service, IPatientTransformer patientTransformer)
        {
            this.settings = settings;
            this.service = service;
            this.patientTransformer = patientTransformer;
        }

        #endregion

        #region Members

        /// <inheritdoc />
        public override ListMeasurementModel Handle(ListMeasurement message)
        {
            var measurementList = this.service.GetMeasurementObservationsList(message.Id);

            return new ListMeasurementModel
            {
                Patient = this.patientTransformer.ToPatient(this.service.GetPatient(message.Id)),
                MeasurementList = measurementList
            };
        }

        #endregion
    }
}