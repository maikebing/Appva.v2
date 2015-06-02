// <copyright file="IAggregateRoot.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Common.Domain
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// A collection of objects that are bound together by a root entity, otherwise 
    /// known as an aggregate root. The aggregate root guarantees the consistency of 
    /// changes being made within the aggregate by forbidding external objects from 
    /// holding references to its members.
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// A list of domain events.
        /// </summary>
        IList<IDomainEvent> Events
        {
            get;
        }

        /// <summary>
        /// Registers a new <see cref="IDomainEvent"/>.
        /// </summary>
        /// <param name="domainEvent">A new event which has taken place</param>
        void RegisterEvent(IDomainEvent domainEvent);
    }
}
