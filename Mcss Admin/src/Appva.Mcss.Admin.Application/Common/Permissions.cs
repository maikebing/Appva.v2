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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Permissions
    {
        public const string PatientCreate = "1.C";
        public const string PatientRead = "2.R";
        public const string PatientUpdate = "3.U";
        public const string PatientDelete = "4.D";
        public const string PatientSchedulesCreate = "5.C";
        public const string PatientSchedulesRead = "6.R";
        public const string PatientSchedulesUpdate = "7.U";
        public const string PatientSchedulesDelete = "8.D";
        public const string PatientSignedEventsRead = "9.R";
        public const string PatientAlertsRead = "10.R";
        public const string PatientReportsRead = "11.R";
        public const string PatientCalendarRead = "12.R";
        public const string PatientInventoryRead = "13.R";
        public const string AccountCreate = "14.C";
        public const string AccountRead = "15.R";
        public const string AccountUpdate = "16.U";
        public const string AccountDelete = "17.D";
        public const string AccountDelegationCreate = "18.C";
        public const string AccountDelegationRead = "19.R";
        public const string AccountDelegationUpdate = "20.U";
        public const string AccountDelegationDelete = "21.D";
        public const string AccountRevisionRead = "22.R";
        public const string AccountIssuedDelegationRead = "23.R";
        public const string AccountReportsRead = "24.R";
        public const string RoleCreate = "25.C";
        public const string RoleRead = "26.R";
        public const string RoleUpdate = "27.U";
        public const string RoleDelete = "28.D";
        public const string NotificationCreate = "29.C";
        public const string NotificationRead = "30.R";
        public const string NotificationUpdate = "31.U";
        public const string NotificationDelete = "32.D";
        public const string DashboardRead = "33.R";
        public const string DashboardControl = "34.R";
        public const string DashboardReport = "35.R";
        public const string DashboardTotalResult = "36.R";
        public const string Area51 = "37.GOD";
    }
}