// <copyright file="VolumeUnits.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.ComponentModel;

    #endregion

    /// <summary>
    /// REMINDER: Add a descriptive summary to increase readability.
    /// </summary>
    [Category("volume")]
    public static class VolumeUnits
    {
        /// <summary>
        /// The volume base unit liter.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static readonly UnitOfMeasurement Liter = UnitOfMeasurement.New("liter", "l", "l");

        /// <summary>
        /// The volume unit for deciliter
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static readonly UnitOfMeasurement Deciliter = UnitOfMeasurement.New("deciliter", "dl", "dl");

        /// <summary>
        /// The volume unit for centiliter.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static readonly UnitOfMeasurement Centiliter = UnitOfMeasurement.New("centiliter", "cl", "cl");

        /// <summary>
        /// The volume unit for milliliter
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static readonly UnitOfMeasurement Milliliter = UnitOfMeasurement.New("milliliter", "ml", "ml");
    }
}
