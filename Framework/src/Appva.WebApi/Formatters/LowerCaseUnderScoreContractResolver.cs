// <copyright file="LowerCaseUnderScoreContractResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi.Formatters
{
    #region Imports.

    using Core.Extensions;
    using Newtonsoft.Json.Serialization;

    #endregion

    /// <summary>
    /// Lowercase underscore JSON contract formatter. 
    /// </summary>
    /// <example>
    /// Below will be formatted as { "foo_bar": "baz" }:
    /// var foo = new { FooBar = "baz" }; 
    /// </example>
    public sealed class LowerCaseUnderScoreContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Resolves contract property names to lowercase/underscore.
        /// </summary>
        /// <param name="propertyName">The property name</param>
        /// <returns>Formatted property name</returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLowerCaseUnderScore();
        }
    }
}