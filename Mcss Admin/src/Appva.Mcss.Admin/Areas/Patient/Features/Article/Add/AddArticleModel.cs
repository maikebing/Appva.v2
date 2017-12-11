// <copyright file="AddArticleModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AddArticleModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The patient id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The article name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The article description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The selected category id.
        /// </summary>
        public string SelectedCategory
        {
            get;
            set;
        }

        /// <summary>
        /// A list of article categories.
        /// </summary>
        public IEnumerable<SelectListItem> Categories
        {
            get;
            set;
        }

        #endregion  
    }
}