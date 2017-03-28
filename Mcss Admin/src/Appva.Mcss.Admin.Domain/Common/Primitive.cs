// <copyright file="Primitive.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class Primitive : ValueObject<Primitive>
    {
        #region Variables.

        /// <summary>
        /// The primitive value.
        /// </summary>
        private readonly string value;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        protected Primitive(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected Primitive()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
        }

        #endregion

        #region ValueObject<>.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.value;
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.value;
        }

        #endregion
    }
}