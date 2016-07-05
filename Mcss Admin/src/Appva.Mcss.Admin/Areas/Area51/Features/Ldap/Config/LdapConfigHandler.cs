// <copyright file="LdapConfigHandler.cs" company="Appva AB">
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
    internal sealed class LdapConfigHandler : RequestHandler<LdapConfig, LdapConfigModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapConfigHandler"/> class.
        /// </summary>
        public LdapConfigHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override LdapConfigModel Handle(LdapConfig message)
        {
            var enabled = this.settings.Find<bool>(ApplicationSettings.IsLdapConnectionEnabled);
            var config = this.settings.Find<LdapConfiguration>(ApplicationSettings.LdapConfiguration);
            if (config == null)
            {
                return new LdapConfigModel();
            }
            return new LdapConfigModel
            {
                LdapConnectionIsEnabled = enabled,
                FirstNameField          = config.FieldFirstName,
                HsaField                = config.FieldHsaId,
                LastNameField           = config.FieldLastName,
                LdapConnectionString    = config.LdapString,
                LdapPassword            = config.LdapPassword,
                LdapUsername            = config.LdapUsername,
                MailField               = config.FieldMail,
                PinField                = config.FieldPin,
                UniqueIdentiferField    = config.FieldUniqueIdentifier,
                UsernameField           = config.FieldUsername
            };
        }

        #endregion
    }
}