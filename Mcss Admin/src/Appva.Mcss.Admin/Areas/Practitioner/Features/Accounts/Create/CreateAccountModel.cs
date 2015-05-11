// <copyright file="CreateAccountModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Accounts.Create
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using Appva.Mcss.Admin.Features.Taxa.Filter;
    using DataAnnotationsExtensions;
    using Appva.Mvc.Html.DataAnnotations;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateAccountModel : IRequest<CreateAccountModel>
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
        [RegularExpression(@"^(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][0-9])|(3[0-1])|([6-8][0-9])|(9[0-1]))-?[0-9pPtTfF][0-9]{3}$", ErrorMessage = "Personnummer måste fyllas i med tolv siffror och bindestreck, t. ex. 19010101-0001.")]
        [DisplayName("Personnummer")]
        public string PersonalIdentityNumber
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
        /// A unique user name.
        /// </summary>
        [DisplayName("Användarnamn")]
        public string Username
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
    }
}