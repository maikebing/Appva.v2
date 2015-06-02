// <copyright file="UpdateRolesFormHandler.cs" company="Appva AB">
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
    internal sealed class UpdateRolesFormHandler : RequestHandler<UpdateRolesForm, ListAccount>
    {
        #region Private fields.

        /// <summary>
        /// The <see cref="IAccountService"/> implementation
        /// </summary>
        private readonly IAccountService service;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesFormHandler"/> class.
        /// </summary>
        /// <param name="service"></param>
        public UpdateRolesFormHandler(IAccountService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListAccount Handle(UpdateRolesForm message)
        {
            /*
            var account = this.Session.Get<Account>(id);
            var roles = this.Session.QueryOver<Role>()
                    .AndRestrictionOn(x => x.Id)
                    .IsIn(model.SelectedRoles.Select(x => new Guid(x)).ToArray())
                    .List();
            account.Roles = roles;
            this.Session.Update(account);
             */
            throw new NotImplementedException();
        }

        #endregion
    }
}