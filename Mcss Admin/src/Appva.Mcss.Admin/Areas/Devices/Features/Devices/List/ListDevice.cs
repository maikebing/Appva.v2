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
    }
}