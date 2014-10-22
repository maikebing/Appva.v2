﻿// <copyright file="Entity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Common.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Implementation of an <see cref="IEntity{T}"/>.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public class Entity<T> : IEquatable<Entity<T>>, IEntity<T> where T : class
    {
        #region Properties.

        /// <summary>
        /// Entity Identifier.
        /// </summary>
        public virtual Guid Id
        {
            get;
            protected set;
        }

        /// <summary>
        /// Concurrency checking (Optimistic concurrency).
        /// </summary>
        public virtual int Version
        {
            get;
            protected set;
        }

        /// <summary>
        /// A list of domain events.
        /// </summary>
        public virtual IList<IDomainEvent> Events
        {
            get;
            protected set;
        }

        /// <summary>
        /// Determine if the entity is transient - an object not yet 
        /// been persisted to the database.
        /// </summary>
        /// <returns>True if the entity is transient</returns>
        public virtual bool IsTransient
        {
            get
            {
                return object.Equals(this.Id, default(Guid));
            }
        }

        #endregion

        #region Object Overrides.

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            return this.Equals(obj as Entity<T>);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (this.GetType().GetHashCode() * 907) + this.Id.GetHashCode();
        }

        #endregion

        #region IEquatable<Entity<T>> Members

        /// <inheritdoc />
        public virtual bool Equals(Entity<T> other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (! this.IsTransient && ! other.IsTransient && object.Equals(this.Id, other.Id))
            {
                var thisType = this.GetType();
                var thatType = other.GetType();
                return thisType.IsAssignableFrom(thatType) || thatType.IsAssignableFrom(thisType);
            }
            return false;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Registers a new <see cref="IDomainEvent"/>.
        /// </summary>
        /// <param name="domainEvent">A new event which has taken place</param>
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