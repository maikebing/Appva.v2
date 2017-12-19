// <copyright file="ArbitraryValue.cs" company="Appva AB">
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
    using Appva.Mcss.Domain.Unit;
    using Validation;

    #endregion

    /// <summary>
    /// Represents the measured value, annotation, quantity or ratio for an 
    /// <see cref="ObservationItem"/>.
    /// </summary>
    /// <remarks>
    /// The value must be either a string or a primitive value.
    /// </remarks>
    public sealed class ArbitraryValue : ValueObject<ArbitraryValue>, IFormattable, IDeepCopyable<ArbitraryValue>
    {
        #region Variables.

        /// <summary>
        /// The string representation of the measured value.
        /// </summary>
        private readonly object value;

        /// <summary>
        /// The underlying type of the measured value.
        /// </summary>
        private readonly Type valueType;

        /// <summary>
        /// The unit of measurement, if any.
        /// </summary>
        private readonly UnitOfMeasurement unit;

        /// <summary>
        /// The level of measurement.
        /// </summary>
        private readonly LevelOfMeasurement level;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArbitraryValue"/> class.
        /// </summary>
        /// <param name="value">
        /// The string value, if <typeparamref name="valueType"/> is of type 
        /// <see cref="System.String"/>; otherwise a string representation of
        /// the struct or enumeration.
        /// </param>
        /// <param name="valueType">The value type.</param>
        /// <param name="level">The level of measurement.</param>
        /// <param name="unit">The unit of measurement.</param>
        private ArbitraryValue(object value, Type valueType, LevelOfMeasurement level, UnitOfMeasurement unit = null)
        {
            Requires.NotNull(value,     "value"    );
            Requires.NotNull(valueType, "valueType");
            Requires.Argument(
                valueType == typeof(string) ||
                valueType.IsPrimitive       ||
                valueType.IsEnum, 
                "valueType", 
                "valueType must be one of: string, enumeration or struct");
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
                return this.value;
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
        public UnitOfMeasurement Unit
        {
            get
            {
                return this.unit;
            }
        }

        /// <inheritdoc />
        public LevelOfMeasurement Level
        {
            get
            {
                return this.level;
            }
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="ArbitraryValue"/> class.
        /// </summary>
        /// <param name="value">
        /// The string value.
        /// </param>
        /// <param name="level">The level of measurement.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <returns>
        /// A new <see cref="ArbitraryValue"/> instance.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="value"/> is null, empty ("") or contains only whitespace 
        /// characters.
        /// </exception>
        public static ArbitraryValue New(string value, LevelOfMeasurement level, UnitOfMeasurement unit)
        {
            return new ArbitraryValue(value, typeof(string), level, unit);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ArbitraryValue"/> class.
        /// </summary>
        /// <typeparam name="T">The <paramref name="value"/> type.</typeparam>
        /// <param name="value">
        /// The string representation of a struct or enumeration.
        /// </param>
        /// <param name="level">The level of measurement.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <returns>
        /// A new <see cref="ArbitraryValue"/> instance.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="value"/> is null, empty ("") or contains only whitespace 
        /// characters.
        /// </exception>
        public static ArbitraryValue New<T>(T value, LevelOfMeasurement level, UnitOfMeasurement unit) where T : struct
        {
            return new ArbitraryValue(value, typeof(T), level, unit);
        }

        /// <summary>
        /// Converts the string representation of a <see cref="ArbitraryValue"/> to 
        /// its <see cref="ArbitraryValue"/> equivalent.
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
        /// An new <see cref="ArbitraryValue"/> that is equivalent to the 
        /// <see cref="ArbitraryValue"/> contained in <paramref name="value"/>,
        /// <paramref name="valueType"/>, <paramref name="unit"/>, <paramref name="level"/>;
        /// or null otherwise.
        /// </returns>
        public static ArbitraryValue Parse(string value, string valueType, string unit, string level)
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
            var levelOfMeasurement = EnumHelper.IsParsable<LevelOfMeasurement>(level) ? EnumHelper.Parse<LevelOfMeasurement>(level) : LevelOfMeasurement.Undetermined; 
            var unitOfMeasurement  = unit == null ? null : UnitOfMeasurement.Parse(unit);
            if (type == typeof(string))
            {
                return new ArbitraryValue(value, type, levelOfMeasurement, unitOfMeasurement);
            }
            if (type.IsEnum)
            {
                return new ArbitraryValue(Enum.Parse(type, value), type, levelOfMeasurement, unitOfMeasurement);
            }
            if (type.IsPrimitive)
            {
                return new ArbitraryValue(Convert.ChangeType(value, type), type, levelOfMeasurement, unitOfMeasurement);
            }
            if (typeof(IUnit).IsAssignableFrom(type))
            {
                var enumType = type.BaseType.GetGenericArguments()[0];
                return new ArbitraryValue(Enum.Parse(enumType, value), enumType, levelOfMeasurement, unitOfMeasurement);
            }
            return null;
        }

        #endregion

        /// <inheritdoc />
        public ArbitraryValue DeepCopy()
        {
            return (ArbitraryValue) this.MemberwiseClone(/* A shallow copy will suffice. */);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.value.ToString();
        }

        /// <inheritdoc />
        public string ToString(string format)
        {
            if (format == "g" && this.Unit != null && this.Unit is UnitOfMeasurement)
            {
                return this.value + " " + this.Unit.Symbol;
            }
            return this.ToString();
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