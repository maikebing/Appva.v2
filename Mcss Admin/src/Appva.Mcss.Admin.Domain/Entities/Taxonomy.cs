// <copyright file="Taxonomy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
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
    public class Taxonomy : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Taxonomy"/> class.
        /// </summary>
        public Taxonomy()
        {
        }

        #endregion

        #region Properties.

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

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="Taxonomy"/> class.
        /// </summary>
        /// <param name="key">The unique machine key</param>
        /// <param name="name">The taxonomy name</param>
        /// <param name="description">The taxonomy description of usage</param>
        /// <param name="sort">Optional sort, defaults to 0</param>
        /// <returns>A new <see cref="Taxonomy"/> instance</returns>
        public static Taxonomy New(string key, string name, string description, int sort = 0)
        {
            return new Taxonomy { MachineName = key, Name = name, Description = description, Weight = sort };
        }

        #endregion
    }
}