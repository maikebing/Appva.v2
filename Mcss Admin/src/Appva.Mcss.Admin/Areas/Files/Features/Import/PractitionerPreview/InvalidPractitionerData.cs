// <copyright file="InvalidPractitionerData.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InvalidPractitionerData
    {
        #region Properties.

        /// <summary>
        /// The personal identity number.
        /// </summary>
        public string PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The email address.
        /// </summary>
        public string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// The role.
        /// </summary>
        public string Role
        {
            get;
            set;
        }

        /// <summary>
        /// The organization node.
        /// </summary>
        public string OrganizationNode
        {
            get;
            set;
        }

        /// <summary>
        /// The HSA id.
        /// </summary>
        public string HsaId
        {
            get;
            set;
        }

        /// <summary>
        /// A string of errors.
        /// </summary>
        public string Errors
        {
            get;
            set;
        }

        #endregion
    }
}