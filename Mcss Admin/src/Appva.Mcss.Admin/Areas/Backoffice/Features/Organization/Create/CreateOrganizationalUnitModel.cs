// <copyright file="CreateOrganizationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateOrganizationalUnitModel : IRequest<bool>
    {
        #region Constructor

        public CreateOrganizationalUnitModel()
        {
            Types = new List<SelectListItem>() {
                new SelectListItem() { Text = "Organisation", Value = "Organisation" },
                new SelectListItem() { Text = "Driftställe", Value = "Driftställe" },
                new SelectListItem() { Text = "Enhet", Value = "Enhet" },
                new SelectListItem() { Text = "Lägenhet", Value = "Lägenhet" }
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// The name
        /// </summary>
        [Required]
        [DisplayName("Namn")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The description
        /// </summary>
        [Required]
        [DisplayName("Beskrivning")]
        public string Description
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Typ")]
        public string Type {
            get;
            set;
        }

        [DisplayName("Rot")]
        public bool IsRoot {
            get;
            set;
        }

        [Required]
        [DisplayName("Vikt")]
        public int Weight {
            get;
            set;
        }

        public Guid ParentId {
            get;
            set;
        }

        public IEnumerable<SelectListItem> Types {
            get;
            set;
        }

        #endregion
    }
}