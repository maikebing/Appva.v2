// <copyright file="InactivateDevice.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices.Inactivate
{
    #region Imports.

    using Admin.Models;
    using List;
    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InactivateDevice : Identity<ListDevice>
    {
        public bool? IsActive
        {
            get;
            set;
        }

        public int Page
        {
            get;
            set;
        }
    }
}