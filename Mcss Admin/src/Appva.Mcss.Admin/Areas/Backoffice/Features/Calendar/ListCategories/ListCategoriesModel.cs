// <copyright file="ListCategoriesModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListCategoriesModel
    {
        #region Properties.

        /// <summary>
        /// The categories
        /// </summary>
        public IList<ScheduleSettings> Categories
        {
            get;
            set;
        }

        #endregion
    }
}