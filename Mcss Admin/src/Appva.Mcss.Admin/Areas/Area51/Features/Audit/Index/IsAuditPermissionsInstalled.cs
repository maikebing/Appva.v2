// <copyright file="IsAuditPermissionsInstalled.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Audit
{
    #region Imports.

    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class IsAuditPermissionsInstalled : IRequest<IsAuditPermissionsInstalled>
    {
        /// <summary>
        /// Whether or not the sensitivity permissions are installed.
        /// </summary>
        public bool IsInstalled
        {
            get;
            set;
        }
    }
}