// <copyright file="PermissionAction.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    /// <summary>
    /// Actino rights for a permission.
    /// </summary>
    public enum PermissionAction
    {
        /// <summary>
        /// Create rights.
        /// </summary>
        Create = 0,

        /// <summary>
        /// Read rights.
        /// </summary>
        Read = 1,

        /// <summary>
        /// Update rights.
        /// </summary>
        Update = 2,

        /// <summary>
        /// Delete rights.
        /// </summary>
        Delete = 3
    }
}