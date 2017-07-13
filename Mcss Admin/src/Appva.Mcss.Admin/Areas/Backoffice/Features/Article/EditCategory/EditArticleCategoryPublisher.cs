// <copyright file="EditArticleCategoryPublisher.cs" company="Appva AB">
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
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditArticleCategoryPublisher : RequestHandler<EditArticleCategoryModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository repository;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditArticleCategoryPublisher"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IArticleRepository"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public EditArticleCategoryPublisher(IArticleRepository repository, IPersistenceContext persistence)
        {
            this.repository = repository;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(EditArticleCategoryModel message)
        {
            if (string.IsNullOrEmpty(message.Name))
            {
                return false;
            }

            var category = this.repository.GetCategory(message.Id);
            category.Name = message.Name;
            category.Description = message.Description;
            this.persistence.Update(category);

            return true;
        }

        #endregion
    }
}