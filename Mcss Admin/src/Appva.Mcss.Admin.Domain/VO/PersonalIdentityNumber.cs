// <copyright file="PersonalIdentityNumber.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;
    using Appva.Core;

    #endregion

    /// <summary>
    /// Represents a national identification number, national identity number, or 
    /// national insurance number which is used by the governments of many countries as 
    /// a means of tracking their citizens, permanent residents, and temporary residents 
    /// for the purposes of work, taxation, government benefits, health care, and other 
    /// governmentally-related functions.
    /// </summary>
    public class PersonalIdentityNumber : ValueObject<PersonalIdentityNumber>, IPersonalIdentityNumber
    {
        #region Variables.

        /// <summary>
        /// The underlying value.
        /// </summary>
        private readonly string value;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalIdentityNumber"/> class.
        /// </summary>
        /// <param name="value">The initial value</param>
        public PersonalIdentityNumber(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalIdentityNumber"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate</remarks>
        protected PersonalIdentityNumber()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The value
        /// </summary>
        public string Value
        {
            get
            {
                return value;
            }
        }

        #endregion

        #region ValueObject overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value;
        }

        /// <inheritdoc />
        public override bool Equals(PersonalIdentityNumber other)
        {
            if (this == null || other == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.Value) || string.IsNullOrWhiteSpace(other.Value))
            {
                return false;
            }
            return this.Value.Replace("-", "").Equals(other.Value.Replace("-", ""));
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        #endregion

        #region IValidator Members

        /// <inheritdoc />
        public bool IsValid()
        {
            return SwedishPersonalIdentityNumberValidator.IsValid(this.Value);
        }

        #endregion
    }
}