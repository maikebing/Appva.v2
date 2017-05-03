// <copyright file="DeviceAlertService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Domain.Repositories;
    using Auditing;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IDeviceAlertService : IService
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
        /// Saves a device alert to database.
        /// </summary>
        /// <param name="deviceAlert"></param>
        void Save(DeviceAlert deviceAlert);

        /// <summary>
        /// Updates the device alert <see cref="DeviceAlert"/>.
        /// </summary>
        /// <param name="deviceAlert"></param>
        void Update(DeviceAlert deviceAlert);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeviceAlertService : IDeviceAlertService
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IDeviceAlertService"/>
        /// </summary>
        private readonly IDeviceAlertRepository repository;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService auditService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceAlertService"/> class.
        /// </summary>
        public DeviceAlertService(IDeviceAlertRepository repository, IAuditService auditService)
        {
            this.repository = repository;
            this.auditService = auditService;
        }

        #endregion

        #region IDeviceAlertService members.

        /// <inheritdoc />
        public DeviceAlert Find(Guid id)
        {
            return this.repository.Find(id);
        }

        /// <inheritdoc />
        public EscalationLevel GetEscalationLevel(Guid id)
        {
            return this.repository.GetEscalationLevel(id);
        }

        /// <inheritdoc />
        public IList<EscalationLevel> GetEscalationLevels()
        {
            return this.repository.GetEscalationLevels();
        }

        /// <inheritdoc />
        public IList<Taxon> ListAllIn(params Guid[] ids)
        {
            return null;
        }

        /// <inheritdoc />
        public void Save(DeviceAlert deviceAlert)
        {
            this.repository.Save(deviceAlert);
        }

        /// <inheritdoc />
        public void Update(DeviceAlert deviceAlert)
        {
            deviceAlert.UpdatedAt = DateTime.Now;
            this.repository.Update(deviceAlert);
            this.auditService.Update("uppdaterade enhetens larmkonfiguration (REF: {0})", deviceAlert.Id);
        }

        #endregion
    }
}