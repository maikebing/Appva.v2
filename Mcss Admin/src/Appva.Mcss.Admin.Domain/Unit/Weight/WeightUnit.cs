// <copyright file="WeightUnit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.com">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Domain.Unit
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class WeightUnit : AbstractUnit<double>
    {
        #region Variables.

        /// <summary>
        /// The unit name identifier / name.
        /// </summary>
        private const string Id = "weight";

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightUnit"/> class.
        /// </summary>
        /// <param name="value">
        /// A string representation of a weight scale value.
        /// </param>
        public WeightUnit(string value)
            : base(Id, Parse(value))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightUnit"/> class.
        /// </summary>
        /// <param name="value">The double value.</param>
        public WeightUnit(double value)
            : base(Id, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightUnit"/> class.
        /// </summary>
        /// <remarks>
        /// For internal serialization.
        /// </remarks>
        private WeightUnit()
            : base(Id)
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Converts a string representation of a weight scale to a 
        /// double.
        /// </summary>
        /// <param name="str">
        /// A string representation of a weight scale value set code.
        /// </param>
        /// <returns>
        /// A <see cref="double"/> which accurately represents the 
        /// <typeparamref name="str"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <typeparamref name="str"/> is null.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <typeparamref name="str"/> does not represent a number in a valid format.
        /// </exception>
        /// <exception cref="System.OverflowException">
        /// <typeparamref name="str"/> represents a number that is less than 
        /// <see cref="System.Double.MinValue"/> or greater than 
        /// <see cref="System.Double.MaxValue"/>
        /// </exception>
        public static double Parse(string str)
        {
            return double.Parse(str);
        }

        #endregion

        #region AbstractUnit Overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value.ToString();
        }

        /// <inheritdoc />
        public override string ToString(IFormatProvider provider)
        {
            return this.Value.ToString(provider);
        }

        #endregion
    }
}