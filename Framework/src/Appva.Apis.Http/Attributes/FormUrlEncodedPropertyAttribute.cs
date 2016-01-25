// <copyright file="FormUrlPropertyAttribute.cs" company="Appva AB">
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
    [AttributeUsage(AttributeTargets.Property)]
    public class FormUrlEncodedPropertyAttribute : Attribute
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FormUrlEncodedPropertyAttribute"/> class.
        /// </summary>
        public FormUrlEncodedPropertyAttribute()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormUrlEncodedPropertyAttribute"/> class with given propertyname
        /// </summary>
        /// <param name="propertyName">The propertyname</param>
        public FormUrlEncodedPropertyAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The name of the encoded property
        /// </summary>
        public string PropertyName { get; set; }

        #endregion
    }
}