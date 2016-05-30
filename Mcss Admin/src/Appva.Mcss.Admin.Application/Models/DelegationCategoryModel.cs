// <copyright file="DelegationCategoryModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DelegationCategoryModel
    {
        #region Properties.

        /// <summary>
        /// The id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The Name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        #endregion
    }
}