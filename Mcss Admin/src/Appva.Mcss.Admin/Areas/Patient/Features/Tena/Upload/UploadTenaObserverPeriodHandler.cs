// <copyright file="UploadTenaObserverPeriodHandler.cs" company="Appva AB">
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

    internal sealed class UploadTenaObserverPeriodHandler : RequestHandler<UploadTenaObserverPeriod, UploadTenaObserverPeriodModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTenaObserverPeriodHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPatientService"/>.</param>
        /// <param name="patientService">The <see cref="ITenaService"/>.</param>
        public UploadTenaObserverPeriodHandler(IPatientService patientService, ITenaService tenaService)
        {
            this.patientService = patientService;
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UploadTenaObserverPeriodModel Handle(UploadTenaObserverPeriod message)
        {
            var patientId = message.Id;
            var periodId = message.PeriodId;

            var responsone = this.tenaService.PostDataToTena(patientId, periodId);




            throw new NotImplementedException();
        }

        #endregion
    }
}