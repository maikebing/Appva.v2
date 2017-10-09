// <copyright file="AddInventoryModel.cs" company="Appva AB">
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
    using System.Collections;
    using System.Web.Mvc;
    using System.Collections.Generic;

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

        [DisplayName("Användningsområde")]
        [Required]
        public string Field
        {
            get;
            set;
        }

        [DisplayName("Enhet")]
        public string Unit
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

        public IList<SelectListItem> SelectField
        {
            get;
            set;
        }

        #endregion
    }
}