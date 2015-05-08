// <copyright file="IExtendable.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Every element in a resource or data type includes an optional "extension" child 
    /// element that may be present any number of times. This is the content model of 
    /// the extension as it appears in each resource:
    /// <externalLink>
    ///     <linkText>Extensibility</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/extensibility.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// <[name] xmlns="http://hl7.org/fhir" url="the meaning of the extension (uri)">
    ///     <!-- from Element: extension -->
    ///     <value[x]><!-- 0..1 * Value of extension --></value[x]>
    /// </[name]>
    /// ]]>
    /// </code>
    /// </example>
    public interface IExtendable
    {
        /// <summary>
        /// The collection of extensions.
        /// </summary>
        IList<Extension> Extension
        {
            get;
            set;
        }
    }
}