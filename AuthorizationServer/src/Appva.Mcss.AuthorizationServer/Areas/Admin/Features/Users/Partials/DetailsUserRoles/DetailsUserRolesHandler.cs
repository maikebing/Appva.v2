// <copyright file="DetailsUserRolesHandler.cs" company="Appva AB">
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
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class DetailsUserRolesHandler : PersistentRequestHandler<DetailsUserRolesId, IList<DetailsUserRoles>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsUserRolesHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public DetailsUserRolesHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override IList<DetailsUserRoles> Handle(DetailsUserRolesId message)
        {
            DetailsUserRoles detailsUserRoles = null;
            return this.Persistence.QueryOver<Role>()
                .JoinQueryOver<User>(x => x.Users)
                .Where(x => x.Id == message.Id)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Role>(x => x.Name).WithAlias(() => detailsUserRoles.Name))
                    .Add(Projections.Property<Role>(x => x.Description).WithAlias(() => detailsUserRoles.ToolTip))
                )
                .TransformUsing(Transformers.AliasToBean<DetailsUserRoles>())
                .List<DetailsUserRoles>();
        }

        #endregion
    }
}