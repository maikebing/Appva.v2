using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class TestMeasurementHandler : RequestHandler<TestMeasurement, TestMeasurementModel>
    {
        #region Variables

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
        public TestMeasurementHandler(ISettingsService settings, IMeasurementService service, IPatientTransformer patientTransformer)
        {
            this.service = service;
            this.patientTransformer = patientTransformer;
        }

        #endregion

        public override TestMeasurementModel Handle(TestMeasurement message)
        {
            if (message.MeasurementId == null)
            {
                return new TestMeasurementModel
                {
                    PatientViewModel = this.patientTransformer.ToPatient(this.service.GetPatient(message.Id))
                };
            }

            var observation = this.service.GetMeasurementObservation(message.MeasurementId);
            
            var model = new TestMeasurementModel();
            model.Id = message.Id;
            model.MeasurementId = message.MeasurementId;
            model.PatientViewModel = this.patientTransformer.ToPatient(observation.Patient);
            model.MeasurementList = this.service.GetMeasurementObservationsList(observation.Patient.Id);
            model.MeasurementValueList = this.service.GetValueList(observation.Id);
            model.MeasurementUnit = MeasurementScale.GetUnitForScale(observation.Scale);
            model.MeasurementLongScale = MeasurementScale.GetNameForScale(observation.Scale);
            model.MeasurementScale = observation.Scale;
            model.Delegation = observation.Delegation;
            model.MeasurementName = observation.Name;
            model.MeasurementInstructions = observation.Description;

            return model;
        }
    }
}

