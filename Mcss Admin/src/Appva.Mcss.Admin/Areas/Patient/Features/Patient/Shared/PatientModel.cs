// <copyright file="PatientModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientModel"/> class.
        /// </summary>
        public PatientModel()
        {
        }

        #endregion

        #region Properties.

        public Guid Id
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public bool HasUnattendedTask
        {
            get;
            set;
        }

        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        public bool IsDeceased
        {
            get;
            set;
        }

        public string Identifier
        {
            get;
            set;
        }

        public Taxon Taxon
        {
            get;
            set;
        }

        public string SeniorAlerts
        {
            get;
            set;
        }

        #endregion
    }
}