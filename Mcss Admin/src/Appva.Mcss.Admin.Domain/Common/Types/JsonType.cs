// <copyright file="JsonType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Common
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using NHibernate;
    using NHibernate.SqlTypes;
    using NHibernate.UserTypes;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Serializable]
    internal sealed class JsonType : IUserType
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonType"/> class.
        /// </summary>
        public JsonType()
        {
        }

        #endregion

        /// <summary>
        /// Deserializes the data.
        /// </summary>
        /// <param name="data">The data to deserialize.</param>
        /// <param name="type">The string representation of the type.</param>
        /// <returns>The deserialized object.</returns>
        private static object Deserialize(string data, string type)
        {
            return Deserialize(data, TypeNameHelper.GetType(type));
        }

        /// <summary>
        /// Deserializes the data.
        /// </summary>
        /// <param name="data">The data to deserialize.</param>
        /// <param name="type">The type of the data.</param>
        /// <returns>The deserialized object.</returns>
        private static object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }

        /// <summary>
        /// Serializes an object to a string representation.
        /// </summary>
        /// <param name="value">The value to be serialized</param>
        /// <returns>A string representation of the object.</returns>
        private static string Serialize(object value)
        {
            if (value == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Returns the string representation of the type of the object.
        /// </summary>
        /// <param name="value">The object to get the type.</param>
        /// <returns></returns>
        private static string GetType(object value)
        {
            if (value == null)
            {
                return null;
            }
            return TypeNameHelper.GetSimpleTypeName(value);
        }

        #region IUserType Members.

        /// <inheritdoc />
        public object Assemble(object cached, object owner)
        {
            var parts = cached as string[];
            if (parts == null)
            {
                return null;
            }
            return Deserialize(parts[1], parts[0]);
        }

        /// <inheritdoc />
        public object DeepCopy(object value)
        {
            if (value == null)
            {
                return null;
            }
            return Deserialize(Serialize(value), GetType(value));
        }

        /// <inheritdoc />
        public object Disassemble(object value)
        {
            if (value == null)
            {
                return null;
            }
            return new string[] { GetType(value), Serialize(value) };
        }

        /// <inheritdoc />
        public bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (ReferenceEquals(null, x) || ReferenceEquals(null, y))
            {
                return false;
            }
            return x.Equals(y);
        }

        /// <inheritdoc />
        public int GetHashCode(object x)
        {
            return HashCode.Combine(new[] { x });
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
        public object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            var typeIndex = rs.GetOrdinal(names[0]);
            var dataIndex = rs.GetOrdinal(names[1]);
            if (rs.IsDBNull(typeIndex) || rs.IsDBNull(dataIndex))
            {
                return null;
            }
            var type = (string) rs.GetValue(typeIndex);
            var data = (string) rs.GetValue(dataIndex);
            return Deserialize(data, type);
        }

        /// <inheritdoc />
        public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                NHibernateUtil.String.NullSafeSet(cmd, null, index);
                NHibernateUtil.String.NullSafeSet(cmd, null, index + 1);
                return;
            }
            var type = GetType(value);
            var data = Serialize(value);
            NHibernateUtil.String.NullSafeSet(cmd, type, index);
            NHibernateUtil.String.NullSafeSet(cmd, data, index + 1);
        }

        /// <inheritdoc />
        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        /// <inheritdoc />
        public Type ReturnedType
        {
            get
            {
                return typeof(DomainEvent);
            }
        }

        /// <inheritdoc />
        /// <remarks>0. Type, 1. Data</remarks>
        public SqlType[] SqlTypes
        {
            get
            {
                return new[]  { SqlTypeFactory.GetString(10000), SqlTypeFactory.GetStringClob(10000) };
            }
        }

        #endregion
    }
}