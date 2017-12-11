// <copyright file="EditArticleCategoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditArticleCategoryHandler : RequestHandler<Identity<EditArticleCategoryModel>, EditArticleCategoryModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository repository;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="IArticleRepository"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IArticleRepository"/>.</param>
        public EditArticleCategoryHandler(IArticleRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override EditArticleCategoryModel Handle(Identity<EditArticleCategoryModel> message)
        {
            var category = this.repository.GetCategory(message.Id);

            return new EditArticleCategoryModel
            {
                Id = message.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        #endregion
    }
}