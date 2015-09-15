// <copyright file="HomeController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Home
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class HomeController : Controller
    {
        #region Routes.

        #region Index.

        /// <summary>
        /// Returns the home view.
        /// </summary>
        /// <returns>The view</returns>
        [Route]
        [HttpGet]
        public ActionResult Index()
        {

            return this.View();
        }

        #endregion

        #endregion
    }
}