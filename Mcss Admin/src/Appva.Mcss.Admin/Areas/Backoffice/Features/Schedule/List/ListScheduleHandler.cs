// <copyright file="ListScheduleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Persistence;
    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListScheduleHandler : RequestHandler<Parameterless<ListScheduleModel>, ListScheduleModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/> 
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/> 
        /// </summary>
        private readonly ITaxonFilterSessionHandler filter;
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListScheduleHandler"/> class.
        /// <param name="scheduleService"><see cref="IScheduleService"/></param>
        /// <param name="persistenceContext"><see cref="IPersistenceContext"/></param>
        /// <param name="filter"><see cref="ITaxonFilterSessionHandler"/></param>
        /// </summary>
        public ListScheduleHandler(IScheduleService scheduleService, IPersistenceContext persistenceContext, ITaxonFilterSessionHandler filter)
        {
            this.scheduleService = scheduleService;
            this.persistenceContext = persistenceContext;
            this.filter = filter;    
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListScheduleModel Handle(Parameterless<ListScheduleModel> message)
        {
            Taxon organization = null;

            var patientFilter = this.persistenceContext.QueryOver<Patient>()
                                .JoinAlias(x => x.Taxon, () => organization)
                                .WhereRestrictionOn(() => organization.Path).IsLike(this.filter.GetCurrentFilter().Path + "%")
                                .List();

            return new ListScheduleModel
            {
                Schedules = this.scheduleService.GetSchedules().Where(x => x.ScheduleType == ScheduleType.Action).ToList(),
                AllSchedules = this.persistenceContext.QueryOver<Schedule>().List(),
                PatientFilterList = patientFilter,
                SequenceList = this.persistenceContext.QueryOver<Sequence>().List()
            };
        }

        #endregion
    }
}