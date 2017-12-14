// <copyright file="ObservationItemService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Mcss.Domain.Unit;

    #endregion

    /// <summary>
    /// Interface IObservationItemService
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IService" />
    public interface IObservationItemService : IService
    {
        /// <summary>
        /// Creates the value.
        /// </summary>
        /// <param name="observation">The observation.</param>
        /// <param name="account">The account.</param>
        /// <param name="value">The value.</param>
        void Create(Observation observation, Account account, IUnit value);

        /// <summary>
        /// Gets the specified item identifier.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>ObservationItem.</returns>
        ObservationItem Get(Guid itemId);

        /// <summary>
        /// Lists the specified observation identifier.
        /// </summary>
        /// <param name="observationId">The observation identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>IList&lt;ObservationItem&gt;.</returns>
        IList<ObservationItem> List(Guid observationId, DateTime? startDate, DateTime? endDate);
    }

    /// <summary>
    /// Class ObservationItemService. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Application.Services.IObservationItemService" />
    public sealed class ObservationItemService : IObservationItemService
    {
        #region Variables.

        /// <summary>
        /// The observation item repository
        /// </summary>
        private readonly IObservationItemRepository observationItemRepository;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationItemService"/> class.
        /// </summary>
        /// <param name="observationItemRepository">The observation item repository.</param>
        /// <param name="auditService">The audit service.</param>
        public ObservationItemService(IObservationItemRepository observationItemRepository, IAuditService auditService)
        {
            this.observationItemRepository = observationItemRepository;
            this.auditService = auditService;
        }

        #endregion

        /// <inheritdoc />
        public void Create(Observation observation, Account account, IUnit value)
        {
            var item = ObservationItem.New(observation, Measurement.New<IUnit>(value), null, Signature.New(account, SignedData.New(value)));
            this.observationItemRepository.Save(item);
            this.auditService.Create(observation.Patient, "skapade mätvärde (ref, {0})", item.Id);
        }

        /// <inheritdoc />
        public ObservationItem Get(Guid itemId)
        {
            return this.observationItemRepository.Get(itemId);
        }

        /// <inheritdoc />
        public IList<ObservationItem> List(Guid observationId, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
            {
                return this.observationItemRepository.ListByDate(observationId, startDate.Value, endDate.HasValue ? endDate.Value: DateTime.UtcNow);
            }
            return this.observationItemRepository.List(observationId);
        }
    }
}
