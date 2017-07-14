// <copyright file="DeleteArticleCategoryModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeleteArticleCategoryModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The article category id.
        /// </summary>
        public Guid Id { get; set; }

        #endregion
    }
}