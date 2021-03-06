﻿// <copyright file="DelegationSettingsPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DelegationSettingsPublisher : RequestHandler<DelegationSettingsModel, object>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationSettingsPublisher"/> class.
        /// </summary>
        public DelegationSettingsPublisher(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides

        public override object Handle(DelegationSettingsModel message)
        {
            this.settings.Upsert<bool>(ApplicationSettings.RequireDelegationActivationAfterChange, message.RequireActivationOnChange);
            this.settings.Upsert<bool>(ApplicationSettings.SpecifyReasonOnDelegationRemoval, message.SpecifyReasonIsActive);
            this.settings.Upsert<List<string>>(
                ApplicationSettings.DelegationRemovalReasons,
                message.Reasons.Replace("\r\n", "\n").Split('\n').Where(x => x.IsNotEmpty()).ToList()
                );
            this.settings.Upsert<string>(ApplicationSettings.PrintDelegationSendToText, message.PrintBottomText);

            return null;
        }

        #endregion
    }
}