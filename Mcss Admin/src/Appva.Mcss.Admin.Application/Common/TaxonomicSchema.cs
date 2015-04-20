// <copyright file="TaxonomicSchema.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TaxonomicSchema
    {
        #region Variables.

        /// <summary>
        /// The taxonomic scheme identifier for organizational taxa.
        /// </summary>
        public static readonly TaxonomicSchema Organization = new TaxonomicSchema("ORG");

        /// <summary>
        /// The taxonomic scheme identifier for delegation taxa.
        /// </summary>
        public static readonly TaxonomicSchema Delegation = new TaxonomicSchema("DEL");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomicSchema"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the taxonomy scheme</param>
        private TaxonomicSchema(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The unique taxonomy schema identifier.
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        #endregion
    }
}