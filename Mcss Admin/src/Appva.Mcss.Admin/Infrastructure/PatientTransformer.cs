// <copyright file="PatientTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Web.ViewModels;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPatientTransformer
    {
        PatientViewModel ToPatient(Patient patient);
        IList<PatientViewModel> ToPatientList(IEnumerable<Patient> patients);
        IList<PatientViewModel> ToPatientList(IEnumerable<PatientModel> patients);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientTransformer : IPatientTransformer
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonService;

        /// <summary>
        /// The <see cref="ITenaService"/>
        /// </summary>
        private readonly ITenaService tenaService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientTransformer"/> class.
        /// </summary>
        /// <param name="patientService">The <see cref="IPatientService"/></param>
        /// <param name="roleService">The <see cref="IRoleService"/></param>
        /// <param name="taxonService">The <see cref="ITaxonomyService"/></param>
        /// <param name="settingsService">The <see cref="ISettingsService"/></param>
        public PatientTransformer(
            IPatientService patientService, 
            IRoleService roleService, 
            ITaxonomyService taxonService,
            ITenaService tenaService,
            ISettingsService settingsService)
        {
            this.patientService  = patientService;
            this.roleService     = roleService;
            this.taxonService    = taxonService;
            this.tenaService     = tenaService;
            this.settingsService = settingsService;
        }

        #endregion

        #region Public Methods.

        public PatientViewModel ToPatient(Patient patient)
        {
            return this.ToPatientList(new List<Patient> { patient }).FirstOrDefault();
        }

        public IList<PatientViewModel> ToPatientList(IEnumerable<Patient> patients)
        {
            var taxons = this.taxonService.List(TaxonomicSchema.Organization);
            ////var superiors = this.roleService.MembersOfRole("_superioraccount");
            ////var overseers = this.roleService.MembersOfRole("_overseeingaccount");
            ////var firstLineContacts = this.roleService.MembersOfRole("_firstlinecontactccount");
            var retval = new List<PatientViewModel>();
            var taxonMap = new Dictionary<string, ITaxon>(taxons.ToDictionary(x => x.Id.ToString().ToLowerInvariant(), x => x));
            var tenantHasSeniorAlert = this.settingsService.HasSeniorAlert();
            foreach (var patient in patients)
            {
                var address = string.Empty;
                var taxon = taxonMap[patient.Taxon.Id.ToString()];
                var paths = taxon.Path.ToLowerInvariant().Split('.');
                foreach (var path in paths)
                {
                    if (! taxonMap.ContainsKey(path))
                    {
                        continue;
                    }
                    if (! taxonMap[path].IsRoot)
                    {
                        address += taxonMap[path].Name + " ";
                    }
                }

                var seniorAlerts = tenantHasSeniorAlert ? patient.SeniorAlerts.Where(x => x.IsActive).ToList().Select(x => new TaxonItem(x.Id, x.Name, x.Description, x.Path, x.Type, x.Weight, null, x.IsActive)).ToList<ITaxon>() : new List<ITaxon>();

                var hasActiveTenaPeriod = this.tenaService.ListTenaObservationPeriod(patient.Id)
                                            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                                            .Count() > 0;
                if (hasActiveTenaPeriod) 
                {
                    seniorAlerts.Add(TaxonItem.FromTaxon(Taxon.New(null, "TENA Identifi", "Observera pågående mätning med TENA Identifi", "ico-tena-identifi.png")));
                }


                ////var superiorList = superiors.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                ////var superior = (superiorList.Count() > 0) ? superiorList.First() : null;
                ////var overseerList = overseers.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                ////var overseer = (overseerList.Count() > 0) ? overseerList.First() : null;
                ////var firstlineContactList = firstLineContacts.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                ////var firstlineContact = (firstlineContactList.Count() > 0) ? firstlineContactList.First() : null;
                retval.Add(new PatientViewModel()
                {
                    Id = patient.Id,
                    Active = patient.IsActive,
                    FullName = patient.FullName,
                    UniqueIdentifier = patient.PersonalIdentityNumber.ToString(),
                    Address = address,
                    Superior = null, ////(superior.IsNotNull()) ? superior.FullName : null,
                    Overseeing = null, ////(overseer.IsNotNull()) ? overseer.FullName : null,
                    FirstLineContact = null, ///(firstlineContact.IsNotNull()) ? firstlineContact.FullName : null,
                    HasUnattendedTasks = false,
                    SeniorAlerts = seniorAlerts
                });
            }
            return retval;
        }

        public IList<PatientViewModel> ToPatientList(IEnumerable<PatientModel> patients)
        {
            var seniorAlerts = this.taxonService.List(TaxonomicSchema.RiskAssessment);
            var taxons = this.taxonService.List(TaxonomicSchema.Organization);
            ////var superiors = this.roleService.MembersOfRole("_superioraccount");
            ////var overseers = this.roleService.MembersOfRole("_overseeingaccount");
            ////var firstLineContacts = this.roleService.MembersOfRole("_firstlinecontactccount");
            var retval = new List<PatientViewModel>();
            var taxonMap = new Dictionary<string, ITaxon>(taxons.ToDictionary(x => x.Id.ToString().ToLowerInvariant(), x => x));
            var tenantHasSeniorAlert = this.settingsService.HasSeniorAlert();
            IDictionary<string, ITaxon> seniorAlertMap = null;
            if (tenantHasSeniorAlert && seniorAlerts.IsNotNull())
            {
                seniorAlertMap = seniorAlerts.ToDictionary(x => x.Id.ToString(), x => x);
            }
            foreach (var patient in patients)
            {
                var address = string.Empty;
                var taxon = taxonMap[patient.Taxon.Id.ToString()];
                var paths = taxon.Path.ToLowerInvariant().Split('.');
                foreach (var path in paths)
                {
                    if (!taxonMap.ContainsKey(path))
                    {
                        continue;
                    }
                    if (!taxonMap[path].IsRoot)
                    {
                        address += taxonMap[path].Name + " ";
                    }
                }
                IList<ITaxon> seniorAlertTaxons = new List<ITaxon>();
                if (tenantHasSeniorAlert && seniorAlertMap.IsNotNull() && patient.ProfileAssements.IsNotEmpty())
                {
                    foreach (var s in patient.ProfileAssements.ToLower().Split('.'))
                    {
                        if (seniorAlertMap.ContainsKey(s))
                        {
                            seniorAlertTaxons.Add(seniorAlertMap[s]);
                        }
                    }
                }
                 var hasActiveTenaPeriod = this.tenaService.ListTenaObservationPeriod(patient.Id)
                                            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                                            .Count() > 0;
                if (hasActiveTenaPeriod) 
                {
                    seniorAlertTaxons.Add(TaxonItem.FromTaxon(Taxon.New(null, "TENA Identifi", "Observera pågående mätning med TENA Identifi", "ico-tena-identifi.png")));
                }
                ////var superiorList = superiors.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                ////var superior = (superiorList.Count() > 0) ? superiorList.First() : null;
                ////var overseerList = overseers.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                ////var overseer = (overseerList.Count() > 0) ? overseerList.First() : null;
                ////var firstlineContactList = firstLineContacts.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                ////var firstlineContact = (firstlineContactList.Count() > 0) ? firstlineContactList.First() : null;
                retval.Add(new PatientViewModel
                {
                    Id = patient.Id,
                    Active = patient.IsActive,
                    FullName = patient.FullName,
                    UniqueIdentifier = patient.PersonalIdentityNumber.ToString(),
                    Address = address,
                    Superior = null, ////(superior.IsNotNull()) ? superior.FullName : null,
                    Overseeing = null, ////(overseer.IsNotNull()) ? overseer.FullName : null,
                    FirstLineContact = null, ////(firstlineContact.IsNotNull()) ? firstlineContact.FullName : null,
                    HasUnattendedTasks = patient.HasUnattendedTasks,
                    SeniorAlerts = seniorAlertTaxons
                });
            }
            return retval;
        }

        #endregion
    }
}