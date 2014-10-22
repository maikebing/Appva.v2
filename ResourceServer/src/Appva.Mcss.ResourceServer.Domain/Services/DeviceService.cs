// <copyright file="InventoryService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Domain.Services
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Domain.Repositories;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Returns the id of the Device root taxon 
        /// </summary>
        /// <param name="deviceId">Id of the device</param>
        /// <returns>Taxon id</returns>
        string GetDeviceOrganisationRootId(Guid deviceId);

        /// <summary>
        /// Returns available taxon for the device
        /// </summary>
        /// <param name="deviceId">Id of the device</param>
        /// <param name="taxonId">Id of the taxon</param>
        /// <returns>Taxon id</returns>
        Guid GetFilterTaxonIdForDevice(Guid deviceId, Guid taxonId);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal class DeviceService : IService, IDeviceService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IDeviceRepository"/>.
        /// </summary>
        private readonly IDeviceRepository deviceRepository;

        /// <summary>
        /// The <see cref="ITaxonRepository"/>.
        /// </summary>
        private readonly ITaxonRepository taxonRepository;

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settingsService;


        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceService"/> class.
        /// </summary>
        /// <param name="deviceRepository">The <see cref="IDeviceRepository"/></param>
        public DeviceService(
            IDeviceRepository deviceRepository,
            ITaxonRepository taxonRepository,
            ISettingsService settingsService
        )
        {
            this.deviceRepository = deviceRepository;
            this.taxonRepository = taxonRepository;
            this.settingsService = settingsService;
        }

        #endregion

        #region IDeviceService Members.

        /// <inheritdoc />
        public string GetDeviceOrganisationRootId(Guid deviceId)
        {
            if (this.settingsService.Get<bool>("MCSS.Security.Device.LockToOrgTaxon", false))
            {
                if (deviceId.IsNotEmpty())
                {
                    var device = this.deviceRepository.Get(deviceId);
                    return device.Taxon.Id.ToString();
                }  
            }
            
            return string.Empty;
        }

        /// <inheritdoc />
        public Guid GetFilterTaxonIdForDevice(Guid deviceId, Guid taxonId)
        {
            if (this.settingsService.Get<bool>("MCSS.Security.Device.LockToOrgTaxon", false))
            {
                if (deviceId.IsNotEmpty())
                {
                    var device = this.deviceRepository.Get(deviceId);
                    if (taxonId.IsNotEmpty())
                    {
                        var taxon = this.taxonRepository.Get(taxonId);
                        if (taxon.Path.Contains(device.Taxon.Id.ToString()))
                        {
                            return taxonId;
                        }
                    }
                    return device.Taxon.Id;
                }
            }

            return taxonId;
        }

        #endregion
    }
}