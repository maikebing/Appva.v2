// <copyright file="AddArticleCategoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddArticleCategoryHandler : RequestHandler<Parameterless<AddArticleCategoryModel>, AddArticleCategoryModel>
    {
        /// <inheritdoc />
        public override AddArticleCategoryModel Handle(Parameterless<AddArticleCategoryModel> message)
        {
            return new AddArticleCategoryModel
            {
                
            };
        }
    }
}