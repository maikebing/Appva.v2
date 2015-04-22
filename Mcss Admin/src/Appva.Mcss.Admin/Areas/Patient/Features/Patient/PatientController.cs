// <copyright file="PatientController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using System.Web.UI;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Web.Controllers;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("Patient")]
    public sealed class PatientController : IdentityController
    {
        #region Private Variables.

        private readonly IPersistenceContext context;
        private readonly ILogService logService;
        private readonly ITaxonomyService taxonomyService;
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        public PatientController(IMediator mediator, IIdentityService identities, IAccountService accounts, IPersistenceContext context, ISettingsService settingsService, ITaxonomyService taxonomyService, ILogService logService)
            : base(mediator, identities, accounts)
        {
            this.context = context;
            this.taxonomyService = taxonomyService;
            this.settingsService = settingsService;
            this.logService = logService;
        }

        #endregion

        #region Routes.

        #region List View.

        /// <summary>
        /// Returns a sub set of patients view. 
        /// </summary>
        /// <param name="q">The query</param>
        /// <param name="page">The current page in the set</param>
        /// <param name="isActive">Optional is active query filter - defaults to true</param>
        /// <param name="isDeceased">Optional is deceased query filter - defaults to false</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("List")]
        public ActionResult List(string q, int? page, bool isActive = true, bool isDeceased = false)
        {
            var account = Identity();
            this.logService.Info(string.Format("Användare {0} genomförde en sökning i patientlistan på {1}.", account.UserName, q), account, LogType.Read);
            return View(new PatientListViewModel
            {
                IsActive = isActive,
                IsDeceased = isDeceased,
                Search = base.ExecuteCommand<SearchViewModel<PatientViewModel>>(new SearchPatientCommand
                {
                    SearchQuery = q,
                    Page = page,
                    IsActive = isActive,
                    IsDeceased = isDeceased
                })
            });
        }

        #endregion

        #region Create View.

        /// <summary>
        /// Returns the create patient view.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            var saItems = this.settingsService.HasSeniorAlert() ? this.context.QueryOver<Taxon>()
                .Where(x => x.IsActive)
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == "SAI")
                    .List() : null;
            return View(new PatientFormViewModel
            {
                Taxons = TaxonomyHelper.SelectList(this.taxonomyService.List(TaxonomicSchema.Organization)),
                SeniorAlerts = saItems,
                HasTagIdentifier = this.settingsService.HasPatientTag()
            });
        }

        /// <summary>
        /// Saves a patient if valid.
        /// </summary>
        /// <param name="model">The patient model</param>
        /// <param name="collection">The form collection</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(PatientFormViewModel model, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var taxon = this.context.Get<Taxon>(TaxonomyHelper.FindTaxon(collection, 
                    this.taxonomyService.List(TaxonomicSchema.Organization).Select(x => x.Id).ToList()));
                if (taxon.IsNotNull())
                {
                    var currentUser = Identity();
                    var seniorAlerts = new List<Taxon>();
                    if (model.PatientSeniorAlerts.IsNotNull())
                    {
                        foreach (var sa in model.PatientSeniorAlerts)
                        {
                            seniorAlerts.Add(this.context.Get<Taxon>(sa));
                        }
                    }
                    var patient = new Patient
                    {
                        FirstName = model.FirstName.FirstToUpper(),
                        LastName = model.LastName.FirstToUpper(),
                        FullName = string.Format("{0} {1}", model.FirstName, model.LastName),
                        PersonalIdentityNumber = model.UniqueIdentifier,
                        Taxon = taxon,
                        SeniorAlerts = seniorAlerts,
                        Identifier = model.Tag
                    };
                    this.context.Save(patient);
                    this.logService.Info(string.Format("Användare {0} lade till boende {1} (REF: {2}).", currentUser.UserName, patient.FullName, patient.Id), currentUser, patient, LogType.Write);
                    foreach (var sa in seniorAlerts)
                    {
                        this.logService.Info(string.Format("Användare {0} lade till skattning {1} (ref:{2}) till {3}.", currentUser.UserName, sa.Name, sa.Id, patient.FullName), currentUser, patient);
                    }
                    return this.RedirectToAction("List");
                }
            }
            model.Taxons = TaxonomyHelper.SelectList(this.taxonomyService.List(TaxonomicSchema.Organization));
            return View(model);
        }

        #endregion

        #region Edit View.

        /// <summary>
        /// Returns the edit patient view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("Edit/{id:guid}")]
        public ActionResult Edit(Guid id)
        {
            var patient = this.context.Get<Patient>(id);
            var saItems = this.settingsService.HasSeniorAlert() ? this.context.QueryOver<Taxon>()
                .Where(x => x.IsActive)
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == "SAI")
                    .List() : null;
            return View(new PatientFormViewModel
            {
                PatientId = id,
                FirstName = patient.FirstName.FirstToUpper(),
                LastName = patient.LastName.FirstToUpper(),
                Taxon = patient.Taxon.Id.ToString(),
                Taxons = TaxonomyHelper.SelectList(this.context.Get<Taxon>(patient.Taxon.Id),
                    this.taxonomyService.List(TaxonomicSchema.Organization)),
                UniqueIdentifier = patient.PersonalIdentityNumber,
                Deceased = patient.Deceased,
                SeniorAlerts = saItems,
                HasTagIdentifier = this.settingsService.HasPatientTag(),
                Tag = patient.Identifier,
                PatientSeniorAlerts = patient.SeniorAlerts.IsNotNull() ? patient.SeniorAlerts.Select(x => x.Id).ToArray() : new Guid[0]
            });
        }

        /// <summary>
        /// Edits a patient if valid.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="model">The patient model</param>
        /// <param name="collection">The form collection</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("Edit/{id:guid}")]
        public ActionResult Edit(Guid id, PatientFormViewModel model, FormCollection collection)
        {
            var patient = this.context.Get<Patient>(id);
            if (ModelState.IsValid)
            {
                var taxon = this.context.Get<Taxon>(TaxonomyHelper.FindTaxon(collection, 
                    this.taxonomyService.List(TaxonomicSchema.Organization).Select(x => x.Id).ToList()));
                var seniorAlerts = new List<Taxon>();
                if (model.PatientSeniorAlerts.IsNotNull())
                {
                    foreach (var sa in model.PatientSeniorAlerts)
                    {
                        seniorAlerts.Add(this.context.Get<Taxon>(sa));
                    }
                }
                if (taxon.IsNotNull())
                {
                    var oldSA = patient.SeniorAlerts;
                    patient.FirstName = model.FirstName;
                    patient.LastName = model.LastName;
                    patient.FullName = string.Format("{0} {1}", model.FirstName, model.LastName);
                    patient.PersonalIdentityNumber = model.UniqueIdentifier;
                    patient.Taxon = taxon;
                    patient.Deceased = model.Deceased;
                    patient.UpdatedAt = DateTime.Now;
                    patient.SeniorAlerts = seniorAlerts;
                    patient.Identifier = model.Tag;
                    this.context.Update(patient);
                    var currentUser = Identity();
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
                    }
                    return this.RedirectToAction("List", "Schedule", new
                    {
                        Id = id
                    });
                }
            }
            model.Taxons = TaxonomyHelper.SelectList(this.context.Get<Taxon>(patient.Taxon.Id), this.taxonomyService.List(TaxonomicSchema.Organization));
            return this.View(model);
        }

        #endregion

        #region Inactivate And Activate View.

        /// <summary>
        /// Inactivates a patient.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("Inactivate/{id:guid}")]
        public ActionResult Inactivate(Guid id)
        {
            var patient = this.context.Get<Patient>(id);
            var currentUser = Identity();
            this.ExecuteCommand(new InactivateOrActivateCommand<Patient>
            {
                Id = id
            });
            this.logService.Info(string.Format("Användare {0} inaktiverade boende {1} (REF: {2}).", currentUser.UserName, patient.FullName, patient.Id), currentUser, patient, LogType.Write);
            return this.RedirectToAction("List");
        }

        /// <summary>
        /// Activates a patient.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("Activate/{id:guid}")]
        public ActionResult Activate(Guid id)
        {
            var patient = this.context.Get<Patient>(id);
            var currentUser = Identity();
            ExecuteCommand(new InactivateOrActivateCommand<Patient>
            {
                Id = id,
                IsActive = true
            });
            this.logService.Info(string.Format("Användare {0} aktiverade boende {1} (REF: {2}).", currentUser.UserName, patient.FullName, patient.Id), currentUser, patient, LogType.Write);
            return this.RedirectToAction("List");
        }

        #endregion

        #region IsUnique Json.

        /// <summary>
        /// Returns whether or not the personal/national identifier number is unique
        /// across the patients.
        /// </summary>
        /// <param name="id">Optional patient id</param>
        /// <param name="uniqueIdentifier">The personal identifier number</param>
        /// <returns><see cref="JsonResult"/></returns>
        [HttpPost, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        [Route("IsUnique")]
        public JsonResult IsUnique(Guid? id, string uniqueIdentifier)
        {
            return this.Json(this.ExecuteCommand<bool>(new IsUniqueIdentifierCommand<Patient>
            {
                Id = id,
                UniqueIdentifier = uniqueIdentifier
            }), JsonRequestBehavior.DenyGet);
        }

        #endregion

        #region QuickSearch Json.

        /// <summary>
        /// Returns a set of patients by filters.
        /// </summary>
        /// <param name="term">The search term</param>
        /// <param name="isActive">Optional is active query filter - defaults to true</param>
        /// <param name="isDeceased">Optional is deceased query filter - defaults to false</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        [Route("QuickSearch")]
        public ActionResult QuickSearch(string term, bool isActive = true, bool isDeceased = false)
        {
            return this.Json(this.ExecuteCommand<IEnumerable<object>>(new PatientQuickSearch()
            {
                Term = term,
                IsActive = isActive,
                IsDeceased = isDeceased,
                Identity = Identity()
            }), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}