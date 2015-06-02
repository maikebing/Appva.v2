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
    using System.Collections.Generic;
    using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;

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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesHandler"/> class.
        /// </summary>
        /// <param name="service"></param>
        public UpdateRolesHandler(IAccountService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateRolesForm Handle(UpdateRoles message)
        {
            /*
             var account = this.Session.Get<Account>(id);
            var query = this.Session.QueryOver<Role>().Where(x => x.Active)
                .OrderBy(x => x.Weight).Asc.ThenBy(x => x.Name).Asc;
            if (!User.IsInRole(RoleUtils.AppvaAccount))
            {
                query.Where(x => x.IsVisible == true);
            }
            var roles = query.List();
            return this.View(new AccountRolesViewModel
            {
                Roles = roles.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Selected = true
                }).ToList(),
                SelectedRoles = account.Roles.Select(x => x.Id.ToString()).ToArray()
            });
             */
            //this.service.InActivate(this.accounts.Find(message.AccountId));
            return null;
        }

        #endregion
    }
}