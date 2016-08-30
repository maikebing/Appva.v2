// <copyright file="CreateDelegationCategoryModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateDelegationCategoryModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The name
        /// </summary>
        [DisplayName("Namn")]
        public string Name
        {
            get;
            set;
        }

        #endregion
    }
}