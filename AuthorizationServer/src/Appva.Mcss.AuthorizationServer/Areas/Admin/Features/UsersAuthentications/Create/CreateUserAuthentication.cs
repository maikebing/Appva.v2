// <copyright file="CreateUser.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateUserAuthentication : IRequest<CreateUserAuthenticationUserId>
    {
        /// <summary>
        /// The user id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The users' authentication method id.
        /// </summary>
        public IList<SelectListItem> AuthenticationMethods
        {
            get;
            set;
        }

        /// <summary>
        /// A password.
        /// </summary>
        [Required]
        public string Password
        {
            get;
            set;
        }

        [Required]
        public string AuthenticationMethodKey
        {
            get;
            set;
        }
    }
}