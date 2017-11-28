// <copyright file="AggregateRoot.cs" company="Appva AB">
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
    /// A collection of objects that are bound together by a root entity, otherwise 
    /// known as an aggregate root. The aggregate root guarantees the consistency of 
    /// changes being made within the aggregate by forbidding external objects from 
    /// holding references to its members.
    /// </summary>
    public abstract class AggregateRoot : Entity,  IAggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <param name="version">The entity version.</param>
        protected AggregateRoot(Guid id, int version)
            : base(id)
        {
            this.Version = version;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected AggregateRoot()
        {
        }

        #endregion

        #region IAggregateRoot Members.

        /// <inheritdoc />
        public virtual int Version
        {
            get;
            internal protected set;
        }

        #endregion
    }
}