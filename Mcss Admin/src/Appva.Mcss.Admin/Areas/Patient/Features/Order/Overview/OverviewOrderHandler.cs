// <copyright file="OverviewOrderHandler.cs" company="Appva AB">
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
    internal sealed class OverviewOrderHandler : RequestHandler<OverviewOrder, OrderOverviewViewModel>
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

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewOrderHandler"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IPatientService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="ILogService"/> implementation</param>
        public OverviewOrderHandler(
            IPatientService patientService, ITaskService taskService, ILogService logService, IPersistenceContext persistence,
            IPatientTransformer transformer, ITaxonFilterSessionHandler filtering)
        {
            this.patientService = patientService;
            this.taskService = taskService;
            this.logService = logService;
            this.persistence = persistence;
            this.transformer = transformer;
            this.filtering = filtering;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override OrderOverviewViewModel Handle(OverviewOrder message)
        {
            var filterTaxon = this.filtering.GetCurrentFilter();
            var orders = this.persistence.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                .And(x => x.RefillInfo.Refill)
                .Fetch(x => x.RefillInfo.RefillOrderedBy).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            orders.JoinQueryOver<Patient>(x => x.Patient)
                .Where(x => x.IsActive)
                .And(x => !x.Deceased)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(filterTaxon.Id.ToString(), MatchMode.Anywhere));
            return new OrderOverviewViewModel
            {
                Orders = orders.OrderBy(x => x.RefillInfo.RefillOrderedDate).Asc.List()
            };
        }

        #endregion
    }
}