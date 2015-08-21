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
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

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

        /// <summary>
        /// The <see cref="IPersistenceContext"/> implementation
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesFormHandler"/> class.
        /// </summary>
        /// <param name="service"></param>
        public UpdateRolesFormHandler(IPersistenceContext persistence, IAccountService service)
        {
            this.service = service;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListAccount Handle(UpdateRolesForm message)
        {
            var account = this.persistence.Get<Account>(message.Id);
            var roles = (message.SelectedRoles != null && message.SelectedRoles.Length > 0) ? this.persistence.QueryOver<Role>()
                    .AndRestrictionOn(x => x.Id)
                    .IsIn(message.SelectedRoles.Select(x => new Guid(x)).ToArray())
                    .List() : null;
            this.service.UpdateRoles(account, roles);
            return new ListAccount();
        }

        #endregion
    }
}