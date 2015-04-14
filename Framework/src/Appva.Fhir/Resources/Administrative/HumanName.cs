﻿// <copyright file="HumanName.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using Appva.Fhir.Primitives;

    #endregion

    /// <summary>
    /// A name of a human with text, parts and usage information.
    /// <para>
    /// Names may be changed or repudiated. People may have different names in different 
    /// contexts. Names may be divided into parts of different type that have variable 
    /// significance depending on context, though the division into parts is not always 
    /// significant. With personal names, the different parts may or may not be imbued 
    /// with some implicit meaning; various cultures associate different importance with 
    /// the name parts and the degree to which systems SHALL care about name parts 
    /// around the world varies widely.
    /// </para>
    /// <para>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Name</term>
    ///         <term>Example</term>
    ///         <term>Destination / Comments</term>
    ///     </listheader>
    ///     <item>
    ///         <term>Surname</term>
    ///         <term>Smith</term>
    ///         <term>Family Name</term>
    ///     </item>
    ///     <item>
    ///         <term>First name</term>
    ///         <term>John</term>
    ///         <term>Given Name</term>
    ///     </item>
    ///     <item>
    ///         <term>Title</term>
    ///         <term>Mr</term>
    ///         <term>Prefix</term>
    ///     </item>
    ///     <item>
    ///         <term>Middle Name</term>
    ///         <term>Samuel</term>
    ///         <term>Subsequent Given Names</term>
    ///     </item>
    ///     <item>
    ///         <term>Patronymic</term>
    ///         <term>bin Osman</term>
    ///         <term>Family Name</term>
    ///     </item>
    ///     <item>
    ///         <term>Initials</term>
    ///         <term>Q.</term>
    ///         <term>Given Name as initial ("." recommended)</term>
    ///     </item>
    ///     <item>
    ///         <term>Nick Name</term>
    ///         <term>Jock</term>
    ///         <term>Given name, with Use = common</term>
    ///     </item>
    ///     <item>
    ///         <term>Qualifications</term>
    ///         <term>PhD</term>
    ///         <term>Suffix</term>
    ///     </item>
    ///     <item>
    ///         <term>Honorifics</term>
    ///         <term>Senior</term>
    ///         <term>Suffix</term>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>
    /// For further information, including all W3C International Examples, consult the 
    /// examples.
    /// The text element specifies the entire name as it should be represented. This may 
    /// be provided instead of or as well as specific parts. The parts of a name SHOULD 
    /// NOT contain whitespace. For family name, hyphenated names such as "Smith-Jones" 
    /// are a single name, but names with spaces such as "Smith Jones" are broken into 
    /// multiple parts. For given names, initials may be used in place of the full name 
    /// if that is all that is recorded. The order of the parts within a given part type 
    /// has significance and SHALL be observed. The appropriate order between family 
    /// name and given names depends on culture and context of use. Systems that operate 
    /// across cultures should generally rely on the text form for presentation, and use 
    /// the parts for index/search functionality.
    /// </para>
    /// <para>
    /// Applications updating a name SHALL ensure either that the text and the parts are 
    /// in agreement, or that only one of the two is present. Systems that do not support 
    /// as many name parts as are provided in an instance may wish to append some of the 
    /// parts together using spaces.
    /// </para>
    /// <externalLink>
    ///     <linkText>1.14.0.12 HumanName</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes.html#HumanName
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class HumanName : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanName"/> class.
        /// </summary>
        /// <param name="given">Given name</param>
        /// <param name="family">The part of a name that links to the genealogy</param>
        public HumanName(string given, string family)
            : this(null, null, given, family, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanName"/> class.
        /// </summary>
        /// <param name="use">Identifies the purpose for this name</param>
        /// <param name="given">Given name</param>
        /// <param name="family">The part of a name that links to the genealogy</param>
        public HumanName(NameUse use, string given, string family)
            : this(use, null, given, family, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanName"/> class.
        /// </summary>
        /// <param name="use">Identifies the purpose for this name</param>
        /// <param name="prefix">
        /// Part of the name that is acquired as a title due to academic, legal, employment 
        /// or nobility status, etc. and that appears at the start of the name
        /// </param>
        /// <param name="given">Given name</param>
        /// <param name="family">The part of a name that links to the genealogy</param>
        /// <param name="suffix">
        /// Part of the name that is acquired as a title due to academic, legal, employment 
        /// or nobility status, etc. and that appears at the end of the name.
        /// </param>
        public HumanName(NameUse use, string prefix, string given, string family, string suffix)
        {
            this.Use = use;
            this.Prefix = prefix;
            this.Given = given;
            this.Family = family;
            this.Suffix = suffix;
            this.Text = this.ConcatenateHumanName();
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Identifies the purpose for this name.
        /// <externalLink>
        ///     <linkText>Use</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#HumanName.use
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// This is labeled as "Is Modifier" because applications should not mistake a 
        /// temporary or old name etc for a current/permanent one. Applications can assume 
        /// that a name is current unless it explicitly says that it is temporary or old.
        /// </remarks>
        public NameUse Use
        {
            get;
            private set;
        }

        /// <summary>
        /// A full text representation of the name.
        /// <externalLink>
        ///     <linkText>Text</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#HumanName.text
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Can provide both a text representation and structured parts.
        /// </remarks>
        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// The part of a name that links to the genealogy. In some cultures (e.g. Eritrea) 
        /// the family name of a son is the first name of his father.
        /// <externalLink>
        ///     <linkText>Family</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#HumanName.family
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// For family name, hyphenated names such as "Smith-Jones" are a single name, but 
        /// names with spaces such as "Smith Jones" are broken into multiple parts.
        /// </remarks>
        public string Family
        {
            get;
            private set;
        }

        /// <summary>
        /// Given name.
        /// <externalLink>
        ///     <linkText>Given</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#HumanName.given
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// If only initials are recorded, they may be used in place of the full name. Not 
        /// called "first name" since given names do not always come first.
        /// </remarks>
        public string Given
        {
            get;
            private set;
        }

        /// <summary>
        /// Part of the name that is acquired as a title due to academic, legal, employment 
        /// or nobility status, etc. and that appears at the start of the name.
        /// <externalLink>
        ///     <linkText>Prefix</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#HumanName.prefix
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        public string Prefix
        {
            get;
            private set;
        }

        /// <summary>
        /// Part of the name that is acquired as a title due to academic, legal, employment 
        /// or nobility status, etc. and that appears at the end of the name.
        /// <externalLink>
        ///     <linkText>Suffix</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#HumanName.suffix
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        public string Suffix
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates the period of time when this name was valid for the named person.
        /// <externalLink>
        ///     <linkText>Period</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#HumanName.period
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Allows names to be placed in historical context.
        /// </remarks>
        public Period Period
        {
            get;
            private set;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Concatenates the full human name as text.
        /// </summary>
        /// <returns>A text representation of the human name</returns>
        private string ConcatenateHumanName()
        {
            return string.Concat(this.Prefix, this.Given, this.Family, this.Suffix);
        }

        #endregion
    }
}