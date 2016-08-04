// <copyright file="FindUserHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Ldap;
    using Appva.Ldap.Configuration;
    using Appva.Ldap.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class FindHandler : RequestHandler<Find, Find>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ILdapService"/>
        /// </summary>
        private readonly ILdapService ldapService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FindHandler"/> class.
        /// </summary>
        public FindHandler(ILdapService ldapService)
        {
            this.ldapService = ldapService;
        }

        #endregion

        #region RequestHandler overrides


        public override Find Handle(Find message)
        {
            if (message.UniqueIdentifier.IsNotEmpty())
            {
                message.User = this.ldapService.Find(message.UniqueIdentifier);
            }
            return message;
        }

        #endregion
    }
}