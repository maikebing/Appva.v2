// <copyright file="SecurityEventObjectType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using Primitives;

    #endregion

    /// <summary>
    /// Code for the participant object type being audited.
    /// <externalLink>
    ///     <linkText>1.15.2.1.166.1 SecurityEventObjectType</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/object-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class SecurityEventObjectType : Code
    {
        #region Variables.

        /// <summary>
        /// Person.
        /// </summary>
        public static readonly SecurityEventObjectType Person = new SecurityEventObjectType("1");

        /// <summary>
        /// System Object.
        /// </summary>
        public static readonly SecurityEventObjectType SystemObject = new SecurityEventObjectType("2");

        /// <summary>
        /// Organization.
        /// </summary>
        public static readonly SecurityEventObjectType Organization = new SecurityEventObjectType("3");

        /// <summary>
        /// Other.
        /// </summary>
        public static readonly SecurityEventObjectType Other = new SecurityEventObjectType("4");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventObjectType"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private SecurityEventObjectType(string value) 
            : base(value)
        {
        }

        #endregion
    }
}