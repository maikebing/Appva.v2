// <copyright file="ListScheduleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        private readonly IPersistenceContext persistenceContext;

        private readonly ITaxonFilterSessionHandler filter;
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListScheduleHandler"/> class.
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
            Taxon org = null;


            var schedulesUsed = this.persistenceContext.QueryOver<Schedule>().List();


            var patientFilter = this.persistenceContext.QueryOver<Patient>()
                                .JoinAlias(x => x.Taxon, () => org)
                                .WhereRestrictionOn(() => org.Path).IsLike(filter.GetCurrentFilter().Path + "%")
                                .List();


            var patientFilterId = new List<Guid>();
           
            foreach (var item in patientFilter)
            {
                patientFilterId.Add(item.Id);
            }




                                                                    







            return new ListScheduleModel
            {
                Schedules = this.scheduleService.GetSchedules().Where(x => x.ScheduleType == ScheduleType.Action).ToList(),
                SchedulesUsedBy = schedulesUsed,
                PatientFilterIdList = patientFilterId
            };
        }

        #endregion
    }
}