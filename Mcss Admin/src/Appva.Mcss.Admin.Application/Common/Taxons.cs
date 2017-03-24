// <copyright file="Taxons.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class Taxons
    {
        #region Risk Assesment
        
        /// <summary>
        /// The Health - Fall risk assesment
        /// </summary>
        public static readonly ITaxon Fall = CreateRiskAssement(
            "Fall",
            "Risk för fall",
            "icn-health-fall.png");

        /// <summary>
        /// The Health - Oral risk assesment
        /// </summary>
        public static readonly ITaxon Oral = CreateRiskAssement(
            "Munhälsa",
            "Observera munhälsa",
            "icn-health-oral.png");

        /// <summary>
        /// The Health - Pressure risk assesment
        /// </summary>
        public static readonly ITaxon Pressure = CreateRiskAssement(
            "Liggsår",
            "Bedöms ha risk för liggsår",
            "icn-health-pressure.png");

        /// <summary>
        /// The Health - Weight risk assesment
        /// </summary>
        public static readonly ITaxon Weight = CreateRiskAssement(
            "Näring",
            "Bedöms ha närings-behov",
            "icn-health-weight.png");

        /// <summary>
        /// The Warining - Diabetes risk assesment
        /// </summary>
        public static readonly ITaxon Diabetes = CreateRiskAssement(
            "Diabetes",
            "Observera diabetes",
            "icn-warning-diabetes.png");

        /// <summary>
        /// The Warning - Infection risk assesment
        /// </summary>
        public static readonly ITaxon Infection = CreateRiskAssement(
            "Smitta",
            "Observera smitta",
            "icn-warning-infection.png");

        /// <summary>
        /// The dualstaffing risk assesment
        /// </summary>
        public static readonly ITaxon Dualstaffing = CreateRiskAssement(
            "Dubbelbemaning",
            "Observera dubbelbemaning",
            "icn-dualstaffing.png");

        /// <summary>
        /// The BPSD risk assesment
        /// </summary>
        public static readonly ITaxon Bpsd = CreateRiskAssement(
            "BPSD",
            "Observera BPSD. För mer info, se journal.",
            "ico-warning-bpsd.png");

        /// <summary>
        /// The hypersensitivity risk assesment
        /// </summary>
        public static readonly ITaxon Hypersensitivity = CreateRiskAssement(
            "Överkänslighet",
            "Observera överkänslighet. För mer info, se journal.",
            "ico-warning-hypersensitive.png");

        /// <summary>
        /// The Antiglukos risk assesment
        /// </summary>
        public static readonly ITaxon Antiglukos = CreateRiskAssement(
            "Blodförtunnande",
            "Observera blodförtunnande",
            "ico-warning-antiglukos.png");

        public static readonly ITaxon TestAssessment = CreateRiskAssement(
            "Test",
            "Observera test",
            "test-warning.png");

        #endregion

        #region Private helpers

        private static ITaxon CreateRiskAssement(string name, string description, string image)
        {
            return new TaxonItem(Guid.Empty, name, description, "", image);
        }

        #endregion
    }
}