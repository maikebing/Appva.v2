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
        IList<IMenuItem> Render();
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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuService"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/></param>
        /// <param name="identity">The <see cref="IIdentityService"/></param>
        public MenuService(ITenantAwareMemoryCache cache, IIdentityService identity)
        {
            this.cache = cache;
            this.identity = identity;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public IList<IMenuItem> Render()
        {
            if (! this.identity.IsAccessControlActiveForUser())
            {
                return this.CreateMenuList(this.CreateDefaultMenu());
            }

            return this.CreateMenuList(Menus.MainMenu);
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
        private IList<IMenuItem> CreateMenuList(IList<IMenuItem> links)
        {
            if(links == null)
            {
                return new List<IMenuItem>();
            }
            var retval = new List<IMenuItem>();
            foreach (var link in links)
            {
                if (link.Permission == null || this.identity.HasPermission(link.Permission.Value))
                {
                    retval.Add(MenuItem.CreateNew(
                        link.Label, 
                        link.Action, 
                        link.Controller, 
                        link.Area, 
                        link.ListItemCssClass, 
                        link.AnchorCssClass, 
                        link.Permission,
                        this.CreateMenuList(link.Children)));
                }
            }
            return retval;
        }

       

        /// <summary>
        /// Creates default menu if access control is not enabled.
        /// </summary>
        /// <returns></returns>
        private IList<IMenuItem> CreateDefaultMenu()
        {
            var items = new List<IMenuItem>();
            //// 1st menu items.
            items.Add(new MenuItem("Översikt", "Index", "Dashboard", "Dashboard", null, null, null, null));
            items.Add(new MenuItem("Boende", "List", "Patient", "Patient", null, null, null, new List<IMenuItem> { 
                new MenuItem("Signeringslistor", "List", "Schedule", "Patient", null, null, null, new List<IMenuItem> {
                    new MenuItem("Dummy endast till för att kunna visa meny för signeringslist objekt", "Details", "Schedule", "Patient", null, null, null, null),
                    new MenuItem("Dummy endast till för att kunna visa meny för iordningstallande objekt", "Schema", "Prepare", "Patient", null, null, null, null)

                }),
                new MenuItem("Signerade händelser", "Sign", "Schedule", "Patient", null, null, null, null),
                new MenuItem("Larm", "List", "Alerts", "Patient", null, null, null, null),
                new MenuItem("Rapport", "ScheduleReport", "Schedule", "Patient", null, null, null, null),
                new MenuItem("Kalender", "List", "Calendar", "Patient", null, null, null, null),
                new MenuItem("Saldon", "List", "Inventory", "Patient", null, null, null, null)
            }));
            items.Add(new MenuItem("Medarbetare", "List", "Accounts", "Practitioner", null, null, null, new List<IMenuItem> { 
                 new MenuItem("Aktuella delegeringar", "List", "Delegation", "Practitioner", null, null, null, null),
                 new MenuItem("Alla mottagna delegeringar", "Revision", "Delegation", "Practitioner", null, null, null, null),
                 new MenuItem("Utställda delegeringar", "Issued", "Delegation", "Practitioner", null, null, null, null),
                 new MenuItem("Rapporter", "DelegationReport", "Delegation", "Practitioner", null, null, null, null)
            
            }));
            if (this.identity.IsInRole(RoleTypes.Appva))
            {
                items.Add(new MenuItem("Roller och behörigheter", "List", "Roles", "Roles", null, null, null, null));
                items.Add(new MenuItem("Notiser", "List", "Notification", "Notification", null, null, null, null));
                items.Add(new MenuItem("Area51", "Index", "Home", "Area51", null, null, null, null));
            }
            items.Add(new MenuItem("Skriv ut sidan", null, null, null, "supp", "print", null, null));

            return items;
        }

        #endregion
    }
}