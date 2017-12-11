// <copyright file="IUnit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.com">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Domain.Unit
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// Gets the name of the unit.
        /// </summary>
        /// <value>
        /// The name of the unit.
        /// </value>
        string UnitName
        {
            get;
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <value>
        /// The string value.
        /// </value>
        string StringValue
        {
            get;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IUnit<T> : IUnit, IConvertible
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        T Value
        {
            get;
        }
    }
}