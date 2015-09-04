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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateRolesPublisher : RequestHandler<CreateRole, bool>
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
        /// Initializes a new instance of the <see cref="CreateRolesPublisher"/> class.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="persistence"></param>
        public CreateRolesPublisher(IPermissionService service, IPersistenceContext persistence)
        {
            this.service = service;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<CreateRole, bool> Overrides.

        /// <inheritdoc />
        public override bool Handle(CreateRole message)
        {
            var permissions = this.service.ListAllIn(message.Permissions.Where(x => x.IsSelected).Select(x => x.Id).ToArray());
            this.persistence.Save(new Role
            {
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Weight = 100,
                Name = message.Name,
                Description = message.Description,
                MachineName = Guid.NewGuid().ToString(),
                IsVisible = true,
                IsDeletable = true,
                Permissions = permissions
            });
            return true;
        }

        #endregion
    }
}