// <copyright file="PatientModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientModel
    {
        #region Properties

        /// <summary>
        /// The id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// If patient is activt
        /// </summary>
        public bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// If patient has unattended tasks
        /// </summary>
        public bool HasUnattendedTasks
        {
            get;
            set;
        }

        /// <summary>
        /// The patient last name
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The patient first name
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Patient fullname
        /// </summary>
        public string FullName
        {
            get;
            set;
        }

        /// <summary>
        /// The patient social securitynumber
        /// </summary>
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        public Taxon Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// First line contact
        /// TODO: implement this
        /// </summary>
        public string FirstLineContact
        {
            get;
            set;
        }

        /// <summary>
        /// Overseeing account
        /// TODO: implement this
        /// </summary>
        public string Overseeing
        {
            get;
            set;
        }

        /// <summary>
        /// Superior account
        /// TODO: implemnet this
        /// </summary>
        public string Superior
        {
            get;
            set;
        }

        /// <summary>
        /// The patientes profile assesments
        /// </summary>
        public string ProfileAssements
        {
            get;
            set;
        }

        /// <summary>
        /// If deceased
        /// </summary>
        public bool IsDeceased
        {
            get;
            set;
        }

        /// <summary>
        /// The identifier
        /// </summary>
        public string Identifier
        {
            get;
            set;
        }

        #endregion
    }
}