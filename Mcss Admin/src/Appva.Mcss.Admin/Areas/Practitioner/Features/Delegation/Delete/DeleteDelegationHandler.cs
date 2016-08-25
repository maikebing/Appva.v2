// <copyright file="DeleteDelegationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteDelegationHandler : RequestHandler<Identity<DeleteDelegationModel>, DeleteDelegationModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDelegationHandler"/> class.
        /// </summary>
        public DeleteDelegationHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override DeleteDelegationModel Handle(Identity<DeleteDelegationModel> message)
        {
            return new DeleteDelegationModel
            {
                DelegationId = message.Id,
                Reasons = this.settings.Find<bool>(ApplicationSettings.SpecifyReasonOnDelegationRemoval) ?
                        this.settings.Find<List<string>>(ApplicationSettings.DelegationRemovalReasons).Select(x => new SelectListItem { Text = x, Value = x }).ToList() : 
                        null
            };
        }

        #endregion
    }
}