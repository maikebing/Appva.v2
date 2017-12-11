// <copyright file="AddArticleCategoryModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AddArticleCategoryModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The article category name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The article category description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        #endregion
    }
}