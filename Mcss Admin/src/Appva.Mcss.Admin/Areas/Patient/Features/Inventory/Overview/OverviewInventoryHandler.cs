// <copyright file="RecountInventoryItemHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;
    using Appva.Mcss.Web.Mappers;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Web.Controllers;
using NHibernate.Criterion;
using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class OverviewInventoryHandler : RequestHandler<OverviewInventory, InventoryOverviewViewModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewInventoryHandler"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IPatientService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="ILogService"/> implementation</param>
        public OverviewInventoryHandler(
            IPatientService patientService, ITaskService taskService, ILogService logService, IPersistenceContext persistence,
            IPatientTransformer transformer)
        {
            this.patientService = patientService;
            this.taskService = taskService;
            this.logService = logService;
            this.persistence = persistence;
            this.transformer = transformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override InventoryOverviewViewModel Handle(OverviewInventory message)
        {
            var taxon = FilterCache.Get(this.persistence);
            /*if (!FilterCache.HasCache())
            {
                taxon = FilterCache.GetOrSet(Identity(), this.context);
            }*/
            var now = DateTime.Now;
            var lastStockCalculationDate = now.AddDays(-30).AddDays(7);
            RecountOverviewItemViewModel dto = null;
            Inventory inventory = null;
            Patient patient = null;
            Schedule schedule = null;
            var allStockCounts = this.persistence.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                .JoinAlias(x => x.Schedule, () => schedule)
                    .Where(() => schedule.IsActive)
                .JoinAlias(x => x.Inventory, () => inventory)
                    .Where(() => inventory.LastRecount < lastStockCalculationDate)
                .JoinAlias(x => x.Patient, () => patient)
                    .Where(() => patient.IsActive && !patient.Deceased)
                    .JoinQueryOver<Taxon>(() => patient.Taxon)
                        .WhereRestrictionOn(x => x.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere)
                .SelectList(list => list
                    .Select(() => patient.FullName).WithAlias(() => dto.Patient)
                    .Select(() => patient.Id).WithAlias(() => dto.PatientId)
                    .Select(() => inventory.LastRecount).WithAlias(() => dto.LastRecount)
                    .Select(() => inventory.Id).WithAlias(() => dto.InventoryId)
                    .Select(x => x.Name).WithAlias(() => dto.SequenceName)
                )
                .OrderBy(() => inventory.LastRecount).Asc
                .TransformUsing(Transformers.AliasToBean<RecountOverviewItemViewModel>())
                .List<RecountOverviewItemViewModel>();
            return new InventoryOverviewViewModel
            {
                DelayedStockCounts = allStockCounts.Where(x => now.Subtract(x.LastRecount.GetValueOrDefault()).TotalDays > 30).ToList(),
                StockCounts = allStockCounts.Where(x => now.Subtract(x.LastRecount.GetValueOrDefault()).TotalDays <= 30).ToList(),
                StockControlIntervalInDays = 30
            };
        }

        #endregion
    }
}