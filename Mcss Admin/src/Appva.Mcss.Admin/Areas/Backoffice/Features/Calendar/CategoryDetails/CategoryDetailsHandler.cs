// <copyright file="CategoryDetailsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CategoryDetailsHandler : RequestHandler<Identity<CategoryDetailsModel>, CategoryDetailsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryDetailsHandler"/> class.
        /// </summary>
        public CategoryDetailsHandler(ISequenceService sequenceService)
        {
            this.sequenceService = sequenceService;            
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override CategoryDetailsModel Handle(Identity<CategoryDetailsModel> message)
        {
            var category = sequenceService.Category(message.Id);

            return new CategoryDetailsModel
            {
                Name = category.Name,
                Absence = category.Absence,
                Color = category.Color,
                Id = category.Id,
                StatusTaxons = category.StatusTaxons,
                NurseConfirmDeviation = category.NurseConfirmDeviation,
                DeviationMessage = category.ConfirmDevitationMessage
            };
        }

        #endregion
    }
}