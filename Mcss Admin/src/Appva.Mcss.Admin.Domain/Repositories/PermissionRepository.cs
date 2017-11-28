// <copyright file="PermissionRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Transform;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission>, IListRepository<Permission>
    {
        /// <summary>
        /// Returns all permissions for the user account.
        /// </summary>
        /// <param name="account">The user account</param>
        /// <returns>A collection of user account permissions</returns>
        IList<Permission> Permissions(Account account);

        /// <summary>
        /// Returns all permissions for the roles.
        /// </summary>
        /// <param name="roles">A collection of roles</param>
        /// <returns>A collection of role permissions</returns>
        IList<Permission> ByRoles(IList<Role> roles);

        /// <summary>
        /// Returns all permissions for the role collection.
        /// </summary>
        /// <param name="roleIds">A collection of role ID</param>
        /// <returns>A collection of role permissions</returns>
        IList<Permission> PermissionsByRoleIds(ICollection roleIds);

        /// <summary>
        /// Returns whether or not the user account is a member of at least one of the 
        /// specified permissions.
        /// </summary>
        /// <param name="account">The user account to check</param>
        /// <param name="permissions">
        /// At least one of the permissions that the member must be a member of
        /// </param>
        /// <returns>
        /// True if the user is a member of any of the specified permissions
        /// </returns>
        bool HasAnyPermissions(Account account, params string[] permissions);

        /// <summary>
        /// Returns a filtered collection of <see cref="Permission"/> by specified 
        /// ID:s.
        /// </summary>
        /// <param name="ids">The ID:s to retrieve</param>
        /// <returns>A filtered collection of <see cref="Permission"/></returns>
        IList<Permission> ListAllIn(params Guid[] ids);

        /// <summary>
        /// Returns all persmissions matching a schema.
        /// </summary>
        /// <param name="bySchema">The schema</param>
        /// <returns>A list of <see cref="Permission"/></returns>
        IList<Permission> Search(string bySchema);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public PermissionRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region IPermissionRepository Members.

        /// <inheritdoc />
        public IList<Permission> Permissions(Account account)
        {
            var roles = account.Roles.Select(x => x.Id).ToArray();
            return this.PermissionsByRoleIds(roles);
        }

        /// <inheritdoc />
        public IList<Permission> ByRoles(IList<Role> roles)
        {
            return this.PermissionsByRoleIds(roles.Select(x => x.Id).ToArray());
        }

        /// <inheritdoc />
        public IList<Permission> PermissionsByRoleIds(ICollection roleIds)
        {
            return this.Context.QueryOver<Permission>()
                .JoinQueryOver<Role>(x => x.Roles)
                    .Where(x => x.IsActive)
                    .WhereRestrictionOn(x => x.Id)
                    .IsIn(roleIds)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        /// <inheritdoc />
        public bool HasAnyPermissions(Account account, params string[] permissions)
        {
            var roles = account.Roles.Select(x => x.Id).ToArray();
            return this.Context.QueryOver<Permission>()
                .WhereRestrictionOn(x => x.Resource)
                .IsIn(permissions)
                .JoinQueryOver<Role>(x => x.Roles)
                    .Where(x => x.IsActive)
                    .WhereRestrictionOn(x => x.Id)
                    .IsIn(roles)
                .TransformUsing(Transformers.DistinctRootEntity)
                .RowCount() > 0;
        }

        /// <inheritdoc />
        public IList<Permission> ListAllIn(params Guid[] ids)
        {
            return this.Context.QueryOver<Permission>()
                .AndRestrictionOn(x => x.Id)
                .IsIn(ids)
                .List();
        }

        /// <inheritdoc />
        public IList<Permission> Search(string bySchema)
        {
            return this.Context.QueryOver<Permission>()
                .WhereRestrictionOn(x => x.Resource)
                    .IsLike(bySchema, MatchMode.Start)
                .OrderBy(x => x.Sort).Asc.List();
        }

        #endregion


        #region IListRepository<Permission> Members.

        /// <inheritdoc />
        public IList<Permission> List()
        {
            return this.Context.QueryOver<Permission>()
                /*.Where(x => x.IsVisible)*/.OrderBy(x => x.Sort).Asc.List();
        }

        #endregion
    }
}