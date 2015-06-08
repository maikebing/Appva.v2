// <copyright file="NameUse.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// The use of a human name.
    /// <externalLink>
    ///     <linkText>1.15.2.1.15.1 NameUse</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/name-use.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class NameUse : Code
    {
        #region Variables.

        /// <summary>
        /// Known as/conventional/the one you normally use.
        /// </summary>
        public static readonly NameUse Usual = new NameUse("usual");

        /// <summary>
        /// The formal name as registered in an official (government) registry, but which 
        /// name might not be commonly used. May be called "legal name".
        /// </summary>
        public static readonly NameUse Official = new NameUse("official");

        /// <summary>
        /// A temporary name. Name.period can provide more detailed information. This may 
        /// also be used for temporary names assigned at birth or in emergency situations.
        /// </summary>
        public static readonly NameUse Temp = new NameUse("temp");

        /// <summary>
        /// A name that is used to address the person in an informal manner, but is not part 
        /// of their formal or usual name.
        /// </summary>
        public static readonly NameUse Nickname = new NameUse("nickname");

        /// <summary>
        /// Anonymous assigned name, alias, or pseudonym (used to protect a person's 
        /// identity for privacy reasons).
        /// </summary>
        public static readonly NameUse Anonymous = new NameUse("anonymous");

        /// <summary>
        /// This name is no longer in use (or was never correct, but retained for records).
        /// </summary>
        public static readonly NameUse Old = new NameUse("old");

        /// <summary>
        /// A name used prior to marriage. Marriage naming customs vary greatly around the 
        /// world. This name use is for use by applications that collect and store "maiden" 
        /// names. Though the concept of maiden name is often gender specific, the use of 
        /// this term is not gender specific. The use of this term does not imply any 
        /// particular history for a person's name, nor should the maiden name be determined 
        /// algorithmically.
        /// </summary>
        public static readonly NameUse Maiden = new NameUse("maiden");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NameUse"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private NameUse(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="NameUse" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private NameUse() 
            : base(null)
        {
        }

        #endregion
    }
}