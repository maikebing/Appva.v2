// <copyright file="CreateObservationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// CreateMeasurementHandler class.
    /// </summary>
    public class CreateObservationHandler : RequestHandler<CreateObservation, CreateObservationModel>
    {
        #region Variables.

        /// <summary>
        /// The delegation service.
        /// </summary>
        private readonly IDelegationService delegationService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateObservationHandler"/> class.
        /// </summary>
        /// <param name="delegationService">The delegation service.</param>
        public CreateObservationHandler(IDelegationService delegationService)
        {
            this.delegationService = delegationService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateObservationModel Handle(CreateObservation message)
        {
            var delegations          = this.delegationService.ListDelegationTaxons();
            var selectDelegationList = delegations.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return new CreateObservationModel(message.Id, selectDelegationList);
        }

        #endregion
    }
}