// <copyright file="EditDeviceModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EditDeviceModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The device guid.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The device name.
        /// </summary>
        [Required(ErrorMessage = "Enhetsbeskrivningen måste fyllas i.")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Organization nodes.
        /// </summary>
        public IEnumerable<SelectListItem> Organizations
        {
            get;
            set;
        }

        /// <summary>
        /// Selected organization node.
        /// </summary>
        public Guid SelectedOrganizationId
        {
            get;
            set;
        }

        public Guid TaxonId { get; set; }

        #endregion
    }
}