// <copyright file="FormUrlObjectAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Apis.Http.Attributes
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FormUrlEncodedObjectAttribute : Attribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FormUrlEncodedObjectAttribute"/> class.
        /// </summary>
        public FormUrlEncodedObjectAttribute()
        {
        }

        #endregion
    }
}