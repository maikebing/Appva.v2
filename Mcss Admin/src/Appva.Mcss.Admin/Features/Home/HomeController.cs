// <copyright file="HomeController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Home
{
    #region Imports.

    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Menus;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mvc;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AuthorizeUserAndTenantAttribute, RoutePrefix("")]
    public class HomeController : Controller
    {
        #region Variables.

        /// <summary>
        /// THe menu key.
        /// </summary>
        private const string MenuKey = "https://schema.appva.se/ui/menu";

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
        public ActionResult Index(string returnUrl)
        {
            //// At this stage the user is already authenticated.
            //// If there is a return URL present then first map it to a route
            //// then check if the user has permissions to view that page, otherwise take the first
            //// item which the user has permissions to read.
            if (returnUrl.IsNotEmpty() && Url.IsLocalUrl(returnUrl))
            {
                var item = this.HandleRedirectUrl(returnUrl);
                if (item != null)
                {
                    return this.RedirectToAction(item.Action, item.Controller, item.Values);
                }
            }
            //// If there is not return URL then take the first from the menu list, this should either be
            //// dashboard OR something else.
            var items = this.menuService.Render(MenuKey, "Index", "Dashboard", "Dashboard");
            if (items.Count > 0)
            {
                var item = items.First();
                return this.RedirectToAction(item.Action, item.Controller, new
                {
                    Area = item.Area
                });
            }
            //// The account has no access so direct back to sign in. This should never happen since the user
            //// should be authenticated at this stage.
            return this.RedirectToAction("SignIn", "Authentication", new
            {
                Area = string.Empty
            });
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Handles the redirect URL.
        /// </summary>
        /// <param name="redirectUrl">The redirect url to process</param>
        /// <returns>A permitted url to redirect to</returns>
        private UrlRedirectResult HandleRedirectUrl(string redirectUrl)
        {
            var values = UriUtils.RouteValues(redirectUrl);
            var redirect = UrlRedirectResult.CreateNew(values);
            var items = this.menuService.Render(MenuKey, redirect.Action, redirect.Controller, redirect.Area);
            if (items == null)
            {
                return null;
            }
            var selected = this.LastSelected(items.Where(x => x.IsSelected).FirstOrDefault());
            if (selected != null)
            {
                return UrlRedirectResult.CreateNew(
                    selected.Action,
                    selected.Controller,
                    selected.Area,
                    redirect.Values);
            }
            var item = items.FirstOrDefault();
            return UrlRedirectResult.CreateNew(
                    item.Action,
                    item.Controller,
                    item.Area);
        }

        /// <summary>
        /// Traverse through selected menu items.
        /// </summary>
        /// <param name="selected">The selected menu item</param>
        /// <returns>A menu item or null</returns>
        private IMenuItem LastSelected(IMenuItem selected)
        {
            if (selected == null)
            {
                return null;
            }
            if (selected.Children.Count > 0)
            {
                var item = selected.Children.Where(x => x.IsSelected).FirstOrDefault();
                if (item != null)
                {
                    return this.LastSelected(item);
                }
            }
            return selected;
        }

        #endregion
    }
}