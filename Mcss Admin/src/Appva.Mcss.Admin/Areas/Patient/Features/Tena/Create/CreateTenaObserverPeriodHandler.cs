// <copyright file="CreateTenaObserverPeriodHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion


    internal sealed class CreateTenaObserverPeriodHandler : RequestHandler<CreateTenaObserverPeriod, CreateTenaObserverPeriodModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTenaObserverPeriodHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        public CreateTenaObserverPeriodHandler(IPatientService patientService, ITenaService tenaService)
        {
            this.patientService = patientService;
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override CreateTenaObserverPeriodModel Handle(CreateTenaObserverPeriod message)
        {
            return new CreateTenaObserverPeriodModel
            {
                Id = message.Id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1)
            };
        }
        #endregion
    }
}