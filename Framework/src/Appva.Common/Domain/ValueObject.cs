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
    /// <externalLink>
    ///     <linkText>Guidelines</linkText>
    ///     <linkUri>
    ///         https://msdn.microsoft.com/en-us/library/ms173147.aspx
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <typeparam name="T">The value object type</typeparam>
    public abstract class ValueObject<T> : IEquatable<T>, IValueObject where T : class
    {
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
        public abstract override int GetHashCode();

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
        
        #region IEquatable<T> Members.

        /// <inheritdoc />
        public abstract bool Equals(T other);

        #endregion
    }
}
