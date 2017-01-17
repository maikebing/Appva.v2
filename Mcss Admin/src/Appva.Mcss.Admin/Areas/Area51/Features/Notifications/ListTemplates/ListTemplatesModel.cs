// <copyright file="ListTemplatesModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListTemplatesModel
    {
        #region Properties.

        /// <summary>
        /// The templates
        /// </summary>
        public IList<string> Templates
        {
            get;
            set;
        }

        #endregion
    }
}