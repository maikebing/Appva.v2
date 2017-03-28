// <copyright file="SearchableScope.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    /// <summary>
    /// The searchable scope for a <see cref="Person"/>.
    /// </summary>
    internal sealed class SearchableScope : Primitive
    {
        #region Variabels.

        /// <summary>
        /// Searches within the given name and family name.
        /// </summary>
        public static readonly SearchableScope GivenAndFamily = New("givenandfamily");

        /// <summary>
        /// Searches within the given name and family name or HSA identity.
        /// </summary>
        public static readonly SearchableScope GivenAndFamilyOrHsaId = New("givenandfamilyorhsaid");

        /// <summary>
        /// Searches within the personal identity number.
        /// </summary>
        public static readonly SearchableScope PersonalIdentityNumber = New("personalidentitynumber");

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchableScope"/> class.
        /// </summary>
        /// <param name="value">The scope string.</param>
        private SearchableScope(string value)
            : base (value)
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="SearchableScope"/> class.
        /// </summary>
        /// <param name="value">The scope string.</param>
        /// <returns>A new <see cref="SearchableScope"/> instance.</returns>
        public static SearchableScope New(string value)
        {
            return new SearchableScope(value);
        }

        #endregion
    }
}