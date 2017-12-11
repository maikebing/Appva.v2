// <copyright file="ValueObject.cs" company="Appva AB">
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
    /// Implementation of a <see cref="IValueObject"/>.
    /// <externalLink>
    ///     <linkText>Guidelines</linkText>
    ///     <linkUri>
    ///         https://msdn.microsoft.com/en-us/library/ms173147.aspx
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <typeparam name="T">The value object type</typeparam>
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>>, IValueObject, IImmutable where T : class
    {
        /// <inheritdoc />
        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            if (((object) left == null) || ((object) right == null))
            {
                return false;
            }
            return left.Equals(right);
        }

        /// <inheritdoc />
        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return ! (left == right);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (! (obj is T))
            {
                return false;
            }
            return this.Equals(((T) obj));
        }

        /// <inheritdoc />
        public bool Equals(ValueObject<T> other)
        {
            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <inheritdoc />
        /// <remarks>
        /// Force the implemented value object to implement a hash code for hash sets, 
        /// dictionary types etc.
        /// <externalLink>
        ///     <linkText>Hash code guidelines</linkText>
        ///     <linkUri>
        ///         http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx
        ///     </linkUri>
        /// </externalLink>
        /// </remarks>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.GetEqualityComponents());
        }

        /// <summary>
        /// When overridden in a derived class, returns all components of a value objects which constitute its identity.
        /// </summary>
        /// <returns>An ordered list of equality components.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}