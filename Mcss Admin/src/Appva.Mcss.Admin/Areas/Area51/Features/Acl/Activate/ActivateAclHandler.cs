// <copyright file="ActivateHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ActivateAclHandler : NotificationHandler<ActivateAcl>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateAclHandler"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        public ActivateAclHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region NotificationHandler<Activate> Overrides.

        /// <inheritdoc />
        public override void Handle(ActivateAcl notification)
        {
            if (settings.Find<bool>(ApplicationSettings.IsAccessControlInstalled, false))
            {
                return;
            }
            this.settings.Upsert(ApplicationSettings.IsAccessControlActivated, true);
        }

        #endregion
    }
}