// <copyright file="InactivateDeviceHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices.Inactivate
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
    public sealed class InactivateDeviceHandler : RequestHandler<InactivateDevice, ListDevice>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IDeviceService"/>
        /// </summary>
        private readonly IDeviceService service;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateDeviceHandler"/> class.
        /// </summary>
        public InactivateDeviceHandler(IDeviceService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListDevice Handle(InactivateDevice message)
        {
            this.service.Inactivate(this.service.Find(message.Id));
            return new ListDevice
            {
                IsActive = message.IsActive,
                Page = message.Page
            };
        }

        #endregion
    }
}