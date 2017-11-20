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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Taxon : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Taxon"/> class.
        /// </summary>
        public Taxon()
        {
        }

        #endregion

        #region Properties.

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

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="Taxon"/> class.
        /// </summary>
        /// <param name="taxonomy">The taxonomy</param>
        /// <param name="name">The taxon name</param>
        /// <param name="description">The taxon description of usage</param>
        /// <param name="type">The taxon type</param>
        /// <param name="parent">Optional parent taxon</param>
        /// <param name="sort">Optional sort order, defaults to 0</param>
        /// <returns>A new <see cref="Taxon"/> instance</returns>
        public static Taxon New(Taxonomy taxonomy, string name, string description, string type, Taxon parent = null, int sort = 0)
        {
            return new Taxon { Taxonomy = taxonomy, Name = name, Description = description, Type = type, Parent = parent, Weight = sort };
        }

        #endregion
    }
}