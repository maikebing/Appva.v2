// <copyright file="CreateMeasurementHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// CreateMeasurementHandler class.
    /// </summary>
    public class CreateMeasurementHandler : RequestHandler<CreateMeasurement, CreateMeasurementModel>
    {
        #region Variables.

        /// <summary>
        /// The delegation service.
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The identity service.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The account service.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMeasurementHandler"/> class.
        /// </summary>
        /// <param name="delegationService">The delegation service.</param>
        /// <param name="identityService">The identity service.</param>
        /// <param name="accountService">The account service.</param>
        public CreateMeasurementHandler(IDelegationService delegationService, IIdentityService identityService, IAccountService accountService)
        {
            this.delegationService = delegationService;
            this.identityService   = identityService;
            this.accountService    = accountService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateMeasurementModel Handle(CreateMeasurement message)
        {
            //// UNRESOLVED: Permissions gällande delegationTaxon för vad vi får välja på observation nivå. Hur skall vi göra?
            var accountId   = this.identityService.PrincipalId;
            var account     = this.accountService.Find(accountId);
            var taxonFilter = account.Locations.Select(x => x.Taxon.Path).FirstOrDefault();
            var delegations = this.delegationService.List(taxonFilter, accountId); ////.ListDelegationTaxons()
            return new CreateMeasurementModel
            {
                PatientId            = message.Id,
                SelectScaleList      = MeasurementScale.All().Skip(1).Select(x => new SelectListItem { Text = x.Name(), Value = x.ToString() }),
                SelectDelegationList = delegations.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
        }

        #endregion
    }
}