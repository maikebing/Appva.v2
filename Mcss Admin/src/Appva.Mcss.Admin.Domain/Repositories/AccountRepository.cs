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
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IAccountRepository : 
        IIdentityRepository<Account>, 
        IUpdateRepository<Account>, 
        IRepository
    {
        /// <summary>
        /// Returns a user account by its unique Personal Identity Number. 
        /// </summary>
        /// <param name="personalIdentityNumber">The unique Personal Identity Number</param>
        /// <returns>An <see cref="Account"/> if found, else null</returns>
        Account FindByPersonalIdentityNumber(string personalIdentityNumber);

        /// <summary>
        /// Returns a user account by its unique user name. 
        /// </summary>
        /// <param name="username">The unique username</param>
        /// <returns>An <see cref="Account"/> if found, else null</returns>
        Account FindByUserName(string username);
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
        public Account FindByPersonalIdentityNumber(string personalIdentityNumber)
        {
            var accounts = this.persistenceContext.QueryOver<Account>()
                .Where(x => x.IsActive)
                .And(x => x.IsPaused == false)
                .And(x => x.PersonalIdentityNumber == personalIdentityNumber)
                .List();
            if (accounts.Count == 1)
            {
                return accounts[0];
            }
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

        #endregion

        #region IUpdateRepository<Account> Members.

        /// <inheritdoc /> 
        public void Update(Account entity)
        {
            this.persistenceContext.Update<Account>(entity);
        }

        #endregion
    }
}