// <copyright file="AbstractUnit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.com">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Domain.Unit
{
    #region Imports.

    using System;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Abstract base implementation of <see cref="IUnit{T}"/>.
    /// </summary>
    /// <typeparam name="T">The unit type.</typeparam>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AbstractUnit<T> : IUnit<T>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractUnit{T}"/> class.
        /// </summary>
        /// <param name="name">The unit name.</param>
        /// <param name="value">The unit value.</param>
        protected AbstractUnit(string name, T value)
        {
            this.UnitName = name;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractUnit{T}"/> class.
        /// </summary>
        /// <remarks>
        /// For serialization purposes.
        /// </remarks>
        internal protected AbstractUnit(string name)
        {
            this.UnitName = name;
        }

        #endregion

        #region Properties.

        /// <inheritdoc />
        public T Value
        {
            get;
            private set;
        }

        /// <inheritdoc />
        [JsonProperty("unit")]
        public virtual string UnitName
        {
            get;
            private set;
        }

        /// <inheritdoc />
        [JsonProperty("value")]
        public string StringValue
        {
            get
            {
                return this.ToString();
            }
        }

        #endregion

        #region IConvertible Members.

        /// <inheritdoc />
        public virtual string ToString(IFormatProvider provider)
        {
            return this.ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value.ToString();
        }

        /// <inheritdoc /> 
        public virtual TypeCode GetTypeCode()
        {
            return Type.GetTypeCode(typeof(T));
        }

        /// <inheritdoc /> 
        public virtual bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this.Value, conversionType, provider);
        }

        /// <inheritdoc /> 
        public virtual ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(this.Value, provider);
        }

        /// <inheritdoc /> 
        public virtual ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(this.Value, provider);
        }

        #endregion
    }
}