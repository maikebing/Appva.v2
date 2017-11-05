// <copyright file="ListTenaHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region imports

    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListTenaHandler : RequestHandler<ListTena, ListTenaModel>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListTenaHandler"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// <param name="patientTransformer">The <see cref="IPatientTransformer"/>.</param>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public ListTenaHandler(IPatientService patientService, IPatientTransformer patientTransformer, ITenaService tenaService)
        {
            this.patientService     = patientService;
            this.patientTransformer = patientTransformer;
            this.tenaService        = tenaService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListTenaModel Handle(ListTena message)
        {
            var patient = this.patientService.Get(message.Id);
            if(patient.TenaId.IsEmpty())
            {
                return null;
            }

            var periods = this.tenaService.ListTenaObservationPeriod(patient.Id)
                .OrderByDescending(x => x.StartDate)
                .ToList();
            return new ListTenaModel
            {
                Patient         = this.patientTransformer.ToPatient(patient),
                Periods         = periods,
                CurrentPeriodId = message.PeriodId.GetValueOrDefault(periods.Select(x => x.Id).FirstOrDefault())

            };
        }

        #endregion
    }
}