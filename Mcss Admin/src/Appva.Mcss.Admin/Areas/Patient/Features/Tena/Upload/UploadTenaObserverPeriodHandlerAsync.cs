// <copyright file="UploadTenaObserverPeriodHandlerAsync.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    using System;
    using System.Collections.Generic;
    #region Imports.

    using Appva.Mcss.Admin.Application.Services;
    using Appva.Sca.Models;
    using System.Threading.Tasks;

    #endregion

    internal sealed class UploadTenaObserverPeriodHandlerAsync
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

        internal async Task<UploadTenaObserverPeriodModel> HandleAsync(UploadTenaObserverPeriod request)
        {
            var period = this.tenaService.GetTenaObservationPeriod(request.PeriodId);
            var response = await this.tenaService.PostManualEventAsync(period);

            var title = string.Empty;
            var message = string.Empty;
            var icon = string.Empty;
            var type = string.Empty;

            if (response == null)
            {
                title = "Mätvärden saknas";
                message = "Kan inte ladda upp en tom lista till Tena Identifi.";
                icon = "alert";
                type = "warning";
            }
            else
            {
                if (response.Count > 0)
                {
                    title = "Uppladdning klar!";
                    message = "Uppladdningen till Tena Identfi lyckades";
                    icon = "check";
                    type = "";
                }
                else
                {
                    title = "Inte lyckad";
                    message = "Uppladdningen till Tena Identifi misslyckades";
                    icon = "alert";
                    type = "warning";
                }
            }

            var model = new UploadTenaObserverPeriodModel
            {
                Title = title,
                Message = message,
                Icon = icon,
                Type = type,
                Period = period
            };

            return model;
        }

        #endregion
    }
}