// <copyright file="ArticleTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Application.Transformers
{
    #region Imports.

    using System;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IArticleTransformer : ITransformer
    {
        /// <summary>
        /// Transforms an <see cref="Article"/> to <see cref="ArticleModel"/>.
        /// </summary>
        /// <param name="article">The <see cref="Article"/>.</param>
        /// <returns>An <see cref="ArticleModel"/>.</returns>
        ArticleModel ToArticleModel(Article article);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArticleTransformer : IArticleTransformer
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleService"/>.
        /// </summary>
        private readonly IArticleService service;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleTransformer"/> class.
        /// </summary>
        /// <param name="service">The <see cref="IArticleService"/>.</param>
        public ArticleTransformer(IArticleService service)
        {
            this.service = service;
        }

        #endregion

        #region Transformations.

        /// <inheritdoc />
        public ArticleModel ToArticleModel(Article article)
        {
            return new ArticleModel
            {
                Id = article.Id,
                Name = article.Name,
                Description = article.Description,
                Category = article.Category,
                OrderedBy = article.RefillOrderedBy,
                OrderDate = article.RefillOrderDate,
                FormattedOrderDate = article.RefillOrderDate.Value.Day == DateTime.Now.Day ? "idag" : (article.RefillOrderDate.Value.Day == DateTime.Now.Day - 1 ? "igår" : article.RefillOrderDate.Value.ToString("d MMM yyyy")),
                Status = article.Status,
                Patient = article.Patient
            };
        }

        #endregion
    }
}