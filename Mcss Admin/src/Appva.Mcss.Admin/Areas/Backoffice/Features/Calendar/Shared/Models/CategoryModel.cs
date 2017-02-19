// <copyright file="CategoryModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Calendar.Shared.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Areas.Backoffice.Features.Schedule.Shared.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CategoryModel
    {
        #region Const.

        /// <summary>
        /// The available colors
        /// </summary>
        Dictionary<string, string> Colors = new Dictionary<string, string> {
            {"#5d5d5d", "Blå"},
            {"#1ed288", "Blå"},
            {"#e28a00", "Blå"},
            {"#b668ca", "Blå"},
            {"#47597f", "Blå"},
            {"#349d00", "Blå"},
            {"#e9d600", "Blå"},
            {"#0091ce", "Blå"}
        };

        #endregion

        #region Properties.

        /// <summary>
        /// The name for the Category
        /// </summary>
        [Required]
        [DisplayName("Namn")]
        public string Name
        {
            get;
            set;
        }

        public string AlternativeName
        {
            get;
            set;
        }

        /// <summary>
        /// Deviation-dialog on/off
        /// </summary>
        [DisplayName("Kontrollruta vid avvikande signering")]
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