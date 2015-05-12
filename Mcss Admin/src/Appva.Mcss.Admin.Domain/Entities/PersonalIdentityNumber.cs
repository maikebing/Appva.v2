// <copyright file="PersonalIdentityNumber.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;
    using NHibernate.UserTypes;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class PersonalIdentityNumber : ValueObject<PersonalIdentityNumber>
    {
        #region Fields.

        /// <summary>
        /// The value
        /// </summary>
        private string _value;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalIdentityNumber"/> class.
        /// </summary>
        public PersonalIdentityNumber()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalIdentityNumber"/> class.
        /// </summary>
        /// <param name="value">The initial value</param>
        public PersonalIdentityNumber(string value)
        {
            this._value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The value
        /// </summary>
        public string Value 
        {
            get { return _value; }
            set { this._value = value; }
        }

        #endregion

        #region Member overrides.

        public override string ToString()
        {
            return this._value.ToString();
        }

        public override bool Equals(PersonalIdentityNumber other)
        {
            var v1 = this._value.Replace("-","");
            var v2 = other.Value.Replace("-","");
            return v1.Equals(v2);
        }

        #endregion
    }
}