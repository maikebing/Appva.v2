// <copyright file="UpdateRoleSchedule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateRoleRole : IRequest<bool>
    {
        [Required]
        public Guid Id
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Roller får ej vara tom.")]
        public IList<Tickable> AdminRoles
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Roller får ej vara tom.")]
        public IList<Tickable> UserRoles
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the role, for displaying
        /// </summary>
        public string RoleName
        {
            get;
            set;
        }
    }
}