// <copyright file="CodeAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Common
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Indicates a code.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class CodeAttribute : Attribute
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeAttribute"/> class.
        /// </summary>
        /// <param name="value">The code.</param>
        public CodeAttribute(string value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeAttribute"/> class.
        /// </summary>
        /// <param name="value">The code.</param>
        /// <param name="description">The description of the code.</param>
        public CodeAttribute(string value, string description)
        {
        }

        #endregion
    }
}