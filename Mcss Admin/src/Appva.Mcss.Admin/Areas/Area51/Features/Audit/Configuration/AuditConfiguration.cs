// <copyright file="AuditConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Audit
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuditConfiguration : IRequest<AuditConfiguration>
    {
        /// <summary>
        /// The organizational units.
        /// </summary>
        public IEnumerable<OrganizationalUnit> OrganizationalUnits
        {
            get;
            set;
        }
    }
}