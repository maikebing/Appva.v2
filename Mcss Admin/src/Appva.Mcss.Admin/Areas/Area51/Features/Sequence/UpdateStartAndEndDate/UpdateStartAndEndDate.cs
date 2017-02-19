// <copyright file="UpdateStartAndEndDate.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Extensions;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateStartAndEndDate : RequestHandler<Identity<ListSequenceModel>, ListSequenceModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISequenceService"/>
        /// </summary>
        private readonly ISequenceService sequenceService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStartAndEndDate"/> class.
        /// </summary>
        public UpdateStartAndEndDate(ISequenceService sequenceService)
        {
            this.sequenceService = sequenceService;
        }

        #endregion

        #region RequestHandler Overrides

        /// <inheritdoc />
        public override ListSequenceModel Handle(Identity<ListSequenceModel> message)
        {
            var sequence = this.sequenceService.Find(message.Id);

            if (sequence != null && sequence.Interval == 0)
            {
                DateTime start;
                DateTime? end;
                DateTimeUtils.GetEarliestAndLatestDateFrom(sequence.Dates.Split(','), out start, out end);

                sequence.StartDate = start;
                sequence.EndDate   = end;
                sequenceService.Update(sequence);
            }

            return new ListSequenceModel();
        }

        #endregion
    }
}