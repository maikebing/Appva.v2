// <copyright file="InactivateArticleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InactivateArticleHandler : RequestHandler<InactivateArticle, InactivateArticleModel>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateArticleHandler"/> class.
        /// </summary>
        public InactivateArticleHandler()
        {

        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override InactivateArticleModel Handle(InactivateArticle message)
        {
            return new InactivateArticleModel
            {
                Id = message.Id,
                Article = message.Article
            };
        }

        #endregion
    }
}