// <copyright file="PopupNewsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PopupNewsHandler : RequestHandler<PopupNews, bool>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupNewsHandler"/> class.
        /// </summary>
        public PopupNewsHandler(IPersistenceContext persistence, IIdentityService identity)
        {
            this.persistence = persistence;
            this.identity = identity;
        }

        #endregion

        #region Overrides

        public override bool Handle(PopupNews message)
        {
            var account = this.persistence.Get<Account>(this.identity.PrincipalId);
            if (account.ShowAdminNewsNotice == true)
            {
                account.ShowAdminNewsNotice = false;
                this.persistence.Update<Account>(account);
                return true;
            }
            return false;
        }

        #endregion 
    }
}