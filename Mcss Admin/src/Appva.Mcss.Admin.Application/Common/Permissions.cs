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
        #region Constants.

        /// <summary>
        /// The permission schema.
        /// </summary>
        private const string Schema = "https://schemas.appva.se/2015/04/permission/";

        #endregion

        #region Admin (Application).

        /// <summary>
        /// The area 51 permissions.
        /// </summary>
        public static class Admin
        {
            /// <summary>
            /// The login value.
            /// </summary>
            public const string LoginValue = Schema + "web/admin";

            /// <summary>
            /// Permission to login.
            /// </summary>
            [Sort(56)]
            [Name("Login MCSS")]
            [Description("Permission to login to MCSS administration")]
            [Key("https://schema.appva.se/permission/mcss/admin/login")]
            public static readonly IPermission Login = PermissionType.CreateNew(LoginValue);
        }

        /// <summary>
        /// The area 51 permissions.
        /// </summary>
        public static class Device
        {
            /// <summary>
            /// The login value.
            /// </summary>
            public const string LoginValue = Schema + "web/device";

            /// <summary>
            /// Permission to login.
            /// </summary>
            [Sort(57)]
            [Name("Login MCSS")]
            [Description("Permission to login to MCSS device")]
            [Key("https://schema.appva.se/permission/mcss/device/login")]
            public static readonly IPermission Login = PermissionType.CreateNew(LoginValue);
        }

        #endregion

        #region Dashboard.

        /// <summary>
        /// The dashboard permissions.
        /// </summary>
        public static class Dashboard
        {
            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "dashboard/read";

            /// <summary>
            /// Permission to read/view the dashboard.
            /// </summary>
            [Sort(0)]
            [Name("Read dashboard")]
            [Description("Permission to read/view the dashboard")]
            [Key("https://schema.appva.se/permission/dashboard/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);
        }

        #endregion

        #region Practitioner.

        /// <summary>
        /// The practitioner permissions.
        /// </summary>
        public static class Practitioner
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "practitioner/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "practitioner/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "practitioner/update";

            /// <summary>
            /// The update role value.
            /// </summary>
            public const string UpdateRolesValue = Schema + "practitioner/update/role";

            /// <summary>
            /// The inactivate value.
            /// </summary>
            public const string InactivateValue = Schema + "practitioner/inactivate";

            /// <summary>
            /// The reactivate value.
            /// </summary>
            public const string ReactivateValue = Schema + "practitioner/reactivate";

            /// <summary>
            /// The pause value.
            /// </summary>
            public const string PauseValue = Schema + "practitioner/pause";

            /// <summary>
            /// The resume value.
            /// </summary>
            public const string ResumeValue = Schema + "practitioner/resume";

            /// <summary>
            /// Permission to create a practitioner.
            /// </summary>
            [Sort(1)]
            [Name("Create practitioner")]
            [Description("Permission to create a practitioner")]
            [Key("https://schema.appva.se/permission/practitioner/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a practitioner.
            /// </summary>
            [Sort(2)]
            [Name("Read practitioner")]
            [Description("Permission to read/view a practitioner")]
            [Key("https://schema.appva.se/permission/practitioner/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a practitioner.
            /// </summary>
            [Sort(3)]
            [Name("Update practitioner")]
            [Description("Permission to update/edit a practitioner")]
            [Key("https://schema.appva.se/permission/practitioner/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to update/edit a practitioner.
            /// </summary>
            [New]
            [Sort(3)]
            [Name("Update practitioner roles")]
            [Description("Permission to update/edit practitioner roles")]
            public static readonly IPermission UpdateRoles = PermissionType.CreateNew(UpdateRolesValue);

            /// <summary>
            /// Permission to inactivate a practitioner.
            /// </summary>
            [Sort(4)]
            [Name("Inactivate practitioner")]
            [Description("Permission to inactivate a practitioner")]
            [Key("https://schema.appva.se/permission/practitioner/inactivate")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew(InactivateValue);

            /// <summary>
            /// Permission to reactivate a practitioner.
            /// </summary>
            [Sort(5)]
            [Name("Reactivate practitioner")]
            [Description("Permission to reactivate a practitioner")]
            [Key("https://schema.appva.se/permission/practitioner/reactivate")]
            public static readonly IPermission Reactivate = PermissionType.CreateNew(ReactivateValue);

            /// <summary>
            /// Permission to pause a practitioner.
            /// </summary>
            [Sort(6)]
            [Name("Pause practitioner")]
            [Description("Permission to pause a practitioner")]
            [Key("https://schema.appva.se/permission/practitioner/pause")]
            public static readonly IPermission Pause = PermissionType.CreateNew(PauseValue);

            /// <summary>
            /// Permission to resume a paused practitioner.
            /// </summary>
            [Sort(7)]
            [Name("Resume paused practitioner")]
            [Description("Permission to resume paused practitioner")]
            [Key("https://schema.appva.se/permission/practitioner/resume")]
            public static readonly IPermission Resume = PermissionType.CreateNew(ResumeValue);
        }

        #endregion

        #region Delegation.

        /// <summary>
        /// The delegation permissions.
        /// </summary>
        public static class Delegation
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "practitioner/delegation/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "practitioner/delegation/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "practitioner/delegation/update";

            /// <summary>
            /// The delete value.
            /// </summary>
            public const string DeleteValue = Schema + "practitioner/delegation/delete";

            /// <summary>
            /// The revision read value.
            /// </summary>
            public const string RevisionValue = Schema + "practitioner/delegation/revision/read";

            /// <summary>
            /// The issued read value.
            /// </summary>
            public const string IssuedValue = Schema + "practitioner/delegation/issued/read";

            /// <summary>
            /// The report read value.
            /// </summary>
            public const string ReportValue = Schema + "practitioner/delegation/report/read";

            /// <summary>
            /// Permission to create a delegation.
            /// </summary>
            [Sort(8)]
            [Name("Create delegation")]
            [Description("Permission to create a delegation")]
            [Key("https://schema.appva.se/permission/practitioner/delegation/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a delegation.
            /// </summary>
            [Sort(9)]
            [Name("Read delegation")]
            [Description("Permission to read/view a delegation")]
            [Key("https://schema.appva.se/permission/practitioner/delegation/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a delegation.
            /// </summary>
            [Sort(10)]
            [Name("Update delegation")]
            [Description("Permission to update/edit a delegation")]
            [Key("https://schema.appva.se/permission/practitioner/delegation/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a delegation.
            /// </summary>
            [Sort(11)]
            [Name("Delete delegation")]
            [Description("Permission to delete a delegation")]
            [Key("https://schema.appva.se/permission/practitioner/delegation/delete")]
            public static readonly IPermission Delete = PermissionType.CreateNew(DeleteValue);

            /// <summary>
            /// Permission to read/view a revision.
            /// </summary>
            [Sort(12)]
            [Name("Read delegation revision")]
            [Description("Permission to read/view a delegation revision")]
            [Key("https://schema.appva.se/permission/practitioner/delegation/revision/read")]
            public static readonly IPermission Revision = PermissionType.CreateNew(RevisionValue);

            /// <summary>
            /// Permission to read/view an issued delegation.
            /// </summary>
            [Sort(13)]
            [Name("Read issued delegation")]
            [Description("Permission to read/view an issued delegation")]
            [Key("https://schema.appva.se/permission/practitioner/delegation/issued/read")]
            public static readonly IPermission Issued = PermissionType.CreateNew(IssuedValue);

            /// <summary>
            /// Permission to read/view a delegation report.
            /// </summary>
            [Sort(14)]
            [Name("Read delegation report")]
            [Description("Permission to read/view a delegation report")]
            [Key("https://schema.appva.se/permission/practitioner/delegation/report/read")]
            public static readonly IPermission Report = PermissionType.CreateNew(ReportValue);
        }

        #endregion

        #region Patient.

        /// <summary>
        /// Patient permissions.
        /// </summary>
        public static class Patient
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "patient/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "patient/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "patient/update";

            /// <summary>
            /// The inactivate value.
            /// </summary>
            public const string InactivateValue = Schema + "patient/inactivate";

            /// <summary>
            /// The reactivate value.
            /// </summary>
            public const string ReactivateValue = Schema + "patient/reactivate";

            /// <summary>
            /// Permission to create a patient.
            /// </summary>
            [Sort(15)]
            [Name("Create patient")]
            [Description("Permission to create a patient")]
            [Key("https://schema.appva.se/permission/patient/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a patient.
            /// </summary>
            [Sort(16)]
            [Name("Read patient")]
            [Description("Permission to read/view a patient")]
            [Key("https://schema.appva.se/permission/patient/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a patient.
            /// </summary>
            [Sort(17)]
            [Name("Update patient")]
            [Description("Permission to update/edit a patient")]
            [Key("https://schema.appva.se/permission/patient/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to inactivate a patient.
            /// </summary>
            [Sort(18)]
            [Name("Inactivate patient")]
            [Description("Permission to inactivate a patient")]
            [Key("https://schema.appva.se/permission/patient/inactivate")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew(InactivateValue);

            /// <summary>
            /// Permission to reactivate a patient.
            /// </summary>
            [Sort(19)]
            [Name("Reactivate patient")]
            [Description("Permission to reactivate a patient")]
            [Key("https://schema.appva.se/permission/patient/reactivate")]
            public static readonly IPermission Reactivate = PermissionType.CreateNew(ReactivateValue);
        }

        #endregion

        #region Schedule.

        /// <summary>
        /// The schedule permissions.
        /// </summary>
        public static class Schedule
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "patient/schedule/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "patient/schedule/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "patient/schedule/update";

            /// <summary>
            /// The inactivate value.
            /// </summary>
            public const string InactivateValue = Schema + "patient/schedule/inactivate";

            /// <summary>
            /// The print value.
            /// </summary>
            public const string PrintValue = Schema + "patient/schedule/print";

            /// <summary>
            /// The report read value.
            /// </summary>
            public const string ReportValue = Schema + "patient/schedule/report/read";

            /// <summary>
            /// The event read value.
            /// </summary>
            public const string EventListValue = Schema + "patient/schedule/event/read";

            /// <summary>
            /// Permission to create a schedule.
            /// </summary>
            [Sort(20)]
            [Name("Create schedule")]
            [Description("Permission to create a schedule")]
            [Key("https://schema.appva.se/permission/patient/schedule/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a schedule.
            /// </summary>
            [Sort(21)]
            [Name("Read schedule")]
            [Description("Permission to read/view a schedule")]
            [Key("https://schema.appva.se/permission/patient/schedule/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit a schedule.
            /// </summary>
            [Sort(22)]
            [Name("Update schedule")]
            [Description("Permission to uppdate/edit a schedule")]
            [Key("https://schema.appva.se/permission/patient/schedule/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to inactivate a schedule.
            /// </summary>
            [Sort(23)]
            [Name("Inactivate schedule")]
            [Description("Permission to inactivate a schedule")]
            [Key("https://schema.appva.se/permission/patient/schedule/inactivate")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew(InactivateValue);

            /// <summary>
            /// Permission to print a schedule.
            /// </summary>
            [Sort(24)]
            [Name("Print schedule")]
            [Description("Permission to print a schedule")]
            [Key("https://schema.appva.se/permission/patient/schedule/print")]
            public static readonly IPermission Print = PermissionType.CreateNew(PrintValue);

            /// <summary>
            /// Permission to read/view the patient reports.
            /// </summary>
            [Sort(25)]
            [Name("Read schedule report")]
            [Description("Permission to read/view the schedule report")]
            [Key("https://schema.appva.se/permission/patient/schedule/report/read")]
            public static readonly IPermission Report = PermissionType.CreateNew(ReportValue);

            /// <summary>
            /// Permission to read/view a signed event.
            /// </summary>
            [Sort(26)]
            [Name("Read signed event")]
            [Description("Permission to read/view a signed event")]
            [Key("https://schema.appva.se/permission/patient/schedule/event/read")]
            public static readonly IPermission EventList = PermissionType.CreateNew(EventListValue);
        }

        #endregion

        #region Sequence.

        /// <summary>
        /// The sequence permissions.
        /// </summary>
        public static class Sequence
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "patient/schedule/sequence/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "patient/schedule/sequence/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "patient/schedule/sequence/update";

            /// <summary>
            /// The inactivate value.
            /// </summary>
            public const string InactivateValue = Schema + "patient/schedule/sequence/inactivate";

            /// <summary>
            /// The print value.
            /// </summary>
            public const string PrintValue = Schema + "patient/schedule/sequence/print";

            /// <summary>
            /// Permission to create a schedule.
            /// </summary>
            [Sort(27)]
            [Name("Create sequence")]
            [Description("Permission to create a sequence")]
            [Key("https://schema.appva.se/permission/patient/schedule/sequence/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a schedule.
            /// </summary>
            [Sort(28)]
            [Name("Read sequence")]
            [Description("Permission to read/view a sequence")]
            [Key("https://schema.appva.se/permission/patient/schedule/sequence/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit a schedule.
            /// </summary>
            [Sort(29)]
            [Name("Update sequence")]
            [Description("Permission to uppdate/edit a sequence")]
            [Key("https://schema.appva.se/permission/patient/schedule/sequence/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to inactivate a schedule.
            /// </summary>
            [Sort(30)]
            [Name("Inactivate sequence")]
            [Description("Permission to inactivate a sequence")]
            [Key("https://schema.appva.se/permission/patient/schedule/sequence/inactivate")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew(InactivateValue);

            /// <summary>
            /// Permission to delete a schedule.
            /// </summary>
            [Sort(31)]
            [Name("Print sequence")]
            [Description("Permission to print a sequence")]
            [Key("https://schema.appva.se/permission/patient/schedule/sequence/print")]
            public static readonly IPermission Print = PermissionType.CreateNew(PrintValue);
        }

        #endregion

        #region Prepared Sequence.

        /// <summary>
        /// The prepared sequence permissions.
        /// </summary>
        public static class Prepare
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "patient/schedule/sequence/prepare/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "patient/schedule/sequence/prepare/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "patient/schedule/sequence/prepare/update";

            /// <summary>
            /// The inactivate value.
            /// </summary>
            public const string InactivateValue = Schema + "patient/schedule/sequence/prepare/inactivate";

            /// <summary>
            /// Permission to create a prepared sequence.
            /// </summary>
            [Sort(32)]
            [Name("Create a prepared sequence")]
            [Description("Permission to create a prepared sequence")]
            [Key("https://schema.appva.se/permission/patient/schedule/sequence/prepare/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a prepared sequence.
            /// </summary>
            [Sort(33)]
            [Name("Read prepared sequence")]
            [Description("Permission to read/view a prepared sequence")]
            [Key("https://schema.appva.se/permission/patient/schedule/sequence/prepare/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit a prepared sequence.
            /// </summary>
            [Sort(34)]
            [Name("Update prepared sequence")]
            [Description("Permission to uppdate/edit a prepared sequence")]
            [Key("https://schema.appva.se/permission/patient/schedule/sequence/prepare/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a prepared sequence.
            /// </summary>
            [Sort(35)]
            [Name("Delete prepared sequence")]
            [Description("Permission to delete a prepared sequence")]
            [Key("https://schema.appva.se/permission/patient/schedule/sequence/prepare/inactivate")]
            public static readonly IPermission Delete = PermissionType.CreateNew(InactivateValue);
        }

        #endregion

        #region Alerts.

        /// <summary>
        /// The alert permissions.
        /// </summary>
        public static class Alert
        {
            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "patient/alerts/read";

            /// <summary>
            /// The handle value.
            /// </summary>
            public const string HandleValue = Schema + "patient/alerts/handle";

            /// <summary>
            /// The handle all value.
            /// </summary>
            public const string HandleAllValue = Schema + "patient/alerts/handle-all";

            /// <summary>
            /// Permission to read/view alerts.
            /// </summary>
            [Sort(36)]
            [Name("Read alerts")]
            [Description("Permission to read/view alerts")]
            [Key("https://schema.appva.se/permission/patient/alerts/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to handle alerts.
            /// </summary>
            [Sort(37)]
            [Name("Handle alerts")]
            [Description("Permission to handle alerts")]
            [Key("https://schema.appva.se/permission/patient/alerts/handle")]
            public static readonly IPermission Handle = PermissionType.CreateNew(HandleValue);

            /// <summary>
            /// Permission to handle all alerts.
            /// </summary>
            [Sort(38)]
            [Name("Handle all alerts")]
            [Description("Permission to handle all alerts")]
            [Key("https://schema.appva.se/permission/patient/alerts/handle-all")]
            public static readonly IPermission HandleAll = PermissionType.CreateNew(HandleAllValue);
        }

        #endregion

        #region Calendar.

        /// <summary>
        /// The calendar permissions.
        /// </summary>
        public static class Calendar
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "patient/calendar/event/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "patient/calendar/event/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "patient/calendar/event/update";
            
            /// <summary>
            /// The delete value.
            /// </summary>
            public const string DeleteValue = Schema + "patient/calendar/event/delete";

            /// <summary>
            /// Permission to create a calendar event.
            /// </summary>
            [Sort(39)]
            [Name("Create calendar event")]
            [Description("Permission to create a calendar event")]
            [Key("https://schema.appva.se/permission/patient/calendar/event/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view calendar events.
            /// </summary>
            [Sort(40)]
            [Name("Read calendar events")]
            [Description("Permission to read/view calendar events")]
            [Key("https://schema.appva.se/permission/patient/calendar/event/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit a calendar event.
            /// </summary>
            [Sort(41)]
            [Name("Update calendar event")]
            [Description("Permission to uppdate/edit a calendar event")]
            [Key("https://schema.appva.se/permission/patient/calendar/event/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a calendar event.
            /// </summary>
            [Sort(42)]
            [Name("Delete calendar event")]
            [Description("Permission to delete a calendar event")]
            [Key("https://schema.appva.se/permission/patient/calendar/event/delete")]
            public static readonly IPermission Delete = PermissionType.CreateNew(DeleteValue);
        }

        #endregion

        #region Inventory.

        /// <summary>
        /// The inventory permissions.
        /// </summary>
        public static class Inventory
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "patient/inventory/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "patient/inventory/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "patient/inventory/update";

            /// <summary>
            /// The delete value.
            /// </summary>
            public const string DeleteValue = Schema + "patient/inventory/delete";

            /// <summary>
            /// Permission to create an inventory.
            /// </summary>
            [Sort(43)]
            [Name("Create inventory")]
            [Description("Permission to create an inventory")]
            [Key("https://schema.appva.se/permission/patient/inventory/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view an inventory.
            /// </summary>
            [Sort(44)]
            [Name("Read inventory")]
            [Description("Permission to read/view an inventory")]
            [Key("https://schema.appva.se/permission/patient/inventory/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit an inventory.
            /// </summary>
            [Sort(45)]
            [Name("Update inventory")]
            [Description("Permission to uppdate/edit an inventory")]
            [Key("https://schema.appva.se/permission/patient/inventory/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete an inventory.
            /// </summary>
            [Sort(46)]
            [Name("Delete inventory")]
            [Description("Permission to delete an inventory")]
            [Key("https://schema.appva.se/permission/patient/inventory/delete")]
            public static readonly IPermission Delete = PermissionType.CreateNew(DeleteValue);
        }

        #endregion

        #region Notification.

        /// <summary>
        /// The notification permissions.
        /// </summary>
        public static class Notification
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "notification/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "notification/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "notification/update";

            /// <summary>
            /// The delete value.
            /// </summary>
            public const string DeleteValue = Schema + "notification/delete";

            /// <summary>
            /// Permission to create a notification.
            /// </summary>
            [Sort(47)]
            [Name("Create notification")]
            [Description("Permission to create a notification")]
            [Visibility(Visibility.Hidden)]
            [Key("https://schema.appva.se/permission/notification/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);
            
            /// <summary>
            /// Permission to read/view notifications.
            /// </summary>
            [Sort(48)]
            [Name("Read notifications")]
            [Description("Permission to read/view notifications")]
            [Visibility(Visibility.Hidden)]
            [Key("https://schema.appva.se/permission/notification/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);
            
            /// <summary>
            /// Permission to update/edit a notification.
            /// </summary>
            [Sort(49)]
            [Name("Update notifications")]
            [Description("Permission to update/edit a notification")]
            [Visibility(Visibility.Hidden)]
            [Key("https://schema.appva.se/permission/notification/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a notification.
            /// </summary>
            [Sort(50)]
            [Name("Delete notification")]
            [Description("Permission to delete a notification")]
            [Visibility(Visibility.Hidden)]
            [Key("https://schema.appva.se/permission/notification/delete")]
            public static readonly IPermission Delete = PermissionType.CreateNew(DeleteValue);
        }

        #endregion

        #region Roles.

        /// <summary>
        /// The role permissions.
        /// </summary>
        public static class Role
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "role/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "role/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "role/update";

            /// <summary>
            /// The delete value.
            /// </summary>
            public const string DeleteValue = Schema + "role/delete";

            /// <summary>
            /// Permission to create a role.
            /// </summary>
            [Sort(51)]
            [Name("Create role")]
            [Description("Permission to create a role")]
            [Visibility(Visibility.Hidden)]
            [Key("https://schema.appva.se/permission/practitioner/role/create")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view roles.
            /// </summary>
            [Sort(52)]
            [Name("Read roles")]
            [Description("Permission to read/view roles")]
            [Visibility(Visibility.Hidden)]
            [Key("https://schema.appva.se/permission/practitioner/role/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a role.
            /// </summary>
            [Sort(53)]
            [Name("Update role")]
            [Description("Permission to update/edit a role")]
            [Visibility(Visibility.Hidden)]
            [Key("https://schema.appva.se/permission/practitioner/role/update")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a role.
            /// </summary>
            [Sort(54)]
            [Name("Delete role")]
            [Description("Permission to delete a role")]
            [Visibility(Visibility.Hidden)]
            [Key("https://schema.appva.se/permission/practitioner/role/delete")]
            public static readonly IPermission Delete = PermissionType.CreateNew(DeleteValue);
        }

        #endregion

        #region Area51.

        /// <summary>
        /// The area 51 permissions.
        /// </summary>
        public static class Area51
        {
            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "area51/read";

            /// <summary>
            /// Permission to read/view area51.
            /// </summary>
            [Sort(55)]
            [Name("Read area51")]
            [Description("Permission to read/view area51")]
            [Visibility(Visibility.Hidden)]
            [Key("https://schema.appva.se/permission/area51/read")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);
        }

        #endregion
    }

    /// <summary>
    /// The <see cref="IPermission"/> implementation.
    /// TODO: Change name to e.g. ClaimPermission, SecurityPermission or InternalPermission
    /// </summary>
    internal sealed class PermissionType : IPermission
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionType"/> class.
        /// </summary>
        /// <param name="value">The value</param>
        private PermissionType(string value)
        {
            this.Key = ClaimTypes.Permission;
            this.Value = value;
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