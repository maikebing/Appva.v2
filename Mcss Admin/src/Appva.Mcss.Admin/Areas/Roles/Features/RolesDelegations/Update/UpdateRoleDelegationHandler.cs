// <copyright file="UpdateRoleDelegationHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Create;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Update;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRoleDelegationHandler : RequestHandler<Identity<UpdateRoleDelegation>, UpdateRoleDelegation>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleDelegationHandler"/> class.
        /// </summary>
        /// <param name="roleService">The <see cref="IRoleService"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public UpdateRoleDelegationHandler(IRoleService roleService, IPersistenceContext persistence)
        {
            this.roleService = roleService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override UpdateRoleDelegation Handle(Identity<UpdateRoleDelegation> message)
        {
            var role  = this.roleService.Find(message.Id);
            var delegations = this.persistence.QueryOver<Taxon>()
                .Where(x => x.IsActive)
                  .And(x => x.IsRoot)
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == TaxonomicSchema.Delegation.Id)
                .OrderBy(x => x.Name).Asc
                .ThenBy(x => x.Weight).Asc
                .List();
            return new UpdateRoleDelegation
            {
                Delegations = this.Merge(delegations, role.Delegations).OrderBy(x => x.Label).ToList(),
                RoleName    = role.Name
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        private IList<Tickable> Merge(IList<Taxon> items, IList<Taxon> selected)
        {
            var selections = selected.Select(x => x.Id).ToList();
            var tickables  = items.Select(x => new Tickable { Id = x.Id, Label = x.Name }).ToList();
            foreach (var tickable in tickables)
            {
                if (selections.Contains(tickable.Id))
                {
                    tickable.IsSelected = true;
                }
            }
            return tickables;
        }

        #endregion
    }
}