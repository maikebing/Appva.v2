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
        IIdentityRepository<DeviceAlert>,
        IUpdateRepository<DeviceAlert>
    {
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

        #endregion

        #region IIdentityRepository Members.

        /// <inheritdoc />
        public DeviceAlert Find(Guid id)
        {
            return this.context.Get<DeviceAlert>(id);
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
    }
}