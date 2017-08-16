// <copyright file="ViewTenaMeasurementsHandler.cs" company="Appva AB">
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

    internal sealed class ViewTenaMeasurementsHandler : RequestHandler<ViewTenaMeasurements, ViewTenaMeasurementsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITenaService"/>.
        /// </summary>
        private readonly ITenaService tenaService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTenaObserverPeriodHandler"/> class.
        /// </summary>
        /// <param name="tenaService">The <see cref="ITenaService"/>.</param>
        public ViewTenaMeasurementsHandler(ITenaService tenaService)
        {
            this.tenaService = tenaService;
        }
        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ViewTenaMeasurementsModel Handle(ViewTenaMeasurements message)
        {
            return new ViewTenaMeasurementsModel
            {
                ObservationItems = this.tenaService
                    .GetTenaObservationPeriod(Guid.Parse(message.Period))
                    .TenaObservationItems
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList()
            };
        }

        #endregion


    }
}