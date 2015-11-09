// <copyright file="ActivateNewsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ActivateNewsHandler : RequestHandler<ActivateNews, ListNotifications>
    {
        #region Field

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateNewsHandler"/> class.
        /// </summary>
        public ActivateNewsHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region Overrides

        public override ListNotifications Handle(ActivateNews message)
        {
            var accounts = this.persistence.QueryOver<Account>().List();
            foreach (var account in accounts)
            {
                account.ShowAdminNewsNotice = true;
            }

            return new ListNotifications { };
        }

        #endregion
    }
}