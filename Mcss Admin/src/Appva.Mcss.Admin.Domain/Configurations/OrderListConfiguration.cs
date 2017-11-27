// <copyright file="OrderListConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class OrderListConfiguration : ValueObject<OrderListConfiguration>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderListConfiguration"/> class.
        /// </summary>
        /// <param name="hasCreatedCategories">If article categories has been migrated.</param>
        /// <param name="hasMigratedArticles">If articles has been migrated.</param>
        [JsonConstructor]
        private OrderListConfiguration(bool hasCreatedCategories, bool hasMigratedArticles)
        {
            this.HasCreatedCategories = hasCreatedCategories;
            this.HasMigratedArticles  = hasMigratedArticles;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// If article categories has been migrated.
        /// </summary>
        [JsonProperty]
        public bool HasCreatedCategories
        {
            get;
            private set;
        }

        /// <summary>
        /// If articles has been migrated.
        /// </summary>
        [JsonProperty]
        public bool HasMigratedArticles
        {
            get;
            private set;
        }

        public bool IsInstalled
        {
            get { return this.HasCreatedCategories && this.HasMigratedArticles; }
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="OrderListConfiguration"/> class.
        /// </summary>
        /// <param name="hasCreatedCategories">If article categories has been migrated.</param>
        /// <param name="hasMigratedArticles">If articles has been migrated.</param>
        /// <param name="hasMigratableItems">If there are any migratable items.</param>
        /// <returns>A new <see cref="OrderListConfiguration"/> instance.</returns>
        public static OrderListConfiguration CreateNew(
            bool hasCreatedCategories = false, 
            bool hasMigratedArticles = false)
        {
            return new OrderListConfiguration(hasCreatedCategories, hasMigratedArticles);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.HasCreatedCategories.GetHashCode() +
                   this.HasMigratedArticles.GetHashCode();
        }

        /// <inheritdoc />
        public bool Equals(OrderListConfiguration other)
        {
            return other != null
                && this.HasCreatedCategories.Equals(other.HasCreatedCategories)
                && this.HasMigratedArticles.Equals(other.HasMigratedArticles);
        }

        #endregion

        protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}