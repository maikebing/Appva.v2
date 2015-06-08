// <copyright file="DomainResource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources
{
    #region Imports.

    using Administrative;
    using Newtonsoft.Json;
    using ProtoBuf;
    using Security;

    #endregion

    /// <summary>
    /// This resource is defined to add text, contained resources, and extension support 
    /// to the base resource. As an abstract resource, this resource never exists 
    /// directly, only one of it's descendent resources.
    /// <externalLink>
    ///     <linkText>DomainResource</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/domainresource.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <example>
    /// <code language="xml" title="DomainResource Example">
    /// <![CDATA[
    /// <[name] xmlns="http://hl7.org/fhir">
    ///     <!-- from Element: extension -->
    ///     <text>
    ///         <!-- ?? 0..1 Narrative Text summary of the resource -->
    ///     </text>
    ///     <contained>
    ///         <!-- 0..* Resource Contained, inline Resources -->
    ///     </contained>
    ///     <extension>
    ///         <!-- 0..* Extension Additional Content defined by implementations -->
    ///     </extension>
    ///     <modifierExtension>
    ///         <!-- 0..* Extension Extensions that cannot be ignored -->
    ///     </modifierExtension>
    /// </[name]>
    /// ]]>
    /// </code>
    /// </example>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    [ProtoInclude(1, typeof(Patient))]
    [ProtoInclude(2, typeof(AuditEvent))]
    public abstract class DomainResource : Resource
    {
        /// <summary>
        /// A human-readable narrative that contains a summary of the resource, and may be 
        /// used to represent the content of the resource to a human. The narrative need not 
        /// encode all the structured data, but is required to contain sufficient detail to 
        /// make it "clinically safe" for a human to just read the narrative. Resource 
        /// definitions may define what content should be represented in the narrative to 
        /// ensure clinical safety.
        /// <externalLink>
        ///     <linkText>DomainResource.Text</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/domainresource-definitions.html#DomainResource.text
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(300), JsonProperty]
        public string Text
        {
            get;
            set;
        }
    }
}