// <copyright file="ArticleController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Patient.Features.Order
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc.Security;

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
        [PermissionsAttribute(Permissions.OrderList.ReadValue)]
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
        [PermissionsAttribute(Permissions.OrderList.UpdateValue)]
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
        [PermissionsAttribute(Permissions.OrderList.CreateValue)]
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
        [PermissionsAttribute(Permissions.OrderList.CreateValue)]
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
        [PermissionsAttribute(Permissions.OrderList.EditValue)]
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
        [PermissionsAttribute(Permissions.OrderList.UpdateValue)]
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
        /// <returns>A <see cref="InactivateArticleModel"/>.</returns>
        [Route("delete/{article:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.OrderList.DeleteValue)]
        public ActionResult Inactivate(InactivateArticle request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the inactivation request.
        /// </summary>
        /// <param name="request">The <see cref="InactivateArticleModel"/>.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        [Route("delete/{article:guid}")]
        [HttpPost, Dispatch("list", "article")]
        [PermissionsAttribute(Permissions.OrderList.DeleteValue)]
        public ActionResult Delete(InactivateArticleModel request)
        {
            return this.View();
        }

        #endregion

        #region Overview widget.

        /// <summary>
        /// Returns the dashboard widget.
        /// </summary>
        /// <returns><see cref="PartialViewResult"/>.</returns>
        [Route("~/article/overview")]
        [HttpGet, Dispatch(typeof(ArticleOverview))]
        [PermissionsAttribute(Permissions.Dashboard.ReadArticleOrderValue)]
        public PartialViewResult Overview()
        {
            return this.PartialView();
        }

        #endregion

        #region OverviewUpdate.

        /// <summary>
        /// Updates selected articles from the dashboard.
        /// </summary>
        /// <param name="request">The <see cref="UpdateArticle"/>.</param>
        /// <returns><see cref="DispatchJsonResult"/>.</returns>
        [Route("~/article/overviewupdate")]
        [HttpPost, Dispatch("overview", "article")]
        [PermissionsAttribute(Permissions.OrderList.UpdateValue)]
        public DispatchJsonResult OverviewUpdate(OverviewUpdateArticle request)
        {
            return this.JsonPost();
        }

        #endregion

        #endregion
    }
}