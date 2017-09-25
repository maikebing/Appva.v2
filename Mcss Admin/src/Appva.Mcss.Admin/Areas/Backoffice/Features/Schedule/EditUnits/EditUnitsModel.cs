// <copyright file="EditUnitsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditUnitsModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// A collection of dosages.
        /// </summary>
        public List<InventoryAmountListModel> Dosages
        {
            get;
            set;
        }

        #endregion
    }
}