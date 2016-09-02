// <copyright file="CreateOrUpdatePatient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// <remarks>
//      Areas/Patient/Features/Patient/Shared/
// </remarks>
// ReSharper disable CheckNamespace
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateOrUpdatePatient
    {
        /// <summary>
        /// The patient first name.
        /// </summary>
        [Required(ErrorMessage = "Förnamn måste fyllas i.")]
        [DisplayName("Förnamn")]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The patient last name.
        /// </summary>
        [Required(ErrorMessage = "Efternamn måste fyllas i.")]
        [DisplayName("Efternamn")]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The patient personal identity number.
        /// </summary>
        [Required(ErrorMessage = "Personnummer måste fyllas i.")]
        [Appva.Mvc.PersonalIdentityNumber(ErrorMessage = "Personnummer måste fyllas i med tolv siffror och bindestreck, t. ex. 19010101-0001.")]
        [DisplayName("Personnummer")]
        [Remote("VerifyUniquePatient", "Patient", AreaReference.UseCurrent, HttpMethod = "POST", ErrorMessage = "Personnumret finns redan tidigare redan i MCSS.")]
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The patient status.
        /// </summary>
        [DisplayName("Avliden")]
        public bool IsDeceased
        {
            get;
            set;
        }

        /// <summary>
        /// The patient address.
        /// </summary>
        [Required(ErrorMessage = "Address måste väljas.")]
        [Remote("VerifyTaxon", "Taxa", AreaReference.UseRoot, HttpMethod = "POST", ErrorMessage = "Address måste väljas.")]
        public string Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// The patient alternative identity.
        /// </summary>
        [DisplayName("Tagg")]
        public string Tag
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
        /// The risk assessments (e.g. senior alerts).
        /// </summary>
        public IEnumerable<Assessment> Assessments
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the tenant has an alternative identity enabled (e.g. tag identifier).
        /// </summary>
        public bool HasAlternativeIdentifier
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the patient is a person of public interest (VIP), or person who is
        /// known to the staff.
        /// </summary>
        public bool IsPersonOfPublicInterestOrVip
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the patient is a person with hightened sensitivity - all demographics
        /// sensitivity.
        /// </summary>
        public bool IsPersonWithHightenedSensitivity
        {
            get;
            set;
        }
    }
}