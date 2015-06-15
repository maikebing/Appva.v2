// <copyright file="CreateUser.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateUser : IRequest<DetailsUserId>
    {
        /// <summary>
        /// A unique identitier for a user.
        /// E.g. for swedish citizens: personnummer.
        /// </summary>
        [Required]
        public string PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The users' first name.
        /// E.g. "John".
        /// </summary>
        [Required]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The users' last name.
        /// E.g. "Doe".
        /// </summary>
        [Required]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The users' e-mail address.
        /// E.g. "example@example.com".
        /// </summary>
        [Required]
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

        /// <summary>
        /// The user's password.
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// The users' last name.
        /// E.g. "Doe".
        /// </summary>
        public IList<Tickable> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// The users' tenants.
        /// </summary>
        public IList<Tickable> Tenants
        {
            get;
            set;
        }

        /// <summary>
        /// The users' profile image.
        /// </summary>
        public HttpPostedFileBase ProfileImage
        {
            get;
            set;
        }
    }
}