// <copyright file="CreateMedicationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Medication.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateMedicationHandler : AsyncRequestHandler<CreateMedicationRequest, CreateMedicationModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IMedicationService"/>.
        /// </summary>
        private readonly IMedicationService medicationService;

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="IDelegationService"/>
        /// </summary>
        private readonly IDelegationService delegationService;

        #endregion

        #region Const.

        private static IList<int> times = new List<int> {
            6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 1, 2, 3, 4, 5
        };

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMedicationHandler"/> class.
        /// </summary>
        /// <param name="scheduleService">The schedule service.</param>
        /// <param name="medicationService">The medication service.</param>
        /// <param name="delegationService">The delegation service.</param>
        public CreateMedicationHandler(
            IScheduleService scheduleService,
            IMedicationService medicationService,
            IDelegationService delegationService
            )
        {
            this.scheduleService    = scheduleService;
            this.medicationService  = medicationService;
            this.delegationService  = delegationService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override async Task<CreateMedicationModel> Handle(CreateMedicationRequest message)
        {
            var schedule    = this.scheduleService.Find(message.Schedule);
            var delegations = schedule.ScheduleSettings.DelegationTaxon != null ? 
                this.delegationService.ListDelegationTaxons(byRoot: schedule.ScheduleSettings.DelegationTaxon.Id, includeRoots: false) :
                null;
                
            //// If ordination-id is set, the request is for an original packaged medication.
            if (message.OrdinationId != Int64.MinValue)
            {
                var medication = await this.medicationService.Find(message.OrdinationId, message.Id);
                return new CreateMedicationModel
                {
                    ScheduleId = message.Schedule,
                    Delegations = delegations.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                    OnNeedBasis = medication.Type == OrdinationType.NeedBased,
                    OnNeedBasisStartDate = medication.Type == OrdinationType.NeedBased ? medication.OrdinationStartsAt : (DateTime?)null,
                    OnNeedBasisEndDate = medication.Type == OrdinationType.NeedBased ? medication.EndsAt : (DateTime?)null,
                    StartDate = medication.Type != OrdinationType.NeedBased ? medication.OrdinationStartsAt : (DateTime?)null,
                    EndDate = medication.Type != OrdinationType.NeedBased ? medication.EndsAt : (DateTime?)null,
                    Name = medication.Article.Name,
                    Interval = medication.DosageScheme != null && (int)medication.DosageScheme.GetPeriodicity < 8 ? (int)medication.DosageScheme.GetPeriodicity : 0,
                    Times = medication.DosageScheme != null && (int)medication.DosageScheme.GetPeriodicity < 8 ?
                        this.GetTimes(times, medication.DosageScheme.Dosages.Select(x => x.Time).ToList()) :
                        this.GetTimes(times, new List<int>())
                };
            }

            //// If ordination-id NOT is set, the request is for dosage-dispensed bags.
            return new CreateMedicationModel
            {
                ScheduleId = message.Schedule,
                Delegations = delegations.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                OnNeedBasis = false,
                StartDate = DateTime.Now,
                Name = "Dos-påse",
                Interval = 1,
                Times = this.GetTimes(times, new List<int>(){ 8,12,16,20 })
            };
        }

        #endregion

        #region Private helpers.

        /// <summary>
        /// Gets the times.
        /// </summary>
        /// <param name="times">The times.</param>
        /// <param name="selected">The selected.</param>
        /// <returns></returns>
        private IList<CheckBoxViewModel> GetTimes(IList<int> times, IList<int> selected)
        {
            return times.Select(x => new CheckBoxViewModel()
            {
                Id = x,
                Checked = selected.Contains(x)
            }).ToList();
        }

        #endregion
    }
}