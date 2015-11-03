// <copyright file="TaxonomicSchema.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Core.Resources;

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
        public static readonly TaxonomicSchema Organization = new TaxonomicSchema("ORG", "organization");

        /// <summary>
        /// The taxonomic scheme identifier for delegation taxa.
        /// </summary>
        public static readonly TaxonomicSchema Delegation   = new TaxonomicSchema("DEL", "delegation");

        /// <summary>
        /// The taxonomic scheme identifier for risk assessment (Senior Alert) taxa.
        /// </summary>
        public static readonly TaxonomicSchema RiskAssessment = new TaxonomicSchema("SAI", "assessment");

        /// <summary>
        /// The taxonomic scheme identifier for sign status taxa.
        /// </summary>
        public static readonly TaxonomicSchema SignStatus = new TaxonomicSchema("SST", "sign-status");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomicSchema"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the taxonomy scheme</param>
        /// <param name="cacheKey">The unique cacheKey</param>
        private TaxonomicSchema(string id, string cacheKey)
        {
            this.Id = id;
            this.CacheKey = CacheTypes.Taxonomy.FormatWith(cacheKey);
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

        /// <summary>
        /// The unique taxonomy cache key.
        /// </summary>
        public string CacheKey
        {
            get;
            private set;
        }

        #endregion
    }
}