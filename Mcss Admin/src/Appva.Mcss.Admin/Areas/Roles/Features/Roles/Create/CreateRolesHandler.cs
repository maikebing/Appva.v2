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
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc.Html.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateRolesHandler : RequestHandler<Parameterless<CreateRole>, CreateRole>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPermissionService"/>.
        /// </summary>
        private readonly IPermissionService service;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListRolesHandler"/> class.
        /// </summary>
        public CreateRolesHandler(IPermissionService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler<Parameterless<CreateRole>, CreateRole> Overrides.

        /// <inheritdoc />
        public override CreateRole Handle(Parameterless<CreateRole> message)
        {
            return new CreateRole
            {
                Permissions = this.service.List().Select(x => new Tickable
                {
                    Id = x.Id,
                    Label = x.Name,
                    HelpText = x.Description
                }).ToList()
            };
        }

        #endregion
    }
}