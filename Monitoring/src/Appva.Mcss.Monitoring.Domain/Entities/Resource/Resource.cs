// <copyright file="Resource.cs" company="Appva AB">
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
    public class Resource : Entity<Resource>, IAggregateRoot
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        public Resource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Resource()
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

        /// <summary>
        /// The name of the resource.
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The description of the resource.
        /// </summary>
        public string Description
        {
            get;
            protected set;
        }

        #endregion
    }
}