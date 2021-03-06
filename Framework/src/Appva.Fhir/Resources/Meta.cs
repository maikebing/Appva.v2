﻿// <copyright file="Meta.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources
{
    #region Imports.

    using System.Collections.Generic;
    using Complex;
    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// The metadata about the resource. This is content that is maintained by the 
    /// infrastructure. Changes to the content may not always be associated with version 
    /// changes to the resource.
    /// <code>
    /// <![CDATA[
    /// <meta>  <!-- 0..1 Metadata about the resource -->
    ///     <versionId value="[id]"/><!-- 0..1 Version specific identifier -->
    ///     <lastUpdated value="[instant]"/><!-- 0..1 When the resource version last changed -->
    ///     <profile value="[uri]"/><!-- 0..* Profiles this resource claims to conform to -->
    ///     <security><!-- 0..* Coding Security Labels applied to this resource --></security>
    ///     <tag><!-- 0..* Coding Tags applied --></tag>
    /// </meta>
    /// ]]>
    /// </code>
    /// <externalLink>
    ///     <linkText>Meta</linkText>
    ///     <linkUri>http://hl7-fhir.github.io/resource-definitions.html#Resource.meta</linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class Meta : Element
    {
        /// <summary>
        /// The version specific identifier, as it appears in the version portion of the 
        /// url. This values changes when the resource is created, updated, or deleted.
        /// <externalLink>
        ///     <linkText>VersionId</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#Resource.meta.versionId
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public string VersionId
        {
            get;
            set;
        }

        /// <summary>
        /// When the resource last changed - e.g. when the version changed.
        /// <externalLink>
        ///     <linkText>LastUpdated</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#Resource.meta.lastUpdated
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(2), JsonProperty]
        public Instant LastUpdated
        {
            get;
            set;
        }

        /// <summary>
        /// A list of profiles that this resource claims to conform to. The URL is a 
        /// reference to Profile.url.
        /// <externalLink>
        ///     <linkText>Profile</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#Resource.meta.profile
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(3), JsonProperty]
        public IList<Uri> Profile
        {
            get;
            set;
        }

        /// <summary>
        /// Security labels applied to this resource. These tags connect specific resources 
        /// to the overall security policy and infrastructure.
        /// <externalLink>
        ///     <linkText>Security</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#Resource.meta.security
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(4), JsonProperty]
        public IList<Coding> Security
        {
            get;
            set;
        }

        /// <summary>
        /// Tags applied to this resource. Tags are intended to to be used to identify and 
        /// relate resources to process and workflow, and applications are not required to 
        /// consider the tags when interpreting the meaning of a resource.
        /// <externalLink>
        ///     <linkText>Tag</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#Resource.meta.tag
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(5), JsonProperty]
        public IList<Coding> Tag
        {
            get;
            set;
        }
    }
}