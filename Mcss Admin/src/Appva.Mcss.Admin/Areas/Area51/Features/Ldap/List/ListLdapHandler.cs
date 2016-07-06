// <copyright file="ListLdapHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Ldap;
    using Appva.Ldap.Configuration;
    using Appva.Ldap.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListLdapHandler : RequestHandler<ListLdap, IList<User>>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListLdapHandler"/> class.
        /// </summary>
        public ListLdapHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        public override IList<User> Handle(ListLdap message)
        {
            if (this.settings.Find<bool>(ApplicationSettings.IsLdapConnectionEnabled))
            {
                var client = new LdapClient(this.settings.Find<LdapConfiguration>(ApplicationSettings.LdapConfiguration));
                return client.List();
            }
            return null;
           
        }
    }
}