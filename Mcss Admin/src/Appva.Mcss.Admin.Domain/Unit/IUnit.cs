﻿// <copyright file="IUnit.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Domain.Unit
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IUnit<T> : IConvertible
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
    }
}