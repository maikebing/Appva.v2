// <copyright file="InstallHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Acl
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InstallHandler : NotificationHandler<Install>
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
        /// Initializes a new instance of the <see cref="InstallHandler"/> class.
        /// </summary>
        /// <param name="roleService">The <see cref="IRoleService"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public InstallHandler(IRoleService roleService, ISettingsService settings, IPersistenceContext persistence)
        {
            this.roleService = roleService;
            this.settings = settings;
            this.persistence = persistence;
        }

        #endregion

        #region NotificationHandler<Install> Overrides.

        /// <inheritdoc />
        public override void Handle(Install notification)
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
        private IDictionary<string, Permission> CreatePermissions()
        {
            var permissions = new Dictionary<string, Permission>
                {
                    { Permissions.PatientCreate, new Permission("Skapa ny boende", "Med denna behörighet kan användaren skapa en ny boende", Permissions.PatientCreate, PermissionAction.Create, PermissionContext.Admin, 0) },
                    { Permissions.PatientRead, new Permission("Läsa boendeuppgifter", "Med denna behörighet kan användaren läsa boendeuppgifter", Permissions.PatientRead, PermissionAction.Read, PermissionContext.Admin, 1) },
                    { Permissions.PatientUpdate, new Permission("Updatera boendeuppgifter", "Med denna behörighet kan användaren uppdatera boendeuppgifter", Permissions.PatientUpdate, PermissionAction.Update, PermissionContext.Admin, 2) },
                    { Permissions.PatientDelete, new Permission("Ta bort boende", "Med denna behörighet kan användaren ta bort en boende", Permissions.PatientDelete, PermissionAction.Delete, PermissionContext.Admin, 3) },
                    { Permissions.PatientSchedulesCreate, new Permission("Skapa patientschema", "Med denna behörighet kan användaren skapa patient scheman", Permissions.PatientSchedulesCreate, PermissionAction.Create, PermissionContext.Admin, 4) },
                    { Permissions.PatientSchedulesRead, new Permission("Läsa patientschema", "Med denna behörighet kan användaren läsa patientscheman", Permissions.PatientSchedulesRead, PermissionAction.Read, PermissionContext.Admin, 5) },
                    { Permissions.PatientSchedulesUpdate, new Permission("Uppdatera patientschema", "Med denna behörighet kan användaren uppdatera patientscheman", Permissions.PatientSchedulesUpdate, PermissionAction.Update, PermissionContext.Admin, 6) },
                    { Permissions.PatientSchedulesDelete, new Permission("Ta bort patientschema", "Med denna behörighet kan användaren ta bort patientscheman", Permissions.PatientSchedulesDelete, PermissionAction.Delete, PermissionContext.Admin, 7) },
                    { Permissions.PatientSignedEventsRead, new Permission("Läsa patient händelser", "Med denna behörighet kan användaren läsa patient händelser", Permissions.PatientSignedEventsRead, PermissionAction.Read, PermissionContext.Admin, 8) },
                    { Permissions.PatientAlertsRead, new Permission("Läsa patient larm", "Med denna behörighet kan användaren läsa patient larm", Permissions.PatientAlertsRead, PermissionAction.Read, PermissionContext.Admin, 9) },
                    { Permissions.PatientReportsRead, new Permission("Läsa patient rapport", "Med denna behörighet kan användaren läsa patient rapport", Permissions.PatientReportsRead, PermissionAction.Read, PermissionContext.Admin, 10) },
                    { Permissions.PatientCalendarRead, new Permission("Läsa patient kalender", "Med denna behörighet kan användaren läsa patient kalender", Permissions.PatientCalendarRead, PermissionAction.Read, PermissionContext.Admin, 11) },
                    { Permissions.PatientInventoryRead, new Permission("Läsa patients narkotikasaldo", "Med denna behörighet kan användaren läsa en patients narkotikasaldo", Permissions.PatientInventoryRead, PermissionAction.Read, PermissionContext.Admin, 12) },

                    { Permissions.AccountCreate, new Permission("Skapa ny medarbetare", "Med denna behörighet kan användaren skapa en ny medarbetare", Permissions.AccountCreate, PermissionAction.Create, PermissionContext.Admin, 13) },
                    { Permissions.AccountRead, new Permission("Läsa medarbetaruppgifter", "Med denna behörighet kan användaren läsa medarbetaruppgifter", Permissions.AccountRead, PermissionAction.Read, PermissionContext.Admin, 14) },
                    { Permissions.AccountUpdate, new Permission("Uppdatera medarbetaruppgifter", "Med denna behörighet kan användaren uppdatera medarbetaruppgifter", Permissions.AccountUpdate, PermissionAction.Update, PermissionContext.Admin, 15) },
                    { Permissions.AccountDelete, new Permission("Ta bort medarbetare", "Med denna behörighet kan användaren ta bort medarbetare", Permissions.AccountDelete, PermissionAction.Delete, PermissionContext.Admin, 16) },
                    { Permissions.AccountDelegationCreate, new Permission("Skapa ny delegering", "Med denna behörighet kan användaren skapa en ny delegering", Permissions.AccountDelegationCreate, PermissionAction.Create, PermissionContext.Admin, 17) },
                    { Permissions.AccountDelegationRead, new Permission("Läsa delegeringar", "Med denna behörighet kan användaren läsa delegeringar", Permissions.AccountDelegationRead, PermissionAction.Read, PermissionContext.Admin, 18) },
                    { Permissions.AccountDelegationUpdate, new Permission("Uppdatera delegeringar", "Med denna behörighet kan användaren uppdatera delegeringar", Permissions.AccountDelegationUpdate, PermissionAction.Update, PermissionContext.Admin, 19) },
                    { Permissions.AccountDelegationDelete, new Permission("Ta bort delegeringar", "Med denna behörighet kan användaren ta bort delegeringar", Permissions.AccountDelegationDelete, PermissionAction.Create, PermissionContext.Admin, 20) },
                    { Permissions.AccountRevisionRead, new Permission("Läsa delegeringar", "Med denna behörighet kan användaren läsa mottagna delegeringar", Permissions.AccountRevisionRead, PermissionAction.Read, PermissionContext.Admin, 21) },
                    { Permissions.AccountIssuedDelegationRead, new Permission("Läsa delegeringar", "Med denna behörighet kan användaren läsa utställda delegeringar", Permissions.AccountIssuedDelegationRead, PermissionAction.Read, PermissionContext.Admin, 22) },
                    { Permissions.AccountReportsRead, new Permission("Läsa medarbetarrapport", "Med denna behörighet kan användaren läsa medarbetarrapport", Permissions.AccountReportsRead, PermissionAction.Read, PermissionContext.Admin, 23) },
                    { Permissions.RoleCreate, new Permission("Skapa ny roll", "Med denna behörighet kan användaren skapa en ny roll", Permissions.RoleCreate, PermissionAction.Create, PermissionContext.Admin, 24) },
                    { Permissions.RoleRead, new Permission("Läsa roller", "Med denna behörighet kan användaren läsa roller", Permissions.RoleRead, PermissionAction.Read, PermissionContext.Admin, 25) },
                    { Permissions.RoleUpdate, new Permission("Uppdatera roller", "Med denna behörighet kan användaren uppdatera roller", Permissions.RoleUpdate, PermissionAction.Update, PermissionContext.Admin, 26) },
                    { Permissions.RoleDelete, new Permission("Ta bort roller", "Med denna behörighet kan användaren ta bort roller", Permissions.RoleDelete, PermissionAction.Delete, PermissionContext.Admin, 27) },
                    { Permissions.NotificationCreate, new Permission("Skapa ny notifiering", "Med denna behörighet kan användaren skapa en ny notifiering", Permissions.NotificationCreate, PermissionAction.Create, PermissionContext.Admin, 28) },
                    { Permissions.NotificationRead, new Permission("Läsa notifiering", "Med denna behörighet kan användaren läsa notifiering", Permissions.NotificationRead, PermissionAction.Read, PermissionContext.Admin, 29) },
                    { Permissions.NotificationUpdate, new Permission("Uppdatera notifiering", "Med denna behörighet kan användaren uppdatera notifiering", Permissions.NotificationUpdate, PermissionAction.Update, PermissionContext.Admin, 30) },
                    { Permissions.NotificationDelete, new Permission("Ta notifiering", "Med denna behörighet kan användaren ta bort notifiering", Permissions.NotificationDelete, PermissionAction.Delete, PermissionContext.Admin, 31) },
                    
                    { Permissions.DashboardRead, new Permission("Läsa översikt", "Med denna behörighet kan användaren läsa översikt", Permissions.DashboardRead, PermissionAction.Read, PermissionContext.Admin, 32) },
                    { Permissions.DashboardControl, new Permission("Läsa kontrollräkning narkotika", "Med denna behörighet kan användaren läsa kontrollräkning narkotika", Permissions.DashboardControl, PermissionAction.Read, PermissionContext.Admin, 33) },
                    { Permissions.DashboardReport, new Permission("Läsa fullständig rapport", "Med denna behörighet kan användaren läsa fullständig rapport", Permissions.DashboardReport, PermissionAction.Read, PermissionContext.Admin, 34) },
                    { Permissions.DashboardTotalResult, new Permission("Läsa översikt totalt resultat", "Med denna behörighet kan användaren läsa totalt resultat på översikt", Permissions.DashboardTotalResult, PermissionAction.Read, PermissionContext.Admin, 35) },
                  
                    { Permissions.Area51, new Permission("Area51", "Med denna behörighet kan se Area51", Permissions.Area51, PermissionAction.Read, PermissionContext.Admin, 36, false) },
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
        private void CreateRolePermissions(IDictionary<string, Permission> permissions)
        {
            var roles = this.roleService.List();
            foreach (var role in roles)
            {
                role.Permissions = permissions.Where(x => x.Key != Permissions.Area51).Select(x => x.Value).ToList();
                if (role.MachineName == "_AA")
                {
                    role.Permissions.Add(permissions[Permissions.Area51]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissions"></param>
        private void CreateMenu(IDictionary<string, Permission> permissions)
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
                    new MenuLink(menus[HeaderMenu], "Översikt", "Index", "Home", string.Empty, 0),
                    new MenuLink(menus[HeaderMenu], "Boende", "List", "Patient", string.Empty, 1, null, null, null, permissions[Permissions.PatientRead]),
                    new MenuLink(menus[HeaderMenu], "Medarbetare", "List", "Account", string.Empty, 2, null, null, null, permissions[Permissions.AccountRead]),
                    new MenuLink(menus[HeaderMenu], "Roller och behörigheter", "Index", "Role", string.Empty, 3, null, null, null, permissions[Permissions.NotificationRead]),
                    new MenuLink(menus[HeaderMenu], "Notiser", "List", "Notification", string.Empty, 4, null, null, null, permissions[Permissions.NotificationRead]),
                    new MenuLink(menus[HeaderMenu], "Area51", "Index", "Home", "Area51", 5, null, null, null, permissions[Permissions.Area51]),
                    new MenuLink(menus[HeaderMenu], "Skriv ut sidan", string.Empty, string.Empty, string.Empty, 6, "supp", "print", null, null),
                    ///////////////////////////  The patient menu links  ////////////////////////////////
                    new MenuLink(menus[PatientMenu], "Signeringslistor", "List", "Schedule", string.Empty, 0, null, null, null, permissions[Permissions.PatientSchedulesRead]),
                    new MenuLink(menus[PatientMenu], "Signerade händelser", "Sign", "Schedule", string.Empty, 1, null, null, null, permissions[Permissions.PatientSignedEventsRead]),
                    new MenuLink(menus[PatientMenu], "Larm", "List", "Alert", string.Empty, 2, null, null, null, permissions[Permissions.PatientAlertsRead]),
                    new MenuLink(menus[PatientMenu], "Rapport", "ScheduleReport", "Schedule", string.Empty, 3, null, null, null, permissions[Permissions.PatientReportsRead]),
                    new MenuLink(menus[PatientMenu], "Kalender", "List", "Event", string.Empty, 4, null, null, null, permissions[Permissions.PatientCalendarRead]),
                    new MenuLink(menus[PatientMenu], "Saldon", "List", "Inventory", string.Empty, 5, null, null, null, permissions[Permissions.PatientInventoryRead]),
                    ///////////////////////////  The account menu links  ////////////////////////////////
                    new MenuLink(menus[AccountMenu], "Aktuella delegeringar", "List", "Delegation", string.Empty, 0, null, null, null, permissions[Permissions.AccountDelegationRead]),
                    new MenuLink(menus[AccountMenu], "Alla mottagna delegeringar", "Revision", "Delegation", string.Empty, 1, null, null, null, permissions[Permissions.AccountRevisionRead]),
                    new MenuLink(menus[AccountMenu], "Utställda delegeringar", "Issued", "Delegation", string.Empty, 2, null, null, null, permissions[Permissions.AccountIssuedDelegationRead]),
                    new MenuLink(menus[AccountMenu], "Rapporter", "DelegationReport", "Delegation", string.Empty, 3, null, null, null, permissions[Permissions.AccountReportsRead])
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