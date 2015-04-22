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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListRolesHandler : RequestHandler<Parameterless<IList<Role>>, IList<Role>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListRolesHandler"/> class.
        /// </summary>
        public ListRolesHandler(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        #endregion

        #region RequestHandler<Parameterless<IList<Role>>, IList<Role>> Overrides.

        /// <inheritdoc />
        public override IList<Role> Handle(Parameterless<IList<Role>> message)
        {
            return this.roleService.List();
        }

        #endregion
    }
}