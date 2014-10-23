// <copyright file="CreatePermission.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class CreatePermission : IRequest<Guid>
    {
        /// <summary>
        /// The permission policy key. 
        /// E.g. users_create, users_update or users_delete.
        /// </summary>
        [Required]
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// The permission policy name.
        /// E.g. "Create Users"
        /// </summary>
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The permission policy description.
        /// E.g. "A user assigned this permission can
        /// create users".
        /// </summary>
        [Required]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The permission policy context.
        /// E.g. "User Management".
        /// </summary>
        public string Context
        {
            get;
            set;
        }
    }
}