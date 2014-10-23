// <copyright file="CreateRole.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateRole : IRequest<Guid>
    {
        /// <summary>
        /// The role key.
        /// E.g. superuser, management_user.
        /// </summary>
        [Required]
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// The role name.
        /// E.g. "Super User".
        /// </summary>
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The role description.
        /// E.g. "A subject assigned this role may do whatever
        /// it wants."
        /// </summary>
        [Required]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// A list of permissions.
        /// </summary>
        public IList<Tickable> Permissions
        {
            get;
            set;
        }
    }
}