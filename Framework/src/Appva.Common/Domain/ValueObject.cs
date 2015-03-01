// <copyright file="ValueObject.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Common.Domain
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Implementation of a <see cref="IValueObject"/>.
    /// </summary>
    /// <typeparam name="T">The value object type</typeparam>
    public abstract class ValueObject<T> : IEquatable<T>, IValueObject where T : class
    {
        #region IEquatable<ValueObject<T>> Members

        /// <inheritdoc />
        public abstract bool Equals(T other);

        #endregion
    }
}
