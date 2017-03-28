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
        public Measurement(object value, Taxon unit)
        {
            Requires.NotNull(value, "value");
            Requires.NotNull(unit , "unit" );
            var type   = value.GetType();
            this.Value = Convert.ChangeType(value, typeof(string)) as string;
            this.Type  = type.FullName;
            this.Code  = System.Type.GetTypeCode(type);
            this.Unit  = unit;
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

        /// <summary>
        /// The unit.
        /// </summary>
        public virtual Taxon Unit
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
        public static Measurement New<T>(T value, Taxon unit) where T : struct
        {
            return new Measurement(value, unit);
        }

        #endregion

        #region Public Members.

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>() where T : struct
        {
            return (T) Convert.ChangeType(this.Value, System.Type.GetType(this.Type));
        }

        #endregion

        #region ValueObject overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            if (this.Unit == null)
            {
                return this.Value;
            }
            return this.Value + " " + this.Unit.Name;
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Value;
            yield return this.Code;
            yield return this.Type;
            yield return this.Unit.Id;
        }

        #endregion
    }
}