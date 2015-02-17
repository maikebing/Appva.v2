// <copyright file="AggregateRoot.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Common.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// A collection of objects that are bound together by a root entity, otherwise 
    /// known as an aggregate root. The aggregate root guarantees the consistency of 
    /// changes being made within the aggregate by forbidding external objects from 
    /// holding references to its members.
    /// </summary>
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot where T : class
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{T}"/> class.
        /// </summary>
        protected AggregateRoot()
        {
        }

        #endregion

        #region IAggregateRoot<T> Members.

        /// <inheritdoc />
        public virtual DateTime CreatedAt
        {
            get;
            protected set;
        }

        /// <inheritdoc />
        public virtual DateTime UpdatedAt
        {
            get;
            protected set;
        }

        /// <inheritdoc />
        public virtual IList<IDomainEvent> Events
        {
            get;
            protected set;
        }

        /// <inheritdoc />
        public virtual void RegisterEvent(IDomainEvent domainEvent)
        {
            if (this.Events == null)
            {
                this.Events = new List<IDomainEvent>();
            }
            this.Events.Add(domainEvent);
        }

        #endregion
    }
}