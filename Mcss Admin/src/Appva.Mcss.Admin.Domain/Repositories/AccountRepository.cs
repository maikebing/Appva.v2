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
    using System.Linq;
    using System.Linq.Expressions;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Persistence;
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
        IRepository<Account>, 
        IListRepository<Account>, 
        IUpdateRepository<Account>
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
        /// <param name="username">The unique username.</param>
        /// <returns>An <see cref="Account"/> if found, else null.</returns>
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
        /// <param name="user">The current user.</param>
        /// <param name="expiringDate">The expiring date</param>
        /// <param name="taxonFilter">The path to filter</param>
        /// <returns>List of <see cref="Account"/></returns>
        IList<AccountModel> ListByExpiringDelegation(Account user, string taxonFilter, DateTime expiringDate, Guid? filterByIssuerId = null);

        /// <summary>
        /// Search for accounts to given search-criteria
        /// </summary>
        /// <param name="model">The <see cref="SearchAccountModel"/></param>
        /// <param name="page">The current page, must be > 0</param>
        /// <param name="pageSize">The page-size</param>
        /// <returns>A <see cref="Paged"/> of <see cref="AccountModel"/></returns>
        Paged<AccountModel> Search(SearchAccountModel model, int page = 1, int pageSize = 10);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AccountRepository : Repository<Account>, IAccountRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public AccountRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region IAccountRepository Members.

        /// <inheritdoc />
        public Account FindByPersonalIdentityNumber(PersonalIdentityNumber personalIdentityNumber)
        {
            var accounts = this.Context
                .QueryOver<Account>()
                    .Where(x => x.PersonalIdentityNumber == personalIdentityNumber)
                .List();
            if (accounts.Count == 0)
            {
                return null;
            }
            if (accounts.Count > 1)
            {
                throw new NonUniqueResultException(accounts.Count);
            }
            return accounts.First();
        }

        /// <inheritdoc />
        public Account FindByUserName(string username)
        {
            var accounts = this.Context
                .QueryOver<Account>()
                    .Where(x => x.IsActive)
                      .And(x => x.IsPaused == false)
                      .And(x => x.UserName == username)
                .List();
            if (accounts.Count == 0)
            {
                return null;
            }
            if (accounts.Count > 1)
            {
                throw new NonUniqueResultException(accounts.Count);
            }
            return accounts.First();
        }

        /// <inheritdoc />
        public Account FindByHsaId(string hsaId)
        {
            var accounts = this.Context
                .QueryOver<Account>()
                    .Where(x => x.IsActive)
                      .And(x => x.IsPaused == false)
                      .And(x => x.HsaId    == hsaId)
                .List();
            if (accounts.Count == 0)
            {
                return null;
            }
            if (accounts.Count > 1)
            {
                throw new NonUniqueResultException(accounts.Count);
            }
            return accounts.First();
        }

        /// <inheritdoc />
        public IList<AccountModel> ListByExpiringDelegation(Account user, string taxonFilter, DateTime expiringDate, Guid? filterByIssuerId = null)
        {
            
            Account accountAlias       = null;
            Delegation delegationAlias = null;
            Taxon taxonAlias           = null;
            Taxon delegationtaxonAlias = null;
            Role roleAlias             = null;
            AccountModel accountModel  = null;
            Expression<Func<bool>> delegationFilter;
            if (filterByIssuerId.HasValue && filterByIssuerId.GetValueOrDefault() != Guid.Empty)
            {
                delegationFilter = () => delegationAlias.IsActive == true         && 
                                         delegationAlias.Pending  == false        && 
                                         delegationAlias.EndDate  <= expiringDate && 
                                         delegationAlias.CreatedBy.Id == filterByIssuerId.GetValueOrDefault();
            }
            else
            {
                delegationFilter = () => delegationAlias.IsActive == true  && 
                                         delegationAlias.Pending  == false && 
                                         delegationAlias.EndDate  <= expiringDate;
            }
            var ids = user.GetRoleDelegationAccess().Select(x => x.Id).ToArray();
            var query = this.Context.QueryOver<Account>(() => accountAlias)
                .Where(x => x.IsActive)
                  .And(x => x.IsPaused == false)
                .Inner.JoinAlias(x => x.Roles, () => roleAlias)
                    .Where(() => roleAlias.IsActive)
                      .And(() => roleAlias.IsVisible)
                .Inner.JoinAlias(x => x.Delegations, () => delegationAlias, delegationFilter)
                    .Inner.JoinAlias(() => delegationAlias.Taxon, () => delegationtaxonAlias)
                        .WhereRestrictionOn(() => delegationtaxonAlias.Parent.Id).IsIn(ids)
                    .Inner.JoinAlias(() => delegationAlias.OrganisationTaxon, () => taxonAlias, TaxonFilterRestrictions.Pipe<Taxon>(x => x.Path, taxonFilter))
                /*.JoinQueryOver<Location>(x => x.Locations)
                    .JoinQueryOver<Taxon>(x => x.Taxon)
                        .WhereRestrictionOn(x => x.Path).IsLike(taxonFilter, MatchMode.Start)*/
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
        public Paged<AccountModel> Search(SearchAccountModel model, int page = 1, int pageSize = 10)
        {
            var pageQuery = PageQuery.New(page, pageSize);   
            Account accountAlias       = null;
            Role roleAlias             = null;
            Delegation delegationAlias = null;
            Location locationAlias     = null;
            Taxon taxonAlias           = null;
            var query = this.Context.QueryOver<Account>(() => accountAlias)
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
                query
                    .WhereRestrictionOn(expression)
                    .IsLike(model.SearchQuery, MatchMode.Anywhere)
                    .OrderBy(x => x.LastName);
            }
            if (model.RoleFilterId.HasValue)
            {
                query.Inner.JoinAlias(x => x.Roles, () => roleAlias)
                    .Where(() => roleAlias.Id == model.RoleFilterId)
                      .And(() => roleAlias.IsVisible);
            }
            else
            {
                query.Inner.JoinAlias(x => x.Roles, () => roleAlias)
                    .Where(Restrictions.Or(
                        Restrictions.On   (() => roleAlias.Accounts).IsNull,
                        Restrictions.Where(() => roleAlias.IsVisible)
                    ));
            }
            if (model.OrganisationFilterTaxonPath.IsNotEmpty())
            {
                query
                    .Inner.JoinAlias(x => x.Locations, () => locationAlias)
                    .Inner.JoinAlias<Taxon>(x => locationAlias.Taxon, () => taxonAlias, TaxonFilterRestrictions.Pipe<Taxon>(x => x.Path, model.OrganisationFilterTaxonPath));
            }
            if (model.IsFilterByCreatedByEnabled)
            {
                query.Inner.JoinAlias<Delegation>(x => x.Delegations, () => delegationAlias, () => delegationAlias.CreatedBy.Id == model.CurrentUserId);
            }
            else if (model.DelegationFilterId.HasValue)
            {
                query.Inner.JoinQueryOver<Delegation>(x => x.Delegations)
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
                      .And(x => x.Pending    == false)
                      .And(x => x.Account.Id == accountAlias.Id)
                      .And(x => x.EndDate    != null)
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

            var isEditableForCurrentUser = QueryOver.Of<Location>()
                .Where(x => x.Account.Id == accountAlias.Id)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                    .WhereRestrictionOn(x => x.Path)
                        .IsLike(model.CurrentUserLocationPath, MatchMode.Start)
                .Select(
                    Projections.Conditional(
                        Restrictions.Gt(Projections.Count<Location>(x => x.Id), 0), 
                        Projections.Constant(true), 
                        Projections.Constant(false))).Take(1);
            //// Merges queries and selects needed columns and order rows for main query
            AccountModel accountModel = null;
            var mainQuery = query.Clone()
                .Select(
                Projections.Distinct(
                    Projections.ProjectionList()
                        .Add(Projections.Property<Account>(x => x.Id).WithAlias(() => accountModel.Id))
                        .Add(Projections.SqlFunction("COALESCE", NHibernateUtil.Boolean, Projections.SubQuery<Delegation>(showAlertOnDaysLeftSubquery), Projections.Constant(false)).WithAlias(() => accountModel.HasExpiringDelegation))
                        .Add(Projections.SqlFunction("COALESCE", NHibernateUtil.Boolean, Projections.SubQuery<Location>(isEditableForCurrentUser), Projections.Constant(false)).WithAlias(() => accountModel.IsEditableForCurrentUser))
                        .Add(Projections.SubQuery<Delegation>(daysLeftSubquery).WithAlias(() => accountModel.DelegationDaysLeft))
                        .Add(Projections.Property<Account>(x => x.IsActive).WithAlias(() => accountModel.IsActive))
                        .Add(Projections.Property<Account>(x => x.FirstName).WithAlias(() => accountModel.FirstName))
                        .Add(Projections.Property<Account>(x => x.LastName).WithAlias(() => accountModel.LastName))
                        .Add(Projections.Property<Account>(x => x.FullName).WithAlias(() => accountModel.FullName))
                        .Add(Projections.Property<Account>(x => x.IsPaused).WithAlias(() => accountModel.IsPaused))
                        .Add(Projections.Property<Account>(x => x.Title).WithAlias(() => accountModel.Title))
                        .Add(Projections.Property<Account>(x => x.IsSynchronized).WithAlias(() => accountModel.IsSynchronized))
                        .Add(Projections.Property<Account>(x => x.LastSynchronized).WithAlias(() => accountModel.LastSynchronized))
                        .Add(Projections.Property<Account>(x => x.PersonalIdentityNumber).WithAlias(() => accountModel.PersonalIdentityNumber))
                    ));                
            //// Ordering and transforming
            mainQuery.OrderByAlias(() => accountModel.HasExpiringDelegation).Desc
                .ThenByAlias(() => accountModel.LastName).Asc
                .TransformUsing(NHibernate.Transform.Transformers.AliasToBean<AccountModel>());
            var count = query.Clone().Select(Projections.CountDistinct("Id")).SingleOrDefault<int>();
            var items = mainQuery.Skip(pageQuery.Skip).Take(pageQuery.PageSize).List<AccountModel>();
            return Paged<AccountModel>.New(pageQuery, items, count);
        }

        #endregion

        #region IUpdateRepository<Account> Members.

        /// <inheritdoc /> 
        public void Update(Account entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.Version = entity.Version++;
            this.Context.Update<Account>(entity);
        }

        #endregion

        #region IListRepository Members.

        /// <inheritdoc />
        public IList<Account> List()
        {
            return this.Context.QueryOver<Account>().List();
        }

        #endregion
    }
}