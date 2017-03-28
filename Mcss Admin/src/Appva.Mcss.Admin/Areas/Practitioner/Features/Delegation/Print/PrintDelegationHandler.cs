// <copyright file="PrintDelegationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Application.Transformers;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PrintDelegationHandler : RequestHandler<Identity<PrintDelegationModel>, PrintDelegationModel>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IDelegationService"/>
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintDelegationHandler"/> class.
        /// </summary>
        public PrintDelegationHandler(
            IAccountService accountService,
            IDelegationService delegationService,
            ITaxonomyService taxonomyService,
            IIdentityService identity,
            IPersistenceContext persistence,
            IAuditService auditing,
            ISettingsService settings)
        {
            this.accountService     = accountService;
            this.delegationService  = delegationService;
            this.taxonomyService    = taxonomyService;
            this.identity           = identity;
            this.persistence        = persistence;
            this.auditing           = auditing;
            this.settings           = settings;
        }

        #endregion

        #region RequestHandler Overrides.
    
        public override PrintDelegationModel Handle(Identity<PrintDelegationModel> message)
        {
            var user = this.accountService.Find(this.identity.PrincipalId);
            var account = this.accountService.Find(message.Id);
            var delegations = this.delegationService.List(
                this.identity.Principal.LocationPath(),
                byAccount: account.Id,
                createdBy: this.identity.PrincipalId,
                isActive: true);

            var knowledgeTests = this.persistence.QueryOver<KnowledgeTest>()
                .Where(x => x.IsActive == true)
                .And(x => x.Account == account)
                .List();
            
            this.auditing.Read(
                "skapade utskrift för delegeringar för användare {0} (REF: {1}).",
                    account.FullName,
                    account.Id);
            return new PrintDelegationModel
            {
                DelegationRecipient = account,
                Delegations = DelegationTransformer.ToDelegationViewModel(delegations, this.taxonomyService.List(TaxonomicSchema.Organization)),
                KnowledgeTests = knowledgeTests,
                DelegationIssuer = user,
                SendToText = this.settings.Find<string>(ApplicationSettings.PrintDelegationSendToText)
            };
        }

        #endregion
    }
}