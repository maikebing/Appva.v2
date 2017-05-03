// <copyright file="DeviceService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Repository;
    using Domain.Repositories;
    using Auditing;
    using Appva.Mcss.Admin.Domain.Entities;
    using Security.Identity;
    using Common;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IDeviceService : IService
    {
        /// <summary>
        /// Lists devices by search query
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageableSet<DeviceModel> Search(SearchDeviceModel model, int page = 1, int pageSize = 10);

        /// <summary>
        /// Activates the inactivated device <see cref="Device"/>
        /// </summary>
        /// <param name="entity"></param>
        void Activate(Device device);

        /// <summary>
        /// Inactivate the activated device <see cref="Device"/>
        /// </summary>
        /// <param name="device"></param>
        void Inactivate(Device device);

        /// <summary>
        /// Locates a device by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Device Find(Guid id);

        /// <summary>
        /// Updates the device <see cref="Device"/>
        /// </summary>
        /// <param name="device"></param>
        void Update(Device device);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeviceService : IDeviceService
    {
        #region Variables

        /// <summary>
        /// The <see cref="IDeviceService"/>
        /// </summary>
        private readonly IDeviceRepository repository;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService auditService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceService"/> class.
        /// </summary>
        public DeviceService(IDeviceRepository repository, IAuditService auditService)
        {
            this.repository = repository;
            this.auditService = auditService;
        }

        #endregion

        #region IDeviceService members.

        /// <inheritdoc />
        public PageableSet<DeviceModel> Search(SearchDeviceModel model, int page = 1, int pageSize = 10)
        {
            var logText = string.Format("läste en lista över enheter på sida {0}", page);
            if (model.SearchQuery.IsNotEmpty())
            {
                logText = string.Format("genomförde en sökning i enhetslistan på {0}", model.SearchQuery);
            }
            this.auditService.Read(logText);
            return this.repository.Search(model, page, pageSize);
        }

        /// <inheritdoc />
        public void Activate(Device device)
        {
            device.IsActive = true;
            device.UpdatedAt = DateTime.Now;
            this.repository.Update(device);
            this.auditService.Update("aktiverade enheten {0} (REF: {1})", device.Description, device.Id);
        }

        /// <inheritdoc />
        public void Inactivate(Device device)
        {
            device.IsActive = false;
            device.UpdatedAt = DateTime.Now;
            this.repository.Update(device);
            this.auditService.Update("inaktiverade enheten {0} (REF: {1})", device.Description, device.Id);
        }

        /// <inheritdoc />
        public Device Find(Guid id)
        {
            return this.repository.Find(id);
        }

        /// <inheritdoc />
        public void Update(Device device)
        {
            device.UpdatedAt = DateTime.Now;
            this.repository.Update(device);
            this.auditService.Update("uppdaterade enheten {0} (REF: {1})",device.Taxon, device.Description, device.Id);
        }

        #endregion
    }
}