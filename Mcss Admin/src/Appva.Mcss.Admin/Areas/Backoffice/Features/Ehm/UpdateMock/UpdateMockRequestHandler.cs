﻿// <copyright file="UpdateMockRequestHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Handlers
{
    #region Imports.

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
    internal sealed class UpdateMockRequestHandler : RequestHandler<Parameterless<UpdateMockModel>, UpdateMockModel>
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
        public UpdateMockRequestHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateMockModel Handle(Parameterless<UpdateMockModel> message)
        {
            var attr = this.settings.Find<EhmMockedParameters>(ApplicationSettings.EhmMockParameters);
            return new UpdateMockModel
            {
                LegitimationCode = attr.LegitimationCode,
                PrescriberCode = attr.PrescriberCode
            };
        }

        #endregion

        
    }
}