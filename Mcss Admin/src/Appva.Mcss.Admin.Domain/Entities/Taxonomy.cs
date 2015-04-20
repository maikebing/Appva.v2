// <copyright file="Taxonomy.cs" company="Appva AB">
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
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Taxonomy : AggregateRoot<Taxonomy>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Taxonomy"/> class.
        /// </summary>
        public Taxonomy()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not the <see cref="Taxonomy"/> is active.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        /// <summary>
        /// Name of the taxonomy
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Description of the taxonomy
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The system name used by MSCC for static internal taxonomies
        /// </summary>
        public virtual string MachineName
        {
            get;
            set;
        }

        /// <summary>
        /// Weight of the taxonomy
        /// </summary>
        /// <remarks>Only for visual ordering context</remarks>
        public virtual int Weight
        {
            get;
            set;
        }

        #endregion
    }
}