﻿// <copyright file="Coding.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Complex
{
    #region Imports.

    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// A <c>Coding</c> is a representation of a defined concept using a symbol from a 
    /// defined "code system" - see Using Codes in resources for more details.
    /// <externalLink>
    ///     <linkText>Using Codes in resources</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/terminologies.html
    ///     </linkUri>
    /// </externalLink>
    /// <externalLink>
    ///     <linkText>Coding</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/datatypes.html#Coding
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <example>
    /// <code language="xml" title="Coding Example">
    /// <![CDATA[
    /// <[name] xmlns="http://hl7.org/fhir">
    ///     <!-- from Element: extension -->
    ///     <system value="[uri]"/><!-- 0..1 Identity of the terminology system -->
    ///     <version value="[string]"/><!-- 0..1 Version of the system - if relevant -->
    ///     <code value="[code]"/><!-- 0..1 Symbol in syntax defined by the system -->
    ///     <display value="[string]"/><!-- 0..1 Representation defined by the system -->
    ///     <primary value="[boolean]"/><!-- 0..1 If this code was chosen directly by the user -->
    /// </[name]>
    /// ]]>
    /// </code>
    /// </example>
    /// <remarks>
    /// <alert class="caution">
    /// <para>
    /// If a valueSet is provided, a system URI SHALL also be provided.
    /// </para>
    /// </alert>
    /// </remarks>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public class Coding : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Coding"/> class.
        /// </summary>
        /// <param name="system">
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code
        /// </param>
        /// <param name="code">A symbol in syntax defined by the system</param>
        /// <param name="display">
        /// A representation of the meaning of the code in the system, following the rules 
        /// of the system.
        /// </param>
        public Coding(Uri system, Code code, string display)
            : this(system, null, code, display)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coding"/> class.
        /// </summary>
        /// <param name="system">
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code
        /// </param>
        /// <param name="version">
        /// The version of the code system which was used when choosing this code
        /// </param>
        /// <param name="code">A symbol in syntax defined by the system</param>
        /// <param name="display">
        /// A representation of the meaning of the code in the system, following the rules 
        /// of the system.
        /// </param>
        /// <param name="primary">
        /// Indicates that this code was chosen by a user directly - i.e. off a pick list of 
        /// available items (codes or displays)
        /// </param>
        public Coding(Uri system, string version, Code code, string display, bool? primary = null)
        {
            this.System = system;
            this.Version = version;
            this.Code = code;
            this.Display = display;
            this.Primary = primary;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Coding" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private Coding()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code.
        /// <externalLink>
        ///     <linkText>Coding.System</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Coding.system
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public Uri System
        {
            get;
            private set;
        }

        /// <summary>
        /// The version of the code system which was used when choosing this code. Note that 
        /// a well-maintained code system does not need the version reported, because the 
        /// meaning of codes is consistent across versions. However this cannot consistently 
        /// be assured. and when the meaning is not guaranteed to be consistent, the version 
        /// SHOULD be exchanged.
        /// <externalLink>
        ///     <linkText>Coding.Version</linkText>
        ///     <linkUri>http://hl7-fhir.github.io/datatypes-definitions.html#Coding.version</linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(2), JsonProperty]
        public string Version
        {
            get;
            private set;
        }

        /// <summary>
        /// A symbol in syntax defined by the system. The symbol may be a predefined code or 
        /// an expression in a syntax defined by the coding system (e.g. post-coordination).
        /// <externalLink>
        ///     <linkText>Coding.Code</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Coding.code
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(3), JsonProperty]
        public Code Code
        {
            get;
            private set;
        }

        /// <summary>
        /// A representation of the meaning of the code in the system, following the rules 
        /// of the system.
        /// <externalLink>
        ///     <linkText>Coding.Display</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Coding.display
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(4), JsonProperty]
        public string Display
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates that this code was chosen by a user directly - i.e. off a pick list of 
        /// available items (codes or displays).
        /// <externalLink>
        ///     <linkText>Coding.Primary</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Coding.primary
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(5), JsonProperty]
        public bool? Primary
        {
            get;
            private set;
        }

        #endregion
    }
}