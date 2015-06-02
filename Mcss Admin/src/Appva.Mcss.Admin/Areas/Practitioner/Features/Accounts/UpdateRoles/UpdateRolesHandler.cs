// <copyright file="UpdateRolesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRolesHandler : RequestHandler<UpdateRoles, UpdateRolesForm>
    {
        #region Private fields.

        /// <summary>
        /// The <see cref="IAccountService"/> implementation
        /// </summary>
        private readonly IAccountService service;

        /// <summary>
        /// The <see cref="IPersistenceContext"/> implementation
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IIdentityService"/> implementation
        /// </summary>
        private readonly IIdentityService identities;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesHandler"/> class.
        /// </summary>
        /// <param name="service"></param>
        public UpdateRolesHandler(IPersistenceContext persistence, IAccountService service, IIdentityService identities)
        {
            this.service = service;
            this.persistence = persistence;
            this.identities = identities;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateRolesForm Handle(UpdateRoles message)
        {

            var account = this.persistence.Get<Account>(message.Id);
            var query = this.persistence.QueryOver<Role>().Where(x => x.IsActive)
                .OrderBy(x => x.Weight).Asc.ThenBy(x => x.Name).Asc;
            if (! this.identities.IsInRole(RoleTypes.Appva))
            {
                query.Where(x => x.IsVisible == true);
            }
            var roles = query.List();
            return new UpdateRolesForm
            {
                Roles = roles.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Selected = true
                }).ToList(),
                SelectedRoles = account.Roles.Select(x => x.Id.ToString()).ToArray()
            };
        }

        #endregion
    }
}