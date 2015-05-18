// <copyright file="CreateRole.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Roles.Update
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateRole : IRequest<bool>
    {
        [Required]
        public Guid Id
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Rollnamn måste fyllas i.")]
        public string Name
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Beskrivning måste fyllas i.")]
        public string Description
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Behörigheter får ej vara tom.")]
        public IList<Tickable> Permissions
        {
            get;
            set;
        }
    }
}