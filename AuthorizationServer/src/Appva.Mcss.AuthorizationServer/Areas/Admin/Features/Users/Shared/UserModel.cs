// <copyright file="UserModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class UserModel : Identity
    {
        /// <summary>
        /// Whether the user is active or not.
        /// </summary>
        public bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// The status of the user.
        /// E.g. verified, pending, banned.
        /// </summary>
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// A unique identitier for a user.
        /// E.g. for swedish citizens: personnummer.
        /// </summary>
        public string PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The users' first name.
        /// E.g. "John".
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The users' last name.
        /// E.g. "Doe".
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The users' e-mail address.
        /// E.g. "example@example.com".
        /// </summary>
        public string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// The users' mobile phone number.
        /// E.g. "+4670000000".
        /// </summary>
        public string MobilePhoneNumber
        {
            get;
            set;
        }
    }
}