// <copyright file="DeviceRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// <author>
//     <a href="mailto:christoffer.rosenqvist@invativa.se">Christoffer Rosenqvist</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Domain.Entities;
    using Appva.Persistence;
    using Appva.Repository;

    #endregion

    /// <summary>
    /// The device repository.
    /// </summary>
    public interface IDeviceRepository : IRepository<Device>
    {
        /// <summary>
        /// Returns the <see cref="Device"/> by uuid.
        /// </summary>
        /// <param name="uuid">The device uuid</param>
        /// <returns>A <see cref="Device"/></returns>
        Device GetByUuid(string uuid);
    }

    /// <summary>
    /// Implementation of <see cref="IDeviceRepository"/>.
    /// </summary>
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public DeviceRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region IDeviceRepository Members

        /// <inheritdoc />
        public Device GetByUuid(string uuid)
        {
            return Where(x => x.Uuid == uuid).SingleOrDefault();
        }

        #endregion
    }
}