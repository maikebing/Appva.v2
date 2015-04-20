// <copyright file="MenuService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services.Menus
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.Caching;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IMenuService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        IMenuList<IMenuItem> Render(string key, string action, string controller, string area);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MenuService : IMenuService
    {
        #region Variables.

        /// <summary>
        /// The implemented <see cref="IRuntimeMemoryCache"/> instance.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        /// <summary>
        /// The implemented <see cref="IIdentityService"/> instance.
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The implemented <see cref="IMenuRepository"/> instance.
        /// </summary>
        private readonly IMenuRepository repository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuService"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/></param>
        /// <param name="identity">The <see cref="IIdentityService"/></param>
        /// <param name="repository">The <see cref="IMenuRepository"/></param>
        public MenuService(IRuntimeMemoryCache cache, IIdentityService identity, IMenuRepository repository)
        {
            this.cache = cache;
            this.identity = identity;
            this.repository = repository;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public IMenuList<IMenuItem> Render(string key, string action, string controller, string area)
        {
            if (! this.identity.IsAccessControlActiveForUser())
            {
                return this.CreateDefault(key, action, controller);
            }
            if (this.cache.Find<IList<MenuItem>>(key) == null)
            {
                CacheUtils.CopyAndCacheList<MenuLink, MenuItem>(this.cache, key, this.repository.ListByMenu(key), x =>
                     new MenuItem(
                        x.Id,
                        x.Label,
                        x.Action,
                        x.Controller,
                        x.Area,
                        false,
                        x.ListItemCssClass,
                        x.AnchorCssClass,
                        x.Sort,
                        x.Parent == null ? (Guid?) null : x.Parent.Id,
                        x.Permission == null ? null : x.Permission.Resource
                     ));
            }
            var items = this.cache.Find<IList<MenuItem>>(key);
            return this.CreateMenuList(action, controller, area, items);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates the menu list.
        /// </summary>
        /// <param name="action">The action route</param>
        /// <param name="controller">The controller route</param>
        /// <param name="items">The menu item collection</param>
        /// <returns>A <see cref="IMenuList{IMenuItem}"/></returns>
        private IMenuList<IMenuItem> CreateMenuList(string action, string controller, string area, IList<MenuItem> items)
        {
            var retval = new MenuList();
            foreach (var item in items)
            {
                if (item.Permission == null || this.identity.HasPermission(item.Permission))
                {
                    var isSelected = item.Controller == controller || (item.Controller == controller && item.Controller == controller);
                    retval.Add(new MenuItem(
                            item.Id,
                            item.Label,
                            item.Action,
                            item.Controller,
                            item.Area,
                            MenuItem.IsSelectedItem(item, action, controller, area),
                            item.ListItemCssClass,
                            item.AnchorCssClass,
                            item.Sort,
                            item.ParentId,
                            item.Permission
                        ));
                }
            }
            return retval;
        }

        private IMenuList<IMenuItem> CreateDefault(string key, string action, string controller)
        {
            switch (key)
            {
                case "Admin.Header.Menu":
                    return this.CreateDefaultHeaderMenu(action, controller);
                case "Admin.Patient.Menu":
                    return this.CreateDefaultPatientMenu(action, controller);
                case "Admin.Account.Menu":
                    return this.CreateDefaultAccountMenu(action, controller);
            }
            return new MenuList();
        }

        /// <summary>
        /// Creates default menu for top header if access controll is not enabled.
        /// TODO: This should be set in menu e.g. role on menu, then we can use the 
        /// same implementation without adding defaults.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        private IMenuList<IMenuItem> CreateDefaultHeaderMenu(string action, string controller)
        {
            var retval = new MenuList
                {
                    new MenuItem(Guid.Empty, "Översikt", "Index", "Home", string.Empty, "Index" == action && "Home" == controller),
                    new MenuItem(Guid.Empty, "Boende", "List", "Patient", string.Empty, "List" == action && "Patient" == controller),
                    new MenuItem(Guid.Empty, "Medarbetare", "List", "Account", string.Empty, "List" == action && "Account" == controller)
                };
            if (this.identity.IsInRole("_AA"))
            {
                retval.Add(new MenuItem(Guid.Empty, "Notiser", "List", "Notification", string.Empty, "List" == action && "Notification" == controller));
                retval.Add(new MenuItem(Guid.Empty, "Area51", "Index", "Area51", string.Empty, "Index" == action && "Area51" == controller));
            }
            retval.Add(new MenuItem(Guid.Empty, "Skriv ut sidan", "", "", string.Empty, "Index" == action && "Area51" == controller, "supp", "print"));
            return retval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        private IMenuList<IMenuItem> CreateDefaultPatientMenu(string action, string controller)
        {
            return new MenuList
                {
                    new MenuItem(Guid.Empty, "Signeringslistor", "List", "Schedule", string.Empty, "List" == action && "Schedule" == controller),
                    new MenuItem(Guid.Empty, "Signerade händelser", "Sign", "Schedule", string.Empty, "Sign" == action && "Schedule" == controller),
                    new MenuItem(Guid.Empty, "Larm", "List", "Alert", string.Empty, "List" == action && "Alert" == controller),
                    new MenuItem(Guid.Empty, "Rapport", "ScheduleReport", "Schedule", string.Empty, "ScheduleReport" == action && "Schedule" == controller),
                    new MenuItem(Guid.Empty, "Kalender", "List", "Event", string.Empty, "List" == action && "Event" == controller),
                    new MenuItem(Guid.Empty, "Saldon", "List", "Inventory", string.Empty, "List" == action && "Inventory" == controller),
                };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        private IMenuList<IMenuItem> CreateDefaultAccountMenu(string action, string controller)
        {
            var retval = new MenuList
            {
                new MenuItem(Guid.Empty, "Aktuella delegeringar", "List", "Delegation", string.Empty, "List" == action && "Delegation" == controller),
                new MenuItem(Guid.Empty, "Alla mottagna delegeringar", "Revision", "Delegation", string.Empty, "Revision" == action && "Delegation" == controller)
            };
            if (this.identity.IsInRole("_TITLE_N"))
            {
                retval.Add(new MenuItem(Guid.Empty, "Utställda delegeringar", "Issued", "Delegation", string.Empty, "Issued" == action && "Delegation" == controller));
            }
            retval.Add(new MenuItem(Guid.Empty, "Rapporter", "DelegationReport", "Delegation", string.Empty, "DelegationReport" == action && "Delegation" == controller));
            return retval;
        }
        
        #endregion
    }
}