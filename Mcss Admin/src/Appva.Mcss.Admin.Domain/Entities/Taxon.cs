// <copyright file="Taxon.cs" company="Appva AB">
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
    public class Taxon : AggregateRoot<Taxon>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Taxon"/> class.
        /// </summary>
        public Taxon()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not the <see cref="Taxon"/> is active.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        /// <summary>
        /// The taxonomy.
        /// </summary>
        public virtual Taxonomy Taxonomy
        {
            get;
            set;
        }

        /// <summary>
        /// Parent taxon, if any.
        /// </summary>
        /// <remarks>If null, then it is a root taxon</remarks>
        public virtual Taxon Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Name of the taxon.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Internal description.
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Materialized Path, separated by '.' (dot)
        /// </summary>
        public virtual string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Type of taxon
        /// </summary>
        public virtual string Type
        {
            get;
            set;
        }

        /// <summary>
        /// If it's a root node or not.
        /// </summary>
        public virtual bool IsRoot
        {
            get;
            set;
        }

        /// <summary>
        /// Weight of the Taxon.
        /// </summary>
        /// <remarks>Only for visual ordering context</remarks>
        public virtual int Weight
        {
            get;
            set;
        }

        /// <summary>
        /// All delegations for this taxon.
        /// </summary>
        public virtual IList<Delegation> Delegations
        {
            get;
            set;
        }

        #endregion
    }
}