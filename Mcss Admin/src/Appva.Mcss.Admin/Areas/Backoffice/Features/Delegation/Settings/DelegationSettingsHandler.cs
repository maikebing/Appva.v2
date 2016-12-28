// <copyright file="DelegationSettingsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:your@email.address">Your name</a></author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DelegationSettingsHandler : RequestHandler<Parameterless<DelegationSettingsModel>, DelegationSettingsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationSettingsHandler"/> class.
        /// </summary>
        public DelegationSettingsHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override DelegationSettingsModel Handle(Parameterless<DelegationSettingsModel> message)
        {
            return new DelegationSettingsModel
            {
                SpecifyReasonIsActive       = this.settings.Find<bool>(ApplicationSettings.SpecifyReasonOnDelegationRemoval),
                Reasons                     = string.Join("\n", this.settings.Find<List<string>>(ApplicationSettings.DelegationRemovalReasons)),
                RequireActivationOnChange   = this.settings.Find<bool>(ApplicationSettings.RequireDelegationActivationAfterChange)
            };
        }

        #endregion
    }
}