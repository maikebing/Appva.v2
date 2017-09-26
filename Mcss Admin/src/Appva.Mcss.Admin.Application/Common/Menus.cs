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
        #region Instance.

        /// <summary>
        /// The instance of <see cref="Menus"/>
        /// </summary>
        private static Menus instance;

        /// <summary>
        /// Getter for the instance
        /// </summary>
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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of <see cref="Menus"/>
        /// </summary>
        private Menus()
        { }

        #endregion

        #region Menus.

        #region Main

        /// <summary>
        /// The main menu field
        /// </summary>
        private IList<IMenuItem> main;

        /// <summary>
        /// The main menu getter
        /// </summary>
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
                        MenuItem.CreateNew("Medarbetare", "List", "Accounts", "Practitioner", null, null, Permissions.Practitioner.Read, this.Account ),
                        MenuItem.CreateNew("Roller och behörigheter", "List", "Roles", "Roles", null, null, Permissions.Role.Read, null),
                        MenuItem.CreateNew("Logg", "List", "Log", "Log", null, null, Permissions.Log.Read, this.Log),
                        MenuItem.CreateNew("Notiser", "List", "Notification", "Notification", null, null, Permissions.Notification.Read, null),
                        MenuItem.CreateNew("Area51", "Index", "Home", "Area51", null, null, Permissions.Area51.Read, null),
                        MenuItem.CreateNew("Administration", "List", "Delegation", "backoffice", null, null, Permissions.Backoffice.Read, null),
                        MenuItem.CreateNew("Skriv ut sidan", string.Empty, string.Empty, string.Empty, "supp", "print", null, null)
                    };
                }
                return this.main;
            }
        }

        #endregion

        #region Patient

        /// <summary>
        /// The patient-menu field
        /// </summary>
        private IList<IMenuItem> patient;

        /// <summary>
        /// The getter for the menu
        /// </summary>
        private IList<IMenuItem> Patient
        {
            get
            {
                if (this.patient == null)
                {
                    this.patient = new List<IMenuItem>()
                    {
                         MenuItem.CreateNew("Signeringslistor", "List", "Schedule", "Patient", null, null, Permissions.Schedule.Read, this.Schedule),
                         MenuItem.CreateNew("Läkemedelslista", "List", "Medication", "Patient", null, null, Permissions.Medication.Read, this.Medication),
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

        /// <summary>
        /// The account field
        /// </summary>
        private IList<IMenuItem> account;

        /// <summary>
        /// The account getter
        /// </summary>
        private IList<IMenuItem> Account
        {
            get
            {
                if (this.account == null)
                {
                    this.account = new List<IMenuItem>()
                    {
                        MenuItem.CreateNew("Aktuella delegeringar", "List", "Delegation", "Practitioner", null, null, Permissions.Delegation.Read, null),
                        MenuItem.CreateNew("Alla mottagna delegeringar", "Revision", "Delegation", "Practitioner", null, null, Permissions.Delegation.Revision, null),
                        MenuItem.CreateNew("Utställda delegeringar", "Issued", "Delegation", "Practitioner", null, null, Permissions.Delegation.Issued, null),
                        MenuItem.CreateNew("Rapporter", "DelegationReport", "Delegation", "Practitioner", null, null, Permissions.Delegation.Report, null),
                        MenuItem.CreateNew("Synkronisering", "GetSynchronizedAccount","Synchronization","Practitioner", null, null, Permissions.Synchronization.Read, null)
                    };
                }
                return this.account;
            }
        }

        #endregion

        #region Schedule 

        /// <summary>
        /// The menu for schedules (Dummy) field
        /// </summary>
        private IList<IMenuItem> schedule;

        /// <summary>
        /// The schedule-menu getter
        /// </summary>
        private IList<IMenuItem> Schedule
        {
            get
            {
                if (this.schedule == null)
                {
                    this.schedule = new List<IMenuItem>()
                    {
                        MenuItem.CreateNew("Signeringslista objekt (Dummy)", "Details", "Schedule", "Patient", null, null, Permissions.Schedule.Read, null),
                        MenuItem.CreateNew("Iordningsställande (Dummy)", "Schema", "Prepare", "Patient", null, null, Permissions.Prepare.Read, null)
                    };
                }
                return this.schedule;
            }
        }


        #endregion

        #region Medication

        /// <summary>
        /// The menu for schedules (Dummy) field
        /// </summary>
        private IList<IMenuItem> medication;

        /// <summary>
        /// The schedule-menu getter
        /// </summary>
        private IList<IMenuItem> Medication
        {
            get
            {
                if (this.medication == null)
                {
                    this.medication = new List<IMenuItem>()
                    {
                        MenuItem.CreateNew("Detaljer läkemedel (Dummy)", "Details", "Medication", "Patient", null, null, Permissions.Medication.Read, null)
                    };
                }
                return this.medication;
            }
        }


        #endregion

        #region Log

        /// <summary>
        /// The log-menu field
        /// </summary>
        private IList<IMenuItem> log;

        /// <summary>
        /// The log-menu getter
        /// </summary>
        private IList<IMenuItem> Log
        {
            get
            {
                if (this.log == null)
                {
                    this.log = new List<IMenuItem>()
                    {
                        MenuItem.CreateNew("Logg", "List", "Log", "Log", null, null, Permissions.Log.Read, null)
                    };
                }
                return this.log;
            }
        }

        #endregion

        #endregion

        #region Static public properties.

        /// <summary>
        /// Returns the main-menu
        /// </summary>
        public static IList<IMenuItem> MainMenu 
        {
            get { return Instance.Main; }
        }

        #endregion
    }
}