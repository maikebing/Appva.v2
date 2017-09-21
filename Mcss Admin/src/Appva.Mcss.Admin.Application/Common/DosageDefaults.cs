// <copyright file="Inventory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Application.Common
{
    using Appva.Mcss.Admin.Application.Models;
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class DosageDefaults
    {
        #region Fields.

        /// <summary>
        /// The default amounts.
        /// </summary>
        public static readonly IReadOnlyCollection<double> AmountList = new List<double>
            {
                 0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
                40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 59, 51, 52, 53, 54, 55, 56, 57, 58, 59,
                60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79,
                80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100
            };

        #endregion

        #region Static Members

        public static List<DosageScaleListModel> Scales ()
        {
            var scaleList = new List<DosageScaleListModel>
            {
                new DosageScaleListModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Dos",
                    Unit = "st",
                    Amounts = CalculateAmount(0, 10, 1.0)
                }
            };
            return scaleList;
        }

        private static List<double> CalculateAmount (double min, double max, double increment)
        {
            var amounts = new List<double>();

            for(var x = min; x <= max; x = x + increment)
            {
                amounts.Add(x);
            }
            return amounts;
        }

        #endregion
    }
}
