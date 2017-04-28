// <copyright file="Device.cs" company="Appva AB">
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

        public virtual DateTime? Modified
        {
            get; set;
        }

        /// <summary>
        /// Last pinged date.
        /// </summary>
        public virtual DateTime? LastPingedDate
        {
            get;
            set;
        }

        /// <summary>
        /// Last used date
        /// </summary>
        public virtual DateTime? LastUsedDate
        {
            get; set;
        }

        /// <summary>
		/// The description of the device, e.g. where it is located etc.
		/// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The OS.
        /// </summary>
        public virtual string OS
        {
            get;
            set;
        }

        /// <summary>
        /// The os version.
        /// </summary>
        public virtual string OSVersion
        {
            get;
            set;
        }

        /// <summary>
        /// The app bundle.
        /// </summary>
        public virtual string AppBundle
        {
            get;
            set;
        }

        /// <summary>
        /// The app version.
        /// </summary>
        public virtual string AppVersion
        {
            get;
            set;
        }

        /// <summary>
        /// The hardware.
        /// </summary>
        public virtual string Hardware
        {
            get;
            set;
        }

        /// <summary>
        /// The AzurePushId
        /// </summary>
        public virtual string AzurePushId
        {
            get; set;
        }

        /// <summary>
        /// Device name
        /// </summary>
        public virtual string Name
        {
            get; set;
        }

        /// <summary>
        /// The Uuid
        /// </summary>
        public virtual string Uuid
        {
            get; set;
        }

        /// <summary>
        /// The Push Uuid
        /// </summary>
        public virtual string PushUuid
        {
            get; set;
        }


        #endregion
    }
}