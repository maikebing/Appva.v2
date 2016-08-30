// <copyright file="DeleteDelegationSettingsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteDelegationSettingsHandler : RequestHandler<Parameterless<DeleteDelegationSettingsModel>, DeleteDelegationSettingsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDelegationSettingsHandler"/> class.
        /// </summary>
        public DeleteDelegationSettingsHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override DeleteDelegationSettingsModel Handle(Parameterless<DeleteDelegationSettingsModel> message)
        {
            return new DeleteDelegationSettingsModel
            {
                SpecifyReasonIsActive = this.settings.Find<bool>(ApplicationSettings.SpecifyReasonOnDelegationRemoval),
                Reasons = string.Join("\n", this.settings.Find<List<string>>(ApplicationSettings.DelegationRemovalReasons))
            };
        }

        #endregion
    }
}