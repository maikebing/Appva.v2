// <copyright file="TenantIdentity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Tenant.Identity
{
    /// <summary>
    /// Representation of a tenant identity.
    /// </summary>
    public interface ITenantIdentity
    {
        /// <summary>
        /// The ID.
        /// </summary>
        ITenantIdentifier Id
        {
            get;
        }

        /// <summary>
        /// The user-friendly name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// The host name.
        /// </summary>
        string HostName
        {
            get;
        }
    }

    /// <summary>
    /// A <see cref="ITenantIdentity"/> implementation.
    /// </summary>
    public sealed class TenantIdentity : ITenantIdentity
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantIdentity"/> class.
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="name">The name</param>
        /// <param name="hostName">The host name</param>
        public TenantIdentity(ITenantIdentifier id, string name, string hostName)
        {
            this.Id = id;
            this.Name = name;
            this.HostName = hostName;
        }

        #endregion

        #region ITenantIdentity Members.

        /// <inheritdoc />
        public ITenantIdentifier Id
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Name
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string HostName
        {
            get;
            private set;
        }

        #endregion
    }
}