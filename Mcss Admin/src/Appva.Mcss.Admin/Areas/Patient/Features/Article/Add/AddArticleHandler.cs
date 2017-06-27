// <copyright file="AddArticleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AddArticleHandler : RequestHandler<Identity<AddArticleModel>, AddArticleModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository articleRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddArticleHandler"/> class.
        /// </summary>
        /// <param name="articleRepository">The <see cref="IArticleRepository"/>.</param>
        public AddArticleHandler(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override AddArticleModel Handle(Identity<AddArticleModel> message)
        {
            var categories = this.articleRepository.GetCategories();
            var categoryList = new List<SelectListItem>();

            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }

            return new AddArticleModel
            {
                Categories = categoryList
            };
        }

        #endregion
    }
}