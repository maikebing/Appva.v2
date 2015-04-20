// <copyright file="PreviewAclHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Acl
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PreviewAclHandler : NotificationHandler<PreviewAcl>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewAclHandler"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        public PreviewAclHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region NotificationHandler<PreviewAcl> Overrides.

        /// <inheritdoc />
        public override void Handle(PreviewAcl notification)
        {
            if (this.settings.Find<bool>(ApplicationSettings.IsAccessControlInstalled, false))
            {
                return;
            }
            if (this.settings.Find<bool>(ApplicationSettings.IsAccessControlInPreviewMode, false))
            {
                return;
            }
            this.settings.Upsert(ApplicationSettings.IsAccessControlInPreviewMode, true);
        }

        #endregion
    }
}