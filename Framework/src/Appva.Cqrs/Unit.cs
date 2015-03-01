// <copyright file="Unit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cqrs
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Dispatcher abstractation.
    /// </summary>
    public sealed class Unit : IComparable
    {
        #region Variables.

        /// <summary>
        /// The unit type.
        /// </summary>
        public static readonly Unit Value = new Unit();

        #endregion

        #region Object Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 0;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj == null || obj is Unit;
        }

        /// <inheritdoc />
        int IComparable.CompareTo(object obj)
        {
            return 0;
        }

        #endregion
    }
}
