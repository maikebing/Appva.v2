// <copyright file="ArticleController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Area51.Features.Account
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc.Security;
    using Appva.Mcss.Admin.Features.Area51.ArticleOption;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("article")]
    [Permissions(Permissions.Area51.ReadValue)]
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

        #region List.

        /// <summary>
        /// The order list options.
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<ArticleOption>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Migrate categories.

        /// <summary>
        /// Runs the category migration script.
        /// </summary>
        /// <returns></returns>
        [Route("migratecategories")]
        [HttpGet, Dispatch("list", "article")]
        [AlertSuccess("Utförde migrering av kategorier!")]
        public ActionResult MigrateCategories(MigrateCategories request)
        {
            return this.View();
        }

        #endregion

        #region Migrate articles.

        /// <summary>
        /// Runs the article migration script.
        /// </summary>
        /// <returns></returns>
        [Route("migratearticles")]
        [AlertSuccess("Utförde migrering av artiklar!")]
        [HttpGet, Dispatch("list", "article")]
        public ActionResult MigrateArticles(MigrateArticles request)
        {
            return this.View();
        }

        #endregion

        #region Enable order list.

        /// <summary>
        /// Runs the category migration script.
        /// </summary>
        /// <returns></returns>
        [Route("enable")]
        [AlertSuccess("Beställningslistan aktiverades!")]
        [HttpGet, Dispatch("list", "article")]
        public ActionResult Enable(EnableOrderList request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}