// <copyright file="RoleRepository.cs" company="Appva AB">
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
    using NHibernate.Transform;
using Appva.Mcss.Admin.Domain.Repositories.Contracts;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IRoleRepository : 
        IIdentityRepository<Role>, 
        IListRepository<Role>, 
        IUpdateRepository<Role>,
        IRepository
    {
        /// <summary>
        /// Returns a <see cref="Role"/> by unique identifier.
        /// </summary>
        /// <param name="identifier">The unique identifier</param>
        /// <returns>A <see cref="Role"/> instance</returns>
        Role Find(string identifier);

        /// <summary>
        /// Returns whether or not the user account is a member of at least one of the 
        /// specified roles.
        /// </summary>
        /// <param name="account">The user account to check</param>
        /// <param name="roles">
        /// At least one of the roles that the member must be a member of
        /// </param>
        /// <returns>True if the user is a member of any of the specified roles</returns>
        bool IsInRoles(Account account, params string[] roles);

        /// <summary>
        /// Returns the collection of <see cref="Role"/> by the user account.
        /// </summary>
        /// <param name="account">The user account to retreive roles from</param>
        /// <returns>A collection os <see cref="Role"/> for the user account</returns>
        IList<Role> Roles(Account account);

        /// <summary>
        /// Returns a collection <see cref="Account"/> whom are member of the specific
        /// role identifier.
        /// </summary>
        /// <param name="identifier">The unique identifier</param>
        /// <returns>A collection of <see cref="Account"/></returns>
        IList<Account> MembersOfRole(string identifier);

        /// <summary>
        /// Returns the collection of <see cref="Role"/> which are visible.
        /// </summary>
        /// <returns>A collection os <see cref="Role"/></returns>
        IList<Role> ListOnlyVisible();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class RoleRepository : IRoleRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> implementation.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">
        /// The <see cref="IPersistenceContext"/> implementation
        /// </param>
        public RoleRepository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IIdentityRepository<Role> Members.

        /// <inheridoc />
        public Role Find(Guid id)
        {
            return this.persistenceContext.Get<Role>(id);
        }

        #endregion

        #region IRoleRepository Members.

        /// <inheridoc />
        public Role Find(string identifier)
        {
            return this.persistenceContext.QueryOver<Role>()
                .Where(x => x.MachineName ==  identifier)
                .SingleOrDefault();
        }

        /// <inheritdoc />
        public bool IsInRoles(Account account, params string[] roles)
        {
            return this.persistenceContext.QueryOver<Role>()
                .Where(x => x.IsActive)
                .AndRestrictionOn(x => x.MachineName)
                .IsIn(roles)
                .JoinQueryOver<Account>(x => x.Accounts)
                    .Where(x => x.Id == account.Id)
                .TransformUsing(Transformers.DistinctRootEntity)
                .RowCount() > 0;
        }

        /// <inheritdoc />
        public IList<Role> Roles(Account account)
        {
            return this.persistenceContext.QueryOver<Role>()
                .Where(x => x.IsActive)
                .JoinQueryOver<Account>(x => x.Accounts)
                    .Where(x => x.Id == account.Id)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        /// <inheritdoc />
        public IList<Account> MembersOfRole(string identifier)
        {
            return this.persistenceContext.QueryOver<Account>()
                .Where(x => x.IsActive == true).JoinQueryOver<Role>(x => x.Roles)
                .Where(x => x.MachineName == identifier).List();
        }

        /// <inheritdoc />
        public IList<Role> ListOnlyVisible()
        {
            return this.persistenceContext.QueryOver<Role>()
                .Where(x => x.IsActive)
                .And(x => x.IsVisible)
                .List();
        }

        #endregion

        #region IListRepository<Role> Members.

        /// <inheridoc />
        public IList<Role> List(ulong maximumItems = long.MaxValue)
        {
            return this.persistenceContext.QueryOver<Role>()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Weight).Asc
                .ThenBy(x => x.Name).Asc
                .List();
        }

        #endregion

        #region IUpdateRepository<Role> Members.

        /// <inheritdoc />
        public void Update(Role entity)
        {
            this.persistenceContext.Update<Role>(entity);
        }

        #endregion
    }
}