// <copyright file="Resource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources
{
    #region Imports.

    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// This specification defines a series of different types of resource that can be 
    /// used to exchange and/or store data in order to solve a wide range of healthcare 
    /// related problems, both clinical and administrative. In addition, this 
    /// specification defines several different ways of exchanging the resources.
    /// <para>
    /// A resource is an entity that:
    /// <list type="bullet">
    /// <item>
    ///     <description>
    ///         has a known identity (a url) by which it can be addressed
    ///     </description>
    /// </item>
    /// <item> 
    ///     <description>
    ///         identifies itself as one of the types of resource defined in this 
    ///         specification
    ///     </description>
    /// </item>
    /// <item> 
    ///     <description>
    ///         contains a set of structured data items as described by the definition
    ///         of the resource type
    ///     </description>
    /// </item>
    /// <item>
    ///     <description>
    ///         contains a human-readable XHTML representation of the content of the 
    ///         resource
    ///     </description>
    /// </item>
    /// <item>
    ///     <description>
    ///         has an identified version that changes if the contents of the resource 
    ///         change
    ///     </description>
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// Resources have multiple representations. A resource is valid if it meets the 
    /// above rules, and is represented in either XML or JSON according to the rules 
    /// defined in this specification. Other representations are allowed, but are not 
    /// described by this specification.
    /// </para>
    /// <externalLink>
    ///     <linkText>Resource</linkText>
    ///     <linkUri>http://hl7-fhir.github.io/resource.html</linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    [ProtoInclude(1, typeof(DomainResource))]
    public class Resource
    {
        /// <summary>
        /// The metadata about the resource. This is content that is maintained by the 
        /// infrastructure. Changes to the content may not always be associated with version 
        /// changes to the resource.
        /// <externalLink>
        ///     <linkText>Meta</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#Resource.meta
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(100), JsonProperty]
        public Meta Meta
        {
            get;
            set;
        }

        /// <summary>
        /// The base language in which the resource is written.
        /// <externalLink>
        ///     <linkText>IETF language tag</linkText>
        ///     <linkUri>
        ///         http://tools.ietf.org/html/bcp47
        ///     </linkUri>
        /// </externalLink>
        /// <externalLink>
        ///     <linkText>Language</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#Resource.language
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(101), JsonProperty]
        public Language Language
        {
            get;
            set;
        }

        /// <summary>
        /// A reference to a set of rules that were followed when the resource was 
        /// constructed, and which must be understood when processing the content.
        /// <externalLink>
        ///     <linkText>implicitRules</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#Resource.implicitRules
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(102), JsonProperty]
        public Uri ImplicitRules
        {
            get;
            set;
        }
    }
}