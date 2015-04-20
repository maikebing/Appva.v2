// <copyright file="CreateSequenceViewHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateSequenceViewHandler : RequestHandler<CreateSequence, SequenceViewModel>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequenceViewHandler"/> class.
        /// </summary>
        public CreateSequenceViewHandler(IPersistenceContext context, ILogService logService)
        {
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region RequestHandler<CreateSequence, SequenceViewModel> Overrides.

        /// <inheritdoc />
        public override SequenceViewModel Handle(CreateSequence message)
        {
            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            return new SequenceViewModel
            {
                Delegations = this.GetDelegations(schedule),
                Times = CreateTimes().Select(x => new CheckBoxViewModel
                {
                    Id = x,
                    Checked = false
                }).ToList()
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: MOVE?
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetDelegations(Schedule schedule)
        {
            var delegations = this.context.QueryOver<Taxon>()
                .Where(x => x.IsActive == true)
                .And(x => x.IsRoot == false)
                .And(x => x.Parent == schedule.ScheduleSettings.DelegationTaxon)
                .OrderBy(x => x.Weight).Asc
                .ThenBy(x => x.Name).Asc
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == TaxonomicSchema.Delegation.Id)
                .List();

            return delegations.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }

        /// <summary>
        /// TODO: REFACTOR?
        /// </summary>
        /// <returns></returns>
        private IList<int> CreateTimes()
        {
            return new List<int>
            {
                6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 1, 2, 3, 4, 5
            };
        }

        #endregion
    }
}