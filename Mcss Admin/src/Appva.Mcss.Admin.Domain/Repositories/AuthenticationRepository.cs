// <copyright file="AuthenticationRepository.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IAuthenticationRepository : IRepository
    {
        /// <summary>
        /// Locates a user account by its unique Personal Identity Number. 
        /// </summary>
        /// <param name="personalIdentityNumber">The unique Personal Identity Number</param>
        /// <returns>An <see cref="Account"/> instance if found, else null</returns>
        /// <exception cref="NHibernate.HibernateException">
        /// If there is more than one matching result
        /// </exception>
        Account FindByPersonalIdentityNumber(PersonalIdentityNumber personalIdentityNumber);

        /// <summary>
        /// Locates a user account by its unique user name. 
        /// </summary>
        /// <param name="userName">The unique user name</param>
        /// <returns>An <see cref="Account"/> instance if found, else null</returns>
        /// <exception cref="NHibernate.HibernateException">
        /// If there is more than one matching result
        /// </exception>
        Account FindByUserName(string userName);

        /// <summary>
        /// Locates a user account by its HSA ID. 
        /// </summary>
        /// <param name="hsaId">The unique HSA id</param>
        /// <returns>An <see cref="Account"/> instance if found, else null</returns>
        /// <exception cref="NHibernate.HibernateException">
        /// If there is more than one matching result
        /// </exception>
        Account FindByHsaId(HsaId hsaId);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthenticationRepository : IAuthenticationRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRepository"/> class.
        /// </summary>
        public AuthenticationRepository(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region IAuthenticationRepository Members.

        /// <inheritdoc />
        public Account FindByPersonalIdentityNumber(PersonalIdentityNumber personalIdentityNumber)
        {
            return this.context.QueryOver<Account>()
                .Where(x => x.PersonalIdentityNumber == personalIdentityNumber)
                .SingleOrDefault();
        }

        /// <inheritdoc />
        public Account FindByUserName(string userName)
        {
            return this.context.QueryOver<Account>()
                .Where(x => x.UserName == userName)
                .SingleOrDefault();
        }

        /// <inheritdoc />
        public Account FindByHsaId(HsaId hsaId)
        {
            return this.context.QueryOver<Account>()
                .Where(x => x.HsaId == hsaId.Value)
                .SingleOrDefault();
        }

        #endregion
    }
}