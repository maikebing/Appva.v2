// <copyright file="Permissions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using System.Diagnostics.CodeAnalysis;
    using Appva.Core.Contracts.Permissions;
    using Appva.Core.Resources;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class Permissions
    {
        #region Admin

        #region Constants.

        /// <summary>
        /// The permission schema.
        /// </summary>
        private const string Schema = "https://schemas.appva.se/2015/04/permission/";

        #endregion

        #region Audit 

        /// <summary>
        /// All permissions to auditable reports.
        /// </summary>
        public static class Reports
        {
            /// <summary>
            /// The audit reports sign in value.
            /// </summary>
            public const string SignInValue = Schema + "/audit/reports/sign-in";
            
            /// <summary>
            /// Permission to sign in to auditing. 
            /// </summary>
            [Sort(1000)]
            [Name("Sign in audit report")]
            [Description("Permission to sign in to audit report system")]
            public static readonly IPermission SignIn = PermissionType.CreateNew(SignInValue);

            /// <summary>
            /// The audit reports notification value.
            /// </summary>
            public const string AuditReportNotificationValue = Schema + "/audit/reports/notification";

            /// <summary>
            /// Permission to receive audit report notification.
            /// </summary>
            [Sort(1001)]
            [Name("Recieve audit report notification")]
            [Description("Permission to recive report notification")]
            public static readonly IPermission AuditReportNotification = PermissionType.CreateNew(AuditReportNotificationValue);

            /// <summary>
            /// The audit excluded value.
            /// </summary>
            public const string ExcludeFromAuditValue = Schema + "/audit/reports/ignore";

            /// <summary>
            /// Permission to be excluded in auditing.
            /// </summary>
            /// <remarks>
            /// Any role which has this permission will be excluded in audits.
            /// </remarks>
            [Sort(1002)]
            [Name("Ignore include in auditing")]
            [Description("Permission to recive report notification")]
            public static readonly IPermission ExcludeFromAudit = PermissionType.CreateNew(ExcludeFromAuditValue);

            /// <summary>
            /// The audit hideable value.
            /// </summary>
            public const string HideAuditRecordFromPublicValue = Schema + "/audit/reports/hide";

            /// <summary>
            /// Permission to hide the audit log from public visibility.
            /// </summary>
            /// <remarks>
            /// Any role which has this permission will be hidden in read audit logs.
            /// </remarks>
            [Sort(1003)]
            [Name("Hide in audit log reads")]
            [Description("Permission to hide audit log record from public view")]
            public static readonly IPermission HideAuditRecordFromPublic = PermissionType.CreateNew(HideAuditRecordFromPublicValue);
        }

        /// <summary>
        /// Information confidentiality settings.
        /// </summary>
        public static class SecurityConfidentiality
        {
            /// <summary>
            /// Policy for handling information related to a celebrity (people of public 
            /// interest (VIP), which will be afforded heightened confidentiality. Celebrities 
            /// are people of public interest (VIP) about whose information an enterprise may 
            /// have a policy that requires heightened confidentiality. Information deemed 
            /// sensitive may include health information and patient role information including 
            /// patient status, demographics, next of kin, and location.
            /// </summary>
            public const string CreatePeopleofPublicInterestValue = Schema + "cel/create";

            /// <summary>
            /// Policy for handling information related to a celebrity (people of public 
            /// interest (VIP), which will be afforded heightened confidentiality. Celebrities 
            /// are people of public interest (VIP) about whose information an enterprise may 
            /// have a policy that requires heightened confidentiality. Information deemed 
            /// sensitive may include health information and patient role information including 
            /// patient status, demographics, next of kin, and location.
            /// </summary>
            public const string UpdatePeopleofPublicInterestValue = Schema + "cel/update";

            /// <summary>
            /// Permission to create people of public interest.
            /// </summary>
            [Sort(1004)]
            [Name("Create people of public interest (VIP)")]
            [Description("Permission to create people of public interest (VIP)")]
            public static readonly IPermission CreatePeopleofPublicInterest = PermissionType.CreateNew(CreatePeopleofPublicInterestValue);

            /// <summary>
            /// Permission to create people of public interest.
            /// </summary>
            [Sort(1005)]
            [Name("Update people of public interest (VIP)")]
            [Description("Permission to update people of public interest (VIP)")]
            public static readonly IPermission UpdatePeopleofPublicInterest = PermissionType.CreateNew(UpdatePeopleofPublicInterestValue);

            /// <summary>
            /// Policy for handling all demographic information about an information subject, 
            /// which will be afforded heightened confidentiality. Policies may govern 
            /// sensitivity of information related to all demographic about an information 
            /// subject, the disclosure of which could impact the privacy, well-being, or 
            /// safety of that subject. 
            /// </summary>
            public const string CreateAllDemographicInformationSensitivityValue = Schema + "demo/create";

            /// <summary>
            /// Policy for handling all demographic information about an information subject, 
            /// which will be afforded heightened confidentiality. Policies may govern 
            /// sensitivity of information related to all demographic about an information 
            /// subject, the disclosure of which could impact the privacy, well-being, or 
            /// safety of that subject. 
            /// </summary>
            public const string UpdateAllDemographicInformationSensitivityValue = Schema + "demo/update";

            /// <summary>
            /// Permission to create patients with a hightened confidentiality about all demographics.
            /// </summary>
            [Sort(1006)]
            [Name("Create patients with a hightened confidentiality about all democraphics")]
            [Description("Permission to create patients with a hightened confidentiality about all democraphics")]
            public static readonly IPermission CreateAllDemographicInformationSensitivity = PermissionType.CreateNew(CreateAllDemographicInformationSensitivityValue);

            /// <summary>
            /// Permission to update patients with a hightened confidentiality about all demographics.
            /// </summary>
            [Sort(1007)]
            [Name("Update patients with a hightened confidentiality about all democraphics")]
            [Description("Permission to update patients with a hightened confidentiality about all democraphics")]
            public static readonly IPermission UpdateAllDemographicInformationSensitivity = PermissionType.CreateNew(UpdateAllDemographicInformationSensitivityValue);
        }

        #endregion

        #region Token (Reset / Registration)

        /// <summary>
        /// The token permissions.
        /// </summary>
        public static class Token
        {
            /// <summary>
            /// The reset token value.
            /// </summary>
            public const string Reset = Schema + "reset";

            /// <summary>
            /// The register token value.
            /// </summary>
            public const string Register = Schema + "register";
        }

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
            /// The read delayed tasks value.
            /// </summary>
            public const string ReadDelayedTasksValue = Schema + "dashboard/tasks/read";

            /// <summary>
            /// The read control count value.
            /// </summary>
            public const string ReadControlCountNarcoticsValue = Schema + "dashboard/control/narcotics/read";

            /// <summary>
            /// The read chart value.
            /// </summary>
            public const string ReadChartValue = Schema + "dashboard/chart/read";

            /// <summary>
            /// The read order refill value.
            /// </summary>
            public const string ReadOrderRefillValue = Schema + "dashboard/order/refill/read";

            /// <summary>
            /// The read calendar value.
            /// </summary>
            public const string ReadCalendarValue = Schema + "dashboard/calendar/read";

            /// <summary>
            /// The read delegation value.
            /// </summary>
            public const string ReadDelegationValue = Schema + "dashboard/delegation/read";

            /// <summary>
            /// Permission to read/view the dashboard.
            /// </summary>
            [Sort(0)]
            [Name("Read dashboard")]
            [Description("Permission to read/view the dashboard")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to read/view delayed tasks.
            /// </summary>
            [New]
            [Sort(0)]
            [Name("Read delayed tasks dashboard")]
            [Description("Permission to read/view delayed tasks widget on the dashboard")]
            public static readonly IPermission ReadDelayedTasks = PermissionType.CreateNew(ReadDelayedTasksValue);

            /// <summary>
            /// Permission to read/view control count narcotics on dashboard.
            /// </summary>
            [New]
            [Sort(0)]
            [Name("Read narcotics control count dashboard")]
            [Description("Permission to read/view narcotics control count widget on the dashboard")]
            public static readonly IPermission ReadControlCountNarcotics = PermissionType.CreateNew(ReadControlCountNarcoticsValue);

            /// <summary>
            /// Permission to read/view chart.
            /// </summary>
            [New]
            [Sort(0)]
            [Name("Read chart dashboard")]
            [Description("Permission to read/view chart widget on the dashboard")]
            public static readonly IPermission ReadChart = PermissionType.CreateNew(ReadChartValue);

            /// <summary>
            /// Permission to read/view order refill.
            /// </summary>
            [New]
            [Sort(0)]
            [Name("Read order refill dashboard")]
            [Description("Permission to read/view order refill widget on the dashboard")]
            public static readonly IPermission ReadOrderRefill = PermissionType.CreateNew(ReadOrderRefillValue);

            /// <summary>
            /// Permission to read/view calender.
            /// </summary>
            [New]
            [Sort(0)]
            [Name("Read calendar dashboard")]
            [Description("Permission to read/view calendar widget on the dashboard")]
            public static readonly IPermission ReadCalendar = PermissionType.CreateNew(ReadCalendarValue);

            /// <summary>
            /// Permission to read/view delegation.
            /// </summary>
            [New]
            [Sort(0)]
            [Name("Read delegation dashboard")]
            [Description("Permission to read/view delegation widget on the dashboard")]
            public static readonly IPermission ReadDelegation = PermissionType.CreateNew(ReadDelegationValue);
        }

        #endregion

        #region Report.

        /// <summary>
        /// The report permissions.
        /// </summary>
        public static class Report
        {
            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "report/read";

            /// <summary>
            /// The permission to read/view full report.
            /// </summary>
            [New]
            [Sort(0)]
            [Name("Read full report")]
            [Description("Permission to read/view full report")]
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
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a practitioner.
            /// </summary>
            [Sort(2)]
            [Name("Read practitioner")]
            [Description("Permission to read/view a practitioner")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a practitioner.
            /// </summary>
            [Sort(3)]
            [Name("Update practitioner")]
            [Description("Permission to update/edit a practitioner")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to update/edit a practitioner.
            /// </summary>
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
            public static readonly IPermission Inactivate = PermissionType.CreateNew(InactivateValue);

            /// <summary>
            /// Permission to reactivate a practitioner.
            /// </summary>
            [Sort(5)]
            [Name("Reactivate practitioner")]
            [Description("Permission to reactivate a practitioner")]
            public static readonly IPermission Reactivate = PermissionType.CreateNew(ReactivateValue);

            /// <summary>
            /// Permission to pause a practitioner.
            /// </summary>
            [Sort(6)]
            [Name("Pause practitioner")]
            [Description("Permission to pause a practitioner")]
            public static readonly IPermission Pause = PermissionType.CreateNew(PauseValue);

            /// <summary>
            /// Permission to resume a paused practitioner.
            /// </summary>
            [Sort(7)]
            [Name("Resume paused practitioner")]
            [Description("Permission to resume paused practitioner")]
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
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a delegation.
            /// </summary>
            [Sort(9)]
            [Name("Read delegation")]
            [Description("Permission to read/view a delegation")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a delegation.
            /// </summary>
            [Sort(10)]
            [Name("Update delegation")]
            [Description("Permission to update/edit a delegation")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a delegation.
            /// </summary>
            [Sort(11)]
            [Name("Delete delegation")]
            [Description("Permission to delete a delegation")]
            public static readonly IPermission Delete = PermissionType.CreateNew(DeleteValue);

            /// <summary>
            /// Permission to read/view a revision.
            /// </summary>
            [Sort(12)]
            [Name("Read delegation revision")]
            [Description("Permission to read/view a delegation revision")]
            public static readonly IPermission Revision = PermissionType.CreateNew(RevisionValue);

            /// <summary>
            /// Permission to read/view an issued delegation.
            /// </summary>
            [Sort(13)]
            [Name("Read issued delegation")]
            [Description("Permission to read/view an issued delegation")]
            public static readonly IPermission Issued = PermissionType.CreateNew(IssuedValue);

            /// <summary>
            /// Permission to read/view a delegation report.
            /// </summary>
            [Sort(14)]
            [Name("Read delegation report")]
            [Description("Permission to read/view a delegation report")]
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
            /// The archive value.
            /// </summary>
            public const string ArchiveValue = Schema + "patient/archive";

            /// <summary>
            /// Permission to create a patient.
            /// </summary>
            [Sort(15)]
            [Name("Create patient")]
            [Description("Permission to create a patient")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a patient.
            /// </summary>
            [Sort(16)]
            [Name("Read patient")]
            [Description("Permission to read/view a patient")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a patient.
            /// </summary>
            [Sort(17)]
            [Name("Update patient")]
            [Description("Permission to update/edit a patient")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to inactivate a patient.
            /// </summary>
            [Sort(18)]
            [Name("Inactivate patient")]
            [Description("Permission to inactivate a patient")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew(InactivateValue);

            /// <summary>
            /// Permission to reactivate a patient.
            /// </summary>
            [Sort(19)]
            [Name("Reactivate patient")]
            [Description("Permission to reactivate a patient")]
            public static readonly IPermission Reactivate = PermissionType.CreateNew(ReactivateValue);
            
            /// <summary>
            /// Permission to inactivate a patient.
            /// </summary>
            [Sort(19)]
            [Name("Archive patient - Only for Appva Admins")]
            [Description("Permission to archive a patient")]
            public static readonly IPermission Archive = PermissionType.CreateNew(ArchiveValue);
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
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a schedule.
            /// </summary>
            [Sort(21)]
            [Name("Read schedule")]
            [Description("Permission to read/view a schedule")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit a schedule.
            /// </summary>
            [Sort(22)]
            [Name("Update schedule")]
            [Description("Permission to uppdate/edit a schedule")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to inactivate a schedule.
            /// </summary>
            [Sort(23)]
            [Name("Inactivate schedule")]
            [Description("Permission to inactivate a schedule")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew(InactivateValue);

            /// <summary>
            /// Permission to print a schedule.
            /// </summary>
            [Sort(24)]
            [Name("Print schedule")]
            [Description("Permission to print a schedule")]
            public static readonly IPermission Print = PermissionType.CreateNew(PrintValue);

            /// <summary>
            /// Permission to read/view the patient reports.
            /// </summary>
            [Sort(25)]
            [Name("Read schedule report")]
            [Description("Permission to read/view the schedule report")]
            public static readonly IPermission Report = PermissionType.CreateNew(ReportValue);

            /// <summary>
            /// Permission to read/view a signed event.
            /// </summary>
            [Sort(26)]
            [Name("Read signed event")]
            [Description("Permission to read/view a signed event")]
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
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a schedule.
            /// </summary>
            [Sort(28)]
            [Name("Read sequence")]
            [Description("Permission to read/view a sequence")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit a schedule.
            /// </summary>
            [Sort(29)]
            [Name("Update sequence")]
            [Description("Permission to uppdate/edit a sequence")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to inactivate a schedule.
            /// </summary>
            [Sort(30)]
            [Name("Inactivate sequence")]
            [Description("Permission to inactivate a sequence")]
            public static readonly IPermission Inactivate = PermissionType.CreateNew(InactivateValue);

            /// <summary>
            /// Permission to delete a schedule.
            /// </summary>
            [Sort(31)]
            [Name("Print sequence")]
            [Description("Permission to print a sequence")]
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
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view a prepared sequence.
            /// </summary>
            [Sort(33)]
            [Name("Read prepared sequence")]
            [Description("Permission to read/view a prepared sequence")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit a prepared sequence.
            /// </summary>
            [Sort(34)]
            [Name("Update prepared sequence")]
            [Description("Permission to uppdate/edit a prepared sequence")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a prepared sequence.
            /// </summary>
            [Sort(35)]
            [Name("Delete prepared sequence")]
            [Description("Permission to delete a prepared sequence")]
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
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to handle alerts.
            /// </summary>
            [Sort(37)]
            [Name("Handle alerts")]
            [Description("Permission to handle alerts")]
            public static readonly IPermission Handle = PermissionType.CreateNew(HandleValue);

            /// <summary>
            /// Permission to handle all alerts.
            /// </summary>
            [Sort(38)]
            [Name("Handle all alerts")]
            [Description("Permission to handle all alerts")]
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
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view calendar events.
            /// </summary>
            [Sort(40)]
            [Name("Read calendar events")]
            [Description("Permission to read/view calendar events")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit a calendar event.
            /// </summary>
            [Sort(41)]
            [Name("Update calendar event")]
            [Description("Permission to uppdate/edit a calendar event")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a calendar event.
            /// </summary>
            [Sort(42)]
            [Name("Delete calendar event")]
            [Description("Permission to delete a calendar event")]
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
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view an inventory.
            /// </summary>
            [Sort(44)]
            [Name("Read inventory")]
            [Description("Permission to read/view an inventory")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to uppdate/edit an inventory.
            /// </summary>
            [Sort(45)]
            [Name("Update inventory")]
            [Description("Permission to uppdate/edit an inventory")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete an inventory.
            /// </summary>
            [Sort(46)]
            [Name("Delete inventory")]
            [Description("Permission to delete an inventory")]
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
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view notifications.
            /// </summary>
            [Sort(48)]
            [Name("Read notifications")]
            [Description("Permission to read/view notifications")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a notification.
            /// </summary>
            [Sort(49)]
            [Name("Update notifications")]
            [Description("Permission to update/edit a notification")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a notification.
            /// </summary>
            [Sort(50)]
            [Name("Delete notification")]
            [Description("Permission to delete a notification")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Delete = PermissionType.CreateNew(DeleteValue);
        }

        #endregion

        #region News

        public static class News
        {
            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "notification/news/create";

            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "notification/news/read";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "notification/news/update";

            /// <summary>
            /// The delete value.
            /// </summary>
            public const string DeleteValue = Schema + "notification/news/delete";

            /// <summary>
            /// Permission to create a news.
            /// </summary>
            [Sort(56)]
            [Name("Create news")]
            [Description("Permission to create a news")]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view news.
            /// </summary>
            [Sort(57)]
            [Name("Read news")]
            [Description("Permission to read/view news")]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a news.
            /// </summary>
            [Sort(58)]
            [Name("Update news")]
            [Description("Permission to update/edit a news")]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a news.
            /// </summary>
            [Sort(59)]
            [Name("Delete news")]
            [Description("Permission to delete a news")]
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
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to read/view roles.
            /// </summary>
            [Sort(52)]
            [Name("Read roles")]
            [Description("Permission to read/view roles")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to update/edit a role.
            /// </summary>
            [Sort(53)]
            [Name("Update role")]
            [Description("Permission to update/edit a role")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);

            /// <summary>
            /// Permission to delete a role.
            /// </summary>
            [Sort(54)]
            [Name("Delete role")]
            [Description("Permission to delete a role")]
            [Visibility(Visibility.Hidden)]
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
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);
        }

        #endregion

        #region Log

        /// <summary>
        /// The log permissions.
        /// </summary>
        public static class Log
        {
            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "log/read";

            /// <summary>
            /// Permission to read/view notifications.
            /// </summary>
            [Sort(100)]
            [Name("Read logs")]
            [Description("Permission to read/view logs")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);
        }

        #endregion

        #region Synchronization

        /// <summary>
        /// The synchronization permissions.
        /// </summary>
        public static class Synchronization
        {
            /// <summary>
            /// The read value.
            /// </summary>
            public const string ReadValue = Schema + "synchronization/read";

            /// <summary>
            /// The create value.
            /// </summary>
            public const string CreateValue = Schema + "synchronization/create";

            /// <summary>
            /// The delete value.
            /// </summary>
            public const string DeleteValue = Schema + "synchronization/delete";

            /// <summary>
            /// The update value.
            /// </summary>
            public const string UpdateValue = Schema + "synchronization/update";

            /// <summary>
            /// Permission to read/view synchronizations.
            /// </summary>
            [Sort(110)]
            [Name("Read synchronization")]
            [Description("Permission to read/view synchronizations")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

            /// <summary>
            /// Permission to create synchronizations.
            /// </summary>
            [Sort(111)]
            [Name("Create synchronization")]
            [Description("Permission to create synchronizations")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Create = PermissionType.CreateNew(CreateValue);

            /// <summary>
            /// Permission to create synchronizations.
            /// </summary>
            [Sort(112)]
            [Name("Delete synchronization")]
            [Description("Permission to delete synchronizations")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Delete = PermissionType.CreateNew(DeleteValue);

            /// <summary>
            /// Permission to update synchronizations.
            /// </summary>
            [Sort(113)]
            [Name("Update synchronization")]
            [Description("Permission to update synchronizations")]
            [Visibility(Visibility.Hidden)]
            public static readonly IPermission Update = PermissionType.CreateNew(UpdateValue);
        }

        #endregion

        #region Backoffice.

    /// <summary>
    /// The Backoffice permissions.
    /// </summary>
    public static class Backoffice
    {
        /// <summary>
        /// The read value.
        /// </summary>
        public const string ReadValue = Schema + "backoffice/read";

        /// <summary>
        /// Permission to read/view backoffice.
        /// </summary>
        [Sort(100000)]
        [Name("Read backoffice")]
        [Description("Permission to read/view backoffice")]
        [Visibility(Visibility.Hidden)]
        public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);
    }

    #endregion

        #endregion

        #region Resource

        
            #region Constants.

            /// <summary>
            /// The permission schema for mobile.
            /// </summary>
            public const string DeviceSchema = "https://schemas.appva.se/permission/device";

            #endregion

            #region NFC.

            public static class NFC
            {
                /// <summary>
                /// The register NFC value.
                /// </summary>
                public const string RegisterValue = DeviceSchema + "/nfc/register";

                /// <summary>
                /// Permission to register a NFC tag by mobile device
                /// </summary>
                [Sort(10000)]
                [Name("Register nfc from mobile device")]
                [Description("Permission to register a NFC tag by a mobiledevice")]
                public static readonly IPermission SignIn = PermissionType.CreateNew(RegisterValue);
            }

            #endregion

            #region Delegations.

            public static class Delegations
            {
                /// <summary>
                /// The delegation read value value.
                /// </summary>
                public const string ReadValue = DeviceSchema + "/account/delegations/read";

                /// <summary>
                /// The delegation read value value.
                /// </summary>
                public const string ReadKnowledgeTestValue = DeviceSchema + "/account/delegations/knowledgetest/read";

                /// <summary>
                /// Permission to read delegations in mobile device
                /// </summary>
                [Sort(20000)]
                [Name("Read delegations in mobile device")]
                [Description("Permission to read delegations in the mobile device")]
                public static readonly IPermission Read = PermissionType.CreateNew(ReadValue);

                /// <summary>
                /// Permission to read delegations in mobile device
                /// </summary>
                [Sort(20100)]
                [Name("Read knowledgetest in mobile device")]
                [Description("Permission to read knowledgetst in the mobile device")]
                public static readonly IPermission ReadKnowledgeTest = PermissionType.CreateNew(ReadKnowledgeTestValue);
            }

            #endregion

            #region Article

            public static class Article
            {
                /// <summary>
                /// The refill article value
                /// </summary>
                public const string SetRefilledValue = DeviceSchema + "/article/refill";

                /// <summary>
                /// Permission to set the article as refilled.
                /// </summary>
                [Sort(10000)]
                [Name("Refill an atricle from mobile device")]
                [Description("Permission to set an article to refilled from a mobile device")]
                public static readonly IPermission SetRefilled = PermissionType.CreateNew(SetRefilledValue);
            }

            #endregion

            #endregion
    }

    /// <summary>
    /// The <see cref="IPermission"/> implementation.
    /// TODO: Change name to e.g. ClaimPermission, SecurityPermission or InternalPermission
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
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

        #region Properties.

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

        #region Internal Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="IPermission"/> class.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>A new <see cref="IPermission"/> instance</returns>
        internal static IPermission CreateNew(string value)
        {
            return new PermissionType(value);
        }

        #endregion
    }
}