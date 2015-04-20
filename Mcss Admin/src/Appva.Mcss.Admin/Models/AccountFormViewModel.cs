// <copyright file="AccountFormViewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Web.ViewModels
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Appva.Mvc.Html.DataAnnotations;
    using DataAnnotationsExtensions;

    #endregion

    /// <summary>
    /// The account form model.
    /// </summary>
    public class AccountFormViewModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountFormViewModel"/> class.
        /// </summary>
        public AccountFormViewModel()
        {
            Titles = new List<SelectListItem>();
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The account id.
        /// </summary>
        public Guid? AccountId
        {
            get;
            set;
        }

        /// <summary>
        /// The first name.
        /// </summary>
        [Required(ErrorMessage = "Förnamn måste fyllas i.")]
        [DisplayName("Förnamn")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The last name.
        /// </summary>
        [Required(ErrorMessage = "Efternamn måste fyllas i.")]
        [DisplayName("Efternamn")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The personal/national identity number.
        /// </summary>
        [Required(ErrorMessage = "Personnummer måste fyllas i.")]
        [RegularExpression(@"^(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][0-9])|(3[0-1])|([6-8][0-9])|(9[0-1]))-?[0-9pPtTfF][0-9]{3}$", ErrorMessage = "Personnummer måste fyllas i med tolv siffror och bindestreck, t. ex. 19010101-0001.")]
        [PlaceHolder("T.ex. 19010101-0001")]
        [DisplayName("Personnummer")]
        public string UniqueIdentifier
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
        [RequiredIf(Target = "PasswordFieldIsVisible", Value = true, ErrorMessage = "En kod måste fyllas i.")]
        [StringLength(7, ErrorMessage = "Koden får högst innehålla 7 siffror.")]
        [DataType(DataType.Password)]
        [Numeric(ErrorMessage = "Koden får högst innehålla 7 siffror.")]
        [DisplayName("Kod för signering")]
        public string Password
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
        /// The organizational taxon - the node which the account 
        /// works at.
        /// </summary>
        public string Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the account is aministrative staff or not.
        /// </summary>
        public bool IsAdminStaff
        {
            get;
            set;
        }

        /// <summary>
        /// A set of taxons to choose from.
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
        public bool PasswordFieldIsVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the device password field is editable.
        /// </summary>
        public bool EditableClientPassword
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not to display the user name.
        /// </summary>
        public bool DisplayUsername
        {
            get;
            set;
        }

        #endregion
    }
}