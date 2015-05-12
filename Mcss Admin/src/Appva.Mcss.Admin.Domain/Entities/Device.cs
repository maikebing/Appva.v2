﻿// <copyright file="Device.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Device : AggregateRoot<Device>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not the <see cref="Device"/> is active.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        /// <summary>
        /// The Device UDID.
        /// </summary>
        public virtual string UDID
        {
            get;
            set;
        }

        /// <summary>
        /// Taxon node.
        /// </summary>
        public virtual Taxon Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// Last pinged date.
        /// </summary>
        public virtual DateTime? LastPingedDate
        {
            get;
            set;
        }

        #endregion
    }
}