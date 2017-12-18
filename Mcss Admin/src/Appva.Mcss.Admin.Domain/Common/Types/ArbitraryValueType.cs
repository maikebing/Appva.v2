// <copyright file="ArbitraryValueType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Common
{
    #region Imports.

    using System;
    using System.Data;
    using Appva.Mcss.Admin.Domain.Entities;
    using NHibernate;
    using NHibernate.SqlTypes;
    using NHibernate.UserTypes;

    #endregion

    /// <summary>
    /// An Nhibernate <see cref="ArbitraryValue"/> mapper.
    /// </summary>
    [Serializable]
    internal sealed class ArbitraryValueType : IUserType
    {
        /// <inheritdoc />
        public bool IsMutable
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        public Type ReturnedType
        {
            get
            {
                return typeof(ArbitraryValue);
            }
        }

        /// <inheritdoc />
        /// <remarks>0. Type, 1. Data</remarks>
        public SqlType[] SqlTypes
        {
            get
            {
                return new[] 
                { 
                    NHibernateUtil.String.SqlType, //// Value
                    NHibernateUtil.String.SqlType, //// ValueType
                    NHibernateUtil.String.SqlType, //// Unit
                    NHibernateUtil.String.SqlType, //// Level
                };
            }
        }

        /// <inheritdoc />
        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        /// <inheritdoc />
        public object DeepCopy(object value)
        {
            return value;
        }

        /// <inheritdoc />
        public object Disassemble(object value)
        {
            return value;
        }

        /// <inheritdoc />
        public new bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y) || x.Equals(y);
        }

        /// <inheritdoc />
        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        /// <inheritdoc />
        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var value       = NHibernateUtil.String.NullSafeGet(rs, names[0]) as string; 
            var valueType   = NHibernateUtil.String.NullSafeGet(rs, names[1]) as string; 
            var unit        = NHibernateUtil.String.NullSafeGet(rs, names[2]) as string;
            var typeOfScale = NHibernateUtil.String.NullSafeGet(rs, names[3]) as string;
            return ArbitraryValue.Parse(value, valueType, unit, typeOfScale);
        }

        /// <inheritdoc />
        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            var parameter = (IDataParameter) cmd.Parameters[index];
            if (value == null)
            {
                NHibernateUtil.String.NullSafeSet(cmd, null, index    ); //// Value
                NHibernateUtil.String.NullSafeSet(cmd, null, index + 1); //// ValueType
                NHibernateUtil.String.NullSafeSet(cmd, null, index + 2); //// Unit
                NHibernateUtil.String.NullSafeSet(cmd, null, index + 3); //// Level
                return;
            }
            var measurement = value as ArbitraryValue;
            var arbitraryValue = Convert.ChangeType(measurement.Value, typeof(string));
            NHibernateUtil.String.NullSafeSet(cmd, arbitraryValue, index); //// Value
            NHibernateUtil.String.NullSafeSet(cmd, measurement.ValueType,                                       index + 1); //// ValueType
            NHibernateUtil.String.NullSafeSet(cmd, measurement.Unit == null ? null: measurement.Unit.Name,      index + 2);    //// Unit
            NHibernateUtil.String.NullSafeSet(cmd, Enum.GetName(typeof(LevelOfMeasurement), measurement.Level), index + 3); //// Level
        }

        /// <inheritdoc />
        public object Replace(object original, object target, object owner)
        {
            return original;
        }
    }
}