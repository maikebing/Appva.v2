// <copyright file="CreateSequenceHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateSequenceHandler : RequestHandler<CreateSequence, CreateSequenceForm>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IDelegationService"/>.
        /// </summary>
        private readonly IDelegationService delegations;

        /// <summary>
        /// The <see cref="IInventoryService"/>
        /// </summary>
        private readonly IInventoryService inventories;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequenceHandler"/> class.
        /// </summary>
        public CreateSequenceHandler(IPersistenceContext context, IDelegationService delegations, IInventoryService inventories)
        {
            this.context = context;
            this.delegations = delegations;
            this.inventories = inventories;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateSequenceForm Handle(CreateSequence message)
        {
            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            return new CreateSequenceForm
            {
                Id = message.Id,
                ScheduleId = schedule.Id,
                Delegations = this.delegations.ListDelegationTaxons(byRoot: schedule.ScheduleSettings.DelegationTaxon.Id, includeRoots: false)
                    .Select(x => new SelectListItem { 
                        Text = x.Name,
                        Value = x.Id.ToString() }),
                Times = CreateTimes().Select(x => new CheckBoxViewModel
                {
                    Id = x,
                    Checked = false
                }).ToList(),
                Inventories = this.inventories.Search(message.Id, true).Select(x => new SelectListItem() { Text = x.Description, Value = x.Id.ToString() }),
                CreateNewInventory = true
            };
        }

        #endregion

        #region Private Methods.

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