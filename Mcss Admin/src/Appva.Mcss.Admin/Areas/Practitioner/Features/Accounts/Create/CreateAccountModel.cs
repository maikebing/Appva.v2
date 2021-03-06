﻿// <copyright file="CreateAccountModel.cs" company="Appva AB">
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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using DataAnnotationsExtensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateAccountModel : IRequest<bool>
    {
        /// <summary>
        /// The account ID.
        /// </summary>
        public Guid? AccountId
        {
            get;
            set;
        }

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
        [RequiredIf(Target = "IsMobileDevicePasswordFieldVisible", Value = true, ErrorMessage = "En kod måste fyllas i.")]
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
        /// The role name, e.g. admin, nurse, etc.
        /// </summary>
        [Required(ErrorMessage = "En titel måste väljas.")]
        [DisplayName("Titel")]
        public String TitleRole
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
        [Required(ErrorMessage = "En adress måste väljas")]
        public string Taxon
        {
            get;
            set;
        }

        public bool UseTaxonAsRootAddress
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
        /// A set of roles/titles to choose from.
        /// </summary>
        public IEnumerable<SelectListItem> Titles
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