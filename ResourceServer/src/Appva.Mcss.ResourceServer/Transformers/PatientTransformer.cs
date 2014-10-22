// <copyright file="PatientTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Transformers
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Models;

    #endregion

    /// <summary>
    /// Patient transforming.
    /// </summary>
    public static class PatientTransformer
    {
        /// <summary>
        /// Transforms an <see cref="Patient"/> to an <see cref="PatientModel"/>
        /// </summary>
        /// <param name="patient">The <see cref="Patient"/> to be transformed</param>
        /// <param name="hasIncompleteTask">TODO: hasIncompleteTask</param>
        /// <returns>TODO: returns</returns>
        public static PatientModel ToPatient(Patient patient, bool hasIncompleteTask)
        {
            return new PatientModel
            {
                Id = patient.Id,
                FullName = patient.FullName,
                PersonalIdentityNumber = patient.UniqueIdentifier,
                HasIncompleteTasks = hasIncompleteTask,
                Profiles = TaxonTransformer.ToProfile(patient.SeniorAlerts),
                OrganisationTaxon = TaxonTransformer.ToTaxon(patient.Taxon, false, string.Empty) //// FIXME: Should not be static false and have patientcount
            };
        }

        /// <summary>
        /// TODO: Summery.
        /// </summary>
        /// <param name="patients">TODO: patients</param>
        /// <param name="withDelayedTask">TODO: withDelayedTask</param>
        /// <returns>TODO: returns</returns>
        public static IList<PatientModel> ToPatient(IList<Patient> patients, IList<Guid> withDelayedTask)
        {
            var retval = new List<PatientModel>();
            foreach (var patient in patients)
            {
                retval.Add(ToPatient(patient, withDelayedTask.Contains(patient.Id)));
            }
            return retval;
        }
    }
}