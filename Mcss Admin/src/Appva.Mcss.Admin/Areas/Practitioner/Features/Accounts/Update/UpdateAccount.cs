﻿// <copyright file="UpdateAccount.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateAccount : Identity<bool>
    {
        /// <summary>
        /// The account first name.
        /// </summary>
        [Required(ErrorMessage = "Förnamn måste fyllas i.")]
        [DisplayName("Förnamn")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The account last name.
        /// </summary>
        [Required(ErrorMessage = "Efternamn måste fyllas i.")]
        [DisplayName("Efternamn")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The account personal identity number.
        /// </summary>
        [Required(ErrorMessage = "Personnummer måste fyllas i.")]
        [Appva.Mvc.PersonalIdentityNumber(ErrorMessage = "Personnummer måste fyllas i med tolv siffror och bindestreck, t. ex. 19010101-0001.")]
        [DisplayName("Personnummer")]
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The E-mail address.
        /// </summary>
        [Required(ErrorMessage = "E-postadress måste fyllas i.")]
        [Email(ErrorMessage = "E-postadress måste anges i korrekt format, t. ex. namn.efternamn@foretag.se.")]
        [DisplayName("E-postadress")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// The device password.
        /// </summary>
        [RequiredIf(Target = "IsMobileDevicePasswordEditable", Value = true, ErrorMessage = "En kod måste fyllas i.")]
        [StringLength(7, ErrorMessage = "Koden får högst innehålla 7 siffror.")]
        [DataType(DataType.Password)]
        [Numeric(ErrorMessage = "Koden får högst innehålla 7 siffror.")]
        [DisplayName("Kod för signering")]
        public string DevicePassword
        {
            get;
            set;
        }

        /// <summary>
        /// A unique user name.
        /// </summary>
        [DisplayName("Användarnamn")]
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// The HSA ID.
        /// </summary>
        public string HsaId
        {
            get;
            set;
        }

        /// <summary>
        /// The account address.
        /// </summary>
        [Required(ErrorMessage = "Adress måste fyllas i.")]
        public string Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// If the account should be locked to the given taxon
        /// </summary>
        public bool RestrictUserToOrganizationTaxon
        {
            get;
            set;
        }

        /// <summary>
        /// If the user can lock current account to a taxon
        /// </summary>
        public bool RestrictUserToOrganizationTaxonIsVisible
        {
            get;
            set;
        }

        /// <summary>
        /// The address taxons.
        /// </summary>
        public IEnumerable<TaxonViewModel> Taxons
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the device password field is visible.
        /// </summary>
        public bool IsMobileDevicePasswordFieldVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the device password field is visible.
        /// </summary>
        public bool IsFirstNameFieldVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the device password field is visible.
        /// </summary>
        public bool IsLastNameFieldVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the device password field is visible.
        /// </summary>
        public bool IsMailFieldVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the device password field is editable.
        /// </summary>
        public bool IsMobileDevicePasswordEditable
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not to display the user name.
        /// </summary>
        public bool IsUsernameVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the HSA ID is visible as input.
        /// </summary>
        public bool IsHsaIdFieldVisible
        {
            get;
            set;
        }
    }
}