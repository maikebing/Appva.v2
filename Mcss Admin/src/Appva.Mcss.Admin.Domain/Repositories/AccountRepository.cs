// <copyright file="AccountRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Linq.Expressions;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Appva.Persistence;
    using Appva.Repository;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Dialect.Function;
    using System.Collections.Generic;
    using NHibernate.SqlCommand;
    using Appva.NHibernateUtils.Restrictions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IAccountRepository : 
        IIdentityRepository<Account>, 
        IUpdateRepository<Account>,
        IProxyRepository<Account>,
        IListRepository<Account>,
        IRepository
    {
        /// <summary>
        /// Returns a user account by its unique Personal Identity Number. 
        /// </summary>
        /// <param name="personalIdentityNumber">The unique Personal Identity Number</param>
        /// <returns>An <see cref="Account"/> if found, else null</returns>
        Account FindByPersonalIdentityNumber(PersonalIdentityNumber personalIdentityNumber);

        /// <summary>
        /// Returns a user account by its unique user name. 
        /// </summary>
        /// <param name="username">The unique username</param>
        /// <returns>An <see cref="Account"/> if found, else null</returns>
        Account FindByUserName(string username);

        /// <summary>
        /// Locates a user account by its HSA ID. 
        /// </summary>
        /// <param name="hsaId">The unique HSA id</param>
        /// <returns>An <see cref="Account"/> if found, else null</returns>
        Account FindByHsaId(string hsaId);

        /// <summary>
        /// Lists all accounts with delegations expiring in given number of days
        /// </summary>
        /// <param name="expiringDate">The expiring date</param>
        /// <param name="taxonFilter">The path to filter</param>
        /// <returns>List of <see cref="Account"/></returns>
        IList<AccountModel> ListByExpiringDelegation(string taxonFilter, DateTime expiringDate);

        /// <summary>
        /// Search for accounts to given search-criteria
        /// </summary>
        /// <param name="model">The <see cref="SearchAccountModel"/></param>
        /// <param name="page">The current page, must be > 0</param>
        /// <param name="pageSize">The page-size</param>
        /// <returns>A <see cref="PageableSet"/> of <see cref="AccountModel"/></returns>
        PageableSet<AccountModel> Search(SearchAccountModel model, int page = 1, int pageSize = 10);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AccountRepository : IAccountRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> implementation.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">
        /// The <see cref="IPersistenceContext"/> implementation
        /// </param>
        public AccountRepository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IIdentifierRepository<Account> Members

        /// <inheritdoc /> 
        public Account Find(Guid id)
        {
            return this.persistenceContext.Get<Account>(id);
        }

        #endregion

        #region IAccountRepository Members.

        /// <inheritdoc />
        public Account FindByPersonalIdentityNumber(PersonalIdentityNumber personalIdentityNumber)
        {
            var accounts = this.persistenceContext.QueryOver<Account>()
                .Where(x => x.PersonalIdentityNumber == personalIdentityNumber)
                .List();
            if (accounts.Count == 1)
            {
                return accounts[0];
            }
            //// FIXME: should throw exception if we have several!
            return null;
        }

        /// <inheritdoc />
        public Account FindByUserName(string username)
        {
            var accounts = this.persistenceContext.QueryOver<Account>()
                .Where(x => x.IsActive)
                .And(x => x.IsPaused == false)
                .And(x => x.UserName == username)
                .List();
            if (accounts.Count == 1)
            {
                return accounts[0];
            }
            return null;
        }

        /// <inheritdoc />
        public Account FindByHsaId(string hsaId)
        {
            var accounts = this.persistenceContext.QueryOver<Account>()
                .Where(x => x.IsActive)
                .And(x => x.IsPaused == false)
                  .And(x => x.HsaId    == hsaId)
                .List();
            if (accounts.Count == 1)
            {
                return accounts[0];
            }
            //// FIXME: should throw exception if we have several!
            return null;
        }

        /// <inheritdoc />
        public IList<AccountModel> ListByExpiringDelegation(string taxonFilter, DateTime expiringDate)
        {
            Account accountAlias = null;
            Delegation delegationAlias = null;
            Taxon taxonAlias = null;
            Role roleAlias = null;
            AccountModel accountModel = null;
            var query = this.persistenceContext.QueryOver<Account>(() => accountAlias)
                .Where(x => x.IsActive)
                .And(x => x.IsPaused == false)
                .Left.JoinAlias(x => x.Roles, () => roleAlias)
                    .Where(() => roleAlias.IsActive)
                    .And(() => roleAlias.IsVisible)
                .Inner.JoinAlias(x => x.Delegations, () => delegationAlias, () => delegationAlias.IsActive == true && delegationAlias.Pending == false && delegationAlias.EndDate <= expiringDate)
                    .Inner.JoinAlias(() => delegationAlias.OrganisationTaxon, () => taxonAlias, TaxonFilterRestrictions.Pipe<Taxon>(x => x.Path, taxonFilter))
                .Select(
                    Projections.ProjectionList()
                        .Add(Projections.Group<Account>(x => x.Id).WithAlias(() => accountModel.Id))
                        .Add(Projections.Constant(true).WithAlias(() => accountModel.HasExpiringDelegation))
                        .Add(Projections.Min(Projections.SqlFunction(
                                        new SQLFunctionTemplate(
                                            NHibernateUtil.Int32,
                                            "DateDiff(day, '" + DateTime.Now.ToString("yyyy-MM-dd") + "', EndDate)"),
                                        NHibernateUtil.Int32)).WithAlias(() => accountModel.DelegationDaysLeft))
                        .Add(Projections.Group<Account>(x => x.IsActive).WithAlias(() => accountModel.IsActive))
                        .Add(Projections.Group<Account>(x => x.FirstName).WithAlias(() => accountModel.FirstName))
                        .Add(Projections.Group<Account>(x => x.LastName).WithAlias(() => accountModel.LastName))
                        .Add(Projections.Group<Account>(x => x.FullName).WithAlias(() => accountModel.FullName))
                        .Add(Projections.Group<Account>(x => x.IsPaused).WithAlias(() => accountModel.IsPaused))
                        .Add(Projections.Group<Account>(x => x.PersonalIdentityNumber).WithAlias(() => accountModel.PersonalIdentityNumber)));

            //// Ordering and transforming
            query.OrderByAlias(() => accountModel.DelegationDaysLeft).Desc
                .TransformUsing(NHibernate.Transform.Transformers.AliasToBean<AccountModel>());

            return query.List<AccountModel>();
        }

        /// <inheritdoc />
        public PageableSet<AccountModel> Search(SearchAccountModel model, int page = 1, int pageSize = 10)
        {
            //// Main query - As a view
            Account account = null;
            var query = this.persistenceContext.QueryOver<Account>(() => account)
                .Where(x => x.IsActive == model.IsFilterByIsActiveEnabled);
            //// If searching in active-users, check paused, else list all 
            if (model.IsFilterByIsActiveEnabled)
            {
                query.Where(x => x.IsPaused == model.IsFilterByIsPausedEnabled);
            } 
            //// Filter by search criterias
            if (model.SearchQuery.IsNotEmpty())
            {
                Expression<Func<Account, object>> expression = x => x.FullName;
                if (model.SearchQuery.First(2).Is(char.IsNumber))
                {
                    expression = x => x.PersonalIdentityNumber.Value;
                }
                query.Where(Restrictions.On<Account>(expression).IsLike(model.SearchQuery, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName);
            }
            if (model.RoleFilterId.HasValue)
            {
                Role role = null;
                query.Left.JoinAlias(x => x.Roles, () => role)
                    .Where(() => role.Id == model.RoleFilterId)
                    .And(() => role.IsVisible);
            }
            else
            {
                Role role = null;
                query.Left.JoinAlias(x => x.Roles, () => role)
                    .Where(Restrictions.Or(
                        Restrictions.On(() => role.Accounts).IsNull,
                        Restrictions.Where(() => role.IsVisible)
                    ));
            }
            if (model.OrganisationFilterTaxonPath.IsNotEmpty())
            {
                Taxon taxonAlias = null;
                query.Inner.JoinAlias<Taxon>(x => x.Taxon, () => taxonAlias, TaxonFilterRestrictions.Pipe<Taxon>(x => x.Path, model.OrganisationFilterTaxonPath));
            }
            if (model.IsFilterByCreatedByEnabled)
            {
                Delegation delegationAlias = null;
                query.Left.JoinAlias<Delegation>(x => x.Delegations, () => delegationAlias, () => delegationAlias.CreatedBy.Id == model.CurrentUserId);
            }
            else if (model.DelegationFilterId.HasValue)
            {
                query.Left.JoinQueryOver<Delegation>(x => x.Delegations)
                    .Where(x => x.Taxon.Id == model.DelegationFilterId.Value
                        && x.IsActive == true && x.Pending == false);
            }
            if (model.IsFilterByIsSynchronizedEnabled.HasValue)
            {
                query.Where(x => x.IsSynchronized == model.IsFilterByIsSynchronizedEnabled.GetValueOrDefault());
            }

            //// Subqueries to get days until delegation expires 
            var delegationSubquery = QueryOver.Of<Delegation>()
                    .Where(x => x.IsActive)
                      .And(x => !x.Pending)
                      .And(x => x.Account.Id == account.Id)
                      .And(x => x.EndDate != null)
                    .OrderBy(x => x.EndDate).Asc;
            var daysLeftSubquery = delegationSubquery.Clone()
                .Select(Projections.SqlFunction(
                                    new SQLFunctionTemplate(
                                        NHibernateUtil.Int32,
                                        "DateDiff(day, '" + DateTime.Now.ToString("yyyy-MM-dd") + "', EndDate)"),
                                    NHibernateUtil.Int32)).Take(1);
            var showAlertOnDaysLeftSubquery = delegationSubquery.Clone()
                .Select(
                    Projections.Conditional(
                        Restrictions.Lt(
                            Projections.SqlFunction(
                                new SQLFunctionTemplate(
                                    NHibernateUtil.Int32,
                                    "DateDiff(day, '" + DateTime.Now.ToString("yyyy-MM-dd") + "', EndDate)"),
                                NHibernateUtil.Int32),
                            50), 
                        Projections.Constant(true), 
                        Projections.Constant(false))).Take(1);

            //// Merges queries and selects needed columns and order rows for main query
            AccountModel accountModel = null;
            var mainQuery = query.Clone().Select(
                Projections.Distinct(
                    Projections.ProjectionList()
                    .Add(Projections.Property<Account>(x => x.Id).WithAlias(() => accountModel.Id))
                    .Add(Projections.SqlFunction("COALESCE", NHibernateUtil.Boolean, Projections.SubQuery<Delegation>(showAlertOnDaysLeftSubquery), Projections.Constant(false)).WithAlias(() => accountModel.HasExpiringDelegation))
                    .Add(Projections.SubQuery<Delegation>(daysLeftSubquery).WithAlias(() => accountModel.DelegationDaysLeft))
                    .Add(Projections.Property<Account>(x => x.IsActive).WithAlias(() => accountModel.IsActive))
                    .Add(Projections.Property<Account>(x => x.FirstName).WithAlias(() => accountModel.FirstName))
                    .Add(Projections.Property<Account>(x => x.LastName).WithAlias(() => accountModel.LastName))
                    .Add(Projections.Property<Account>(x => x.FullName).WithAlias(() => accountModel.FullName))
                    .Add(Projections.Property<Account>(x => x.IsPaused).WithAlias(() => accountModel.IsPaused))
                    .Add(Projections.Property<Account>(x => x.Title).WithAlias(() => accountModel.Title))
                    .Add(Projections.Property<Account>(x => x.IsSynchronized).WithAlias(() => accountModel.IsSynchronized))
                    .Add(Projections.Property<Account>(x => x.LastSynchronized).WithAlias(() => accountModel.LastSynchronized))
                    .Add(Projections.Property<Account>(x => x.PersonalIdentityNumber).WithAlias(() => accountModel.PersonalIdentityNumber))));                
            //// Ordering and transforming
            mainQuery.OrderByAlias(() => accountModel.HasExpiringDelegation).Desc
                .ThenByAlias(() => accountModel.LastName).Asc
                .TransformUsing(NHibernate.Transform.Transformers.AliasToBean<AccountModel>());
            //// Checks that page is greater then 0
            if (page < 1)
            {
                page = 1;
            }
            //// Number of rows to skip
            var skip = (page - 1) * pageSize;
            return new PageableSet<AccountModel>()
            {
                CurrentPage = page,
                NextPage    = page++,
                PageSize    = pageSize,
                TotalCount  = query.Clone().Select(Projections.CountDistinct("Id")).SingleOrDefault<int>(),
                Entities    = mainQuery.Skip(skip).Take(pageSize).List<AccountModel>()               
            };
        }

        #endregion

        #region IUpdateRepository<Account> Members.

        /// <inheritdoc /> 
        public void Update(Account entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.Version = entity.Version++;
            this.persistenceContext.Update<Account>(entity);
        }

        #endregion

        #region IProxyRepository Members.
    
        /// <inheritdoc /> 
        public Account Load(Guid id)
        {
 	        return this.persistenceContext.Session.Load<Account>(id);
        }

        #endregion

        #region IListRepository Members.

        /// <inheritdoc />
        public IList<Account> List(ulong maximumItems = long.MaxValue)
        {
            return this.persistenceContext.QueryOver<Account>().List();
        }

        #endregion
    }
}