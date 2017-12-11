// <copyright file="HumanName.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Appva.Core;

    #endregion

    /// <summary>
    /// Names may be changed or repudiated. People may have different names in different 
    /// contexts. Names may be divided into parts of different type that have variable 
    /// significance depending on context, though the division into parts is not always 
    /// significant. With personal names, the different parts may or may not be imbued 
    /// with some implicit meaning; various cultures associate different importance with 
    /// the name parts and the degree to which systems SHALL care about name parts 
    /// around the world varies widely.
    /// </summary>
    public class HumanName : ValueObject<HumanName>, IValidator
    {
        #region Variables.

        /// <summary>
        /// Given names (not always 'first'). Includes middle names.
        /// </summary>
        private readonly string given;

        /// <summary>
        /// Family name (often called 'Surname').
        /// </summary>
        private readonly string family;

        /// <summary>
        /// Text representation of the full name.
        /// </summary>
        private readonly string text;

        /// <summary>
        /// Regular expression which targets double white spaces.
        /// </summary>
        private static readonly Regex MoreThanOneWhitespace = new Regex(@"[ ]{2,}", RegexOptions.None);

        /// <summary>
        /// Valid characters for validation of given and family name.
        /// </summary>
        private static readonly Regex ValidCharacters = new Regex(@"/^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u", RegexOptions.Compiled);
        
        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanName"/> class.
        /// </summary>
        /// <param name="given">Given names</param>
        /// <param name="family">Family name</param>
        public HumanName(string given, string family)
        {
            this.given  = this.RemoveMultipleWhiteSpace(given);
            this.family = this.RemoveMultipleWhiteSpace(family);
            this.text   = this.ToFullNameText();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanName"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected HumanName()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the given name.
        /// </summary>
        public string Given
        {
            get
            {
                return this.given;
            }
        }

        /// <summary>
        /// Returns the family name.
        /// </summary>
        public string Family
        {
            get
            {
                return this.family;
            }
        }

        /// <summary>
        /// Returns the text representation of the full name.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }
        }

        /// <summary>
        /// Returns the initials.
        /// </summary>
        public string Initials
        {
            get
            {
                return this.ToInitials();
            }
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Initializes a new instance of the <see cref="HumanName"/> class.
        /// </summary>
        /// <param name="given">Given names</param>
        /// <param name="family">Family name</param>
        /// <returns>A new <see cref="HumanName"/> instance</returns>
        public static HumanName New(string given, string family)
        {
            return new HumanName(given, family);
        }

        /// <summary>
        /// Creates a new gender based <see cref="HumanName"/>.
        /// </summary>
        /// <param name="gender">The gender</param>
        /// <returns>A new <see cref="HumanName"/> instance</returns>
        public static HumanName Random(Gender gender)
        {
            return HumanNameRandomizer.Random(gender);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToFullNameText();
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Given; 
            yield return this.Family;
        }

        #endregion

        #region IValidator Members.

        /// <inheritdoc />
        public bool IsValid()
        {
            return ValidCharacters.IsMatch(this.Text);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Removes multiple white spaces and replaces it with a single one.
        /// </summary>
        /// <param name="str">The string to be processed</param>
        /// <returns>A trimmed string</returns>
        private string RemoveMultipleWhiteSpace(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            return MoreThanOneWhitespace.Replace(str.Trim(), @" ");
        }

        /// <summary>
        /// Returns a full name text representation of the given and family names.
        /// </summary>
        /// <returns>A full name text representation</returns>
        private string ToFullNameText()
        {
            return string.Join(" ", this.given, this.family);
        }

        /// <summary>
        /// Returns the initials.
        /// </summary>
        /// <returns>The initals of the full name text representation</returns>
        private string ToInitials()
        {
            var names = this.text.Split(' ');
            return string.Join(string.Empty, names.Select(x => x[0].ToString()).ToArray());
        }

        #endregion
    }
}