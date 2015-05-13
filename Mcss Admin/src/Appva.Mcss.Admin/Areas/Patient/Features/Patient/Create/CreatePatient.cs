// <copyright file="CreatePatient.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreatePatient : IRequest<bool>
    {
        /// <summary>
        /// The patient ID.
        /// </summary>
        public Guid? PatientId
        {
            get;
            set;
        }

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
        [RegularExpression(@"^(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][0-9])|(3[0-1])|([6-8][0-9])|(9[0-1]))-?[0-9pPtTfF][0-9]{3}$", ErrorMessage = "Personnummer måste fyllas i med tolv siffror och bindestreck, t. ex. 19010101-0001.")]
        [DisplayName("Personnummer")]
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
        [DisplayName("Tag")]
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
        /// The risk assessments.
        /// </summary>
        public IEnumerable<Assessment> SeniorAlerts
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the tenant has an alternative identity enabled.
        /// </summary>
        public bool HasTagIdentifier
        {
            get;
            set;
        }
    }
}