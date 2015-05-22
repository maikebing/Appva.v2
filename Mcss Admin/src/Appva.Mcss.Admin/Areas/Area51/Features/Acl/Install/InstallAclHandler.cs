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
    using Appva.Core.Contracts.Permissions;
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
            if (settings.Find<bool>(ApplicationSettings.IsAccessControlInstalled, false))
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
            var permissions = new Dictionary<IPermission, Permission>
                {
                    { Permissions.Create.Patient,  Permissions.Create.Patient.Convert() },
                    { Permissions.Read.Patient,    Permissions.Read.Patient.Convert()   },
                    { Permissions.Update.Patient,  Permissions.Update.Patient.Convert() },
                    { Permissions.Delete.Patient,  Permissions.Delete.Patient.Convert() },
                    
                    { Permissions.Create.Schedule, Permissions.Create.Schedule.Convert() },
                    { Permissions.Read.Schedule,   Permissions.Read.Schedule.Convert()   },
                    { Permissions.Update.Schedule, Permissions.Update.Schedule.Convert() },
                    { Permissions.Delete.Schedule, Permissions.Delete.Schedule.Convert() },
                    
                    { Permissions.Read.EventList, Permissions.Read.EventList.Convert() },
                    { Permissions.Read.Alert,     Permissions.Read.Alert.Convert() },
                    //// TODO: { Permissions.Read.PatientReport, Permissions.Read.PatientReport.Convert() },
                    { Permissions.Read.CalendarEvent, Permissions.Read.CalendarEvent.Convert() },
                    { Permissions.Read.Inventory, Permissions.Read.Inventory.Convert() },
                    
                    { Permissions.Create.Practitioner, Permissions.Create.Practitioner.Convert() },
                    { Permissions.Read.Practitioner,   Permissions.Read.Practitioner.Convert()   },
                    { Permissions.Update.Practitioner, Permissions.Update.Practitioner.Convert() },
                    { Permissions.Delete.Practitioner, Permissions.Delete.Practitioner.Convert() },
                    
                    { Permissions.Create.Delegation, Permissions.Create.Delegation.Convert() },
                    { Permissions.Read.Delegation,   Permissions.Read.Delegation.Convert()   },
                    { Permissions.Update.Delegation, Permissions.Update.Delegation.Convert() },
                    { Permissions.Delete.Delegation, Permissions.Delete.Delegation.Convert() },
                    
                    { Permissions.Read.Revision, Permissions.Read.Revision.Convert() },
                    { Permissions.Read.IssuedDelegation, Permissions.Read.IssuedDelegation.Convert() },
                    { Permissions.Read.PractitionerReport, Permissions.Read.PractitionerReport.Convert() },
                    
                    { Permissions.Create.Role, Permissions.Create.Role.Convert() },
                    { Permissions.Read.Role,   Permissions.Read.Role.Convert()   },
                    { Permissions.Update.Role, Permissions.Update.Role.Convert() },
                    { Permissions.Delete.Role, Permissions.Delete.Role.Convert() },
                    
                    { Permissions.Create.Notification, Permissions.Create.Notification.Convert() },
                    { Permissions.Read.Notification, Permissions.Read.Notification.Convert() },
                    { Permissions.Update.Notification, Permissions.Update.Notification.Convert() },
                    { Permissions.Delete.Notification, Permissions.Delete.Notification.Convert() },
                    
                    { Permissions.Read.Dashboard, Permissions.Read.Dashboard.Convert() },
                    //// TODO:{ Permissions.Read.DashboardControl, new Permission("Läsa kontrollräkning narkotika", "Med denna behörighet kan användaren läsa kontrollräkning narkotika", Permissions.DashboardControl, PermissionAction.Read, PermissionContext.Admin, 33) },
                    //// TODO:{ Permissions.Read.DashboardReport, new Permission("Läsa fullständig rapport", "Med denna behörighet kan användaren läsa fullständig rapport", Permissions.DashboardReport, PermissionAction.Read, PermissionContext.Admin, 34) },
                    //// TODO:{ Permissions.Read.DashboardTotalResult, new Permission("Läsa översikt totalt resultat", "Med denna behörighet kan användaren läsa totalt resultat på översikt", Permissions.DashboardTotalResult, PermissionAction.Read, PermissionContext.Admin, 35) },
                  
                    { Permissions.Read.Area51, Permissions.Read.Area51.Convert() }, /// remember visibility
                    
                };
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
                role.Permissions = permissions.Where(x => x.Key != Permissions.Read.Area51).Select(x => x.Value).ToList();
                if (role.MachineName == "_AA")
                {
                    role.Permissions.Add(permissions[Permissions.Read.Area51]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissions"></param>
        private void CreateMenu(IDictionary<IPermission, Permission> permissions)
        {
            const string HeaderMenu = "Admin.Header.Menu";
            const string PatientMenu = "Admin.Patient.Menu";
            const string AccountMenu = "Admin.Account.Menu";
            var menus = new Dictionary<string, Menu>
            {
                { HeaderMenu,  new Menu(HeaderMenu, "Header menu", "The header menu") },
                { PatientMenu, new Menu(PatientMenu, "Patient menu", "The patient menu") },
                { AccountMenu, new Menu(AccountMenu, "Account menu", "Account menu") }
            };
            foreach (var menu in menus)
            {
                this.persistence.Save(menu.Value);
            }
            var menuLinks = new List<MenuLink>
                {
                    /////////////////////////  The admin header menu links  /////////////////////////////
                    new MenuLink(menus[HeaderMenu], "Översikt", "Index", "Dashboard", "Dashboard", 0),
                    new MenuLink(menus[HeaderMenu], "Boende", "List", "Patient", "Patient", 1, null, null, null, permissions[Permissions.Read.Patient]),
                    new MenuLink(menus[HeaderMenu], "Medarbetare", "List", "Account", "Practitioner", 2, null, null, null, permissions[Permissions.Read.Practitioner]),
                    new MenuLink(menus[HeaderMenu], "Roller och behörigheter", "List", "Roles", "Roles", 3, null, null, null, permissions[Permissions.Read.Role]),
                    new MenuLink(menus[HeaderMenu], "Notiser", "List", "Notification", "Notification", 4, null, null, null, permissions[Permissions.Read.Notification]),
                    new MenuLink(menus[HeaderMenu], "Area51", "Index", "Home", "Area51", 5, null, null, null, permissions[Permissions.Read.Area51]),
                    new MenuLink(menus[HeaderMenu], "Skriv ut sidan", string.Empty, string.Empty, string.Empty, 6, "supp", "print", null, null),
                    ///////////////////////////  The patient menu links  ////////////////////////////////
                    new MenuLink(menus[PatientMenu], "Signeringslistor", "List", "Schedule", "Patient", 0, null, null, null, permissions[Permissions.Read.Schedule]),
                    new MenuLink(menus[PatientMenu], "Signerade händelser", "Sign", "Schedule", "Patient", 1, null, null, null, permissions[Permissions.Read.EventList]),
                    new MenuLink(menus[PatientMenu], "Larm", "List", "Alert", "Patient", 2, null, null, null, permissions[Permissions.Read.Alert]),
                    new MenuLink(menus[PatientMenu], "Rapport", "ScheduleReport", "Schedule", "Patient", 3, null, null, null, null/*permissions[Permissions.Read.PatientReport]*/),
                    new MenuLink(menus[PatientMenu], "Kalender", "List", "Calendar", "Patient", 4, null, null, null, permissions[Permissions.Read.CalendarEvent]),
                    new MenuLink(menus[PatientMenu], "Saldon", "List", "Inventory", "Patient", 5, null, null, null, permissions[Permissions.Read.Inventory]),
                    ///////////////////////////  The account menu links  ////////////////////////////////
                    new MenuLink(menus[AccountMenu], "Aktuella delegeringar", "List", "Delegation", string.Empty, 0, null, null, null, permissions[Permissions.Read.Delegation]),
                    new MenuLink(menus[AccountMenu], "Alla mottagna delegeringar", "Revision", "Delegation", string.Empty, 1, null, null, null, permissions[Permissions.Read.Revision]),
                    new MenuLink(menus[AccountMenu], "Utställda delegeringar", "Issued", "Delegation", string.Empty, 2, null, null, null, permissions[Permissions.Read.IssuedDelegation]),
                    new MenuLink(menus[AccountMenu], "Rapporter", "DelegationReport", "Delegation", string.Empty, 3, null, null, null, permissions[Permissions.Read.PractitionerReport])
                };
            foreach (var menuLink in menuLinks)
            {
                this.persistence.Save(menuLink);
            }
            this.settings.Upsert(ApplicationSettings.IsAccessControlInstalled, true);
        }
        #endregion
    }

    internal static class IPermissionExtensions
    {
        private static int sort = 0;

        public static Permission Convert(this IPermission permission)
        {
            return new Permission(permission.Name, permission.Description, permission.Value, PermissionAction.Create, PermissionContext.Admin, sort++);
        }
    }
}