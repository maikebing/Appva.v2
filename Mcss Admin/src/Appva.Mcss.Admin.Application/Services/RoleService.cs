// <copyright file="RoleService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    public interface IRoleService : IService
    {
        /// <summary>
        /// Returns a <see cref="Role"/> by ID.
        /// </summary>
        /// <param name="id">The role ID</param>
        /// <returns>A <see cref="Role"/> instance</returns>
        Role Find(Guid id);

        /// <summary>
        /// Returns a <see cref="Role"/> by unique identifier.
        /// </summary>
        /// <param name="identifier">The unique identifier</param>
        /// <returns>A <see cref="Role"/> instance</returns>
        Role Find(string identifier);

        /// <summary>
        /// Returns all <see cref="Role"/>.
        /// </summary>
        /// <returns>A collection of <see cref="Role"/></returns>
        IList<Role> List();

        /// <summary>
        /// Returns all <see cref="Role"/>.
        /// </summary>
        /// <returns>A collection of <see cref="Role"/></returns>
        IList<Role> ListVisible();

        /// <summary>
        /// Returns a collection <see cref="Account"/> whom are member of the specific
        /// role identifier.
        /// </summary>
        /// <param name="identifier">The unique identifier</param>
        /// <returns>A collection of <see cref="Account"/></returns>
        IList<Account> MembersOfRole(string identifier);

        /// <summary>
        /// Updates a role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Role Update(Role role);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class RoleService : IRoleService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleRepository"/>.
        /// </summary>
        private readonly IRoleRepository repository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IRoleRepository"/></param>
        public RoleService(IRoleRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region IRoleService Members.

        /// <inheritdoc />
        public Role Find(Guid id)
        {
            return this.repository.Get(id);
        }

        /// <inheritdoc />
        public Role Find(string identifier)
        {
            return this.repository.Find(identifier);
        }

        /// <inheritdoc />
        public IList<Role> List()
        {
            return this.repository.List();
        }

        /// <inheritdoc />
        public IList<Role> ListVisible()
        {
            return this.repository.ListOnlyVisible();
        }

        /// <inheritdoc />
        public IList<Account> MembersOfRole(string identifier)
        {
            return this.repository.MembersOfRole(identifier);
        }

        /// <inheritdoc />
        public Role Update(Role role)
        {
            this.repository.Update(role);
            return role;
        }

        #endregion
    }
}