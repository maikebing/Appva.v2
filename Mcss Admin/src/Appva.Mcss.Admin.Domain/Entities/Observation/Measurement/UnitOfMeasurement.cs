// <copyright file="UnitOfMeasurement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validation;

    #endregion

    /// <summary>
    /// Represents a code system intended to include all units of measures being 
    /// contemporarily used in health care.
    /// </summary>
    public class UnitOfMeasurement : IUnitOfMeasurement
    {
        #region Variables.

        /// <summary>
        /// The official name of the unit.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The official symbol used in print.
        /// </summary>
        private readonly string symbol;

        /// <summary>
        /// The case insensitive code.
        /// </summary>
        private readonly string code;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfMeasurement"/> class.
        /// </summary>
        /// <param name="name">The official name of the unit.</param>
        /// <param name="symbol">The official symbol used in print.</param>
        /// <param name="code">The case insensitive code.</param>
        public UnitOfMeasurement(string name, string symbol, string code)
        {
            Requires.NotNullOrWhiteSpace(name,     "name");
            Requires.NotNullOrWhiteSpace(code,     "code");
            this.name   = name;
            this.symbol = symbol;
            this.code   = code;
        }

        #endregion

        #region Properties.

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <inheritdoc />
        public string Symbol
        {
            get
            {
                return this.symbol;
            }
        }

        /// <inheritdoc />
        public string Code
        {
            get
            {
                return this.code;
            }
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="UnitOfMeasurement"/> class.
        /// </summary>
        /// <param name="name">The official name of the unit.</param>
        /// <param name="symbol">The official symbol used in print.</param>
        /// <param name="code">The case insensitive code.</param>
        /// <returns>A new <see cref="UnitOfMeasurement"/> instance.</returns>
        public static UnitOfMeasurement New(string name, string symbol, string code)
        {
            return new UnitOfMeasurement(name, symbol, code);
        }

        /// <summary>
        /// Converts the string representation of a unit of measurement (the code) to its 
        /// unit of measurement equivalent.
        /// </summary>
        /// <param name="str">
        /// A string that contains a unit of measurement code to convert.
        /// </param>
        /// <returns>
        /// An new date that is equivalent to the date contained in <paramref name="str"/>.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="str"/> is null, empty ("") or contains only whitespace 
        /// characters.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <paramref name="str"/> does not contain a valid string representation of a date.
        /// </exception>
        public static IUnitOfMeasurement Parse(string str)
        {
            Requires.NotNullOrWhiteSpace(str, "str");
            var unit  = new[] 
                { 
                    MassUnits.Kilogram, MassUnits.Gram, MassUnits.Pound,
                    NonUnits.ArbitraryStoolScale, NonUnits.BristolStoolScale, NonUnits.TenaIdentifiScale
                }.Where(x => x.Name == str).FirstOrDefault();
            if (unit != null)
            {
                return unit;
            }
            throw new FormatException("No unit of measurement found for code: " + unit);
        }

        #endregion
    }
}