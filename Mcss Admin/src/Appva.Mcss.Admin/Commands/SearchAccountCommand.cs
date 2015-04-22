using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NHibernate.Criterion;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;
using Appva.Core.Extensions;
using Appva.Mcss.Web.Mappers;
using System.Web.Mvc;
using Appva.Cqrs;
using Appva.Persistence;
using Appva.Core.Resources;

namespace Appva.Mcss.Web.Controllers {

    public class SearchAccountCommand : IRequest<SearchViewModel<AccountViewModel>>
    {
        public Account Identity
        {
            get;
            set;
        }
        public string SearchQuery
        {
            get;
            set;
        }
        public int? Page
        {
            get;
            set;
        }
        public bool IsActive
        {
            get;
            set;
        }
        public bool IsPaused
        {
            get;
            set;
        }
        public Guid? FilterByDelegation
        {
            get;
            set;
        }
        public Guid? FilterByTitle
        {
            get;
            set;
        }
        public bool FilterByCreatedByIdentity
        {
            get;
            set;
        }
    }

    public sealed class SearchAccountHandler : RequestHandler<SearchAccountCommand, SearchViewModel<AccountViewModel>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAccountHandler"/> class.
        /// </summary>
        public SearchAccountHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<PatientQuickSearch, IEnumerable<object>> Overrides.

        /// <inheritdoc /> 
        public override SearchViewModel<AccountViewModel> Handle(SearchAccountCommand message)
        {
            var pageSize = 10;
            var pageIndex = message.Page ?? 1;
            var firstResult = (pageIndex - 1) * pageSize;
            var query = this.persistence.QueryOver<Account>()
                .Where(x => x.IsActive == message.IsActive)
                .And(x => x.IsPaused == message.IsPaused);

            if (message.SearchQuery.IsNotEmpty())
            {
                Expression<Func<Account, object>> expression = x => x.FullName;
                if (message.SearchQuery.First(2).Is(Char.IsNumber))
                {
                    expression = x => x.PersonalIdentityNumber;
                }
                query.Where(Restrictions.On<Account>(expression)
                    .IsLike(message.SearchQuery, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName);
            }
            if (message.FilterByTitle.HasValue)
            {
                Role role = null;
                query.Left.JoinAlias(x => x.Roles, () => role)
                    .Where(() => role.Id == message.FilterByTitle);
            }

            if (FilterCache.HasCache()) {
                var taxon = FilterCache.GetOrSet(message.Identity, this.persistence);
                 query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
            }

            if (message.FilterByCreatedByIdentity)
            {
                query.Left.JoinQueryOver<Delegation>(x => x.Delegations)
                    .Where(x => x.CreatedBy.Id == message.Identity.Id);
            }
            else if (message.FilterByDelegation.HasValue)
            {
                query.Left.JoinQueryOver<Delegation>(x => x.Delegations)
                    .Where(x => x.Taxon.Id == message.FilterByDelegation.Value 
                        && x.IsActive == true && x.Pending == false);
            }

            
            var accountMap = new Dictionary<Guid, AccountViewModel>();
            IEnumerable<Account> accounts = null;
            if (!message.Identity.Roles.Any(x => x.MachineName.StartsWith(RoleTypes.Developer)))
            {
                accounts = query.List().Where(r => r.Roles.All(x => !x.MachineName.StartsWith(RoleTypes.AdminPrefix)));
            } else {
                accounts = query.List();
            }
            foreach (var account in accounts) {
                var delegations = account.Delegations.Where(x => x.IsActive).ToList();
                if (delegations.Count > 0) {
                    foreach (var delegation in delegations) {
                        var daysLeft = delegation.EndDate.Subtract(DateTime.Now.LastInstantOfDay()).Days;
                        if (accountMap.ContainsKey(account.Id)) {
                            if (daysLeft < accountMap[account.Id].DaysLeft) {
                                accountMap[account.Id].DaysLeft = (daysLeft < 50) ? daysLeft : 99999;
                                accountMap[account.Id].ShowAlertOnDaysLeft = daysLeft < 50;
                            }
                        } else {
                            accountMap.Add(account.Id, new AccountViewModel() {
                                Active = account.IsActive,
                                Id = account.Id,
                                FirstName = account.FirstName,
                                LastName = account.LastName,
                                FullName = account.FullName,
                                Title = TitleHelper.GetTitle(account.Roles),
                                UniqueIdentifier = account.PersonalIdentityNumber,
                                Account = account,
                                DaysLeft = (daysLeft < 50) ? daysLeft : 99999,
                                ShowAlertOnDaysLeft = daysLeft < 50,
                                IsPaused = account.IsPaused
                            });
                        }
                    }
                } else {
                    if (!message.FilterByDelegation.HasValue && !message.FilterByCreatedByIdentity)
                    {
                        if (!accountMap.ContainsKey(account.Id)) {
                            accountMap.Add(account.Id, new AccountViewModel() {
                                Active = account.IsActive,
                                Id = account.Id,
                                FirstName = account.FirstName,
                                LastName = account.LastName,
                                FullName = account.FullName,
                                Title = TitleHelper.GetTitle(account.Roles),
                                UniqueIdentifier = account.PersonalIdentityNumber,
                                DaysLeft = 99999,
                                Account = account,
                                IsPaused = account.IsPaused
                            });
                        }
                    }
                }
            }
            var accountList = accountMap.Values.ToList().OrderBy(x => x.DaysLeft).ThenBy(x => x.LastName).ToList();
            var items = accountList.Skip(firstResult).Take(pageSize).ToList();
            return new SearchViewModel<AccountViewModel>() {
                Items = AccountMapper.ToListOfAccountViewModel(this.persistence, items),
                PageNumber = pageIndex,
                PageSize = pageSize,
                TotalItemCount = accountList.Count
            };
        }

        #endregion
    }

}