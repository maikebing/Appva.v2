// <copyright file="UpdateRoleDelegationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Roles.List
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Create;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Update;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRoleDelegationPublisher : RequestHandler<UpdateRoleDelegation, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleDelegationPublisher"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public UpdateRoleDelegationPublisher(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(UpdateRoleDelegation message)
        {
            var ids    = message.Delegations.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
            var taxons = this.persistence.QueryOver<Taxon>()
                .AndRestrictionOn(x => x.Id)
                .IsIn(ids)
                .List();
            var role         = this.persistence.Get<Role>(message.Id);
            role.Delegations = taxons;
            this.persistence.Update(role);
            return true;
        }

        #endregion
    }
}