using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NHibernate.Criterion;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Business;
using Appva.Mcss.Web.ViewModels;
using Appva.Core.Extensions;
using Appva.Mcss.Web.Mappers;
using System.Web.Mvc;
using Appva.Mcss.Infrastructure;
namespace Appva.Mcss.Web.Controllers {

    public class SearchAccountCommand : Command<SearchViewModel<AccountViewModel>> {

        public Account Identity { get; set; }
        public string SearchQuery { get; set; }
        public int? Page { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaused { get; set; }
        public Guid? FilterByDelegation { get; set; }
        public Guid? FilterByTitle { get; set; }
        public bool FilterByCreatedByIdentity { get; set; }

        public override void Execute() {

            var pageSize = 10;
            var pageIndex = Page ?? 1;
            var firstResult = (pageIndex - 1) * pageSize;
            var query = Session.QueryOver<Account>()
                .Where(x => x.Active == IsActive)
                .And(x => x.IsPaused == IsPaused);

            if (SearchQuery.IsNotEmpty()) {
                Expression<Func<Account, object>> expression = x => x.FullName;
                if (SearchQuery.First(2).Is(Char.IsNumber)) {
                    expression = x => x.UniqueIdentifier;
                }
                query.Where(Restrictions.On<Account>(expression).IsLike(SearchQuery, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName);
            }
            if (FilterByTitle.HasValue) {
                Role role = null;
                query.Left.JoinAlias(x => x.Roles, () => role)
                    .Where(() => role.Id == FilterByTitle);
            }

            if (FilterCache.HasCache()) {
                var taxon = FilterCache.GetOrSet(Identity, Session);
                 query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
            }

            if (FilterByCreatedByIdentity) {
                query.Left.JoinQueryOver<Delegation>(x => x.Delegations)
                    .Where(x => x.CreatedBy.Id == Identity.Id);
            } else if (FilterByDelegation.HasValue) {
                query.Left.JoinQueryOver<Delegation>(x => x.Delegations)
                    .Where(x => x.Taxon.Id == FilterByDelegation.Value 
                        && x.Active == true && x.Pending == false);
            }

            
            var accountMap = new Dictionary<Guid, AccountViewModel>();
            IEnumerable<Account> accounts = null;
            if (!Identity.Roles.Any(x => x.MachineName.StartsWith(RoleUtils.Developer))) {
                accounts = query.List().Where(r => r.Roles.All(x => !x.MachineName.StartsWith(RoleUtils.AdminPrefix)));
            } else {
                accounts = query.List();
            }
            foreach (var account in accounts) {
                var delegations = account.Delegations.Where(x => x.Active).ToList();
                if (delegations.Count > 0) {
                    foreach (var delegation in delegations) {
                        var daysLeft = delegation.EndDate.Subtract(DateTime.Now.Latest()).Days;
                        if (accountMap.ContainsKey(account.Id)) {
                            if (daysLeft < accountMap[account.Id].DaysLeft) {
                                accountMap[account.Id].DaysLeft = (daysLeft < 50) ? daysLeft : 99999;
                                accountMap[account.Id].ShowAlertOnDaysLeft = daysLeft < 50;
                            }
                        } else {
                            accountMap.Add(account.Id, new AccountViewModel() {
                                Active = account.Active,
                                Id = account.Id,
                                FirstName = account.FirstName,
                                LastName = account.LastName,
                                FullName = account.FullName,
                                Title = TitleHelper.GetTitle(account.Roles),
                                UniqueIdentifier = account.UniqueIdentifier,
                                Account = account,
                                DaysLeft = (daysLeft < 50) ? daysLeft : 99999,
                                ShowAlertOnDaysLeft = daysLeft < 50,
                                IsPaused = account.IsPaused
                            });
                        }
                    }
                } else {
                    if (!FilterByDelegation.HasValue && !FilterByCreatedByIdentity) {
                        if (!accountMap.ContainsKey(account.Id)) {
                            accountMap.Add(account.Id, new AccountViewModel() {
                                Active = account.Active,
                                Id = account.Id,
                                FirstName = account.FirstName,
                                LastName = account.LastName,
                                FullName = account.FullName,
                                Title = TitleHelper.GetTitle(account.Roles),
                                UniqueIdentifier = account.UniqueIdentifier,
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
            Result = new SearchViewModel<AccountViewModel>() {
                Items = AccountMapper.ToListOfAccountViewModel(Session, items),
                PageNumber = pageIndex,
                PageSize = pageSize,
                TotalItemCount = accountList.Count
            };
        }

    }

}