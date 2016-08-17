// <copyright file="UploadPractitionerModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
{
    using Admin.Models;
    using Cqrs;
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UploadPractitionerModel : IRequest<ListAccount>
    {
        #region Properties.

        /// <summary>
        /// The file
        /// </summary>
        [Required]
        [Display(Name="Välj fil")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File 
        { 
            get;
            set;
        }

        [Display(Name ="Beskrivning")]
        [DataType(DataType.Text)]
        public string Description
        {
            get;
            set;
        }

        #endregion
    }
}