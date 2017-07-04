// <copyright file="ArticleController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Patient.Features.Order
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/article")]
    public sealed class ArticleController : Controller
    {
        #region Routes.

        #region List.

        /// <summary>
        /// Returns a list of articles.
        /// </summary>
        /// <returns>A <see cref="ListArticleModel"/>.</returns>
        [Route("list")]
        [HttpGet, Dispatch]
        public ActionResult List(ListArticle request)
        {
            return this.View();
        }

        #endregion

        #region Status.

        /// <summary>
        /// Handles the article order status request.
        /// </summary>
        /// <param name="request">The <see cref="ArticleStatusModel"/>.</param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost, Dispatch("list", "article")]
        public ActionResult Status(ArticleStatusModel request)
        {
            return this.View();
        }

        #endregion

        #region Add.

        /// <summary>
        /// Add an article.
        /// </summary>
        /// <param name="request">The <see cref="Identity{AddArticleModel}"/>.</param>
        /// <returns>An <see cref="AddArticleModel"/>.</returns>
        [Route("add")]
        [HttpGet, Dispatch]
        public ActionResult Add(Identity<AddArticleModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the new article request.
        /// </summary>
        /// <param name="request">The <see cref="AddArticleModel"/>.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        [Route("add")]
        [HttpPost, Dispatch("list", "article")]
        public ActionResult Add(AddArticleModel request)
        {
            return this.View();
        }

        #endregion

        #region Edit.

        /// <summary>
        /// Returns the article edit view.
        /// </summary>
        /// <param name="request">The <see cref="EditArticle"/>.</param>
        /// <returns>A <see cref="EditArticleModel"/>.</returns>
        [Route("edit/{article:guid}")]
        [HttpGet, Dispatch]
        public ActionResult Edit(EditArticle request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the edit article request.
        /// </summary>
        /// <param name="request">The <see cref="EditArticleModel"/>.</param>
        /// <returns>An <see cref="EditArticle"/>.</returns>
        [Route("edit/{article:guid}")]
        [HttpPost, Dispatch("list", "article")]
        public ActionResult Edit(EditArticleModel request)
        {
            return this.View();
        }

        #endregion

        #region Inactivate.

        /// <summary>
        /// Inactivates an article.
        /// </summary>
        /// <param name="request">The <see cref="InactivateArticle"/>.</param>
        /// <returns>A <see cref="ListArticle"/>.</returns>
        [Route("delete/{article:guid}")]
        [HttpGet, Dispatch("list", "article")]
        public ActionResult Delete(InactivateArticle request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}