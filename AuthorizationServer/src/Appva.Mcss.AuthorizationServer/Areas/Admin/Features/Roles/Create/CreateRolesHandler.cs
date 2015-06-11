// <copyright file="CreateRoleHandler.cs" company="Appva AB">
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
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Appva.Core.Extensions;
    using Appva.Mvc;

    #endregion

    #region Get Request Handler.

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateRoleHttpHandler : PersistentRequestHandler<NoParameter<CreateRole>, CreateRole>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoleHttpHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateRoleHttpHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override CreateRole Handle(NoParameter<CreateRole> message)
        {
            Tickable tickable = null;
            var permissions = this.Persistence.QueryOver<Permission>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Permission>(x => x.Id).WithAlias(() => tickable.Id))
                    .Add(Projections.Property<Permission>(x => x.Name).WithAlias(() => tickable.Label))
                    .Add(Projections.Property<Permission>(x => x.Description).WithAlias(() => tickable.HelpText))
                    .Add(Projections.Property<Permission>(x => x.Context).WithAlias(() => tickable.Group))
                )
                .OrderBy(x => x.Context).Asc
                .ThenBy(x => x.Name).Asc
                .TransformUsing(Transformers.AliasToBean<Tickable>())
                .List<Tickable>();
            return new CreateRole()
            {
                Permissions = permissions
            };
        }

        #endregion
    }

    #endregion

    #region Post Request Handler.

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateRoleHandler : PersistentRequestHandler<CreateRole, Guid>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoleHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateRoleHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Guid Handle(CreateRole message)
        {
            if (message.Permissions.IsEmpty() || message.Permissions.Select(x => x.IsSelected).All(x => x != true))
            {
                throw new ArgumentException("Permissions must be selected!");
            }
            var permissionIds = message.Permissions.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
            var permissions = this.Persistence.QueryOver<Permission>().Where(Restrictions.In(Projections.Property<Permission>(x => x.Id), permissionIds)).List();
            var role = new Role(message.Key, message.Name, message.Description, permissions);
            return (Guid) this.Persistence.Save(role);
        }

        #endregion
    }

    #endregion
}