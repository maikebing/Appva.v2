// <copyright file="DeleteDelegationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteDelegationPublisher : RequestHandler<DeleteDelegationModel, ListDelegation>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IDelegationService"/>
        /// </summary>
        private readonly IDelegationService delegationService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDelegationPublisher"/> class.
        /// </summary>
        public DeleteDelegationPublisher(ISettingsService settings, IDelegationService delegationService)
        {
            this.settings           = settings;
            this.delegationService  = delegationService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListDelegation Handle(DeleteDelegationModel message)
        {
            var delegation = this.delegationService.Find(message.DelegationId);
            if (this.settings.Find<bool>(ApplicationSettings.SpecifyReasonOnDelegationRemoval))
            {
                this.delegationService.Delete(delegation, message.ReasonText.IsEmpty() ? message.Reason : message.ReasonText);
            }
            else
            {
                this.delegationService.Delete(delegation);
            }
           
            return new ListDelegation { Id = delegation.Account.Id };
        }

        #endregion
    }
}