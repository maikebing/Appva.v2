// <copyright file="OverviewOrderHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
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
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewOrderHandler"/> class.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        /// <param name="filtering">The <see cref="ITaxonFilterSessionHandler"/></param>
        public OverviewOrderHandler(
            IIdentityService identityService,
            ISettingsService settingsService,
            IPersistenceContext persistence,
            ITaxonFilterSessionHandler filtering)
        {
            this.identityService = identityService;
            this.settingsService = settingsService;
            this.persistence = persistence;
            this.filtering = filtering;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override OrderOverviewViewModel Handle(OverviewOrder message)
        {
            var orderListConfiguration = this.settingsService.Find(ApplicationSettings.OrderListConfiguration);
            var account = this.persistence.Get<Account>(this.identityService.PrincipalId);
            var scheduleList = TaskService.GetRoleScheduleSettingsList(account);
            var filterTaxon = this.filtering.GetCurrentFilter();
            Schedule scheduleAlias = null;
            ScheduleSettings scheduleSettingsAlias = null;
            var orders = this.persistence.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                  .And(x => x.RefillInfo.Refill)
                .Fetch(x => x.RefillInfo.RefillOrderedBy).Eager
                .JoinAlias(x => x.Schedule, () => scheduleAlias)
                    .Where(() => scheduleAlias.IsActive)
                    .JoinAlias(x => scheduleAlias.ScheduleSettings, () => scheduleSettingsAlias)
                        .WhereRestrictionOn(() => scheduleSettingsAlias.Id).IsIn(scheduleList.Select(x => x.Id).ToArray())
                .TransformUsing(new DistinctRootEntityResultTransformer());
            orders.JoinQueryOver<Patient>(x => x.Patient)
                .Where(x => x.IsActive)
                  .And(x => !x.Deceased)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(filterTaxon.Id.ToString(), MatchMode.Anywhere));
            return new OrderOverviewViewModel
            {
                Orders = orders.OrderBy(x => x.RefillInfo.RefillOrderedDate).Asc.List(),
                HasMigratedArticles = orderListConfiguration.HasMigratedArticles,
                SequencesWithoutArticlesCount = orders.Where(x => x.Article == null).RowCount()
            };
        }

        #endregion
    }
}