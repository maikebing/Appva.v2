// <copyright file="ListDevice.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices.List
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListDevice : IRequest<ListDeviceModel>
    {
        #region Fields.

        /// <summary>
        /// Set default sort mode.
        /// </summary>
        private bool isAscending = true;

        #endregion

        #region Properties.

        public string SearchQuery
        {
            get;
            set;
        }

        public bool? IsActive
        {
            get;
            set;
        }

        public int? Page
        {
            get;
            set;
        }

        public string OrderBy
        {
            get; set;
        }

        public bool IsAscending
        {
            get { return isAscending; }
            set { isAscending = value; }
        }

        public bool IsCurrentNode
        {
            get;
            set;
        }

        #endregion
    }
}