// <copyright file="Heartbeat.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.Monitoring.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Heartbeat : Entity<Heartbeat>, IAggregateRoot
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Heartbeat"/> class.
        /// </summary>
        public Heartbeat()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Heartbeat"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Heartbeat()
        {
        }
        #endregion

        #region Properties.

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

        public virtual Resource 
        #endregion
    }
}