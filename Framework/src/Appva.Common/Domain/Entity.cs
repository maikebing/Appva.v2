﻿// <copyright file="Entity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Common.Domain
{
    #region Imports.

    using System;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// Implementation of an <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public abstract class Entity<T> : IEquatable<Entity<T>>, IEntity where T : class
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Reviewed")]
        protected Entity()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.IsActive = true;
        }

        #endregion

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
            set;
        }

        /// <inheritdoc />
        public virtual DateTime CreatedAt
        {
            get;
            set;
        }

        /// <inheritdoc />
        public virtual DateTime UpdatedAt
        {
            get;
            set;
        }

        /// <inheritdoc />
        public virtual bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// Determine if the entity is transient - an object not yet been persisted to the 
        /// database.
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
            if (obj == null)
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
            if (ReferenceEquals(this, other))
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
    }
}
