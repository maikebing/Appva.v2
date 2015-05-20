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
                    { Permissions.Create.Patient, new Permission("Skapa ny boende", "Med denna behörighet kan användaren skapa en ny boende", Permissions.Create.Patient.Value, PermissionAction.Create, PermissionContext.Admin, 0) },
                    { Permissions.Read.Patient, new Permission("Läsa boendeuppgifter", "Med denna behörighet kan användaren läsa boendeuppgifter", Permissions.Read.Patient.Value, PermissionAction.Read, PermissionContext.Admin, 1) },
                    { Permissions.Update.Patient, new Permission("Updatera boendeuppgifter", "Med denna behörighet kan användaren uppdatera boendeuppgifter", Permissions.Update.Patient.Value, PermissionAction.Update, PermissionContext.Admin, 2) },
                    { Permissions.Delete.Patient, new Permission("Ta bort boende", "Med denna behörighet kan användaren ta bort en boende", Permissions.Delete.Patient.Value, PermissionAction.Delete, PermissionContext.Admin, 3) },
                    
                    { Permissions.Create.Schedule, new Permission("Skapa patientschema", "Med denna behörighet kan användaren skapa patient scheman", Permissions.Create.Schedule.Value, PermissionAction.Create, PermissionContext.Admin, 4) },
                    { Permissions.Read.Schedule, new Permission("Läsa patientschema", "Med denna behörighet kan användaren läsa patientscheman", Permissions.Read.Schedule.Value, PermissionAction.Read, PermissionContext.Admin, 5) },
                    { Permissions.Update.Schedule, new Permission("Uppdatera patientschema", "Med denna behörighet kan användaren uppdatera patientscheman", Permissions.Update.Schedule.Value, PermissionAction.Update, PermissionContext.Admin, 6) },
                    { Permissions.Delete.Schedule, new Permission("Ta bort patientschema", "Med denna behörighet kan användaren ta bort patientscheman", Permissions.Delete.Schedule.Value, PermissionAction.Delete, PermissionContext.Admin, 7) },
                    
                    { Permissions.Read.EventList, new Permission("Läsa patient händelser", "Med denna behörighet kan användaren läsa patient händelser", Permissions.Read.EventList.Value, PermissionAction.Read, PermissionContext.Admin, 8) },
                    { Permissions.Read.Alert, new Permission("Läsa patient larm", "Med denna behörighet kan användaren läsa patient larm", Permissions.Read.Alert.Value, PermissionAction.Read, PermissionContext.Admin, 9) },
                    //// TODO: { Permissions.Read.PatientReport, new Permission("Läsa patient rapport", "Med denna behörighet kan användaren läsa patient rapport", Permissions.PatientReportsRead, PermissionAction.Read, PermissionContext.Admin, 10) },
                    { Permissions.Read.CalendarEvent, new Permission("Läsa patient kalender", "Med denna behörighet kan användaren läsa patient kalender", Permissions.Read.CalendarEvent.Value, PermissionAction.Read, PermissionContext.Admin, 11) },
                    { Permissions.Read.Inventory, new Permission("Läsa patients narkotikasaldo", "Med denna behörighet kan användaren läsa en patients narkotikasaldo", Permissions.Read.Inventory.Value, PermissionAction.Read, PermissionContext.Admin, 12) },
                    
                    { Permissions.Create.Practitioner, new Permission("Skapa ny medarbetare", "Med denna behörighet kan användaren skapa en ny medarbetare", Permissions.Create.Practitioner.Value, PermissionAction.Create, PermissionContext.Admin, 13) },
                    { Permissions.Read.Practitioner, new Permission("Läsa medarbetaruppgifter", "Med denna behörighet kan användaren läsa medarbetaruppgifter", Permissions.Read.Practitioner.Value, PermissionAction.Read, PermissionContext.Admin, 14) },
                    { Permissions.Update.Practitioner, new Permission("Uppdatera medarbetaruppgifter", "Med denna behörighet kan användaren uppdatera medarbetaruppgifter", Permissions.Update.Practitioner.Value, PermissionAction.Update, PermissionContext.Admin, 15) },
                    { Permissions.Delete.Practitioner, new Permission("Ta bort medarbetare", "Med denna behörighet kan användaren ta bort medarbetare", Permissions.Delete.Practitioner.Value, PermissionAction.Delete, PermissionContext.Admin, 16) },
                    
                    { Permissions.Create.Delegation, new Permission("Skapa ny delegering", "Med denna behörighet kan användaren skapa en ny delegering", Permissions.Create.Delegation.Value, PermissionAction.Create, PermissionContext.Admin, 17) },
                    { Permissions.Read.Delegation, new Permission("Läsa delegeringar", "Med denna behörighet kan användaren läsa delegeringar", Permissions.Read.Delegation.Value, PermissionAction.Read, PermissionContext.Admin, 18) },
                    { Permissions.Update.Delegation, new Permission("Uppdatera delegeringar", "Med denna behörighet kan användaren uppdatera delegeringar", Permissions.Update.Delegation.Value, PermissionAction.Update, PermissionContext.Admin, 19) },
                    { Permissions.Delete.Delegation, new Permission("Ta bort delegeringar", "Med denna behörighet kan användaren ta bort delegeringar", Permissions.Delete.Delegation.Value, PermissionAction.Delete, PermissionContext.Admin, 20) },
                    
                    { Permissions.Read.Revision, new Permission("Läsa delegeringar", "Med denna behörighet kan användaren läsa mottagna delegeringar", Permissions.Read.Revision.Value, PermissionAction.Read, PermissionContext.Admin, 21) },
                    { Permissions.Read.IssuedDelegation, new Permission("Läsa delegeringar", "Med denna behörighet kan användaren läsa utställda delegeringar", Permissions.Read.IssuedDelegation.Value, PermissionAction.Read, PermissionContext.Admin, 22) },
                    { Permissions.Read.PractitionerReport, new Permission("Läsa medarbetarrapport", "Med denna behörighet kan användaren läsa medarbetarrapport", Permissions.Read.PractitionerReport.Value, PermissionAction.Read, PermissionContext.Admin, 23) },
                    
                    { Permissions.Create.Role, new Permission("Skapa ny roll", "Med denna behörighet kan användaren skapa en ny roll", Permissions.Create.Role.Value, PermissionAction.Create, PermissionContext.Admin, 24) },
                    { Permissions.Read.Role, new Permission("Läsa roller", "Med denna behörighet kan användaren läsa roller", Permissions.Read.Role.Value, PermissionAction.Read, PermissionContext.Admin, 25) },
                    { Permissions.Update.Role, new Permission("Uppdatera roller", "Med denna behörighet kan användaren uppdatera roller", Permissions.Update.Role.Value, PermissionAction.Update, PermissionContext.Admin, 26) },
                    { Permissions.Delete.Role, new Permission("Ta bort roller", "Med denna behörighet kan användaren ta bort roller", Permissions.Delete.Role.Value, PermissionAction.Delete, PermissionContext.Admin, 27) },
                    
                    { Permissions.Create.Notification, new Permission("Skapa ny notifiering", "Med denna behörighet kan användaren skapa en ny notifiering", Permissions.Create.Notification.Value, PermissionAction.Create, PermissionContext.Admin, 28) },
                    { Permissions.Read.Notification, new Permission("Läsa notifiering", "Med denna behörighet kan användaren läsa notifiering", Permissions.Read.Notification.Value, PermissionAction.Read, PermissionContext.Admin, 29) },
                    { Permissions.Update.Notification, new Permission("Uppdatera notifiering", "Med denna behörighet kan användaren uppdatera notifiering", Permissions.Update.Notification.Value, PermissionAction.Update, PermissionContext.Admin, 30) },
                    { Permissions.Delete.Notification, new Permission("Ta notifiering", "Med denna behörighet kan användaren ta bort notifiering", Permissions.Delete.Notification.Value, PermissionAction.Delete, PermissionContext.Admin, 31) },
                    
                    { Permissions.Read.Dashboard, new Permission("Läsa översikt", "Med denna behörighet kan användaren läsa översikt", Permissions.Read.Dashboard.Value, PermissionAction.Read, PermissionContext.Admin, 32) },
                    //// TODO:{ Permissions.Read.DashboardControl, new Permission("Läsa kontrollräkning narkotika", "Med denna behörighet kan användaren läsa kontrollräkning narkotika", Permissions.DashboardControl, PermissionAction.Read, PermissionContext.Admin, 33) },
                    //// TODO:{ Permissions.Read.DashboardReport, new Permission("Läsa fullständig rapport", "Med denna behörighet kan användaren läsa fullständig rapport", Permissions.DashboardReport, PermissionAction.Read, PermissionContext.Admin, 34) },
                    //// TODO:{ Permissions.Read.DashboardTotalResult, new Permission("Läsa översikt totalt resultat", "Med denna behörighet kan användaren läsa totalt resultat på översikt", Permissions.DashboardTotalResult, PermissionAction.Read, PermissionContext.Admin, 35) },
                  
                    { Permissions.Read.Area51, new Permission("Area51", "Med denna behörighet kan se Area51", Permissions.Read.Area51.Value, PermissionAction.Read, PermissionContext.Admin, 36, false) },
                    
                };
            foreach (var permission in permissions)
            {
                this.persistence.Save(permission.Value);
            }
            return permissions;
        }

        /// <summary>
        /// 
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
}