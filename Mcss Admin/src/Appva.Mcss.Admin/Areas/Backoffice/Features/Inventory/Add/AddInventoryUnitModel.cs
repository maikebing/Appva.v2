﻿// <copyright file="AddInventoryModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AddInventoryUnitModel : IRequest<bool>
    {
        #region Properties

        [DisplayName("Namn")]
        [Required]
        public string Name
        {
            get;
            set;
        }

        [DisplayName("Enheter")]
        [Required]
        public string Amounts
        {
            get;
            set;
        }

        #endregion
    }
}