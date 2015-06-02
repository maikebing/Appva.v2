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
        #region Admin (Application).

        /// <summary>
        /// The area 51 permissions.
        /// </summary>
        public static class Admin
        {
            /// <summary>
            /// Permission to login.
            /// </summary>
            [Sort(56)]
            [Name("Login MCSS")]
            [Description("Permission to login to MCSS administration")]
            public static readonly IPermission Login = PermissionType.CreateNew("mcss/admin/login");
        }

        /// <summary>
        /// The area 51 permissions.
        /// </summary>
        public static class Device
        {
            /// <summary>
            /// Permission to login.
            /// </summary>
            [Sort(57)]
            [Name("Login MCSS")]
            [Description("Permission to login to MCSS device")]
            public static readonly IPermission Login = PermissionType.CreateNew("mcss/device/login");
        }

        #endregion

        #region Dashboard.

        /// <summary>
        /// The dashboard permissions.
        /// </summary>
        public static class Dashboard
        {
            /// <summary>
            /// Permission to read/view the dashboard.
            /// </summary>
            [Sort(0)]
            [Name("Read dashboard")]
            [Description("Permission to read/view the dashboard")]
            public static readonly IPermission Read = PermissionType.CreateNew("dashboard/read");
        }

        #endregion

        #region Practitioner.

        /// <summary>
        /// The practitioner permissions.
        /// </summary>
        public static class Practitioner
        {
            /// <summary>
            /// Permission to create a practitioner.
            /// </summary>
            [Sort(1)]
            [Name("Create practitioner")]
            [Description("Permission to create a practitioner")]
            public static readonly IPermission Create = PermissionType.CreateNew("practitioner/create");

            /// <summary>
            /// Permission to read/view a practitioner.
            /// </summary>
            [Sort(2)]
            [Name("Read practitioner")]
            [Description("Permission to read/view a practitioner")]
            public static readonly IPermission Read = PermissionType.CreateNew("practitioner/read");

            /// <summary>
            /// Permission to update/edit a practitioner.
            /// </summary>
            [Sort(3)]
            [Name("Update practitioner")]
            [Description("Permission to update/edit a practitioner")]
            public static readonly IPermission Update = PermissionType.CreateNew("practitioner/update");

            /// <summary>
            /// Permission to inactivate a practitioner.
            /// </summary>
            [Sort(4)]
            [Name("Inactivate practitioner")]
            [Description("Permission to inactivate a practitioner")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew("practitioner/inactivate");

            /// <summary>
            /// Permission to reactivate a practitioner.
            /// </summary>
            [Sort(5)]
            [Name("Reactivate practitioner")]
            [Description("Permission to reactivate a practitioner")]
            public static readonly IPermission Reactivate = PermissionType.CreateNew("practitioner/reactivate");

            /// <summary>
            /// Permission to pause a practitioner.
            /// </summary>
            [Sort(6)]
            [Name("Pause practitioner")]
            [Description("Permission to pause a practitioner")]
            public static readonly IPermission Pause = PermissionType.CreateNew("practitioner/pause");

            /// <summary>
            /// Permission to resume a paused practitioner.
            /// </summary>
            [Sort(7)]
            [Name("Resume paused practitioner")]
            [Description("Permission to resume paused practitioner")]
            public static readonly IPermission Resume = PermissionType.CreateNew("practitioner/resume");
        }

        #endregion

        #region Delegation.

        /// <summary>
        /// The delegation permissions.
        /// </summary>
        public static class Delegation
        {
            /// <summary>
            /// Permission to create a delegation.
            /// </summary>
            [Sort(8)]
            [Name("Create delegation")]
            [Description("Permission to create a delegation")]
            public static readonly IPermission Create = PermissionType.CreateNew("practitioner/delegation/create");

            /// <summary>
            /// Permission to read/view a delegation.
            /// </summary>
            [Sort(9)]
            [Name("Read delegation")]
            [Description("Permission to read/view a delegation")]
            public static readonly IPermission Read = PermissionType.CreateNew("practitioner/delegation/read");

            /// <summary>
            /// Permission to update/edit a delegation.
            /// </summary>
            [Sort(10)]
            [Name("Update delegation")]
            [Description("Permission to update/edit a delegation")]
            public static readonly IPermission Update = PermissionType.CreateNew("practitioner/delegation/update");

            /// <summary>
            /// Permission to delete a delegation.
            /// </summary>
            [Sort(11)]
            [Name("Delete delegation")]
            [Description("Permission to delete a delegation")]
            public static readonly IPermission Delete = PermissionType.CreateNew("practitioner/delegation/delete");

            /// <summary>
            /// Permission to read/view a revision.
            /// </summary>
            [Sort(12)]
            [Name("Read delegation revision")]
            [Description("Permission to read/view a delegation revision")]
            public static readonly IPermission Revision = PermissionType.CreateNew("practitioner/delegation/revision/read");

            /// <summary>
            /// Permission to read/view an issued delegation.
            /// </summary>
            [Sort(13)]
            [Name("Read issued delegation")]
            [Description("Permission to read/view an issued delegation")]
            public static readonly IPermission Issued = PermissionType.CreateNew("practitioner/delegation/issued/read");

            /// <summary>
            /// Permission to read/view a delegation report.
            /// </summary>
            [Sort(14)]
            [Name("Read delegation report")]
            [Description("Permission to read/view a delegation report")]
            public static readonly IPermission Report = PermissionType.CreateNew("practitioner/delegation/report/read");
        }

        #endregion

        #region Patient.

        /// <summary>
        /// Patient permissions.
        /// </summary>
        public static class Patient
        {
            /// <summary>
            /// Permission to create a patient.
            /// </summary>
            [Sort(15)]
            [Name("Create patient")]
            [Description("Permission to create a patient")]
            public static readonly IPermission Create = PermissionType.CreateNew("patient/create");

            /// <summary>
            /// Permission to read/view a patient.
            /// </summary>
            [Sort(16)]
            [Name("Read patient")]
            [Description("Permission to read/view a patient")]
            public static readonly IPermission Read = PermissionType.CreateNew("patient/read");

            /// <summary>
            /// Permission to update/edit a patient.
            /// </summary>
            [Sort(17)]
            [Name("Update patient")]
            [Description("Permission to update/edit a patient")]
            public static readonly IPermission Update = PermissionType.CreateNew("patient/update");

            /// <summary>
            /// Permission to inactivate a patient.
            /// </summary>
            [Sort(18)]
            [Name("Inactivate patient")]
            [Description("Permission to inactivate a patient")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew("patient/inactivate");

            /// <summary>
            /// Permission to reactivate a patient.
            /// </summary>
            [Sort(19)]
            [Name("Reactivate patient")]
            [Description("Permission to reactivate a patient")]
            public static readonly IPermission Reactivate = PermissionType.CreateNew("patient/reactivate");
        }

        #endregion

        #region Schedule.

        /// <summary>
        /// The schedule permissions.
        /// </summary>
        public static class Schedule
        {
            /// <summary>
            /// Permission to create a schedule.
            /// </summary>
            [Sort(20)]
            [Name("Create schedule")]
            [Description("Permission to create a schedule")]
            public static readonly IPermission Create = PermissionType.CreateNew("patient/schedule/create");

            /// <summary>
            /// Permission to read/view a schedule.
            /// </summary>
            [Sort(21)]
            [Name("Read schedule")]
            [Description("Permission to read/view a schedule")]
            public static readonly IPermission Read = PermissionType.CreateNew("patient/schedule/read");

            /// <summary>
            /// Permission to uppdate/edit a schedule.
            /// </summary>
            [Sort(22)]
            [Name("Update schedule")]
            [Description("Permission to uppdate/edit a schedule")]
            public static readonly IPermission Update = PermissionType.CreateNew("patient/schedule/update");

            /// <summary>
            /// Permission to inactivate a schedule.
            /// </summary>
            [Sort(23)]
            [Name("Inactivate schedule")]
            [Description("Permission to inactivate a schedule")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew("patient/schedule/inactivate");

            /// <summary>
            /// Permission to print a schedule.
            /// </summary>
            [Sort(24)]
            [Name("Print schedule")]
            [Description("Permission to print a schedule")]
            public static readonly IPermission Print = PermissionType.CreateNew("patient/schedule/print");

            /// <summary>
            /// Permission to read/view the patient reports.
            /// </summary>
            [Sort(25)]
            [Name("Read schedule report")]
            [Description("Permission to read/view the schedule report")]
            public static readonly IPermission Report = PermissionType.CreateNew("patient/schedule/report/read");

            /// <summary>
            /// Permission to read/view a signed event.
            /// </summary>
            [Sort(26)]
            [Name("Read signed event")]
            [Description("Permission to read/view a signed event")]
            public static readonly IPermission EventList = PermissionType.CreateNew("patient/schedule/event/read");
        }

        #endregion

        #region Sequence.

        /// <summary>
        /// The sequence permissions.
        /// </summary>
        public static class Sequence
        {
            /// <summary>
            /// Permission to create a schedule.
            /// </summary>
            [Sort(27)]
            [Name("Create sequence")]
            [Description("Permission to create a sequence")]
            public static readonly IPermission Create = PermissionType.CreateNew("patient/schedule/sequence/create");

            /// <summary>
            /// Permission to read/view a schedule.
            /// </summary>
            [Sort(28)]
            [Name("Read sequence")]
            [Description("Permission to read/view a sequence")]
            public static readonly IPermission Read = PermissionType.CreateNew("patient/schedule/sequence/read");

            /// <summary>
            /// Permission to uppdate/edit a schedule.
            /// </summary>
            [Sort(29)]
            [Name("Update sequence")]
            [Description("Permission to uppdate/edit a sequence")]
            public static readonly IPermission Update = PermissionType.CreateNew("patient/schedule/sequence/update");

            /// <summary>
            /// Permission to inactivate a schedule.
            /// </summary>
            [Sort(30)]
            [Name("Inactivate sequence")]
            [Description("Permission to inactivate a sequence")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew("patient/schedule/sequence/inactivate");

            /// <summary>
            /// Permission to delete a schedule.
            /// </summary>
            [Sort(31)]
            [Name("Print sequence")]
            [Description("Permission to print a sequence")]
            public static readonly IPermission Print = PermissionType.CreateNew("patient/schedule/sequence/print");
        }

        #endregion

        #region Prepared Sequence.

        /// <summary>
        /// The prepared sequence permissions.
        /// </summary>
        public static class Prepare
        {
            /// <summary>
            /// Permission to create a prepared sequence.
            /// </summary>
            [Sort(32)]
            [Name("Create a prepared sequence")]
            [Description("Permission to create a prepared sequence")]
            public static readonly IPermission Create = PermissionType.CreateNew("patient/schedule/sequence/prepare/create");

            /// <summary>
            /// Permission to read/view a prepared sequence.
            /// </summary>
            [Sort(33)]
            [Name("Read prepared sequence")]
            [Description("Permission to read/view a prepared sequence")]
            public static readonly IPermission Read = PermissionType.CreateNew("patient/schedule/sequence/prepare/read");

            /// <summary>
            /// Permission to uppdate/edit a prepared sequence.
            /// </summary>
            [Sort(34)]
            [Name("Update prepared sequence")]
            [Description("Permission to uppdate/edit a prepared sequence")]
            public static readonly IPermission Update = PermissionType.CreateNew("patient/schedule/sequence/prepare/update");

            /// <summary>
            /// Permission to delete a prepared sequence.
            /// </summary>
            [Sort(35)]
            [Name("Delete prepared sequence")]
            [Description("Permission to delete a prepared sequence")]
            public static readonly IPermission Delete = PermissionType.CreateNew("patient/schedule/sequence/prepare/inactivate");
        }

        #endregion

        #region Alerts.

        /// <summary>
        /// The alert permissions.
        /// </summary>
        public static class Alert
        {
            /// <summary>
            /// Permission to read/view alerts.
            /// </summary>
            [Sort(36)]
            [Name("Read alerts")]
            [Description("Permission to read/view alerts")]
            public static readonly IPermission Read = PermissionType.CreateNew("patient/alerts/read");

            /// <summary>
            /// Permission to handle alerts.
            /// </summary>
            [Sort(37)]
            [Name("Handle alerts")]
            [Description("Permission to handle alerts")]
            public static readonly IPermission Handle = PermissionType.CreateNew("patient/alerts/handle");

            /// <summary>
            /// Permission to handle all alerts.
            /// </summary>
            [Sort(38)]
            [Name("Handle all alerts")]
            [Description("Permission to handle all alerts")]
            public static readonly IPermission HandleAll = PermissionType.CreateNew("patient/alerts/handle-all");
        }

        #endregion

        #region Calendar.

        /// <summary>
        /// The calendar permissions.
        /// </summary>
        public static class Calendar
        {
            /// <summary>
            /// Permission to create a calendar event.
            /// </summary>
            [Sort(39)]
            [Name("Create calendar event")]
            [Description("Permission to create a calendar event")]
            public static readonly IPermission Create = PermissionType.CreateNew("patient/calendar/event/create");

            /// <summary>
            /// Permission to read/view calendar events.
            /// </summary>
            [Sort(40)]
            [Name("Read calendar events")]
            [Description("Permission to read/view calendar events")]
            public static readonly IPermission Read = PermissionType.CreateNew("patient/calendar/event/read");

            /// <summary>
            /// Permission to uppdate/edit a calendar event.
            /// </summary>
            [Sort(41)]
            [Name("Update calendar event")]
            [Description("Permission to uppdate/edit a calendar event")]
            public static readonly IPermission Update = PermissionType.CreateNew("patient/calendar/event/update");

            /// <summary>
            /// Permission to delete a calendar event.
            /// </summary>
            [Sort(42)]
            [Name("Delete calendar event")]
            [Description("Permission to delete a calendar event")]
            public static readonly IPermission Delete = PermissionType.CreateNew("patient/calendar/event/delete");
        }

        #endregion

        #region Inventory.

        /// <summary>
        /// The inventory permissions.
        /// </summary>
        public static class Inventory
        {
            /// <summary>
            /// Permission to create an inventory.
            /// </summary>
            [Sort(43)]
            [Name("Create inventory")]
            [Description("Permission to create an inventory")]
            public static readonly IPermission Create = PermissionType.CreateNew("patient/inventory/create");

            /// <summary>
            /// Permission to read/view an inventory.
            /// </summary>
            [Sort(44)]
            [Name("Read inventory")]
            [Description("Permission to read/view an inventory")]
            public static readonly IPermission Read = PermissionType.CreateNew("patient/inventory/read");

            /// <summary>
            /// Permission to uppdate/edit an inventory.
            /// </summary>
            [Sort(45)]
            [Name("Update inventory")]
            [Description("Permission to uppdate/edit an inventory")]
            public static readonly IPermission Update = PermissionType.CreateNew("patient/inventory/update");

            /// <summary>
            /// Permission to delete an inventory.
            /// </summary>
            [Sort(46)]
            [Name("Delete inventory")]
            [Description("Permission to delete an inventory")]
            public static readonly IPermission Delete = PermissionType.CreateNew("patient/inventory/delete");
        }

        #endregion

        #region Notification.

        /// <summary>
        /// The notification permissions.
        /// </summary>
        public static class Notification
        {
            /// <summary>
            /// Permission to create a notification.
            /// </summary>
            [Sort(47)]
            [Name("Create notification")]
            [Description("Permission to create a notification")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Create = PermissionType.CreateNew("notification/create");
            
            /// <summary>
            /// Permission to read/view notifications.
            /// </summary>
            [Sort(48)]
            [Name("Read notifications")]
            [Description("Permission to read/view notifications")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Read = PermissionType.CreateNew("notification/read");
            
            /// <summary>
            /// Permission to update/edit a notification.
            /// </summary>
            [Sort(49)]
            [Name("Update notifications")]
            [Description("Permission to update/edit a notification")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Update = PermissionType.CreateNew("notification/update");

            /// <summary>
            /// Permission to delete a notification.
            /// </summary>
            [Sort(50)]
            [Name("Delete notification")]
            [Description("Permission to delete a notification")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Delete = PermissionType.CreateNew("notification/delete");
        }

        #endregion

        #region Roles.

        /// <summary>
        /// The role permissions.
        /// </summary>
        public static class Role
        {
            /// <summary>
            /// Permission to create a role.
            /// </summary>
            [Sort(51)]
            [Name("Create role")]
            [Description("Permission to create a role")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Create = PermissionType.CreateNew("practitioner/role/create");

            /// <summary>
            /// Permission to read/view roles.
            /// </summary>
            [Sort(52)]
            [Name("Read roles")]
            [Description("Permission to read/view roles")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Read = PermissionType.CreateNew("practitioner/role/read");

            /// <summary>
            /// Permission to update/edit a role.
            /// </summary>
            [Sort(53)]
            [Name("Update role")]
            [Description("Permission to update/edit a role")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Update = PermissionType.CreateNew("practitioner/role/update");

            /// <summary>
            /// Permission to delete a role.
            /// </summary>
            [Sort(54)]
            [Name("Delete role")]
            [Description("Permission to delete a role")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Delete = PermissionType.CreateNew("practitioner/role/delete");
        }

        #endregion

        #region Area51.

        /// <summary>
        /// The area 51 permissions.
        /// </summary>
        public static class Area51
        {
            /// <summary>
            /// Permission to read/view area51.
            /// </summary>
            [Sort(55)]
            [Name("Read area51")]
            [Description("Permission to read/view area51")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Read = PermissionType.CreateNew("area51/read");
        }

        #endregion
    }

    /// <summary>
    /// The <see cref="IPermission"/> implementation.
    /// TODO: Change name to e.g. ClaimPermission, SecurityPermission or InternalPermission
    /// </summary>
    internal sealed class PermissionType : IPermission
    {
        #region Variables.

        /// <summary>
        /// The permission schema.
        /// </summary>
        private const string Schema = "https://schema.appva.se/permission/{0}";

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionType"/> class.
        /// </summary>
        /// <param name="value">The value</param>
        private PermissionType(string value)
        {
            this.Key = ClaimTypes.Permission; 
            this.Value = string.Format(Schema, value);
        }

        #endregion

        #region Internal Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationSettingIdentity"/> class.
        /// </summary>
        /// <param name="value">The value</param>
        internal static IPermission CreateNew(string value)
        {
            return new PermissionType(value);
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

        #endregion
    }
}