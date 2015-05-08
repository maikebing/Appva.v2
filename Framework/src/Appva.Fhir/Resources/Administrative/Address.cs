// <copyright file="Address.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using Newtonsoft.Json;
using ProtoBuf;

    #endregion

    /// <summary>
    /// A postal address. There is a variety of postal address formats defined around 
    /// the world. Postal addresses are often also used to record a location that can be 
    /// visited to find a patient or person.
    /// <externalLink>
    ///     <linkText>1.18.0.13 Address</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/datatypes.html#Address
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class Address : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="use">
        /// The purpose of this address
        /// </param>
        /// <param name="lines">
        /// This component contains the house number, apartment number, street name, street 
        /// direction, P.O. Box number, delivery hints, and similar address information.
        /// </param>
        /// <param name="city">
        /// The name of the city, town, village or other community or delivery center
        /// </param>
        /// <param name="state">
        /// Sub-unit of a country with limited sovereignty in a federally organized country. 
        /// A code may be used if codes are in common use (i.e. US 2 letter state codes)
        /// </param>
        /// <param name="postalCode">
        /// A postal code designating a region defined by the postal service
        /// </param>
        /// <param name="country">
        /// Country - a nation as commonly understood or generally accepted
        /// </param>
        /// <param name="period">
        /// Time period when address was/is in use
        /// </param>
        public Address(AddressUse use, string[] lines, string city, string state, string postalCode, string country, Period period)
        {
            this.Use = use;
            this.Lines = lines;
            this.City = city;
            this.State = state;
            this.PostalCode = postalCode;
            this.Country = country;
            this.Period = period;
            this.Text = this.CreateTextRepresentationOfAddress();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Address" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private Address()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The purpose of this address.
        /// <externalLink>
        ///     <linkText>Use</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Address.use
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public AddressUse Use
        {
            get;
            private set;
        }

        /// <summary>
        /// A full text representation of the address.
        /// <externalLink>
        ///     <linkText>Text</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Address.text
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(2), JsonProperty]
        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// This component contains the house number, apartment number, street name, street 
        /// direction, P.O. Box number, delivery hints, and similar address information.
        /// <externalLink>
        ///     <linkText>Line</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Address.line
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(3), JsonProperty]
        public string[] Lines
        {
            get;
            private set;
        }

        /// <summary>
        /// The name of the city, town, village or other community or delivery center.
        /// <externalLink>
        ///     <linkText>City</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Address.city
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(4), JsonProperty]
        public string City
        {
            get;
            private set;
        }

        /// <summary>
        /// Sub-unit of a country with limited sovereignty in a federally organized country. 
        /// A code may be used if codes are in common use (i.e. US 2 letter state codes).
        /// <externalLink>
        ///     <linkText>State</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Address.state
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(5), JsonProperty]
        public string State
        {
            get;
            private set;
        }

        /// <summary>
        /// A postal code designating a region defined by the postal service.
        /// <externalLink>
        ///     <linkText>PostalCode</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Address.postalCode
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(6), JsonProperty]
        public string PostalCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Country - a nation as commonly understood or generally accepted.
        /// <externalLink>
        ///     <linkText>Country</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Address.country
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// ISO 3166 3 letter codes can be used in place of a full country name
        /// </remarks>
        [ProtoMember(7), JsonProperty]
        public string Country
        {
            get;
            private set;
        }

        /// <summary>
        /// Time period when address was/is in use.
        /// <externalLink>
        ///     <linkText>Period</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Address.period
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Allows addresses to be placed in historical context.
        /// </remarks>
        [ProtoMember(8), JsonProperty]
        public Period Period
        {
            get;
            private set;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Returns a full text representation of the address.
        /// </summary>
        /// <returns>A full text representation of the address</returns>
        private string CreateTextRepresentationOfAddress()
        {
            return string.Concat(this.Lines, this.City, this.State, this.PostalCode, this.Country);
        }

        #endregion
    }
}