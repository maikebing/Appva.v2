// <copyright file="ListClientsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Infrastructure;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListClientsHandler : PersistentRequestHandler<PageableQueryParams<Pageable<ListClients>>, Pageable<ListClients>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListClientsHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ListClientsHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Pageable<ListClients> Handle(PageableQueryParams<Pageable<ListClients>> message)
        {
            ListClients clients = null;
            var query = this.Persistence.QueryOver<Client>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Client>(x => x.Id).WithAlias(() => clients.Id))
                    .Add(Projections.Property<Client>(x => x.Slug.Name).WithAlias(() => clients.Slug))
                    .Add(Projections.Property<Client>(x => x.Name).WithAlias(() => clients.Name))
                    .Add(Projections.Property<Client>(x => x.Description).WithAlias(() => clients.Description))
                    .Add(Projections.Property<Client>(x => x.Image).WithAlias(() => clients.Logotype))
                )
                .TransformUsing(Transformers.AliasToBean<ListClients>());
            if (message.SearchQuery.IsNotEmpty())
            {
                query.WhereRestrictionOn(x => x.Name).IsLike(message.SearchQuery, MatchMode.Anywhere);
            }
            return PageableHelper.ToPageable<Client, ListClients>(message, query);
        }

        #endregion
    }
}