// <copyright file="CreatePermissionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreatePermissionHandler : PersistentRequestHandler<CreatePermission, Guid>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePermissionHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreatePermissionHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Guid Handle(CreatePermission message)
        {
            // TODO: Create save permission implementation!
            return (Guid) this.Persistence.Save(new Permission(message.Name, message.Description, "", PermissionAction.Create, null));
        }

        #endregion
    }
}