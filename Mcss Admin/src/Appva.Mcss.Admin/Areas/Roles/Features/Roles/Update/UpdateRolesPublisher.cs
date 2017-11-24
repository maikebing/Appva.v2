// <copyright file="CreateRolesHandler.cs" company="Appva AB">
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
    internal sealed class UpdateRolesPublisher : RequestHandler<UpdateRole, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPermissionService"/>.
        /// </summary>
        private readonly IPermissionService service;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesPublisher"/> class.
        /// </summary>
        public UpdateRolesPublisher(IPermissionService service, IPersistenceContext persistence)
        {
            this.service = service;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<UpdateRole, bool> Overrides.

        /// <inheritdoc />
        public override bool Handle(UpdateRole message)
        {
            var role = this.persistence.Get<Role>(message.Id);
            role.Name = message.Name;
            role.Description = message.Description;
            role.Permissions = this.service.ListAllIn(message.Permissions.Where(x => x.IsSelected).Select(x => new Guid(x.Id)).ToArray());
            role.IsVisible = message.IsHiddenRole == false;
            this.persistence.Update(role);
            return true;
        }

        #endregion
    }
}