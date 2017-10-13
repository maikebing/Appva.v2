// <copyright file="EhmSettingsRequestHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Handlers
{
    #region Imports.

    using Appva.Core.Environment;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Mock;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EhmSettingsRequestHandler : RequestHandler<Parameterless<EhmSettingsModel>, EhmSettingsModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EhmSettingsRequestHandler"/> class.
        /// </summary>
        public EhmSettingsRequestHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override EhmSettingsModel Handle(Parameterless<EhmSettingsModel> message)
        {
            return new EhmSettingsModel
            {
                MockedParameters = ApplicationEnvironment.Is.Production != true ? this.settings.Find<EhmMockedParameters>(ApplicationSettings.EhmMockParameters) : null,
                TenantSettings   = this.settings.Find<TenantAttributes>(ApplicationSettings.EhmTenantUserAttributes)
            };
        }

        #endregion

        
    }
}