// <copyright file="UpdateCategoryHandler.cs" company="Appva AB">
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
    internal sealed class UpdateCategoryHandler : RequestHandler<Identity<UpdateCategoryModel>, UpdateCategoryModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCategoryHandler"/> class.
        /// </summary>
        public UpdateCategoryHandler(ISequenceService sequenceService)
        {
            this.sequenceService = sequenceService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override UpdateCategoryModel Handle(Identity<UpdateCategoryModel> message)
        {
            var category = this.sequenceService.Category(message.Id);

            return new UpdateCategoryModel
            {
                Id      = category.Id,
                Absence = category.Absence,
                Color   = category.Color,
                Name    = category.Name,
                DeviationMessage        = category.ConfirmDevitationMessage,
                NurseConfirmDeviation   = category.NurseConfirmDeviation
            };
        }

        #endregion
    }
}