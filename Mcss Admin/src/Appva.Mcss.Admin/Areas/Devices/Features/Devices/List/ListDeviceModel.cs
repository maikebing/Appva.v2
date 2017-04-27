// <copyright file="ListDeviceModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices.List
{
    #region Imports.

    using Admin.Models;
    using System;
    using System.Collections.Generic;
    using Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListDeviceModel : SearchViewModel<DeviceViewModel>
    {   
        public bool? IsActive
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
            get;
            set;
        }

        public bool IsCurrentNode
        {
            get;
            set;
        }
    }
}