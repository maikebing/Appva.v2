// <copyright file="CreateNotificationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateNotificationModel : IRequest<Object>
    {
        #region Properties.

        /// <summary>
        /// The template for the notification
        /// </summary>
        [Required]
        [Display(Name="Template")]
        public string Template
        {
            get;
            set;
        }

        /// <summary>
        /// The date for publishing the notification
        /// </summary>
        [Required]
        [Display(Name = "Datum för publicering")]
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The date for unpublishing the notification
        /// </summary>
        [Display(Name="Datum för avpublicering")]
        public DateTime? EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the notification
        /// </summary>
        [Required]
        [Display(Name="Namn på notisen")]
        public string Name
        {
            get;
            set;
        }

        #endregion

        #region Lists.

        /// <summary>
        /// The available templates
        /// </summary>
        public IList<SelectListItem> Templates
        {
            get;
            set;
        }

        #endregion
    }
}