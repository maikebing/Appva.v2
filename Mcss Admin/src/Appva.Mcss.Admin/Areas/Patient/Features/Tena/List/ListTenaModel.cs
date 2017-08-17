// <copyright file="ListTenaModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region imports

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: 
    /// </summary>
    public sealed class ListTenaModel
    {
        /// <summary>
        /// Patient
        /// </summary>
        public PatientViewModel PatientViewModel
        {
            get;
            set;
        }

        /// <summary>
        /// Patient
        /// </summary>
        public Patient Patient
        {
            get;
            set;
        }

        /// <summary>
        /// ExternalId
        /// </summary>
        public string ExternalId
        {
            get;
            set;
        }

        /// <summary>
        /// IsInstalled
        /// </summary>
        public bool IsInstalled
        {
            get;
            set;
        }

        /// <summary>
        /// Message
        /// </summary>
        public string Message
        {
            get;
            set;
        }
    }
}