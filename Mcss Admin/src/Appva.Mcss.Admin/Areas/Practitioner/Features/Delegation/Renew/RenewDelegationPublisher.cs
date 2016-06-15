// <copyright file="RenewDelegationPublisher.cs" company="Appva AB">
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
using Appva.Mcss.Admin.Application.Security.Identity;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Areas.Models;
using Appva.Mcss.Admin.Areas.Practitioner.Models;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class RenewDelegationPublisher : RequestHandler<RenewDelegationsModel, ListDelegation>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService auditService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RenewDelegationPublisher"/> class.
        /// </summary>
        public RenewDelegationPublisher(IIdentityService identityService, ITaxonomyService taxonomyService, IAccountService accountService, IAuditService auditService,  IPersistenceContext persistence)
        {
            this.identityService    = identityService;
            this.taxonomyService    = taxonomyService;
            this.accountService     = accountService;
            this.auditService       = auditService;
            this.persistence        = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListDelegation Handle(RenewDelegationsModel message)
        {
            var currentUser = this.accountService.Find(this.identityService.PrincipalId);
            var taxon = this.taxonomyService.Get(message.DelegationCategoryId);
            var delegations = this.persistence.QueryOver<Delegation>()
                .Where(x => x.Account.Id == message.AccountId)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                .Where(x => x.Parent.Id == taxon.Parent.Id)
                .List();
            foreach (var delegation in delegations)
            {
                this.persistence.Save(new ChangeSet
                {
                    EntityId = delegation.Id,
                    Entity = typeof(Delegation).ToString(),
                    Revision = delegation.Version,
                    ModifiedBy = currentUser,
                    Changes = new List<Change> {
                        new Change {
                            Property = "StartDate",
                            OldState = delegation.StartDate.ToShortDateString(),
                            NewState = message.StartDate.ToShortDateString(),
                            TypeOf = typeof(DateTime).ToString()
                        },
                        new Change {
                            Property = "EndDate",
                            OldState = delegation.EndDate.ToShortDateString(),
                            NewState = message.EndDate.ToShortDateString(),
                            TypeOf = typeof(DateTime).ToString()
                        },
                        new Change {
                            Property = "CreatedBy",
                            OldState = delegation.CreatedBy.ToString(),
                            NewState = currentUser.ToString(),
                            TypeOf = typeof(Account).ToString()
                        }
                    }
                });
                delegation.StartDate = message.StartDate;
                delegation.EndDate = message.EndDate;
                delegation.UpdatedAt = DateTime.Now;
                delegation.CreatedBy = currentUser;
                this.persistence.Update(delegation);
                this.auditService.Update(
                    "förnyade start och slutdatum för delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för användare {4} (REF: {5}).",
                    delegation.Name,
                    delegation.StartDate,
                    delegation.EndDate,
                    delegation.Id,
                    delegation.Account.FullName,
                    delegation.Account.Id);
            }
            return new ListDelegation
            {
                Id = message.AccountId
            };
        }

        #endregion
    }
}