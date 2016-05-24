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
    using Appva.Caching.Providers;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Mcss.Admin.Application.Caching;

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
        IList<IMenuItem> Render(string key, string action, string controller, string area);
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
        private readonly ITenantAwareMemoryCache cache;

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
        public MenuService(ITenantAwareMemoryCache cache, IIdentityService identity, IMenuRepository repository)
        {
            this.cache = cache;
            this.identity = identity;
            this.repository = repository;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public IList<IMenuItem> Render(string key, string action, string controller, string area)
        {
            if (! this.identity.IsAccessControlActiveForUser())
            {
                return this.CreateMenuList(action, controller, area, this.CreateDefaultMenu());
            }

            return this.CreateMenuList(action, controller, area, Menus.MainMenu);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates the menu list.
        /// </summary>
        /// <param name="action">The action route</param>
        /// <param name="controller">The controller route</param>
        /// <param name="area">The area route</param>
        /// <param name="items">The menu item collection</param>
        /// <returns>A <see cref="IMenuList{IMenuItem}"/></returns>
        private IList<IMenuItem> CreateMenuList(string action, string controller, string area, IList<IMenuItem> links)
        {
            var retval = new List<IMenuItem>();
            foreach (var link in links)
            {
                if (link.Permission == null || this.identity.HasPermission(link.Permission.Value))
                {
                    var item = link.Clone() as MenuItem;
                    retval.Add(item);
                    if(item.Children != null)
                    {
                        bool childSelected;
                        item.Children = this.CreateMenuList(action, controller, area, item.Children, out childSelected);
                        if(childSelected)
                        { 
                            ((MenuItem)item).MarkAsSelected(); 
                        }
                    }
                    if(MenuItem.IsSelectedItem(item, action, controller, area))
                    {
                        ((MenuItem)item).MarkAsSelected();
                    }
                }

            }
            return retval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <param name="items"></param>
        /// <param name="isSelected"></param>
        /// <returns></returns>
        private IList<IMenuItem> CreateMenuList(string action, string controller, string area, IList<IMenuItem> links, out bool isSelected)
        {
            isSelected = false;
            var retval = new List<IMenuItem>();
            foreach (var link in links)
            {
                if (link.Permission == null || this.identity.HasPermission(link.Permission.Value))
                {
                    var item = link.Clone() as MenuItem;
                    retval.Add(item);
                    if(item.Children != null)
                    {
                        bool childSelected;
                        item.Children = this.CreateMenuList(action, controller, area, item.Children, out childSelected);
                        if(childSelected)
                        {
                            ((MenuItem)item).MarkAsSelected();
                            isSelected = true;
                        }
                    }
                    if(MenuItem.IsSelectedItem(item, action, controller, area))
                    {
                        ((MenuItem)item).MarkAsSelected();
                        isSelected = true;
                    }
                }

            }
            return retval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="items"></param>
        private void MarkParentsAsSelected(Guid? parentId, IDictionary<Guid, MenuItem> items)
        {
            if (! parentId.HasValue || ! items.ContainsKey(parentId.Value))
            {
                return;
            }
            items[parentId.Value].MarkAsSelected();
            this.MarkParentsAsSelected(items[parentId.Value].ParentId, items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        /*private IList<IMenuItem> CreateHierarchyTree(IList<MenuItem> items)
        {
            return items.Where(x => x.ParentId.IsEmpty())
                .OrderBy(x => x.Sort)
                .Select(x => MenuItem.CreateNew(
                            x.Label,
                            x.Action,
                            x.Controller,
                            x.Area,
                            x.IsSelected,
                            x.ListItemCssClass,
                            x.AnchorCssClass,
                            this.CreateHierarchyTree(x, items)))
                .Cast<IMenuItem>()
                .ToList();
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        /*private IList<IMenuItem> CreateHierarchyTree(MenuItem item, IList<MenuItem> items)
        {
            return items.Where(x => x.ParentId.IsNotEmpty() && x.ParentId.Equals(item.Id))
                .OrderBy(x => x.Sort)
                .Select(x => MenuItem.CreateNew(
                            x.Label,
                            x.Action,
                            x.Controller,
                            x.Area,
                            x.IsSelected,
                            x.ListItemCssClass,
                            x.AnchorCssClass,
                            this.CreateHierarchyTree(x, items)))
                .Cast<IMenuItem>()
                .ToList();
        }*/

        /// <summary>
        /// Creates default menu if access control is not enabled.
        /// </summary>
        /// <returns></returns>
        private IList<IMenuItem> CreateDefaultMenu()
        {
            var items = new List<IMenuItem>();
            //// 1st menu items.
            items.Add(new MenuItem(new Guid("7D2FD5DC-F3B1-4C23-80D8-A4A301313DA3"), "Översikt", "Index", "Dashboard", "Dashboard", false));
            items.Add(new MenuItem(new Guid("3D395383-AB8E-4A78-BC86-A4A301313DA4"), "Boende", "List", "Patient", "Patient", false));
            items.Add(new MenuItem(new Guid("593928BE-930F-4D5D-8981-A4A301313DA4"), "Medarbetare", "List", "Accounts", "Practitioner", false));
            if (this.identity.IsInRole(RoleTypes.Appva))
            {
                items.Add(new MenuItem(new Guid("7B302508-C610-4898-9594-A4A301313DA4"), "Roller och behörigheter", "List", "Roles", "Roles", false));
                items.Add(new MenuItem(new Guid("EA9849DE-B18D-46C6-8CD9-A4A301313DA4"), "Notiser", "List", "Notification", "Notification", false));
                items.Add(new MenuItem(new Guid("AD5B2F7C-071B-4590-8CBE-A4A301313DA4"), "Area51", "Index", "Home", "Area51", false));
            }
            items.Add(new MenuItem(new Guid("BEA905BC-CEAE-4DA7-B0AD-A4A301313DA4"), "Skriv ut sidan", null, null, null, false, "supp", "print"));
            //// 2nd menu items.
            items.Add(new MenuItem(new Guid("B9DD1CC0-F34E-4833-9728-A4A301313DA4"), "Aktuella delegeringar", "List", "Delegation", "Practitioner", false, null, null, 0, new Guid("593928BE-930F-4D5D-8981-A4A301313DA4")));
            items.Add(new MenuItem(new Guid("9EC649B2-55AB-47B2-8F16-A4A301313DA4"), "Alla mottagna delegeringar", "Revision", "Delegation", "Practitioner", false, null, null, 1, new Guid("593928BE-930F-4D5D-8981-A4A301313DA4")));

            if (this.identity.IsInRole(RoleTypes.Nurse))
            {
                items.Add(new MenuItem(new Guid("D92CCDFC-83AD-4572-A8BC-A4A301313DA4"), "Utställda delegeringar", "Issued", "Delegation", "Practitioner", false, null, null, 2, new Guid("593928BE-930F-4D5D-8981-A4A301313DA4")));
            }
            items.Add(new MenuItem(new Guid("62233CBE-0287-4838-9C16-A4A301313DA4"), "Rapporter", "DelegationReport", "Delegation", "Practitioner", false, null, null, 3, new Guid("593928BE-930F-4D5D-8981-A4A301313DA4")));
            //// 2nd menu items.
            items.Add(new MenuItem(new Guid("20C161E1-0A4E-4BA0-AA2F-A4A301313DA4"), "Signeringslistor", "List", "Schedule", "Patient", false, null, null, 0, new Guid("3D395383-AB8E-4A78-BC86-A4A301313DA4")));
            //// Dummy
            items.Add(new MenuItem(new Guid("4695B0B9-485B-4044-8ECA-59603889456D"), "Dummy endast till för att kunna visa meny för signeringslist objekt", "Details", "Schedule", "Patient", false, null, null, 0, new Guid("20C161E1-0A4E-4BA0-AA2F-A4A301313DA4")));
            //// Dummy
            items.Add(new MenuItem(new Guid("6b7738da-a70d-4d7b-9f8c-a48301459200"), "Dummy endast till för att kunna visa meny för iordningstallande objekt", "Schema", "Prepare", "Patient", false, null, null, 0, new Guid("4695B0B9-485B-4044-8ECA-59603889456D")));
            items.Add(new MenuItem(new Guid("4F4CBAFA-C618-415D-9375-A4A301313DA4"), "Signerade händelser", "Sign", "Schedule", "Patient", false, null, null, 1, new Guid("3D395383-AB8E-4A78-BC86-A4A301313DA4")));
            items.Add(new MenuItem(new Guid("CC718004-8AE6-49AB-9342-A4A301313DA4"), "Larm", "List", "Alerts", "Patient", false, null, null, 2, new Guid("3D395383-AB8E-4A78-BC86-A4A301313DA4")));
            items.Add(new MenuItem(new Guid("9285856A-3278-4D4C-B155-A4A301313DA4"), "Rapport", "ScheduleReport", "Schedule", "Patient", false, null, null, 3, new Guid("3D395383-AB8E-4A78-BC86-A4A301313DA4")));
            items.Add(new MenuItem(new Guid("D78F3803-837B-4A56-8109-A4A301313DA4"), "Kalender", "List", "Calendar", "Patient", false, null, null, 4, new Guid("3D395383-AB8E-4A78-BC86-A4A301313DA4")));
            items.Add(new MenuItem(new Guid("FB4CDF53-8C07-4F1F-9D6E-A4A301313DA4"), "Saldon", "List", "Inventory", "Patient", false, null, null, 5, new Guid("3D395383-AB8E-4A78-BC86-A4A301313DA4")));
            return items;
        }

        /// <summary>
        /// Returns the new cached key.
        /// </summary>
        /// <param name="key">The key to be cached or retrieved from cache</param>
        /// <returns>A new menu cache key</returns>
        private string CreateCacheKey(string key)
        {
            return CacheTypes.Menu.FormatWith(key);
        }

        #endregion
    }
}