// <copyright file="ReactivateDeviceHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices.ReActivate
{
    #region Imports.

    using Application.Services;
    using Cqrs;
    using List;
    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ReactivateDeviceHandler : RequestHandler<ReactivateDevice, ListDevice>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IDeviceService"/>
        /// </summary>
        private readonly IDeviceService service;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactivateDeviceHandler"/> class.
        /// </summary>
        public ReactivateDeviceHandler(IDeviceService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListDevice Handle(ReactivateDevice message)
        {
            this.service.Activate(this.service.Find(message.Id));
            return new ListDevice
            {
                IsActive = message.IsActive,
                Page = message.Page
            };
        }

        #endregion
    }
}