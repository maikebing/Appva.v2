// <copyright file="UpdateRolesCategoriesPermissions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Roles.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateRolesCategoriesPermissions : IRequest<bool>
    {
        #region Properties.

        [Required]
        public Guid Id
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Kategorilistan får ej vara tom.")]
        public IList<Tickable> Categories
        {
            get;
            set;
        }

        #endregion
    }
}