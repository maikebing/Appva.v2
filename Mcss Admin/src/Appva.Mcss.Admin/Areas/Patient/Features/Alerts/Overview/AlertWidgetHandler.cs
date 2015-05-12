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
    using Appva.Mcss.Web.Mappers;
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

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="HandleAlertHandler"/> class.
		/// </summary>
        /// <param name="settings">The <see cref="IIdentityService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="IAccountService"/> implementation</param>
        public AlertWidgetHandler(IIdentityService identityService, IAccountService accountService, IPatientTransformer transformer, IPersistenceContext persistence)
		{
            this.transformer = transformer;
            this.identityService = identityService;
            this.accountService = accountService;
            this.persistence = persistence;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override AlertOverviewViewModel Handle(AlertWidget message)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var taxon = FilterCache.Get(this.persistence);
            if (!FilterCache.HasCache())
            {
                taxon = FilterCache.GetOrSet(account, this.persistence);
            }
            Taxon taxonAlias = null;
            var query = this.persistence.QueryOver<Patient>()
                .Where(x => x.IsActive)
                .And(x => !x.Deceased)
                .And(x => x.HasUnattendedTasks)
                .JoinAlias(x => x.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                    .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
            var countAll = query.RowCount();
            IList<Patient> patients = null;
            if (! message.Status.Equals("notsigned"))
            {
                patients = query.List();
            }
            Task task = null;
            query.Inner.JoinAlias(x => x.Tasks, () => task)
                .Where(() => task.IsActive)
                .And(() => task.Delayed)
                .And(() => task.DelayHandled == false)
                .And(() => task.IsCompleted == false)
                .TransformUsing(new DistinctRootEntityResultTransformer());
            if (patients == null)
            {
                patients = query.List();
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