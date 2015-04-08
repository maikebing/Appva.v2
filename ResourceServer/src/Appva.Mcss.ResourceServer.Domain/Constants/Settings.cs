// <copyright file="Settings.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Constants
{
    #region Imports.

    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// Available settings for the resource server.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public static class Settings
    {
        /// <summary>
        /// The history length.
        /// </summary>
        public const string AllowedHistorySize = "MCSS.Device.Security.HistoryLength";

        /// <summary>
        /// The device organistaiton-lock setting
        /// </summary>
        public const string LockToOrgTaxon = "MCSS.Security.Device.LockToOrgTaxon";

        /// <summary>
        /// Which task-types should be included on overview timeline (e.g "ordination", "calendar")
        /// </summary>
        public const string OverviewTimelineTaskTypes = "MCSS.Device.Settings.Timeline.OverviewTimelineTaskTypes";
    }
}