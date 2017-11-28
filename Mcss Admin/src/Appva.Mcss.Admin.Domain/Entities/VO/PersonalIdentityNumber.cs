// <copyright file="PersonalIdentityNumber.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Core;

    #endregion

    /// <summary>
    /// Represents a national identification number, national identity number, or 
    /// national insurance number which is used by the governments of many countries as 
    /// a means of tracking their citizens, permanent residents, and temporary residents 
    /// for the purposes of work, taxation, government benefits, health care, and other 
    /// governmentally-related functions.
    /// </summary>
    public class PersonalIdentityNumber : Primitive, IPersonalIdentityNumber
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalIdentityNumber"/> class.
        /// </summary>
        /// <param name="value">The initial value</param>
        public PersonalIdentityNumber(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalIdentityNumber"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// <remarks>
        protected PersonalIdentityNumber()
        {
        }

        #endregion       

        #region IValidator Members.

        /// <inheritdoc />
        public bool IsValid()
        {
            return SwedishPersonalIdentityNumberValidator.IsValid(this.Value);
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="PersonalIdentityNumber"/> class.
        /// </summary>
        /// <param name="value">The initial value</param>
        /// <returns>A new <see cref="PersonalIdentityNumber"/> instance</returns>
        public static PersonalIdentityNumber New(string value)
        {
            return new PersonalIdentityNumber(value);
        }

        /// <summary>
        /// Creates a random <see cref="PersonalIdentityNumber"/> by gender.
        /// </summary>
        /// <param name="gender">The gender</param>
        /// <returns>A new <see cref="PersonalIdentityNumber"/> instance</returns>
        public static PersonalIdentityNumber Random(Gender gender)
        {
            return PersonalIdentityNumberRandomizer.Random(gender);
        }

        #endregion
    }
}