// <copyright file="DeviceAlertRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Contracts;
    using Entities;
    using Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IDeviceAlertRepository :
        IRepository,
        IUpdateRepository<DeviceAlert>,
        ISaveRepository<DeviceAlert>
    {
        /// <summary>
        /// Find a device alert by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DeviceAlert Find(Guid id);

        /// <summary>
        /// Get a single escalation level.
        /// </summary>
        /// <returns></returns>
        EscalationLevel GetEscalationLevel(Guid id);

        /// <summary>
        /// Lists all escalation levels.
        /// </summary>
        /// <returns></returns>
        IList<EscalationLevel> GetEscalationLevels();

        /// <summary>
        /// Returns a filtered collection of <see cref="Taxon"/> by specified ID:s.
        /// </summary>
        /// <param name="ids">The ID:s to retrieve</param>
        /// <returns>A filtered collection of <see cref="Taxon"/></returns>
        IList<Taxon> ListAllIn(params Guid[] ids);

        /// <summary>
        /// Deletes a device alert.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(DeviceAlert entity);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeviceAlertRepository : IDeviceAlertRepository
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceAlertRepository"/> class.
        /// </summary>
        public DeviceAlertRepository(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region IDeviceAlertRepository Members.

        /// <inheritdoc />
        public DeviceAlert Find(Guid id)
        {
            return this.context.QueryOver<DeviceAlert>()
                .Where(x => x.Device.Id == id)
                    .SingleOrDefault();
        }

        /// <inheritdoc />
        public EscalationLevel GetEscalationLevel(Guid id)
        {
            return this.context.QueryOver<EscalationLevel>()
                .Where(x => x.Id == id)
                    .SingleOrDefault();
        }

        /// <inheritdoc />
        public IList<EscalationLevel> GetEscalationLevels()
        {
            return this.context.QueryOver<EscalationLevel>().List();
        }

        /// <inheritdoc />
        public IList<Taxon> ListAllIn(params Guid[] ids)
        {
            return this.context.QueryOver<Taxon>()
                .AndRestrictionOn(x => x.Id)
                    .IsIn(ids)
                        .List();
        }

        /// <inheritdoc />
        public void Delete(DeviceAlert entity)
        {
            this.context.Delete<DeviceAlert>(entity);
        }

        #endregion

        #region IUpdateRepository Members.

        /// <inheritdoc />
        public void Update(DeviceAlert entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.Version = entity.Version++;
            this.context.Update<DeviceAlert>(entity);
        }

        #endregion

        #region ISaveRepository Members.
        
        /// <inheritdoc />
        public void Save(DeviceAlert entity)
        {
            this.context.Save(entity);
        }
        
        #endregion
    }
}