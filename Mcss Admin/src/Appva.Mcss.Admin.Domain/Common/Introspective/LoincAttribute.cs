// <copyright file="LoincAttribute.cs" company="Appva AB">
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
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class LoincAttribute : Attribute
    {
        #region Variables.

        /// <summary>
        /// The LOINC URI format.
        /// </summary>
        private const string UriFormat = "http://loinc.org/rdf/{code}";

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="LoincAttribute"/> class.
        /// </summary>
        /// <param name="code">The LOINC code.</param>
        /// <param name="description">The description.</param>
        public LoincAttribute(string code, string description)
        {
        }

        #endregion
    }
}