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

        #region List.

        /// <summary>
        /// The article list.
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        [Route("list")]
        [Dispatch(typeof(Parameterless<ArticleListModel>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}