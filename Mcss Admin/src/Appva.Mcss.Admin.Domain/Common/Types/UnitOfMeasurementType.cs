// <copyright file="UnitOfMeasurementType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
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
    /// An NHibernante <see cref="UnitOfMeasurement"/> mapper.
    /// </summary>
    internal sealed class UnitOfMeasurementType : IUserType
    {


        /// <inheritdoc />
        public SqlType[] SqlTypes
        {
            get
            {
                return new[]
                {
                    NHibernateUtil.String.SqlType  //// Code
                };
            }
        }

        /// <inheritdoc />
        public Type ReturnedType
        {
            get
            {
                return typeof(UnitOfMeasurement);
            }
        }

        /// <inheritdoc />
        public bool IsMutable
        {
            get
            {
                return false;
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
            var typeIndex = rs.GetOrdinal(names[0]); //// code
            if (rs.IsDBNull(typeIndex))
            {
                return null;
            }
            var code = rs.GetValue(typeIndex) as string;
            return UnitOfMeasurement.Parse(code);
        }

        /// <inheritdoc />
        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                NHibernateUtil.String.NullSafeSet(cmd, null, index    ); //// Code
                return;
            }
            var unit = value as UnitOfMeasurement;
            NHibernateUtil.String.NullSafeSet(cmd, unit.Code,   index); //// Code
        }

        /// <inheritdoc />
        public object Replace(object original, object target, object owner)
        {
            return original;
        }
    }
}
