using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Web.ViewModels;
using Appva.Mcss.Admin.Domain.Entities;
using NHibernate;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Domain.Repositories;
using Appva.Persistence;
using System.Runtime.Caching;
using Appva.Caching.Providers;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Application.Models;
using Appva.Core.Extensions;

namespace Appva.Mcss.Web.Mappers {

    public class PatientMapper {

        public static readonly IRuntimeMemoryCache RTC = new RuntimeMemoryCache("test");
        public static PatientViewModel ToPatientViewModel(IPersistenceContext session, Patient patient)
        {
            return ToListOfPatientViewModel(session, new List<Patient>() { patient }).First();
        }

        public static IList<PatientViewModel> ToListOfPatientViewModel(IPersistenceContext session, IList<Patient> patients)
        {
            var roleService = new RoleService(new RoleRepository(session));
            var taxonomyService = new TaxonomyService(RTC, new TaxonRepository(session));
            var settingService = new SettingsService(RTC, new SettingsRepository(session));
            var taxons = taxonomyService.List(TaxonomicSchema.Organization);
            var superiors = roleService.MembersOfRole("_superioraccount");
            var overseers = roleService.MembersOfRole("_overseeingaccount");
            var firstLineContacts = roleService.MembersOfRole("_firstlinecontactccount");
            var retval = new List<PatientViewModel>();
            var taxonMap = new Dictionary<string, ITaxon>(taxons.ToDictionary(x => x.Id.ToString(), x => x));
            var tenantHasSeniorAlert = settingService.HasSeniorAlert();
            foreach (var patient in patients) {
                var address = string.Empty;
                var taxon = taxonMap[patient.Taxon.Id.ToString()];
                var paths = taxon.Path.Split('.');
                foreach (var path in paths) {
                    if (!taxonMap[path].IsRoot) {
                        address += taxonMap[path].Name + " ";
                    }
                }
                var superiorList = superiors.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                var superior = (superiorList.Count() > 0) ? superiorList.First() : null;
                var overseerList = overseers.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                var overseer = (overseerList.Count() > 0) ? overseerList.First() : null;
                var firstlineContactList = firstLineContacts.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                var firstlineContact = (firstlineContactList.Count() > 0) ? firstlineContactList.First() : null;
                retval.Add(new PatientViewModel() {
                    Id = patient.Id,
                    Active = patient.IsActive,
                    FullName = patient.FullName,
                    UniqueIdentifier = patient.PersonalIdentityNumber,
                    Address = address,
                    Superior = (superior.IsNotNull()) ? superior.FullName : null,
                    Overseeing = (overseer.IsNotNull()) ? overseer.FullName : null,
                    FirstLineContact = (firstlineContact.IsNotNull()) ? firstlineContact.FullName : null,
                    HasUnattendedTasks = patient.HasUnattendedTasks,
                    SeniorAlerts = tenantHasSeniorAlert ? patient.SeniorAlerts.ToList() : null
                });
            }
            return retval;
        }
    }
}