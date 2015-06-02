// <copyright file="InstallAclHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Appva.Core.Contracts.Permissions;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InstallAclHandler : NotificationHandler<InstallAcl>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallAclHandler"/> class.
        /// </summary>
        /// <param name="roleService">The <see cref="IRoleService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public InstallAclHandler(IRoleService roleService, ISettingsService settings, IPersistenceContext persistence)
        {
            this.roleService = roleService;
            this.settings = settings;
            this.persistence = persistence;
        }

        #endregion

        #region NotificationHandler Overrides.

        /// <inheritdoc />
        public override void Handle(InstallAcl notification)
        {
            if (this.settings.IsAccessControlListInstalled())
            {
                return;
            }
            var permissions = this.CreatePermissions();
            this.CreateRolePermissions(permissions);
            this.CreateMenu(permissions);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates permissions.
        /// </summary>
        /// <returns></returns>
        private IDictionary<IPermission, Permission> CreatePermissions()
        {
            var permissions = new Dictionary<IPermission, Permission>();
            foreach (var type in typeof(Permissions).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
            {
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                {
                    var nameAttr = field.GetCustomAttributes(typeof(NameAttribute), false).SingleOrDefault() as NameAttribute;
                    var descAttr = field.GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault() as DescriptionAttribute;
                    var sortAttr = field.GetCustomAttributes(typeof(SortAttribute), false).SingleOrDefault() as SortAttribute;
                    var visiAttr = field.GetCustomAttributes(typeof(VisibilityAttribute), false).SingleOrDefault() as VisibilityAttribute;
                    var name = nameAttr.Value;
                    var description = descAttr.Value;
                    var sort = sortAttr != null ? sortAttr.Value : 0;
                    var isVisible = visiAttr != null ? visiAttr.Value == Visibility.Visible : true;
                    var permission = (IPermission) field.GetValue(null);
                    permissions.Add(permission, new Permission(name, description, permission.Value, sort, isVisible));
                }
            }
            foreach (var permission in permissions)
            {
                this.persistence.Save(permission.Value);
            }
            return permissions;
        }

        /// <summary>
        /// Add all permissions to all roles - this MUST be edited manually later.
        /// </summary>
        /// <param name="permissions"></param>
        private void CreateRolePermissions(IDictionary<IPermission, Permission> permissions)
        {
            var roles = this.roleService.List();
            foreach (var role in roles)
            {
                role.Permissions = permissions.Where(x => x.Key != Permissions.Area51.Read).Select(x => x.Value).ToList();
                if (role.MachineName == RoleTypes.Appva)
                {
                    role.Permissions.Add(permissions[Permissions.Area51.Read]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissions"></param>
        private void CreateMenu(IDictionary<IPermission, Permission> permissions)
        {
            //// The menu.
            var menu = new Menu("https://schema.appva.se/ui/menu", "Menu", "The menu");
            this.persistence.Save(menu);
            
            //// 1st menu links.
            var menuLinks1 = new Dictionary<string, MenuLink>()
            {
                { "Dashboard",    new MenuLink(menu, "Översikt", "Index", "Dashboard", "Dashboard", 0, null, null, null, permissions[Permissions.Dashboard.Read]) }, 
                { "Patient",      new MenuLink(menu, "Boende", "List", "Patient", "Patient", 1, null, null, null, permissions[Permissions.Patient.Read]) },
                { "Practitioner", new MenuLink(menu, "Medarbetare", "List", "Accounts", "Practitioner", 2, null, null, null, permissions[Permissions.Practitioner.Read]) },
                { "Roles",        new MenuLink(menu, "Roller och behörigheter", "List", "Roles", "Roles", 3, null, null, null, permissions[Permissions.Role.Read]) },
                { "Notification", new MenuLink(menu, "Notiser", "List", "Notification", "Notification", 4, null, null, null, permissions[Permissions.Notification.Read]) },
                { "Area51",       new MenuLink(menu, "Area51", "Index", "Home", "Area51", 5, null, null, null, permissions[Permissions.Area51.Read]) },
                { "Print",        new MenuLink(menu, "Skriv ut sidan", string.Empty, string.Empty, string.Empty, 6, "supp", "print", null, null)}
            };
            foreach (var menuLink in menuLinks1)
            {
                this.persistence.Save(menuLink.Value);
            }
            //// 2nd menu links.
            var menuLinks2 = new Dictionary<string, MenuLink>()
                {
                    //// The patient menu links
                    { "0", new MenuLink(menu, "Signeringslistor", "List", "Schedule", "Patient", 0, null, null, menuLinks1["Patient"], permissions[Permissions.Schedule.Read]) },
                    { "1", new MenuLink(menu, "Signerade händelser", "Sign", "Schedule", "Patient", 1, null, null, menuLinks1["Patient"], permissions[Permissions.Schedule.EventList]) },
                    { "2", new MenuLink(menu, "Larm", "List", "Alerts", "Patient", 2, null, null, menuLinks1["Patient"], permissions[Permissions.Alert.Read]) },
                    { "3", new MenuLink(menu, "Rapport", "ScheduleReport", "Schedule", "Patient", 3, null, null, menuLinks1["Patient"], permissions[Permissions.Schedule.Report]) },
                    { "4", new MenuLink(menu, "Kalender", "List", "Calendar", "Patient", 4, null, null, menuLinks1["Patient"], permissions[Permissions.Calendar.Read]) },
                    { "5", new MenuLink(menu, "Saldon", "List", "Inventory", "Patient", 5, null, null, menuLinks1["Patient"], permissions[Permissions.Inventory.Read]) },
                    //// The account menu links
                    { "6", new MenuLink(menu, "Aktuella delegeringar", "List", "Delegation", "Practitioner", 0, null, null, menuLinks1["Practitioner"], permissions[Permissions.Delegation.Read]) },
                    { "7", new MenuLink(menu, "Alla mottagna delegeringar", "Revision", "Delegation", "Practitioner", 1, null, null, menuLinks1["Practitioner"], permissions[Permissions.Delegation.Revision]) },
                    { "8", new MenuLink(menu, "Utställda delegeringar", "Issued", "Delegation", "Practitioner", 2, null, null, menuLinks1["Practitioner"], permissions[Permissions.Delegation.Issued]) },
                    { "9", new MenuLink(menu, "Rapporter", "DelegationReport", "Delegation", "Practitioner", 3, null, null, menuLinks1["Practitioner"], permissions[Permissions.Delegation.Report]) }
                };
            foreach (var menuLink in menuLinks2)
            {
                this.persistence.Save(menuLink.Value);
            }
            //// 2nd menu links.
            var menuLinks3 = new List<MenuLink>
                {
                    new MenuLink(menu, "Signeringslista objekt (Dummy)", "Details", "Schedule", "Patient", 0, null, null, menuLinks2["0"], permissions[Permissions.Schedule.Read]),
                    new MenuLink(menu, "Iordningsställande (Dummy)", "Schema", "Prepare", "Patient", 1, null, null, menuLinks2["0"], permissions[Permissions.Prepare.Read])
                };
            foreach (var menuLink in menuLinks3)
            {
                this.persistence.Save(menuLink);
            }
            this.settings.Upsert(ApplicationSettings.IsAccessControlInstalled, true);
        }
        #endregion
    }
}