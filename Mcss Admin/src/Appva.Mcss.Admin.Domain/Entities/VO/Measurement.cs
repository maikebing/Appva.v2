// <copyright file="Measurement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Validation;
    using System.Reflection;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Measurement : ValueObject<Measurement>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Measurement"/> class.
        /// </summary>
        /// <param name="value">The string representation of the value.</param>
        /// <param name="unit">The unit.</param>
        public Measurement(object value)
        {
            Requires.NotNull(value, "value");
            var type   = value.GetType();
            this.Value = Convert.ChangeType(value, typeof(string)) as string;
            this.Type  = type.FullName;
            this.Code  = System.Type.GetTypeCode(type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Measurement"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected Measurement()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The value.
        /// </summary>
        public virtual string Value
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The value type.
        /// </summary>
        public virtual string Type
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The value type.
        /// </summary>
        public virtual TypeCode Code
        {
            get;
            internal protected set;
        }

        #endregion

        #region Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="Measurement"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="Measurement"/> instance.</returns>
        public static Measurement New<T>(T value) where T : struct
        {
            return new Measurement(value);
        }

        #endregion

        #region Public Members.

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            var type = System.Type.GetType(this.Type);
            if (type.IsValueType)
            {
                return (T)Convert.ChangeType(this.Value, System.Type.GetType(this.Type));
            }
            ConstructorInfo ctor = type.GetConstructor(new[] { typeof(string) });
            return (T)ctor.Invoke(new object[] { this.Value });
        }

        #endregion

        #region ValueObject overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            //// TODO: Should involve the type to also print the unit 
            return this.Value;
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Value;
            yield return this.Code;
            yield return this.Type;
        }

        #endregion
    }
}