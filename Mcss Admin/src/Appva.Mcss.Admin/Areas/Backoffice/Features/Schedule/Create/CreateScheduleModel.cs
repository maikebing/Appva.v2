// <copyright file="CreateScheduleModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateScheduleModel : IRequest<Identity<DetailsScheduleModel>>
    {
        #region Properties.

        /// <summary>
        /// The name
        /// </summary>
        [Required]
        [DisplayName("Namn")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The alternative name
        /// </summary>
        [Required]
        [DisplayName("Alternativt namn")]
        public string AlternativeName
        {
            get;
            set;
        }

        /// <summary>
        /// If sequences could be paused
        /// </summary>
        [DisplayName("Kan pausas")]
        public bool IsPausable
        {
            get;
            set;
        }

        /// <summary>
        /// If schedule should creat alerts
        /// </summary>
        [DisplayName("Skapar larm vid utebliven insats")]
        public bool CanRaiseAlerts
        {
            get;
            set;
        }

        /// <summary>
        /// Setup drugs on/off
        /// </summary>
        [DisplayName("Har iordningställande-funktion")]
        public bool HasSetupDrugsPanel
        {
            get;
            set;
        }

        /// <summary>
        /// If sequences in current list should have inventory
        /// </summary>
        [DisplayName("Har saldo")]
        public bool HasInventory
        {
            get;
            set;
        }

        /// <summary>
        /// If inventories needs to be recalculated
        /// TODO: Obsolete?
        /// </summary>
        [DisplayName("Kräver kontrollräkning")]
        public bool CountInventory
        {
            get;
            set;
        }

        /// <summary>
        /// If schedule is collecting Given Dosage
        /// </summary>
        [DisplayName("Hantera given mängd")]
        public bool IsCollectingGivenDosage
        {
            get;
            set;
        }

        /// <summary>
        /// Deviation-dialog on/off
        /// </summary>
        [DisplayName("Kontrollruta vid avvikande signering")]
        public bool NurseConfirmDeviation
        {
            get;
            set;
        }

        /// <summary>
        /// The popup message when task signed as a deviation
        /// </summary>
        public ConfirmDeviationMessage DeviationMessage
        {
            get;
            set;
        }

        /*/// <summary>
        /// Specify contacted nurse on/off
        /// </summary>
        [DisplayName("Ange kontaktad SSK vid avikande signering")]
        public bool SpecificNurseConfirmDeviation
        {
            get;
            set;
        }

        /// <summary>
        /// Custom dialog message
        /// </summary>
        [DisplayName("Meddelande vid avvikande signering")]
        public string NurseConfirmDeviationMessage
        {
            get;
            set;
        }*/

        /// <summary>
        /// Order refill on/off
        /// </summary>
        [DisplayName("Aktivera 'Beställ påfyllning'")]
        public bool OrderRefill
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation-category for this schedule
        /// </summary>
        [DisplayName("Delegeringar")]
        public Guid? DelegationTaxon
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation categories
        /// </summary>
        public IList<SelectListItem> Delegations
        {
            get;
            set;
        }

        #endregion
    }
}