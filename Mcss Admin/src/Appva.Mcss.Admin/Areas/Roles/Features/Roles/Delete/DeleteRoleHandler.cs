// <copyright file="ListRolesHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Areas.Roles.Roles.Update;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteRoleHandler : RequestHandler<DeleteRole, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoleHandler"/> class.
        /// </summary>
        public DeleteRoleHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        #endregion

        #region RequestHandler<DeleteRole, bool> Overrides.

        /// <inheritdoc />
        public override bool Handle(DeleteRole message)
        {
            return true;
        }

        #endregion
    }
}