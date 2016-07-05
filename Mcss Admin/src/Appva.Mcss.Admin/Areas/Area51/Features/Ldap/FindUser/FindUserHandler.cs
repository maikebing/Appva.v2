// <copyright file="FindUserHandler.cs" company="Appva AB">
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
    public sealed class FindUserHandler : RequestHandler<FindUser, User>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FindUserHandler"/> class.
        /// </summary>
        public FindUserHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides


        public override User Handle(FindUser message)
        {
            
            var client = new LdapClient(this.settings.GetLdapConfiguration());

            return client.Find(message.UniqueIdentifier);
        }

        #endregion
    }
}