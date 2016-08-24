// <copyright file="LdapConfigPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Ldap.Configuration;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class LdapConfigPublisher : RequestHandler<LdapConfigModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapConfigPublisher"/> class.
        /// </summary>
        public LdapConfigPublisher(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override bool Handle(LdapConfigModel message)
        {
            var config = LdapConfiguration.CreateNew(
                message.LdapConnectionString,
                message.LdapUsername,
                message.LdapPassword,
                message.UsernameField,
                message.PinField,
                message.MailField,
                message.HsaField,
                message.UniqueIdentiferField,
                message.FirstNameField,
                message.LastNameField
                );

            this.settings.Upsert<LdapConfiguration>(ApplicationSettings.LdapConfiguration, config as LdapConfiguration);
            this.settings.Upsert<bool>(ApplicationSettings.IsLdapConnectionEnabled, message.LdapConnectionIsEnabled);

            return true;
        }

        #endregion
    }
}