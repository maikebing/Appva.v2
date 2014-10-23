// <copyright file="CreateUserRolesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mvc.Imaging;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateUserRolesHandler : PersistentRequestHandler<CreateUserRoles, CreateUserRoles>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserRolesHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateUserRolesHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override CreateUserRoles Handle(CreateUserRoles message)
        {
            Tickable tickable = null;
            var roles = this.Persistence.QueryOver<Role>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Role>(x => x.Name).WithAlias(() => tickable.Name))
                    .Add(Projections.Property<Role>(x => x.Id).WithAlias(() => tickable.Id)))
                .TransformUsing(Transformers.AliasToBean<Tickable>())
                .List<Tickable>();
            return new CreateUserRoles()
            {
                Roles = roles
            };
        }

        #endregion
    }
}