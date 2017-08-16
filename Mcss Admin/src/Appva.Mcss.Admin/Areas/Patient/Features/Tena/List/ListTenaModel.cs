// <copyright file="ListTenaModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion


    /// <summary>
    /// TODO: 
    /// </summary>

    public class ListTenaModel
    {
        /// <summary>
        /// Patient
        /// </summary>
        public PatientViewModel patientViewModel { get; set; }
        public Patient patient { get; set; }
        public string ExternalId { get; set; }
        public bool isInstalled { get; set; }
        public string Message { get; set; }
    }
}