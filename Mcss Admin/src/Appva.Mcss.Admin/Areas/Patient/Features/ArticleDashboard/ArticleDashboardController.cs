// <copyright file="ArticleDashboardController.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("articledashboard")]
    public sealed class ArticleDashboardController : Controller
    {
        #region Routes.

        #region Overview widget.

        /// <summary>
        /// Returns the dashboard widget.
        /// </summary>
        /// <returns><see cref="PartialViewResult"/></returns>
        [Route("overview")]
        [HttpGet, Dispatch(typeof(ArticleOverview))]
        [PermissionsAttribute(Permissions.Dashboard.ReadArticleOrderValue)]
        public PartialViewResult Overview()
        {
            return this.PartialView();
        }

        #endregion

        #region Update.

        /// <summary>
        /// Updates selected articles.
        /// </summary>
        /// <param name="sequence">The sequence id</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("update")]
        [HttpPost, Dispatch]
        public DispatchJsonResult Update(UpdateArticle request)
        {
            return this.JsonPost();
        }

        #endregion

        #endregion
    }
}