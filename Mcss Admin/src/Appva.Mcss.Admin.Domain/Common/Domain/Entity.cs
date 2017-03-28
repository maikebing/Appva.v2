// <copyright file="Entity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class Entity : IEntity
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        protected Entity(Guid id)
            :this()
        {
            this.Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected Entity()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.IsActive  = true;
        }

        #endregion

        #region Properties.

        /// <inheritdoc />
        public virtual Guid Id
        {
            get;
            internal protected set;
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

        #region IEntity Members.

        /// <inheritdoc />
        public virtual void MarkAsUpdated()
        {
            this.UpdatedAt = DateTime.Now;
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
            return this == obj;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (this.GetType().GetHashCode() * 907) + this.Id.GetHashCode();
        }

        #endregion
    }
}