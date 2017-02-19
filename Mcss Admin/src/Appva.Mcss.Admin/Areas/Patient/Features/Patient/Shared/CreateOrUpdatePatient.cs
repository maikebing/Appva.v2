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
        [Required(ErrorMessageResourceName = "Förnamn_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Förnamn", ResourceType = typeof(Resources.Language))]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The patient last name.
        /// </summary>
        [Required(ErrorMessageResourceName = "Efternamn_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Efternamn", ResourceType = typeof(Resources.Language))]
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The patient personal identity number.
        /// </summary>
        [Required(ErrorMessageResourceName = "Personnummer_måste_fyllas_i", ErrorMessageResourceType = typeof(Resources.Language))]
        [Appva.Mvc.PersonalIdentityNumber(ErrorMessageResourceName = "Personnummer_måste_fyllas_i_med_tolv_siffror_och_bindestreck__t__ex__19010101_0001", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Personnummer", ResourceType = typeof(Resources.Language))]
        [Remote("VerifyUniquePatient", "Patient", AreaReference.UseCurrent, HttpMethod = "POST", ErrorMessageResourceName = "Personnumret_finns_redan_tidigare_redan_i_MCSS", ErrorMessageResourceType = typeof(Resources.Language))]
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The patient status.
        /// </summary>
        [Display(Name = "Avliden", ResourceType = typeof(Resources.Language))]
        public bool IsDeceased
        {
            get;
            set;
        }

        /// <summary>
        /// The patient address.
        /// </summary>
        [Required(ErrorMessageResourceName = "Adress_måste_väljas", ErrorMessageResourceType = typeof(Resources.Language))]
        [Remote("VerifyTaxon", "Taxa", AreaReference.UseRoot, HttpMethod = "POST", ErrorMessageResourceName = "Adress_måste_väljas", ErrorMessageResourceType = typeof(Resources.Language))]
        public string Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// The patient alternative identity.
        /// </summary>
        [Display(Name = "Tagg_id", ResourceType = typeof(Resources.Language))]
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