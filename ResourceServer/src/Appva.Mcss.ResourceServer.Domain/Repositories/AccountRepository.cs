// <copyright file="AccountRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Persistence;
    using Appva.Repository;
    using Mcss.Domain.Entities;

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
        /// <param name="machineName"></param>
        /// <returns></returns>
        IList<Account> GetAccountsByRole(string machineName);
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
                .SingleOrDefault();
        }

        /// <inheritdoc />
        public Account VerifyCredentialsByPersonalIdentityNumber(string personalIdentityNumber, string password)
        {
            return Where(x => x.UniqueIdentifier == personalIdentityNumber)
                .And(x => x.Password == password)
                .SingleOrDefault();
        }

        /// <inheritdoc />
        public IList<Account> GetAccountsByRole(string machineName)
        {
            return Where(x => x.Active)
                .JoinQueryOver<Role>(x => x.Roles)
                    .Where(r => r.MachineName == machineName)
                .List();
        }

        #endregion
    }
}