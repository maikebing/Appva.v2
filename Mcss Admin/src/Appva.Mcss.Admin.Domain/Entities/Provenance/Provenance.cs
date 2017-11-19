// <copyright file="Provenance.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Validation;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Provenance : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Provenance"/> class.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <param name="version">The entity version.</param>
        /// <param name="domainEvent">The domain event.</param>
        public Provenance(Guid id, int version, IDomainEvent domainEvent)
        {
            Requires.Range     (version > 0,     "version", "Version must be greater than zero.");
            Requires.ValidState(id.IsNotEmpty(), "The entity id cannot be an empty guid.");
            Requires.NotNull   (domainEvent,     "domainEvent");
            this.Id      = id;
            this.Version = version;
            this.Data    = domainEvent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Provenance"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected Provenance()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The data event.
        /// </summary>
        public virtual IDomainEvent Data
        {
            get;
            internal protected set;
        }

        #endregion

        #region Constructors.

        /// <summary>
        /// Creates a new instance of the <see cref="Provenance"/> class.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <param name="version">The entity version.</param>
        /// <param name="domainEvent">The domain event.</param>
        /// <returns>A new <see cref="Provenance"/> instance.</returns>
        public static Provenance New(Guid id, int version, IDomainEvent domainEvent)
        {
            return new Provenance(id, version, domainEvent);
        }

        #endregion
    }
}