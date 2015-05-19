// <copyright file="Permissions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Contracts.Permissions;
    using Appva.Core.Resources;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class Permissions
    {
        #region Create.

        /// <summary>
        /// The create permissions.
        /// </summary>
        public static class Create
        {
            /// <summary>
            /// Permission to create a calendar event.
            /// </summary>
            public static readonly IPermission CalendarEvent = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/calendar/event/create",
                "Create calendar event",
                "Permission to create a calendar event");

            /// <summary>
            /// Permission to create a delegation.
            /// </summary>
            public static readonly IPermission Delegation = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/delegation/create",
                "Create delegation",
                "Permission to create a delegation");

            /// <summary>
            /// Permission to create an inventory.
            /// </summary>
            public static readonly IPermission Inventory = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/inventory/create",
                "Create inventory",
                "Permission to create an inventory");

            /// <summary>
            /// Permission to create a notification.
            /// </summary>
            public static readonly IPermission Notification = PermissionType.CreateNew(
                "https://schema.appva.se/permission/notification/create",
                "Create notification",
                "Permission to create a notification");

            /// <summary>
            /// Permission to create a patient.
            /// </summary>
            public static readonly IPermission Patient = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/create",
                "Create patient",
                "Permission to create a patient");

            /// <summary>
            /// Permission to create a practitioner.
            /// </summary>
            public static readonly IPermission Practitioner = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/create",
                "Create practitioner",
                "Permission to create a practitioner");

            /// <summary>
            /// Permission to create a role.
            /// </summary>
            public static readonly IPermission Role = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/role/create",
                "Create role",
                "Permission to create a role");

            /// <summary>
            /// Permission to create a schedule.
            /// </summary>
            public static readonly IPermission Schedule = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/schedule/create",
                "Create schedule",
                "Permission to create a schedule");
        }

        #endregion

        #region Read.

        /// <summary>
        /// The read permissions.
        /// </summary>
        public static class Read
        {
            /// <summary>
            /// Permission to read/view alert list.
            /// </summary>
            public static readonly IPermission Alert = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/alerts/read",
                "Read alerts",
                "Permission to read/view alert list");

            /// <summary>
            /// Permission to read/view area51.
            /// </summary>
            public static readonly IPermission Area51 = PermissionType.CreateNew(
                "https://schema.appva.se/permission/area51/read",
                "Read area51",
                "Permission to read/view area51");

            /// <summary>
            /// Permission to read/view calendar events.
            /// </summary>
            public static readonly IPermission CalendarEvent = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/calendar/event/read",
                "Read calendar events",
                "Permission to read/view calendar events");

            /// <summary>
            /// Permission to read/view the dashboard.
            /// </summary>
            public static readonly IPermission Dashboard = PermissionType.CreateNew(
                "https://schema.appva.se/permission/dashboard/read",
                "Read dashboard",
                "Permission to read/view the dashboard");

            /// <summary>
            /// Permission to read/view a delegation.
            /// </summary>
            public static readonly IPermission Delegation = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/delegation/read",
                "Read delegation",
                "Permission to read/view a delegation");

            /// <summary>
            /// Permission to read/view a signed event.
            /// </summary>
            public static readonly IPermission EventList = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/schedule/event/read",
                "Read signed event",
                "Permission to read/view a signed event");

            /// <summary>
            /// Permission to read/view an inventory.
            /// </summary>
            public static readonly IPermission Inventory = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/inventory/read",
                "Read inventory",
                "Permission to read/view an inventory");

            /// <summary>
            /// Permission to read/view an issued delegation.
            /// </summary>
            public static readonly IPermission IssuedDelegation = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/delegation/issued/read",
                "Read issued delegation",
                "Permission to read/view an issued delegation");

            /// <summary>
            /// Permission to read/view notifications.
            /// </summary>
            public static readonly IPermission Notification = PermissionType.CreateNew(
                "https://schema.appva.se/permission/notification/read",
                "Read issued notifications",
                "Permission to read/view notifications");

            /// <summary>
            /// Permission to read/view a patient.
            /// </summary>
            public static readonly IPermission Patient = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/read",
                "Read patient",
                "Permission to read/view a patient");

            /// <summary>
            /// Permission to read/view a practitioner.
            /// </summary>
            public static readonly IPermission Practitioner = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/read",
                "Read practitioner",
                "Permission to read/view a practitioner");

            /// <summary>
            /// Permission to read/view a practitioner report.
            /// </summary>
            public static readonly IPermission PractitionerReport = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/report/read",
                "Read practitioner report",
                "Permission to read/view a practitioner report");

            /// <summary>
            /// Permission to read/view a revision.
            /// </summary>
            public static readonly IPermission Revision = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/delegation/revision/read",
                "Read revision",
                "Permission to read/view a revision");

            /// <summary>
            /// Permission to read/view roles.
            /// </summary>
            public static readonly IPermission Role = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/role/read",
                "Read roles",
                "Permission to read/view roles");

            /// <summary>
            /// Permission to read/view a schedule.
            /// </summary>
            public static readonly IPermission Schedule = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/schedule/read",
                "Read schedule",
                "Permission to read/view a schedule");

            /// <summary>
            /// Permission to read/view the patient reports.
            /// </summary>
            public static readonly IPermission ScheduleReport = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/schedule/report/read",
                "Read patient reports",
                "Permission to read/view the patient reports");
        }

        #endregion

        #region Update.

        /// <summary>
        /// The update permissions.
        /// </summary>
        public static class Update
        {
            /// <summary>
            /// Permission to update/edit an alert.
            /// </summary>
            public static readonly IPermission Alert = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/alerts/update",
                "Update alert",
                "Permission to update/edit an alert");

            /// <summary>
            /// Permission to uppdate/edit a calendar event.
            /// </summary>
            public static readonly IPermission CalendarEvent = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/calendar/event/update",
                "Update calendar event",
                "Permission to uppdate/edit a calendar event");

            /// <summary>
            /// Permission to update/edit a delegation.
            /// </summary>
            public static readonly IPermission Delegation = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/delegation/update",
                "Update delegation",
                "Permission to update/edit a delegation");

            /// <summary>
            /// Permission to uppdate/edit an inventory.
            /// </summary>
            public static readonly IPermission Inventory = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/inventory/update",
                "Update inventory",
                "Permission to uppdate/edit an inventory");

            /// <summary>
            /// Permission to update/edit a notification.
            /// </summary>
            public static readonly IPermission Notification = PermissionType.CreateNew(
                "https://schema.appva.se/permission/notification/update",
                "Update notification",
                "Permission to update/edit a notification");

            /// <summary>
            /// Permission to update/edit a patient.
            /// </summary>
            public static readonly IPermission Patient = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/update",
                "Update patient",
                "Permission to update/edit a patient");

            /// <summary>
            /// Permission to update/edit a practitioner.
            /// </summary>
            public static readonly IPermission Practitioner = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/update",
                "Update practitioner",
                "Permission to update/edit a practitioner");

            /// <summary>
            /// Permission to update/edit a role.
            /// </summary>
            public static readonly IPermission Role = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/role/update",
                "Update role",
                "Permission to update/edit a role");

            /// <summary>
            /// Permission to uppdate/edit a schedule.
            /// </summary>
            public static readonly IPermission Schedule = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/schedule/update",
                "Update role",
                "Permission to uppdate/edit a schedule");
        }

        #endregion

        #region Delete.

        /// <summary>
        /// The delete permissions.
        /// </summary>
        public static class Delete
        {
            /// <summary>
            /// Permission to delete a calendar event.
            /// </summary>
            public static readonly IPermission CalendarEvent = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/calendar/event/delete",
                "Delete calendar event",
                "Permission to delete a calendar event");

            /// <summary>
            /// Permission to delete a delegation.
            /// </summary>
            public static readonly IPermission Delegation = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/delegation/delete",
                "Delete delegation",
                "Permission to delete a delegation");

            /// <summary>
            /// Permission to delete an inventory.
            /// </summary>
            public static readonly IPermission Inventory = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/inventory/delete",
                "Delete inventory",
                "Permission to delete an inventory");

            /// <summary>
            /// Permission to delete a notification.
            /// </summary>
            public static readonly IPermission Notification = PermissionType.CreateNew(
                "https://schema.appva.se/permission/notification/delete",
                "Delete notification",
                "Permission to delete a notification");

            /// <summary>
            /// Permission to delete a patient.
            /// </summary>
            public static readonly IPermission Patient = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/delete",
                "Delete patient",
                "Permission to delete a patient");

            /// <summary>
            /// Permission to delete a practitioner.
            /// </summary>
            public static readonly IPermission Practitioner = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/delete",
                "Delete practitioner",
                "Permission to delete a practitioner");

            /// <summary>
            /// Permission to delete a role.
            /// </summary>
            public static readonly IPermission Role = PermissionType.CreateNew(
                "https://schema.appva.se/permission/practitioner/role/delete",
                "Delete role",
                "Permission to delete a role");

            /// <summary>
            /// Permission to delete a schedule.
            /// </summary>
            public static readonly IPermission Schedule = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/schedule/delete",
                "Delete schedule",
                "Permission to delete a schedule");
        }

        #endregion

        #region Print.

        /// <summary>
        /// The print permissions.
        /// </summary>
        public static class Print
        {
            /// <summary>
            /// Permission to print sequences.
            /// </summary>
            public static readonly IPermission Sequence = PermissionType.CreateNew(
                "https://schema.appva.se/permission/patient/sequence/print",
                "Print sequences",
                "Permission to print sequences");
        }

        #endregion
    }

    /// <summary>
    /// The <see cref="IPermission"/> implementation.
    /// </summary>
    internal sealed class PermissionType : IPermission
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionType"/> class.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="name">The friendly name</param>
        /// <param name="description">The description of usage</param>
        private PermissionType(string value, string name, string description)
        {
            this.Key = ClaimTypes.Permission;
            this.Value = value;
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Internal Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationSettingIdentity"/> class.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="name">The friendly name</param>
        /// <param name="context">The namespace or context</param>
        /// <param name="description">The description of usage</param>
        internal static IPermission CreateNew(string value, string name, string description)
        {
            return new PermissionType(value, name, description);
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The unique key.
        /// </summary>
        public string Key
        {
            get;
            private set;
        }

        /// <summary>
        /// The value.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        /// <summary>
        /// The friendly name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The description of the permission usage.
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        #endregion
    }
}