// <copyright file="DeleteArticleCategoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// Delete an article category.
    /// </summary>
    public class DeleteArticleCategoryHandler : RequestHandler<DeleteArticleCategoryModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository repository;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private IPersistenceContext persistence;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteArticleCategoryHandler"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IArticleRepository"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public DeleteArticleCategoryHandler(IArticleRepository repository, IPersistenceContext persistence)
        {
            this.repository = repository;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(DeleteArticleCategoryModel message)
        {
            var category = this.repository.GetCategory(message.Id);

            int articleCount = this.persistence.QueryOver<Article>()
                .Where(x => x.IsActive == true)
                    .And(x => x.Category == category)
                        .RowCount();

            if (articleCount > 0)
            {
                return false;
            }

            category.IsActive = false;
            this.persistence.Update(category);

            return true;
        }

        #endregion
    }
}