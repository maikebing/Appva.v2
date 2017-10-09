// <copyright file="UpdateInventoryUnitModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateInventoryUnitModel : IRequest<Parameterless<ListInventoriesModel>>
    {
        #region Properties

        /// <summary>
        /// The setting id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The Name of the unit
        /// </summary>
        [DisplayName("Namn")]
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The Field of usage
        /// </summary>
        [DisplayName("Användning")]
        [Required]
        public string Field
        {
            get;
            set;
        }

        /// <summary>
        /// The unit.
        /// </summary>
        [DisplayName("Enhet")]
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// The list of amounts as a string
        /// </summary>
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