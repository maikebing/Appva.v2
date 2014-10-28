// <copyright file="DetailsUserAuthenticationsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DetailsUserAuthenticationsHandler : PersistentRequestHandler<DetailsUserAuthenticationsId, IList<DetailsUserAuthentication>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsUserAuthenticationsHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public DetailsUserAuthenticationsHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override IList<DetailsUserAuthentication> Handle(DetailsUserAuthenticationsId message)
        {
            DetailsUserAuthentication item = null;
            return this.Persistence.QueryOver<UserAuthenticationMethod>()
                .WithSubquery.WhereProperty(x => x.Id).In(
                    QueryOver.Of<UserAuthentication>().Where(x => x.User.Id == message.Id).Select(x => x.UserAuthenticationMethod.Id)
                )
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<UserAuthenticationMethod>(x => x.Key).WithAlias(() => item.Key))
                    .Add(Projections.Property<UserAuthenticationMethod>(x => x.Name).WithAlias(() => item.Name))
                )
                .TransformUsing(Transformers.AliasToBean<DetailsUserAuthentication>())
                .List<DetailsUserAuthentication>();
        }

        #endregion
    }
}