// <copyright file="ArticleController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Article
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("article")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class ArticleController : Controller
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleController"/> class.
        /// </summary>
        public ArticleController()
        {

        }

        #endregion

        #region Routes.

        #region Categories.

        #region List.

        /// <summary>
        /// The article category list.
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("listcategory")]
        [HttpGet, Dispatch(typeof(Parameterless<ArticleCategoryListModel>))]
        public ActionResult ListCategory()
        {
            return this.View();
        }

        #endregion

        #region Add.

        /// <summary>
        /// The add article category view.
        /// </summary>
        /// <returns></returns>
        [Route("addcategory")]
        [HttpGet, Dispatch(typeof(Parameterless<AddArticleCategoryModel>))]
        public ActionResult AddCategory()
        {
            return this.View();
        }

        /// <summary>
        /// Add a new article category.
        /// </summary>
        /// <returns></returns>
        [Route("addcategory")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("listcategory", "article")]
        public ActionResult AddCategory(AddArticleCategoryModel request)
        {
            return this.View();
        }

        #endregion

        #endregion

        #endregion
    }
}