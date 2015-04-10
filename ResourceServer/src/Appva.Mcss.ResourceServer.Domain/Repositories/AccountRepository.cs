// <copyright file="AccountRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Persistence;
    using Appva.Repository;
    using Mcss.Domain.Entities;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// The account repository.
    /// </summary>
    public interface IAccountRepository : IPagingAndSortingRepository<Account>
    {
        /// <summary>
        /// Verifies the account credentials.
        /// </summary>
        /// <param name="username">The account user name</param>
        /// <param name="password">The account password</param>
        /// <returns><see cref="Account"/></returns>
        Account VerifyCredentialsByUsername(string username, string password);

        /// <summary>
        /// Verifies credentials by Personal Identity Number and password.
        /// </summary>
        /// <param name="personalIdentityNumber">The account personal identity number</param>
        /// <param name="password">The account password</param>
        /// <returns><see cref="Account"/></returns>
        Account VerifyCredentialsByPersonalIdentityNumber(string personalIdentityNumber, string password);

        /// <summary>
        /// Returns all accounts with a specified role.
        /// </summary>
        /// <param name="machineName">The role machine name</param>
        /// <param name="excludeMachineName">Accounts with this role will not be included</param>
        /// <returns>A collection of <see cref="Account"/></returns>
        IList<Account> GetAccountsByRole(string machineName, string excludeMachineName = null);
    }

    /// <summary>
    /// Implementation of <see cref="IAccountRepository"/>.
    /// </summary>
    public class AccountRepository : PagingAndSortingRepository<Account>, IAccountRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public AccountRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region IAccountRepository Members.

        /// <inheritdoc />
        public Account VerifyCredentialsByUsername(string username, string password)
        {
            return Where(x => x.UserName == username)
                .And(x => x.Password == password)
                .And(x => x.Active)
                .And(x => !x.IsPaused)
                .SingleOrDefault();
        }

        /// <inheritdoc />
        public Account VerifyCredentialsByPersonalIdentityNumber(string personalIdentityNumber, string password)
        {
            return Where(x => x.UniqueIdentifier == personalIdentityNumber)
                .And(x => x.Password == password)
                .And(x => x.Active)
                .And(x => !x.IsPaused)
                .SingleOrDefault();
        }

        /// <inheritdoc />
        public IList<Account> GetAccountsByRole(string machineName, string excludeMachineName = null)
        {
            var excludedMachineNames = new List<string>() { "_AA", "_ADMIN_D" };
            if (excludeMachineName != null)
            {
                excludedMachineNames.Add(excludeMachineName);
            }
            var excludedQuery = QueryOver.Of<Account>()
                .Where(x => x.Active)
                .Select(x => x.Id)
                .JoinQueryOver<Role>(x => x.Roles)
                    .WhereRestrictionOn(r => r.MachineName).IsIn(excludedMachineNames);
            
            return Where(x => x.Active)
                .And(x => !x.IsPaused)
                .OrderBy(x => x.LastName).Asc
                .WithSubquery.WhereProperty(x => x.Id).NotIn<Account>(excludedQuery)
                .JoinQueryOver<Role>(x => x.Roles)
                    .Where(x => x.MachineName == machineName)
                    .List();
        }

        #endregion
    }
}