// <copyright file="Language.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources
{
    #region Imports.

    using Appva.Fhir.Primitives;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Language : Code
    {
        #region Variables.

        /// <summary>
        /// Swedish language.
        /// </summary>
        /// <returns></returns>
        public static readonly Language Swedish = new Language("sv");
        
        /// <summary>
        /// English language.
        /// </summary>
        /// <returns></returns>
        public static readonly Language English = new Language("en");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        public Language(string value) 
            : base(value)
        {
        }

        #endregion
    }
}