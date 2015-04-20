// <copyright file="AccountQuickSearch.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Web.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Core.Extensions;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using Appva.Core.Resources;

    #endregion

    /// <summary>
    /// The account quick search command.
    /// </summary>
    public class AccountQuickSearch : IRequest<IEnumerable<object>>
    {
        /// <summary>
        /// The <see cref="Account"/>.
        /// </summary>
        public Account Identity
        {
            get;
            set;
        }

        /// <summary>
        /// The term to search for.
        /// </summary>
        public string Term
        {
            get;
            set;
        }

        /// <summary>
        /// The is active filter.
        /// </summary>
        public bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// The is paused filter.
        /// </summary>
        public bool IsPaused
        {
            get;
            set;
        }
    }

    public sealed class AccountQuickSearchHandler : RequestHandler<AccountQuickSearch, IEnumerable<object>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountQuickSearchHandler"/> class.
        /// </summary>
        public AccountQuickSearchHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        /// <inheritDoc/>
        public override IEnumerable<object> Handle(AccountQuickSearch message)
        {
            var query = this.persistence.QueryOver<Account>()
                .Where(x => x.IsActive == message.IsActive)
                .And(x => x.IsPaused == message.IsPaused);
            if (message.Term.IsNotEmpty())
            {
                Expression<Func<Account, object>> expression = x => x.FullName;
                if (message.Term.First(2).Is(Char.IsNumber))
                {
                    expression = x => x.PersonalIdentityNumber;
                }
                if (FilterCache.HasCache())
                {
                    var taxon = FilterCache.GetOrSet(message.Identity, this.persistence);
                    query.JoinQueryOver<Taxon>(x => x.Taxon)
                        .Where(Restrictions.On<Taxon>(x => x.Path)
                            .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
                }
                if (!message.Identity.Roles.Any(x => x.MachineName.StartsWith(RoleTypes.Developer)))
                {
                    //// If the account is not a developer then filter out any admin roles.
                    query.JoinQueryOver<Role>(x => x.Roles)
                        .WhereRestrictionOn(x => x.MachineName).Not.IsLike(RoleTypes.AdminPrefix, MatchMode.Start);
                }
                var result = query.Where(Restrictions.On<Account>(expression)
                    .IsLike(message.Term, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName).Asc
                    .Take(10).List();
                return result.Select(x => new
                {
                    value = x.FullName
                })
                .ToList();
            }
            return new List<object>();
        }
    }
}