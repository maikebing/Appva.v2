// <copyright file="Authorization.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Common.Domain;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// A <see cref="Client"/> authorization.
    /// </summary>
    public class Authorization : Entity<Authorization>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Authorization"/> class.
        /// </summary>
        /// <param name="client">The client</param>
        /// <param name="userIdentifier">The user identifier</param>
        /// <param name="scopes">The list of scopes</param>
        /// <param name="expiration">Optional expiration date time</param>
        public Authorization(Client client, string userIdentifier, IList<string> scopes, DateTime? expiration)
        {
            this.Client = client;
            this.UserIdentifier = userIdentifier;
            this.Scope = scopes.IsEmpty() ? null : string.Join(",", scopes.ToArray());
            this.CreatedAt = DateTime.UtcNow;
            this.ExpiresAt = expiration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authorization"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Authorization()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The client.
        /// </summary>
        public virtual Client Client
        {
            get;
            protected set;
        }

        /// <summary>
        /// The user identifier. 
        /// TODO: The user identifier can currently be null.
        /// </summary>
        public virtual string UserIdentifier
        {
            get;
            protected set;
        }

        /// <summary>
        /// The authorized scopes as comma delimited string.
        /// TODO: Create an NHibernate IUserType for this.
        /// </summary>
        public virtual string Scope
        {
            get;
            protected set;
        }

        /// <summary>
        /// The authorized scopes.
        /// </summary>
        public virtual IList<string> Scopes
        {
            get
            {
                return this.Scope.IsEmpty() ? new List<string>() : this.Scope.Split(',').ToList();
            }
        }

        /// <summary>
        /// The authorization created at date time (UTC).
        /// </summary>
        public virtual DateTime CreatedAt
        {
            get;
            protected set;
        }

        /// <summary>
        /// The authorization expiration at date time (UTC).
        /// </summary>
        public virtual DateTime? ExpiresAt
        {
            get;
            protected set;
        }

        #endregion
    }
}