// <copyright file="EditDevicePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Devices.Features.Devices.Edit
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EditDevicePublisher : RequestHandler<EditDeviceModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IDeviceService deviceService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditDeviceHandler"/> class.
        /// </summary>
        /// <param name="deviceService">The <see cref="IDeviceService"/> implementation.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation.</param>
        public EditDevicePublisher(IDeviceService deviceService, ITaxonomyService taxonomyService)
        {
            this.deviceService = deviceService;
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(EditDeviceModel message)
        {
            var device = this.deviceService.Find(message.Id);

            if (device == null)
                return false;

            device.Description = string.IsNullOrEmpty(device.Description) ? device.Description : message.Description;
            this.deviceService.Update(device);
            return true;
        }

        #endregion
    }
}