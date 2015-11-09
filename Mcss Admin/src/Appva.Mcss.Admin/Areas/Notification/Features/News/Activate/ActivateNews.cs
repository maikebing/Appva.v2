// <copyright file="ActivateNews.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ActivateNews : IRequest<ListNotifications>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivateNews"/> class.
        /// </summary>
        public ActivateNews()
        {
        }

        #endregion
    }
}