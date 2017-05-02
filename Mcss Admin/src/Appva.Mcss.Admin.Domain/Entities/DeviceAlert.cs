// <copyright file="DeviceAlerts.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    class DeviceAlert : AggregateRoot<DeviceAlert>
    {
        /// <summary>
        /// The device.
        /// </summary>
        public virtual Device Device
        {
            get;
            set;
        }

        /// <summary>
        /// The escalation level.
        /// </summary>
        public virtual EscalationLevel EscalationLevel
        {
            get;
            set;
        }

        /// <summary>
        /// List of taxons.
        /// </summary>
        public virtual IList<Taxon> Taxons
        {
            get;
            set;
        }
    }
}
