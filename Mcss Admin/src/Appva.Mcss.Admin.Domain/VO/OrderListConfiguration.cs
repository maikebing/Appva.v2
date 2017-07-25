﻿// <copyright file="OrderListConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using Appva.Common.Domain;
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
        private OrderListConfiguration(bool hasCreatedCategories, bool hasMigratedArticles, bool isEnabled)
        {
            this.HasCreatedCategories = hasCreatedCategories;
            this.HasMigratedArticles = hasMigratedArticles;
            this.IsEnabled = isEnabled;
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

        /// <summary>
        /// If the order list has been enabled.
        /// </summary>
        [JsonProperty]
        public bool IsEnabled
        {
            get;
            set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="OrderListConfiguration"/> class.
        /// </summary>
        /// <returns>A new <see cref="OrderListConfiguration"/> instance.</returns>
        public static OrderListConfiguration CreateNew(bool hasCreatedCategories = false, bool hasMigratedArticles = false, bool isEnabled = false)
        {
            return new OrderListConfiguration(hasCreatedCategories, hasMigratedArticles, isEnabled);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.HasCreatedCategories.GetHashCode() +
                   this.HasMigratedArticles.GetHashCode() +
                   this.IsEnabled.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(OrderListConfiguration other)
        {
            return other != null
                && this.HasCreatedCategories.Equals(other.HasCreatedCategories)
                && this.HasMigratedArticles.Equals(other.HasMigratedArticles)
                &&  this.IsEnabled.Equals(other.IsEnabled);
        }

        #endregion
    }
}