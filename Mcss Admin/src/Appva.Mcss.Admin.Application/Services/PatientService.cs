// <copyright file="PatientService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using Appva.Core.Extensions;
    #endregion

    public interface IPatientService : IService
    {
        /// <summary>
        /// Returns a single account by id.
        /// </summary>
        /// <param name="id"></param>
        Patient Get(Guid id);

        IList<Patient> FindByTaxon(Taxon taxon, bool deceased = true);

        /// <summary>
        /// Activates a patient.
        /// </summary>
        /// <param name="patient">The patient to be activated</param>
        void Activate(Patient patient);

        /// <summary>
        /// Inctivates a patient.
        /// </summary>
        /// <param name="patient">The patient to be inactivated</param>
        void Inactivate(Patient patient);

        /// <summary>
        /// Returns whether or not a patient with the specfied personal identity number
        /// already exists.
        /// </summary>
        /// <param name="personalIdentityNumber">The unique personal identity number</param>
        /// <returns></returns>
        bool PatientWithPersonalIdentityNumberExist(string personalIdentityNumber);

        /// <summary>
        /// Updates the property HasUnatendedTasks.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hasUnattendedTasks"></param>
        void UpdateUnattendantTasks(Patient patient, bool hasUnattendedTasks);

        /// <summary>
        /// Creates a new patient.
        /// </summary>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="personalIdentityNumber">The personal identity number</param>
        /// <param name="alternativeIdentity">The alternative identity</param>
        /// <param name="address">The address</param>
        /// <param name="assessments">Optional collection of risk assessments</param>
        /// <param name="patient">The created patient</param>
        /// <returns>True if the patient is created successfully</returns>
        bool Create(string firstName, string lastName, string personalIdentityNumber, string alternativeIdentity, Taxon address, IList<Taxon> assessments, out Patient patient);

        /// <summary>
        /// Updates a patient.
        /// </summary>
        /// <param name="id">The patient ID updated</param>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="personalIdentityNumber">The personal identity number</param>
        /// <param name="alternativeIdentity">The alternative identity/identifier</param>
        /// <param name="isDeceased">Whether or not the patient is deceased</param>
        /// <param name="address">The address</param>
        /// <param name="assessments">The assessments to be updated</param>
        /// <param name="patient">The updated patient</param>
        /// <returns>True if successfully updated</returns>
        bool Update(Guid id, string firstName, string lastName, string personalIdentityNumber, string alternativeIdentity, bool isDeceased, Taxon address, IList<Taxon> assessments, out Patient patient);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientService : IPatientService
    {
        #region Variables.

        private readonly IPersistenceContext persistence;
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientService"/> class.
        /// </summary>
        public PatientService(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region 

        /// <inheritdoc />
        public Patient Get(Guid id)
        {
            return this.persistence.Get<Patient>(id);
        }

        /// <inheritdoc />
        public IList<Patient> FindByTaxon(Taxon taxon, bool deceased = true)
        {
            return this.persistence.QueryOver<Patient>()
                .Where(x => x.IsActive == true)
                .And(x => x.Deceased == deceased)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                .Where(Restrictions.On<Taxon>(x => x.Path)
                .IsLike(taxon.Id.ToString(), MatchMode.Anywhere))
                .List();
        }

        /// <inheritdoc />
        public void Activate(Patient patient)
        {
            patient.IsActive = true;
            patient.UpdatedAt = DateTime.Now;
            this.persistence.Update(patient);
            //this.logService.Info(string.Format("Användare {0} aktiverade boende {1} (REF: {2}).", currentUser.UserName, patient.FullName, patient.Id), currentUser, patient, LogType.Write);
        }

        /// <inheritdoc />
        public void Inactivate(Patient patient)
        {
            patient.IsActive = false;
            patient.UpdatedAt = DateTime.Now;
            this.persistence.Update(patient);
            //this.logService.Info(string.Format("Användare {0} inaktiverade boende {1} (REF: {2}).", currentUser.UserName, patient.FullName, patient.Id), currentUser, patient, LogType.Write);
        }

        /// <inheritdoc />
        public bool PatientWithPersonalIdentityNumberExist(string personalIdentityNumber)
        {
            return this.persistence.QueryOver<Patient>()
                    .Where(x => x.PersonalIdentityNumber == personalIdentityNumber)
                    .RowCount() > 0;
        }

        /// <inheritdoc />
        public void UpdateUnattendantTasks(Patient patient, bool hasUnattendedTasks)
        {
            if (patient == null)
            {
                return;
            }
            patient.HasUnattendedTasks = hasUnattendedTasks;
            this.persistence.Update(patient);
        }

        /// <inheritdoc />
        public bool Create(string firstName, string lastName, string personalIdentityNumber, string alternativeIdentity, Taxon address, IList<Taxon> assessments, out Patient patient)
        {
            patient = new Patient
                {
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    FirstName = firstName.TrimEnd().TrimStart().FirstToUpper(),
                    LastName = lastName.TrimEnd().TrimStart().FirstToUpper(),
                    FullName = string.Format("{0} {1}", firstName, lastName),
                    PersonalIdentityNumber = personalIdentityNumber,
                    Taxon = address,
                    Identifier = alternativeIdentity
                };
            this.persistence.Save(patient);
            //this.logService.Info(string.Format("Användare {0} lade till boende {1} (REF: {2}).", currentUser.UserName, patient.FullName, patient.Id), currentUser, patient, LogType.Write);
            return true;  
        }

        /// <inheritdoc />
        public bool Update(Guid id, string firstName, string lastName, string personalIdentityNumber, string alternativeIdentity, bool isDeceased, Taxon address, IList<Taxon> assessments, out Patient patient)
        {
            patient = this.Get(id);
            var oldSA = patient.SeniorAlerts;
            patient.FirstName = firstName;
            patient.LastName = lastName;
            patient.FullName = string.Format("{0} {1}", firstName, lastName);
            patient.PersonalIdentityNumber = personalIdentityNumber;
            patient.Taxon = address;
            patient.Deceased = isDeceased;
            patient.UpdatedAt = DateTime.Now;
            patient.SeniorAlerts = assessments;
            patient.Identifier = alternativeIdentity;
            this.persistence.Update(patient);
            /*var currentUser = Identity();
            this.logService.Info(string.Format("Användare {0} ändrade boendes {1} uppgifter (REF: {2}).", currentUser.UserName, patient.FullName, patient.Id), currentUser, patient, LogType.Write);
            foreach (var sa in seniorAlerts)
            {
                if (!oldSA.Contains(sa))
                {
                    this.logService.Info(string.Format("Användare {0} lade till skattning {1} (ref:{2}) till {3}.", currentUser.UserName, sa.Name, sa.Id, patient.FullName), currentUser, patient);
                }
            }
            foreach (var sa in oldSA)
            {
                if (!seniorAlerts.Contains(sa))
                {
                    this.logService.Info(string.Format("Användare {0} tog bort skattning {1} (ref:{2}) till {3}.", currentUser.UserName, sa.Name, sa.Id, patient.FullName), currentUser, patient);
                }
            }*/
            return true;
        }

        #endregion
    }
}