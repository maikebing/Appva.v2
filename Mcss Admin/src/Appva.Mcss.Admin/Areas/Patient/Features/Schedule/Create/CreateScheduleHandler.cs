// <copyright file="CreateScheduleHandler.cs" company="Appva AB">
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
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateScheduleHandler : RequestHandler<CreateSchedule, CreateScheduleForm>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identities;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateScheduleHandler"/> class.
        /// </summary>
        public CreateScheduleHandler(IPatientService patientService, IPatientTransformer transformer,
            IPersistenceContext persistence, IIdentityService identities)
        {
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
            this.identities = identities;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override CreateScheduleForm Handle(CreateSchedule message)
        {
            var account   = this.persistence.Get<Account>(this.identities.PrincipalId);
            //// Retrieve the schedule settings which the user has permissions for.
            var schedules = TaskService.GetRoleScheduleSettingsList(account);
            //// Any inactive schedulesettings will not be recreated.
            var query   = this.persistence.QueryOver<ScheduleSettings>()
                    .WhereRestrictionOn(x => x.Id)
                        .IsIn(schedules.Where(x => x.IsActive).Select(x => x.Id).ToArray())
                    .And(x => x.ScheduleType == ScheduleType.Action)
                    .OrderBy(x => x.Name)
                        .Asc;
            var items = query.List()
                    .Select(x => new SelectListItem
                    {
                        Text  = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
            return new CreateScheduleForm
            {
                Id    = message.Id,
                Items = items
            };
        }

        #endregion
    }
}