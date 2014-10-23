// <copyright file="ClientActivated.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using Common.Domain;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class ClientActivated : IDomainEvent
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientActivated"/> class.
        /// </summary>
        /// <param name="client">The client activated</param>
        public ClientActivated(Client client)
        {
            this.Occurred = DateTime.Now;
            this.Version  = client.Version + 1;
            this.IsActive = client.IsActive;
        }

        #endregion

        #region Properties.

        /// <inheritdoc />
        public virtual DateTime Occurred
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public virtual int Version
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether the client is active or not.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            private set;
        }

        #endregion
    }
}