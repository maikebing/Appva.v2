// <copyright file="InstallAuditPermissions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Audit
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Caching;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InstallAuditPermissions : IRequest<IsAuditPermissionsInstalled>
    {
    }
}