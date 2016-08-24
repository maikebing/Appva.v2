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
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Repository;
    #endregion

    /// <summary>
    /// The <see cref="Patient"/> service.
    /// </summary>
    public interface IPatientService : IService
    {
        /// <summary>
        /// Returns a single account by id.
        /// </summary>
        /// <param name="id"></param>
        Patient Get(Guid id);

        IList<Patient> FindByTaxon(Guid taxon, bool deceased = true);

        /// <summary>
        /// Locates a patient by its unique Personal Identity Number. 
        /// </summary>
        /// <param name="personalIdentityNumber">The unique Personal Identity Number</param>
        /// <returns>An <see cref="Patient"/> instance if found, else null</returns>
        Patient FindByPersonalIdentityNumber(PersonalIdentityNumber personalIdentityNumber);

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
        bool PatientWithPersonalIdentityNumberExist(PersonalIdentityNumber personalIdentityNumber);

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
        bool Create(string firstName, string lastName, PersonalIdentityNumber personalIdentityNumber, string alternativeIdentity, Taxon address, IList<Taxon> assessments, bool isPersonOfPublicInterest, bool isPersonWithAllDemographicsSensitivity, out Patient patient);

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
        bool Update(Guid id, string firstName, string lastName, PersonalIdentityNumber personalIdentityNumber, string alternativeIdentity, bool isDeceased, Taxon address, IList<Taxon> assessments, bool isPersonOfPublicInterest, bool isPersonWithAllDemographicsSensitivity, out Patient patient);

        /// <summary>
        /// Loads a proxy of the patient from the id
        /// </summary>
        /// <param name="id">Patient id</param>
        /// <returns>A proxy of <see cref="Patient"/></returns>
        Patient Load(Guid id);

        /// <summary>
        /// Gets all patients with delayed tasks
        /// </summary>
        /// <param name="taxon"></param>
        /// <param name="incompleteTasks"></param>
        /// <returns></returns>
        IList<PatientModel> FindDelayedPatientsBy(ITaxon taxon, bool? incompleteTasks = null);

        /// <summary>
        /// Lists patients by search criteria
        /// </summary>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageableSet<PatientModel> Search(SearchPatientModel model, int page = 1, int pageSize = 10);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientService : IPatientService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPatientRepository"/>
        /// </summary>
        private readonly IPatientRepository repository;

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientService"/> class.
        /// </summary>
        public PatientService(IAuditService auditing, IPatientRepository repository, IIdentityService identity, IPersistenceContext persistence)
        {
            this.auditing = auditing;
            this.persistence = persistence;
            this.repository = repository;
            this.identity = identity;
        }

        #endregion

        #region IPatientService members.

        /// <inheritdoc />
        public Patient Get(Guid id)
        {
            return this.persistence.Get<Patient>(id);
        }

        /// <inheritdoc />
        /// <exception cref="NotUniquePersonalIdentityNumberException">If the personal identity number is not unique an exception will be thrown</exception>
        public Patient FindByPersonalIdentityNumber(PersonalIdentityNumber personalIdentityNumber)
        {
            var accounts = this.persistence.QueryOver<Patient>()
                .And(x => x.PersonalIdentityNumber == personalIdentityNumber)
                .List();
            if (accounts.Count > 1)
            {
                throw new NotUniquePersonalIdentityNumberException("Not unique personal identity number");
            }
            if (accounts.Count == 1)
            {
                return accounts.First();
            }
            return null;
        }

        /// <inheritdoc />
        public IList<Patient> FindByTaxon(Guid taxon, bool deceased = true)
        {
            return this.persistence.QueryOver<Patient>()
                .Where(x => x.IsActive == true)
                .And(x => x.Deceased == deceased)
                .OrderBy(x => x.LastName).Asc
                .ThenBy(x => x.FirstName).Asc
                .JoinQueryOver<Taxon>(x => x.Taxon)
                .Where(Restrictions.On<Taxon>(x => x.Path)
                       .IsLike(taxon.ToString(), MatchMode.Anywhere))
                .List();
        }

        /// <inheritdoc />
        public void Activate(Patient patient)
        {
            patient.IsActive = true;
            patient.UpdatedAt = DateTime.Now;
            patient.LastActivatedAt = DateTime.Now;
            this.persistence.Update(patient);
            this.auditing.Update(
                patient,
                "aktiverade boende {0} (REF: {1}).", 
                patient.FullName, 
                patient.Id);
        }

        /// <inheritdoc />
        public void Inactivate(Patient patient)
        {
            patient.IsActive = false;
            patient.UpdatedAt = DateTime.Now;
            patient.LastInActivatedAt = DateTime.Now;
            this.persistence.Update(patient);
            this.auditing.Update(
                patient,
                "inaktiverade boende {0} (REF: {1}).", 
                patient.FullName, 
                patient.Id);
        }

        /// <inheritdoc />
        public bool PatientWithPersonalIdentityNumberExist(PersonalIdentityNumber personalIdentityNumber)
        {
            return this.persistence.QueryOver<Patient>()
                    .Where(x => x.PersonalIdentityNumber == personalIdentityNumber)
                    .RowCount() > 0;
        }

        /// <inheritdoc />
        public bool Create(string firstName, string lastName, PersonalIdentityNumber personalIdentityNumber, string alternativeIdentity, Taxon address, IList<Taxon> assessments, bool isPersonOfPublicInterest, bool isPersonWithAllDemographicsSensitivity, out Patient patient)
        {
            var check = this.FindByPersonalIdentityNumber(personalIdentityNumber);
            if (check != null)
            {
                throw new NotUniquePersonalIdentityNumberException("Personal identity number already exists!");
            }
            patient = new Patient
                {
                    IsActive                             = true,
                    CreatedAt                            = DateTime.Now,
                    UpdatedAt                            = DateTime.Now,
                    FirstName                            = firstName.TrimEnd().TrimStart().FirstToUpper(),
                    LastName                             = lastName.TrimEnd().TrimStart().FirstToUpper(),
                    FullName                             = string.Format("{0} {1}", firstName, lastName),
                    PersonalIdentityNumber               = personalIdentityNumber,
                    Taxon                                = address,
                    Identifier                           = alternativeIdentity,
                    IsAllDemographicInformationSensitive = isPersonWithAllDemographicsSensitivity,
                    IsPersonOfPublicInterest             = isPersonOfPublicInterest
                };
            this.persistence.Save(patient);
            this.auditing.Create(
                patient,
                "lade till boende {0} (REF: {1}).", 
                patient.FullName, 
                patient.Id);
            return true;  
        }

        /// <inheritdoc />
        public bool Update(Guid id, string firstName, string lastName, PersonalIdentityNumber personalIdentityNumber, string alternativeIdentity, bool isDeceased, Taxon address, IList<Taxon> assessments, bool isPersonOfPublicInterest, bool isPersonWithAllDemographicsSensitivity, out Patient patient)
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
            patient.IsAllDemographicInformationSensitive = isPersonWithAllDemographicsSensitivity;
            patient.IsPersonOfPublicInterest = isPersonOfPublicInterest;
            this.persistence.Update(patient);
            this.auditing.Update(
                patient,
                "ändrade boendes {0} uppgifter (REF: {1}).", 
                patient.FullName, 
                patient.Id);
            foreach (var sa in assessments)
            {
                if (! oldSA.Contains(sa))
                {
                    this.auditing.Update(
                        patient,
                        "lade till skattning {0} (ref:{1}) till {2}.", 
                        sa.Name, 
                        sa.Id, 
                        patient.FullName);
                }
            }
            foreach (var sa in oldSA)
            {
                if (! assessments.Contains(sa))
                {
                    this.auditing.Update(
                        patient,
                        "tog bort skattning {0} (ref:{1}) till {2}.", 
                        sa.Name, 
                        sa.Id, 
                        patient.FullName);
                }
            }
            return true;
        }

        /// <inheritdoc />
        public IList<PatientModel> FindDelayedPatientsBy(ITaxon taxon, bool? incompleteTasks = null)
        {
            var schedulesettings = this.identity.SchedulePermissions().Select(x => new Guid(x.Value)).ToList();
            return this.repository.FindDelayedPatientsBy(taxon.Path, incompleteTasks.GetValueOrDefault(false), schedulesettings);
        }

        /// <inheritdoc />
        public PageableSet<PatientModel> Search(SearchPatientModel model, int page = 1, int pageSize = 10)
        {
            this.auditing.Read("genomförde en sökning i patientlistan på {0}.", model.SearchQuery);

            var schedulesettings = this.identity.SchedulePermissions().Select(x => new Guid(x.Value)).ToList();
            return this.repository.Search(model, schedulesettings, page, pageSize);
        }

        /// <inheritdoc />
        public Patient Load(Guid id)
        {
            return this.repository.Load(id);
        }

        #endregion 
    }

    /// <summary>
    /// Represents errors that occur a personal identity number is not unique.
    /// </summary>
    [Serializable]
    public sealed class NotUniquePersonalIdentityNumberException : Exception
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="NotUniquePersonalIdentityNumberException"/> class.
        /// </summary>
        public NotUniquePersonalIdentityNumberException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="NotUniquePersonalIdentityNumberException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public NotUniquePersonalIdentityNumberException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="NotUniquePersonalIdentityNumberException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified
        /// </param>
        public NotUniquePersonalIdentityNumberException(string message, Exception inner)
            : base(message, inner)
        {
        }

        #endregion
    }
}