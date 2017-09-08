// <copyright file="ViewTenaMeasurementsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
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
        /// Initializes a new instance of the <see cref="ViewTenaMeasurementsHandler"/> class.
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
            var period = this.tenaService.GetTenaObservationPeriod(message.PeriodId);
            //var x = period.Items.OrderByDescending(d => d.CreatedAt).ToList();
            return new ViewTenaMeasurementsModel
            {
                ObservationPeriod = period,
                ObservationItems = period.Items.OrderByDescending(d => d.CreatedAt).ToList()
            };
        }

        #endregion
    }
}