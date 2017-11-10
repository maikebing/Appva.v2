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
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;

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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMeasurementHandler"/> class.
        /// </summary>
        /// <param name="service">The Measurement Service<see cref="IMeasurementService"/>.</param>
        public CreateMeasurementHandler(IMeasurementService service)
        {
            this.service = service;
        }

        #endregion

        #region Members

        /// <inheritdoc />
        public override CreateMeasurementModel Handle(CreateMeasurement message)
        {
            return new CreateMeasurementModel
            {
                PatientId = message.Id,
                SelectScaleList = Enum.GetValues(typeof(MeasurementScale.Scale)).Cast<MeasurementScale.Scale>().Skip(1).Select(v => new SelectListItem
                {
                    Text = MeasurementScale.GetNameForScale(v),
                    Value = v.ToString()
                }).ToList(),
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