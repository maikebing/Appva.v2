// <copyright file="Menus.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Menus
    {
        private static Menus instance;

        private static Menus Instance
        {
	        get 
            { 
                if(instance == null)
                {
                    instance = new Menus();
                }
                return instance; 
            }
        }

        #region Main

        /// <summary>
        /// The main menu
        /// </summary>
        private IList<IMenuItem> main;

        private IList<IMenuItem> Main
        {
            get
            {
                if (this.main == null)
                {
                    this.main = new List<IMenuItem>()
                    {
                        MenuItem.CreateNew("Översikt", "Index", "Dashboard", "Dashboard", null, null, Permissions.Dashboard.Read, null), 
                        MenuItem.CreateNew("Boende", "List", "Patient", "Patient", null, null, Permissions.Patient.Read, this.Patient),
                        MenuItem.CreateNew("Medarbetare", "List", "Accounts", "Practitioner", null, null, Permissions.Practitioner.Read, this.account ),
                        MenuItem.CreateNew("Roller och behörigheter", "List", "Roles", "Roles", null, null, Permissions.Role.Read, null),
                        MenuItem.CreateNew("Logg", "List", "Log", "Log", null, null, Permissions.Log.Read, this.log),
                        MenuItem.CreateNew("Notiser", "List", "Notification", "Notification", null, null, Permissions.Notification.Read, null),
                        MenuItem.CreateNew("Area51", "Index", "Home", "Area51", null, null, Permissions.Area51.Read, null),
                        MenuItem.CreateNew("Skriv ut sidan", string.Empty, string.Empty, string.Empty, "supp", "print", null, null)
                    };
                }
                return this.main;
            }
        }

        #endregion

        #region Patient

        /// <summary>
        /// The patient-menu
        /// </summary>
        private IList<IMenuItem> patient;

        private IList<IMenuItem> Patient
        {
            get
            {
                if (this.patient == null)
                {
                    this.patient = new List<IMenuItem>()
                    {
                         MenuItem.CreateNew("Signeringslistor", "List", "Schedule", "Patient", null, null, Permissions.Schedule.Read, this.schedule),
                         MenuItem.CreateNew("Signerade händelser", "Sign", "Schedule", "Patient", null, null, Permissions.Schedule.EventList, null),
                         MenuItem.CreateNew("Larm", "List", "Alerts", "Patient", null, null, Permissions.Alert.Read, null),
                         MenuItem.CreateNew("Rapport", "ScheduleReport", "Schedule", "Patient", null, null, Permissions.Schedule.Report, null),
                         MenuItem.CreateNew("Kalender", "List", "Calendar", "Patient", null, null, Permissions.Calendar.Read, null),
                         MenuItem.CreateNew("Saldon", "List", "Inventory", "Patient", null, null, Permissions.Inventory.Read, null)
                    };
                }
                return this.patient;
            }
        }

        #endregion

        #region Account

        private readonly IList<IMenuItem> account = new List<IMenuItem>()
        {
            MenuItem.CreateNew("Aktuella delegeringar", "List", "Delegation", "Practitioner", null, null, Permissions.Delegation.Read, null),
            MenuItem.CreateNew("Alla mottagna delegeringar", "Revision", "Delegation", "Practitioner", null, null, Permissions.Delegation.Revision, null),
            MenuItem.CreateNew("Utställda delegeringar", "Issued", "Delegation", "Practitioner", null, null, Permissions.Delegation.Issued, null),
            MenuItem.CreateNew("Rapporter", "DelegationReport", "Delegation", "Practitioner", null, null, Permissions.Delegation.Report, null)
        };

        #endregion

        #region Schedule 

        /// <summary>
        /// The menu for schedules (Dummy)
        /// </summary>
        private readonly IList<IMenuItem> schedule = new List<IMenuItem>()
        {
            MenuItem.CreateNew("Signeringslista objekt (Dummy)", "Details", "Schedule", "Patient", null, null, Permissions.Schedule.Read, null),
            MenuItem.CreateNew("Iordningsställande (Dummy)", "Schema", "Prepare", "Patient", null, null, Permissions.Prepare.Read, null)
        };

        #endregion

        #region Log

        /// <summary>
        /// The menu for schedules (Dummy)
        /// </summary>
        private readonly IList<IMenuItem> log = new List<IMenuItem>()
        {
            MenuItem.CreateNew("Logg", "List", "Log", "Log", null, null, Permissions.Log.Read, null)
        };

        #endregion

        public static IList<IMenuItem> MainMenu 
        {
            get { return Instance.Main; }
        }
    }
}