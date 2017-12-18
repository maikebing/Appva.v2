// <copyright file="ArbituraryMeasuredValue.cs" company="Appva AB">
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
    using System.Diagnostics;
    using Appva.Mcss.Admin.Domain.Common;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArbituraryMeasuredValue : ValueObject<ArbituraryMeasuredValue>, IArbituraryValue
    {
        #region Variables.

        /// <summary>
        /// The string representation of the measured value.
        /// </summary>
        private readonly string value;

        /// <summary>
        /// The underlying type of the measured value.
        /// </summary>
        private readonly Type valueType;

        /// <summary>
        /// The unit of measurement, if any.
        /// </summary>
        private readonly IUnitOfMeasurement unit;

        /// <summary>
        /// The level of measurement.
        /// </summary>
        private readonly LevelOfMeasurement level;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArbituraryMeasuredValue"/> class.
        /// </summary>
        /// <param name="value">
        /// The string value, if <typeparamref name="valueType"/> is of type 
        /// <see cref="System.String"/>; otherwise a string representation of
        /// the struct or enumeration.
        /// </param>
        /// <param name="valueType">The value type.</param>
        /// <param name="level">The level of measurement.</param>
        /// <param name="unit">The unit of measurement.</param>
        private ArbituraryMeasuredValue(string value, Type valueType, LevelOfMeasurement level, IUnitOfMeasurement unit = null)
        {
            Requires.NotNullOrWhiteSpace(value, "value");
            Requires.NotNull(valueType, "valueType");
            /*Requires.Argument(
                valueType == typeof(string) ||
                valueType.IsValueType || 
                valueType.IsEnum, 
                "valueType", 
                "valueType must be one of: string, enumeration or struct");*/
            this.value     = value;
            this.valueType = valueType;
            this.level     = level;
            this.unit      = unit;
        }

        #endregion

        #region Properties.

        /// <inheritdoc />
        public object Value
        {
            get
            {
                return this.valueType.IsEnum ?
                    Enum.Parse(this.valueType, this.value) :
                    Convert.ChangeType(this.value, this.valueType);
            }
        }

        /// <inheritdoc />
        public string ValueType
        {
            get
            {
                return this.valueType.IsEnum ?
                    this.valueType.Name :
                    this.valueType.FullName;
            }
        }

        /// <inheritdoc />
        public IUnitOfMeasurement Unit
        {
            get
            {
                return this.unit;
            }
        }

        /// <inheritdoc />
        public LevelOfMeasurement TypeOfScale
        {
            get
            {
                return this.level;
            }
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="ArbituraryMeasuredValue"/> class.
        /// </summary>
        /// <param name="value">
        /// The string value.
        /// </param>
        /// <param name="level">The level of measurement.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <returns>
        /// A new <see cref="ArbituraryMeasuredValue"/> instance.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="value"/> is null, empty ("") or contains only whitespace 
        /// characters.
        /// </exception>
        public static ArbituraryMeasuredValue New(string value, LevelOfMeasurement level, IUnitOfMeasurement unit)
        {
            return new ArbituraryMeasuredValue(value, typeof(string), level, unit);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ArbituraryMeasuredValue"/> class.
        /// </summary>
        /// <typeparam name="T">The <paramref name="value"/> type.</typeparam>
        /// <param name="value">
        /// The string representation of a struct or enumeration.
        /// </param>
        /// <param name="level">The level of measurement.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <returns>
        /// A new <see cref="ArbituraryMeasuredValue"/> instance.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="value"/> is null, empty ("") or contains only whitespace 
        /// characters.
        /// </exception>
        public static ArbituraryMeasuredValue New<T>(T value, LevelOfMeasurement level, IUnitOfMeasurement unit) where T : struct
        {
            return new ArbituraryMeasuredValue(Convert.ChangeType(value, typeof(string)) as string, typeof(T), level, unit);
        }

        /// <summary>
        /// Converts the string representation of a <see cref="ArbituraryMeasuredValue"/> to 
        /// its <see cref="ArbituraryMeasuredValue"/> equivalent.
        /// </summary>
        /// <param name="value">
        /// A string that contains a value, quantity, ratio to convert.
        /// </param>
        /// <param name="valueType">
        /// A string that contains a type to convert.
        /// </param>
        /// <param name="unit">
        /// A string that contains a unit of measurement code to convert.
        /// </param>
        /// <param name="level">
        /// A string that contains a level of measurement code to convert.
        /// </param>
        /// <returns>
        /// An new <see cref="ArbituraryMeasuredValue"/> that is equivalent to the 
        /// <see cref="ArbituraryMeasuredValue"/> contained in <paramref name="value"/>,
        /// <paramref name="valueType"/>, <paramref name="unit"/>, <paramref name="level"/>;
        /// or null otherwise.
        /// </returns>
        public static ArbituraryMeasuredValue Parse(string value, string valueType, string unit, string level)
        {
            Debug.Assert(value     != null, "value is null"    );
            Debug.Assert(valueType != null, "valueType is null");
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(valueType))
            {
                return null;
            }
            var type = System.Type.GetType(valueType, false);
            //// If the value type does not start with system or the type is null, we are dealing with an enumeration
            //// and we need to resolve the assembly.
            if (valueType.StartsWith("System") == false || type == null)
            {
                type = AssemblyHelper.Resolve(valueType);
            }
            if (type == null)
            {
                return null;
            }
            return new ArbituraryMeasuredValue(value, type, EnumHelper.IsParsable<LevelOfMeasurement>(level) ? EnumHelper.Parse<LevelOfMeasurement>(level) : LevelOfMeasurement.Undetermined, unit == null ? null : UnitOfMeasurement.Parse(unit));
        }

        #endregion

        /// <inheritdoc />
        public IArbituraryValue DeepCopy()
        {
            //// A shallow copy is sufficient enough.
            return (IArbituraryValue) this.MemberwiseClone();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.value;
        }

        /// <inheritdoc />
        public string ToString(string format)
        {
            if (format == "g" && this.Unit != null && this.Unit is UnitOfMeasurement)
            {
                return this.value + " " + this.Unit.Symbol;
            }
            return this.value;
        }

        /// <inheritdoc />
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format("{0:" + format + "}", formatProvider);
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.value;
            yield return this.valueType;
            yield return this.unit;
            yield return this.level;
        }
    }
}