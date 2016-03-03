// <copyright file="AlertWidgetHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Security.Identity;
    using NHibernate.Transform;
    using NHibernate.Criterion;
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Admin.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AlertWidgetHandler : RequestHandler<AlertWidget, AlertOverviewViewModel>
    {
        #region Variables.

		/// <summary>
        /// The <see cref="IIdentityService"/>.
		/// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

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
        /// Initializes a new instance of the <see cref="HandleAlertHandler"/> class.
		/// </summary>
        /// <param name="settings">The <see cref="IIdentityService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="IAccountService"/> implementation</param>
        public AlertWidgetHandler(IIdentityService identityService, IAccountService accountService, ITaxonFilterSessionHandler filtering, IPatientTransformer transformer, IPersistenceContext persistence)
		{
            this.transformer = transformer;
            this.identityService = identityService;
            this.accountService = accountService;
            this.persistence = persistence;
            this.filtering = filtering;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override AlertOverviewViewModel Handle(AlertWidget message)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var scheduleSettings = TaskService.GetRoleScheduleSettingsList(account);
            var taxon = this.filtering.GetCurrentFilter();
            Task taskAlias = null;
            Schedule scheduleAlias = null;
            Taxon taxonAlias = null;
            Patient patient = null;
            var query = this.persistence.QueryOver<Patient>(() => patient)
                .Where(x => x.IsActive)
                  .And(x => !x.Deceased)
                .JoinAlias(x => x.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                    .IsLike(taxon.Id.ToString(), MatchMode.Anywhere))
                .JoinQueryOver(x => x.Tasks, () => taskAlias)
                    .Where(() => taskAlias.IsActive)
                      .And(() => taskAlias.Delayed)
                      .And(() => taskAlias.DelayHandled == false)
                .JoinQueryOver(x => x.Schedule, () => scheduleAlias, NHibernate.SqlCommand.JoinType.InnerJoin, Restrictions.Where(() => scheduleAlias.IsActive && scheduleAlias.Patient.Id == patient.Id))
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray());
            var countAll = query.Clone().Select(Projections.CountDistinct<Patient>(x => x.Id))
                .FutureValue<int>()
                .Value; 
            IList<Patient> patients = null;
            if (! message.Status.Equals("notsigned"))
            {
                patients = query.TransformUsing(new DistinctRootEntityResultTransformer()).List();
            }
            query = query.And(() => taskAlias.IsCompleted == false);
            if (patients == null)
            {
                patients = query.TransformUsing(new DistinctRootEntityResultTransformer()).List();
            }
            var countNotSigned = query.Select(Projections.CountDistinct<Patient>(x => x.Id))
                .FutureValue<int>()
                .Value;
            return new AlertOverviewViewModel
            {
                Patients = this.transformer.ToPatientList(patients),
                CountAll = countAll,
                CountNotSigned = countNotSigned
            };
        }

        #endregion
    }
}