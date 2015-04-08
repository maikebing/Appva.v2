// <copyright file="AccountTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Transformers
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Models;
    using Appva.Repository;

    #endregion

    /// <summary>
    /// Account transforming.
    /// </summary>
    public static class AccountTransformer
    {
        /// <summary>
        /// Transforms an <see cref="Account"/> to an <see cref="SessionStateModel"/>
        /// </summary>
        /// <param name="account">The <see cref="Account"/> to be transformed</param>
        /// <returns>A <see cref="SessionStateModel{AccountModel}"/></returns>
        public static SessionStateModel<AccountModel> ToSessionBoundAccount(Account account)
        {
            return new SessionStateModel<AccountModel>()
            {
                TimeOut = 2880,
                Entity = ToSingle(account)
            };
        }

        /// <summary>
        /// Transforms an <see cref="Account"/> to an <see cref="AccountModel"/>
        /// </summary>
        /// <param name="account">The <see cref="Account"/> to be transformed</param>
        /// <returns>A <see cref="AccountModel"/></returns>
        public static AccountModel ToSingle(Account account)
        {
            return new AccountModel
            {
                Id = account.Id,
                FullName = account.FullName,
                PersonalIdentityNumber = account.UniqueIdentifier,
                Delegations = DelegationTransformer.ToDelegation(account.Delegations),
                Beacon = account.Beacon != null ? new BeaconModel { Major=account.Beacon.Major, Minor=account.Beacon.Minor } : null
            };
        }

        /// <summary>
        /// Transforms a collection of <see cref="Account"/>.
        /// </summary>
        /// <param name="accounts">The <see cref="IList{Account}"/> to be transformed</param>
        /// <returns>A collection of <see cref="AccountModel"/></returns>
        public static IList<AccountModel> ToList(IList<Account> accounts)
        {
            var retval = new List<AccountModel>();
            if (accounts != null && accounts.Count > 0)
            {
                foreach (var account in accounts)
                {
                    retval.Add(ToSingle(account));
                }
            }

            return retval;
        }

        /// <summary>
        /// Transforms a collection of <see cref="Accounts"/> to a dictionary of account-id and name
        /// </summary>
        /// <param name="accounts">The accounts</param>
        /// <returns>Dictionary of with account-id and name</returns>
        public static IDictionary<Guid, string> ToSimpleList(IList<Account> accounts)
        {
            var retval = new Dictionary<Guid, string>();
            if (accounts == null)
            {
                return retval;
            }

            foreach (var account in accounts)
            {
                retval.Add(account.Id, account.FullName);
            }
            return retval;
        }

        /// <summary>
        /// Transforms a pageable set of <see cref="Account"/>.
        /// </summary>
        /// <param name="pageableSet">The pageable set of <see cref="Account"/></param>
        /// <returns>A <see cref="PagingModel{AccountModel}"/></returns>
        public static PagingModel<AccountModel> ToPaging(PageableSet<Account> pageableSet)
        {
            return new PagingModel<AccountModel>()
            {
                MaxResults = pageableSet.TotalCount,
                PageNumber = pageableSet.CurrentPage,
                NextPageNumber = pageableSet.NextPage,
                Entities = ToList(pageableSet.Entities)
            };
        }
    }
}