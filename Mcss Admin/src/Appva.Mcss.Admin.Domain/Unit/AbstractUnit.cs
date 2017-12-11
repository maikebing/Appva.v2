// <copyright file="Unit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Domain.Unit
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion


    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class AbstractUnit<T> : IUnit<T>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractUnit{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public AbstractUnit(T value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public T Value
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public abstract string UnitName
        {
            get;
        }

        #endregion

        #region IConvertible members.

        #region Implemented.

        /// <inheritdoc />
        public abstract string ToString(IFormatProvider provider);

        #endregion

        #region Not implemented

        public virtual TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public virtual bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public virtual ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
 
    }
}