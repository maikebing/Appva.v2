// <copyright file="OrderListConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Models;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DosageConfiguration : ValueObject<DosageConfiguration>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderListConfiguration"/> class.
        /// </summary>
        /// <param name="hasCreatedCategories">If article categories has been migrated.</param>
        /// <param name="hasMigratedArticles">If articles has been migrated.</param>
        /// <param name="hasMigratableItems">If there are any migratable items.</param>
        [JsonConstructor]
        private DosageConfiguration(List<DosageScaleModel> dosageScaleList)
        {
            this.DosageScaleModelList = dosageScaleList;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// If article categories has been migrated.
        /// </summary>
        [JsonProperty]
        public List<DosageScaleModel> DosageScaleModelList
        {
            get;
            private set;
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
        public static DosageConfiguration CreateNew(List<DosageScaleModel> dosageScaleList = null)
        {
            if (dosageScaleList == null)
            {
                dosageScaleList = new List<DosageScaleModel> {
                    new DosageScaleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Antal",
                        Unit = "st",
                        Values = CalculateValues(0.5, 10, 0.5)
                    },
                    new DosageScaleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Antal",
                        Unit = "mg",
                        Values = CalculateValues(25, 1000, 25)
                    },
                    new DosageScaleModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Mängd",
                        Unit = "ml",
                        Values = CalculateValues(25, 1000, 25)
                    }
                };
            }
            return new DosageConfiguration(dosageScaleList);
        }

        #endregion

        #region Private members.

        private static string CalculateValues(double min, double max, double increment)
        {
            var amounts = new List<string>();

            for (var x = min; x <= max; x = x + increment)
            {
                amounts.Add(x.ToString().Replace(",", "."));
            }

            return string.Join(", ", amounts);
        }

        #endregion

        #region ValueObject Overrides.

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.DosageScaleModelList;

        }

        #endregion
    }
}