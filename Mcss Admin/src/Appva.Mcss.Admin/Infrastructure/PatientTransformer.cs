// <copyright file="PatientTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using System.Linq;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPatientTransformer
    {
        PatientViewModel ToPatient(Patient patient);
        IList<PatientViewModel> ToPatientList(IList<Patient> patients);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PatientTransformer : IPatientTransformer
    {
        #region Private Variables.

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
        public PatientTransformer(IPatientService patientService, IRoleService roleService, ITaxonomyService taxonService, ISettingsService settingsService)
        {
            this.patientService = patientService;
            this.roleService = roleService;
            this.taxonService = taxonService;
            this.settingsService = settingsService;
        }

        #endregion

        #region Public Methods.

        public PatientViewModel ToPatient(Patient patient)
        {
            return this.ToPatientList(new List<Patient> { patient }).FirstOrDefault();
        }

        public IList<PatientViewModel> ToPatientList(IList<Patient> patients)
        {
            var taxons = this.taxonService.List(TaxonomicSchema.Organization);
            var superiors = this.roleService.MembersOfRole("_superioraccount");
            var overseers = this.roleService.MembersOfRole("_overseeingaccount");
            var firstLineContacts = this.roleService.MembersOfRole("_firstlinecontactccount");
            var retval = new List<PatientViewModel>();
            var taxonMap = new Dictionary<string, ITaxon>(taxons.ToDictionary(x => x.Id.ToString(), x => x));
            var tenantHasSeniorAlert = this.settingsService.HasSeniorAlert();
            foreach (var patient in patients)
            {
                var address = string.Empty;
                var taxon = taxonMap[patient.Taxon.Id.ToString()];
                var paths = taxon.Path.Split('.');
                foreach (var path in paths)
                {
                    if (!taxonMap[path].IsRoot)
                    {
                        address += taxonMap[path].Name + " ";
                    }
                }
                var superiorList = superiors.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                var superior = (superiorList.Count() > 0) ? superiorList.First() : null;
                var overseerList = overseers.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                var overseer = (overseerList.Count() > 0) ? overseerList.First() : null;
                var firstlineContactList = firstLineContacts.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                var firstlineContact = (firstlineContactList.Count() > 0) ? firstlineContactList.First() : null;
                retval.Add(new PatientViewModel()
                {
                    Id = patient.Id,
                    Active = patient.IsActive,
                    FullName = patient.FullName,
                    UniqueIdentifier = patient.PersonalIdentityNumber.ToString(),
                    Address = address,
                    Superior = (superior.IsNotNull()) ? superior.FullName : null,
                    Overseeing = (overseer.IsNotNull()) ? overseer.FullName : null,
                    FirstLineContact = (firstlineContact.IsNotNull()) ? firstlineContact.FullName : null,
                    HasUnattendedTasks = patient.HasUnattendedTasks,
                    SeniorAlerts = tenantHasSeniorAlert ? patient.SeniorAlerts.ToList().Select(x => x).ToList() : null
                });
            }
            return retval;
        }

        #endregion
    }
}