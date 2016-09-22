// <copyright file="DeviceModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeviceModel
    {
        #region Properties.

        /// <summary>
        /// The id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        public DateTime CreatedAt
        {
            get;
            set;
        }

        /// <summary>
        /// The description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The OS.
        /// </summary>
        public string OS
        {
            get;
            set;
        }

        /// <summary>
        /// The os version.
        /// </summary>
        public string OSVersion
        {
            get;
            set;
        }

        /// <summary>
        /// The app bundle.
        /// </summary>
        public string AppBundle
        {
            get;
            set;
        }

        /// <summary>
        /// The app version.
        /// </summary>
        public string AppVersion
        {
            get;
            set;
        }

        /// <summary>
        /// The hardware.
        /// </summary>
        public string Hardware
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        #endregion
    }
}