// <copyright file="MenusController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Menus
{
    #region Imports.

    using System.Web.Mvc;
    using Infrastructure.Attributes;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize, RoutePrefix("menus")]
    public sealed class MenusController : Controller
    {
        #region Routes.

        /// <summary>
        /// Return the menu list partial view.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [ChildActionOnly, Route("render"), Dispatch]
        public PartialViewResult Menu(Menu request)
        {
            return this.PartialView();
        }

        #endregion
    }
}