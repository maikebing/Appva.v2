// <copyright file="UploadTenaObserverPeriodHandlerAsync.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Sca.Models;
    using System.Threading.Tasks;
    using Appva.Cqrs;

    #endregion

    internal sealed class UploadTenaObserverPeriodHandlerAsync : AsyncRequestHandler<UploadTenaObserverPeriod, UploadTenaObserverPeriodModel>
    {
        #region Fields.


        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadTenaObserverPeriodHandlerAsync"/> class.
        /// </summary>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public UploadTenaObserverPeriodHandlerAsync(ITenaService tenaService)
        {
            this.tenaService = tenaService;
        }

        #endregion

        #region AsyncRequestHandler overrides.

        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public override async Task<UploadTenaObserverPeriodModel> Handle(UploadTenaObserverPeriod message)
        {
            var response = await this.tenaService.UploadAllEventsFor(message.PeriodId);

            if (response == null)
            {
                return new UploadTenaObserverPeriodModel();
            }

            return new UploadTenaObserverPeriodModel
            {
                EventsCreated          = response.Count(x => x.ImportResult == ImportResult.Created),
                EventsUpdated          = response.Count(x => x.ImportResult == ImportResult.Updated),
                EventsWithError        = response.Count(x => x.ImportResult == ImportResult.ErrorOccured),
                EventsWthoutAssesments = response.Count(x => x.ImportResult == ImportResult.AssessmentNotFound),
            };
   

            
        }

        #endregion
    }
}