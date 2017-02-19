// <copyright file="UpdateCategoryModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateCategoryModel : IRequest<Identity<CategoryDetailsModel>>
    {
        #region Const.              

        /// <summary>
        /// The available colors
        /// </summary>
        public readonly List<SelectListItem> Colors = new List<SelectListItem>()
        {
            new SelectListItem { Text = Application.Common.Color.Blue.Name() , Value = Application.Common.Color.Blue.Hex() },
            new SelectListItem { Text = Application.Common.Color.DarkBlue.Name() , Value = Application.Common.Color.DarkBlue.Hex() },
            new SelectListItem { Text = Application.Common.Color.DarkGrey.Name() , Value = Application.Common.Color.DarkGrey.Hex() },
            new SelectListItem { Text = Application.Common.Color.Green.Name() , Value = Application.Common.Color.Green.Hex() },
            new SelectListItem { Text = Application.Common.Color.Orange.Name() , Value = Application.Common.Color.Orange.Hex() },
            new SelectListItem { Text = Application.Common.Color.Purple.Name() , Value = Application.Common.Color.Purple.Hex() },
            new SelectListItem { Text = Application.Common.Color.Turquoise.Name() , Value = Application.Common.Color.Turquoise.Hex() },
            new SelectListItem { Text = Application.Common.Color.Yellow.Name() , Value = Application.Common.Color.Yellow.Hex() }
        };

        #endregion

        #region Properties.

        /// <summary>
        /// The id of the category
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the category
        /// </summary>
        [Required]
        [Display(Name="Namn")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The category-color
        /// </summary
        [Required]
        [Display(Name = "Färg")]
        public string Color
        {
            get;
            set;
        }

        /// <summary>
        /// If the category should be declared as an absence-category
        /// E.g. if the patient is away during the activity and the 
        /// possibility to pause all tasks should be active
        /// </summary>
        [Display(Name="Frånvaro")]
        public bool Absence
        {
            get;
            set;
        }

        /// <summary>
        /// Deviation-dialog on/off
        /// </summary>
        [Display(Name = "Kontrollruta vid avvikande signering")]
        public bool NurseConfirmDeviation
        {
            get;
            set;
        }

        /// <summary>
        /// The popup message when task signed as a deviation
        /// </summary>
        public ConfirmDeviationMessage DeviationMessage
        {
            get;
            set;
        }

        #endregion
    }
}