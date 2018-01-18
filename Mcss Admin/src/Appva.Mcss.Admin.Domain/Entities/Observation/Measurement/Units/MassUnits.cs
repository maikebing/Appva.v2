// <copyright file="MassUnits.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.ComponentModel;

    #endregion

    /// <summary>
    /// The kind of quality for mass unit of measurement.
    /// </summary>
    [Category("mass")]
    public static class MassUnits
    {
        /// <summary>
        /// The SI mass base unit kilogram.
        /// </summary>
        /// <remarks>
        /// UCUM uses gram as the mass base value.
        /// </remarks>
        public static readonly UnitOfMeasurement Kilogram = UnitOfMeasurement.New("kilogram", "kg", "kg");

        /// <summary>
        /// The mass unit for hektogram
        /// </summary>
        /// <remarks>
        /// UCUM uses gram as the mass base value.
        /// </remarks>
        public static readonly UnitOfMeasurement Hektogram = UnitOfMeasurement.New("hektogram", "hg", "hg");

        /// <summary>
        /// The UCUM mass base unit gram.
        /// </summary>
        public static readonly UnitOfMeasurement Gram = UnitOfMeasurement.New("gram", "g", "g");

        /// <summary>
        /// The mass unit for milligram
        /// </summary>
        /// <remarks>
        /// UCUM uses gram as the mass base value.
        /// </remarks>
        public static readonly UnitOfMeasurement Milligram = UnitOfMeasurement.New("milligram", "mg", "mg");

        /// <summary>
        ///  The mass unit for pound.
        /// </summary>
        /// <remarks>
        /// Uses the avoirdupois system of mass units.
        /// </remarks>
        public static readonly UnitOfMeasurement Pound = UnitOfMeasurement.New("pound", "lb", "lb_av");
    }
}