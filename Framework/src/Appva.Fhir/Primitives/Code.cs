// <copyright file="Code.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Primitives
{
    #region Imports.

    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using ProtoBuf;
    using Resources;
    using Resources.Administrative;
    using Resources.Security.Extensions;
    using Resources.Security.ValueSets;

    #endregion

    /// <summary>
    /// Indicates that the value is taken from a set of controlled strings defined 
    /// elsewhere (see Using codes for further discussion). Technically, a code is 
    /// restricted to string which has at least one character and no leading or trailing 
    /// whitespace, and where there is no whitespace other than single spaces in the 
    /// contents regex: <c>[^\s]+([\s]+[^\s]+)*</c>
    /// <externalLink>
    ///     <linkText>FHIR Code</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/datatypes.html#code
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    [ProtoInclude(2000, typeof(AddressUse))]
    [ProtoInclude(2001, typeof(AdministrativeGender))]
    [ProtoInclude(2002, typeof(ContactPointSystem))]
    [ProtoInclude(2003, typeof(ContactPointUse))]
    [ProtoInclude(2004, typeof(IdentifierUse))]
    [ProtoInclude(2005, typeof(NameUse))]
    [ProtoInclude(2006, typeof(Language))]
    [ProtoInclude(2007, typeof(AuditEventAction))]
    [ProtoInclude(2008, typeof(AuditEventObjectLifecycle))]
    [ProtoInclude(2009, typeof(AuditEventObjectRole))]
    [ProtoInclude(2010, typeof(AuditEventObjectType))]
    [ProtoInclude(2011, typeof(AuditEventOutcome))]
    [ProtoInclude(2012, typeof(AuditEventParticipantNetworkType))]
    [ProtoInclude(2013, typeof(AuditEventSourceType))]
    [ProtoInclude(2014, typeof(AuditEventPurpose))]
    public class Code : Primitive
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Code"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        public Code(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Code" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private Code() 
            : base(null)
        {
        }

        #endregion

        #region Primitive Overrides.

        /// <inheritdoc />
        public override bool IsValid()
        {
            return Regex.IsMatch(this.Value, @"^[^\s]+([\s]+[^\s]+)*$", RegexOptions.Singleline);
        }

        #endregion
    }
}