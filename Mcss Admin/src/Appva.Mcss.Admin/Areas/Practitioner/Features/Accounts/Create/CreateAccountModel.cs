// <copyright file="CreateAccountModel.cs" company="Appva AB">
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
        [Required(ErrorMessageResourceName = "Förnamn_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Förnamn", ResourceType = typeof(Resources.Language))]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The account last name.
        /// </summary>
        [Required(ErrorMessageResourceName = "Efternamn_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Efternamn", ResourceType = typeof(Resources.Language))]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The account personal identity number.
        /// </summary>
        [Required(ErrorMessageResourceName = "Personnummer_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [Appva.Mvc.PersonalIdentityNumber(ErrorMessageResourceName = "Personnummer_måste_fyllas_i_med_tolv_siffror_och_bindestreck__t__ex__19010101_0001", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Personnummer", ResourceType = typeof(Resources.Language))]
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The E-mail address.
        /// </summary>
        [Required(ErrorMessageResourceName = "E_postadress_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [Email(ErrorMessage = "E-postadress måste anges i korrekt format, t. ex. namn.efternamn@foretag.se.")]
        [Display(Name = "E_postadress", ResourceType = typeof(Resources.Language))]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// The device password.
        /// </summary>
        [RequiredIf(Target = "IsMobileDevicePasswordFieldVisible", Value = true, ErrorMessageResourceName = "En_kod_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [StringLength(7, ErrorMessageResourceName = "Koden_får_högst_innehålla_7_siffror", ErrorMessageResourceType = typeof(Resources.Language))]
        [DataType(DataType.Password)]
        [Numeric(ErrorMessageResourceName = "Koden_får_högst_innehålla_7_siffror", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Kod_för_signering", ResourceType = typeof(Resources.Language))]
        public string DevicePassword
        {
            get;
            set;
        }

        /// <summary>
        /// The role name, e.g. admin, nurse, etc.
        /// </summary>
        [Required(ErrorMessage = "En_titel_måste_väljas", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Titel", ResourceType = typeof(Resources.Language))]
        public String TitleRole
        {
            get;
            set;
        }

        /// <summary>
        /// A unique user name.
        /// </summary>
        [Display(Name = "Användarnamn", ResourceType = typeof(Resources.Language))]
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
        public string Taxon
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