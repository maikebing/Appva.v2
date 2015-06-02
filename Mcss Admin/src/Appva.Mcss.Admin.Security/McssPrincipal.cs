// <copyright file="McssPrincipal.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// Defines the basic functionality of a principal object.
    /// </summary>
    public interface IMcssPrincipal
    {
        /// <summary>
        /// Returns the name for the current principal.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Returns the ID for the current principal.
        /// </summary>
        Guid Identifier
        {
            get;
        }

        /// <summary>
        /// Returns the tenant identity for the current principal.
        /// </summary>
        ITenantIdentity Tenant
        {
            get;
        }

        /// <summary>
        /// Returns the current principal roles.
        /// </summary>
        /// <returns>The current principal roles or if not authenticated; null</returns>
        IEnumerable<Claim> Roles
        {
            get;
        }

        /// <summary>
        /// Returns the current principal permissions.
        /// </summary>
        /// <returns>The current principal permissions or if not authenticated; null</returns>
        IEnumerable<Claim> Permissions
        {
            get;
        }

        /// <summary>
        /// Returns whether the current principal is a member of the specified role.
        /// </summary>
        /// <param name="role">The role which the principal must be a member of</param>
        /// <returns>True, if the principal is a member of the specified role</returns>
        bool IsInRole(string role);

        /// <summary>
        /// Returns whether the current principal is a member of the specified permission.
        /// </summary>
        /// <param name="role">The permission which the principal must be a member of</param>
        /// <returns>True, if the principal is a member of the specified permission</returns>
        bool HasPermission(string permission);
    }

    /// <summary>
    /// An System.Security.Principal.IPrincipal implementation that supports multiple claims-based identities.
    /// </summary>
    public sealed class McssPrincipal : ClaimsPrincipal, IMcssPrincipal
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="McssPrincipal"/> class.
        /// </summary>
        /// <param name="principal">The principal from which to initialize the new claims principal</param>
        public McssPrincipal(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        #endregion

        #region IMcssPrincipal Members.

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }

        /// <inheritdoc />
        public Guid Identifier
        {
            get
            {
                return new Guid(this.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }

        /// <inheritdoc />
        public ITenantIdentity Tenant
        {
            get
            {
                var id = this.FindFirst(Core.Resources.ClaimTypes.TenantId).Value;
                var name = this.FindFirst(Core.Resources.ClaimTypes.TenantName).Value;
                var hostName = this.FindFirst(Core.Resources.ClaimTypes.TenantHostName).Value;
                return new TenantIdentity(new TenantIdentifier(id), name, hostName);
            }
        }

        /// <inheritdoc />
        public IEnumerable<Claim> Roles
        {
            get
            {
                return this.Claims.Where(x => x.Type.Equals(ClaimTypes.Role));
            }
        }

        /// <inheritdoc />
        public IEnumerable<Claim> Permissions
        {
            get
            {
                return this.Claims.Where(x => x.Type.Equals(Core.Resources.ClaimTypes.Permission));
            }
        }

        /// <inheritdoc />
        public bool IsInRole(string role)
        {
            return this.IsInRole(role);
        }


        /// <inheritdoc />
        public bool HasPermission(string permission)
        {
            return this.Permissions.Any(x => x.Value.Equals(permission));
        }

        #endregion
    }
}