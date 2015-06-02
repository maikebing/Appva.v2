// <copyright file="HomeController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Home
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Core.Messaging;
    using Appva.Mcss.Admin.Application.Services.Menus;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc.Messaging;
    using Appva.Persistence;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize, RoutePrefix("")]
    public class HomeController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMenuService"/>.
        /// </summary>
        private readonly IMenuService menuService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public HomeController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route]
        public ActionResult Index()
        {
            var menuLinks = this.menuService.Render("https://schema.appva.se/ui/menu", "Index", "Dashboard", "Dashboard");
            if (menuLinks.Count > 0)
            {
                var menuLink = menuLinks.First();
                return this.RedirectToAction(menuLink.Action, menuLink.Controller, new
                {
                    Area = menuLink.Area
                });
            }
            return this.RedirectToAction("SignIn", "Authentication", new
            {
                Area = string.Empty
            });
        }

        #endregion
    }
}