// <copyright file="UpdateOrganizationModel.cs" company="Appva AB">
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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateOrganizationalUnitModel : IRequest<bool>
    {
        #region Properties

        /// <summary>
        /// The taxon Id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name
        /// </summary>
        /// 
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
        [DisplayName("Vikt")]
        public int Weight
        {
            get;
            set;
        }

        #endregion
    }
}