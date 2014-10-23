// <copyright file="ListUserHandler.cs" company="Appva AB">
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
    internal sealed class ListUserHandler : PersistentRequestHandler<PageableQueryParams<Pageable<ListUser>>, Pageable<ListUser>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListUserHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ListUserHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Pageable<ListUser> Handle(PageableQueryParams<Pageable<ListUser>> message)
        {
            var perPage = 20;
            ListUser user = null;
            var query = this.Persistence.QueryOver<User>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<User>(x => x.Id).WithAlias(() => user.Id))
                    .Add(Projections.Property<User>(x => x.Slug.Name).WithAlias(() => user.Slug))
                    .Add(Projections.Property<User>(x => x.PersonalIdentityNumber).WithAlias(() => user.PersonalIdentityNumber))
                    .Add(Projections.Property<User>(x => x.FullName.FirstName).WithAlias(() => user.FirstName))
                    .Add(Projections.Property<User>(x => x.FullName.LastName).WithAlias(() => user.LastName))
                )
                .TransformUsing(Transformers.AliasToBean<ListUser>());
            var page = message.Page == 0 ? 1 : message.Page;
            var items = query.Skip((int) (page - 1) * perPage).Take(perPage).List<ListUser>();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();
            return new Pageable<ListUser>
            {
                Page = page,
                Next = ((page + 1) > (totalCount.Value / perPage)) ? page + 1 : page,
                Prev = ((page - 1) > 0) ? page - 1 : page,
                TotalCount = (long) totalCount.Value,
                PerPage = perPage,
                Items = items
            };
        }

        #endregion
    }
}