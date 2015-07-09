// <copyright file="DelegationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Delegations
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Core.Extensions;
    using Appva.Core.IO;
    using Appva.Core.Resources;
    using Appva.Core.Utilities;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web;
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc.Security;
    using Appva.Office;
    using Appva.Persistence;
    using Appva.Tenant.Identity;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// The delegation controller.
    /// </summary>
    [RouteArea("Practitioner"), RoutePrefix("Delegation")]
    public sealed class DelegationController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/> dispatcher.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The <see cref="ITaxonomyService"/> dispatcher.
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IIdentityService"/> dispatcher.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="ILogService"/> dispatcher.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IReportService"/> dispatcher.
        /// </summary>
        private readonly IReportService reports;

        /// <summary>
        /// The <see cref="ITaskService"/> dispatcher.
        /// </summary>
        private readonly ITaskService tasks;

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenantService;

        /// <summary>
        /// The <see cref="IAccountTransformer"/>.
        /// </summary>
        private readonly IAccountTransformer transformer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationController"/> class.
        /// </summary>
        public DelegationController(IMediator mediator, IIdentityService identityService, 
            ITaxonomyService taxonomyService, IAuditService auditing, IPersistenceContext persistence,
            ITaxonFilterSessionHandler filtering, ITenantService tenantService,
            IAccountTransformer transformer,  IReportService reports, ITaskService tasks)
        {
            this.mediator = mediator;
            this.identityService = identityService;
            this.taxonomyService = taxonomyService;
            this.reports = reports;
            this.tasks = tasks;
            this.auditing = auditing;
            this.persistence = persistence;
            this.filtering = filtering;
            this.transformer = transformer;
        }

        #endregion

        #region TO MOVE TO CONTROLLER.

        private Account Identity()
        {
            return this.persistence.Get<Account>(this.identityService.PrincipalId);
        }

        #endregion

        #region List.

        /// <summary>
        /// Lists all delegations by account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("list/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.ReadValue)]
        public ActionResult List(Guid id)
        {
            var user = this.Identity();
            var account = this.persistence.Get<Account>(id);
            if (account.Roles.Where(x => x.MachineName.StartsWith(RoleTypes.Developer)).Count() != 0 &&
                user.Roles.Where(x => x.MachineName.StartsWith(RoleTypes.AdminPrefix)).Count() == 0)
            {
                account = null;
            }
            var delegations = this.persistence.QueryOver<Delegation>()
                .Where(d => d.Account.Id == id && d.IsActive == true)
                .Fetch(x => x.CreatedBy).Eager
                 .TransformUsing(new DistinctRootEntityResultTransformer())
                .List();
            var patients = this.persistence.QueryOver<Patient>()
                .Where(p => p.IsActive == true)
                .List();
            var taxons = this.persistence.QueryOver<Taxon>()
                .Where(t => t.IsActive == true)
                .OrderBy(t => t.Parent).Asc
                .ThenBy(x => x.Weight).Asc
                .JoinQueryOver<Taxonomy>(t => t.Taxonomy)
                    .Where(t => t.MachineName == "DEL")
                    .And(t => t.IsActive == true)
                .List();
            var map = new Dictionary<Taxon, IList<Taxon>>();
            foreach (var taxon in taxons)
            {
                if (taxon.Parent.IsNull())
                {
                    map.Add(taxon, new List<Taxon>());
                }
                else
                {
                    if (map.ContainsKey(taxon.Parent))
                    {
                        map[taxon.Parent].Add(taxon);
                    }
                }
            }
            var flattenedStructure = new Dictionary<Guid, Taxon>(taxons.ToDictionary(x => x.Id, x => x));
            var delegationMap = new Dictionary<string, IList<Delegation>>();
            foreach (var delegation in delegations)
            {
                var name = flattenedStructure[delegation.Taxon.Id].Parent.Name;
                if (delegationMap.ContainsKey(name))
                {
                    delegationMap[name].Add(delegation);
                }
                else
                {
                    delegationMap.Add(name, new List<Delegation>() { delegation });
                }
            }
            var knowledgeTestMap = new Dictionary<string, IList<KnowledgeTest>>();
            var knowledgeTests = this.persistence.QueryOver<KnowledgeTest>()
                    .Where(d => d.Account.Id == id && d.IsActive == true)
                .List();
            foreach (var knowledgeTest in knowledgeTests)
            {
                if (flattenedStructure.ContainsKey(knowledgeTest.DelegationTaxon.Id))
                {
                    var name = flattenedStructure[knowledgeTest.DelegationTaxon.Id].Name;
                    if (knowledgeTestMap.ContainsKey(name))
                    {
                        knowledgeTestMap[name].Add(knowledgeTest);
                    }
                    else
                    {
                        knowledgeTestMap.Add(name, new List<KnowledgeTest>() { knowledgeTest });
                    }
                }
            }
            var taxonomy = this.taxonomyService.List(TaxonomicSchema.Organization);
            this.auditing.Read("läste delegeringar för användare {0}", account.FullName);
            return View(new DelegationListViewModel
            {
                AccountId = account.Id,
                Account = MapToPatientViewModel(taxonomy, account),
                DelegationMap = delegationMap.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value),
                KnowledgeTestMap = knowledgeTestMap,
                Patients = patients,
                Template = map
            });
        }

        #endregion

        #region Create View.

        /// <summary>
        /// Returns the create delegation view.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Create/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.CreateValue)]
        public ActionResult Create(Guid id)
        {
            var filterT = this.filtering.GetCurrentFilter();
            var account = this.persistence.Get<Account>(id);
            var patients = this.persistence.QueryOver<Patient>()
                .Where(p => p.IsActive == true)
                .And(p => p.Deceased == false)
                .OrderBy(x => x.LastName).Asc
                .ThenBy(x => x.FirstName).Asc
                .JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                    .IsLike(filterT.Id.ToString(), MatchMode.Anywhere))
                .List();
            var taxons = this.persistence.QueryOver<Taxon>()
                .Where(t => t.IsActive == true)
                .OrderBy(t => t.Parent).Asc
                .ThenBy(x => x.Weight).Asc
                .ThenBy(x => x.Name).Asc
                .JoinQueryOver<Taxonomy>(t => t.Taxonomy)
                .Where(t => t.MachineName == "DEL")
                .And(t => t.IsActive == true)
                .List();
            var patientTaxons = this.persistence.QueryOver<Delegation>()
                .Where(x => x.Account == account)
                .And(x => x.IsActive == true && x.IsGlobal == true)
                .List();
            var existingDelegations = new HashSet<Guid>(patientTaxons.Select(x => x.Taxon.Id));
            var delegationTypes = new List<SelectListItem>();
            var map = new Dictionary<Taxon, IList<Taxon>>();
            foreach (var taxon in taxons)
            {
                if (taxon.Parent.IsNull())
                {
                    delegationTypes.Add(new SelectListItem
                    {
                        Text = taxon.Name,
                        Value = taxon.Id.ToString()
                    });
                    map.Add(taxon, new List<Taxon>());
                }
                else
                {
                    if (map.ContainsKey(taxon.Parent))
                    {
                        map[taxon.Parent].Add(taxon);
                    }
                }
            }
            map = map.OrderBy(x => x.Value.FirstOrDefault().Weight).ToDictionary(x => x.Key, x => x.Value);
            return View(
                new DelegationViewModel
                {
                    AccountId = id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365),
                    DelegationTemplate = map,
                    DelegationsTaken = existingDelegations,
                    DelegationTypes = delegationTypes,
                    PatientItems = patients.Select(p => new SelectListItem
                    {
                        Text = p.FullName,
                        Value = p.Id.ToString()
                    }).ToList(),
                    Taxons = TaxonomyHelper.SelectList(this.taxonomyService.List(TaxonomicSchema.Organization))
                }
            );
        }

        /// <summary>
        /// Creates a delegation if valid.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="model">The delegation model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost]
        [Route("Create/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.CreateValue)]
        public ActionResult Create(Guid id, DelegationViewModel model)
        {
            var user = Identity();
            var account = this.persistence.Get<Account>(id);
            if (ModelState.IsValid)
            {
                var patients = new List<Patient>();
                foreach (string guid in model.Patients)
                {
                    Guid patientId;
                    if (Guid.TryParse(guid, out patientId))
                    {
                        var patient = this.persistence.Get<Patient>(patientId);
                        if (! patients.Contains(patient))
                        {
                            patients.Add(patient);
                        }
                    }
                }
                var root = this.taxonomyService.Roots(TaxonomicSchema.Organization).FirstOrDefault();
                var orgTaxon = model.Taxon.IsNotNull() ? this.persistence.Get<Taxon>(new Guid(model.Taxon)) : this.persistence.Get<Taxon>(root.Id);
                if (!model.CreateNew)
                {
                    foreach (Guid delegation in model.Delegations)
                    {
                        var taxon = this.persistence.Get<Taxon>(delegation);
                        var del = new Delegation
                        {
                            Account = account,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            Name = taxon.Name,
                            Pending = true,
                            Patients = patients,
                            OrganisationTaxon = orgTaxon,
                            Taxon = taxon,
                            CreatedBy = user,
                            IsGlobal = (model.Patients.IsNotNull() && model.Patients.Count() > 0) ? false : true
                        };
                        this.persistence.Save(del);
                        this.auditing.Create(
                            "lade till delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för patient/patienter {4} för användare {5} (REF: {6}).",
                            del.Name,
                            del.StartDate, 
                            del.EndDate,
                            del.Id,
                            del.IsGlobal ? "alla" : string.Join(",", del.Patients.ToArray().Select(x => x.FullName)),
                            del.Account.FullName,
                            del.Account.Id);
                    }
                }
                else
                {
                    Guid taxon;
                    if (Guid.TryParse(model.DelegationType, out taxon))
                    {
                        var node = this.persistence.Get<Taxon>(taxon);
                        var taxonToInsert = new Taxon
                        {
                            Parent = node,
                            Path = node.Path,
                            Name = model.Delegation,
                            Taxonomy = node.Taxonomy,
                            Weight = 0
                        };
                        this.persistence.Save(taxonToInsert);
                        this.persistence.Save(new Delegation
                        {
                            Account = account,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            Name = model.Delegation,
                            Pending = true,
                            Patients = patients,
                            OrganisationTaxon = orgTaxon,
                            IsGlobal = (model.Patients.IsNotNull() && model.Patients.Count() > 0) ? false : true,
                            CreatedBy = user,
                            Taxon = taxonToInsert
                        });
                    }

                }
                return this.RedirectToAction("List", new { Id = id });
            }
            var p = this.persistence.QueryOver<Patient>()
                .Where(x => x.IsActive == true)
                .List();
            var taxons = this.persistence.QueryOver<Taxon>()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Parent).Asc
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                .Where(x => x.MachineName == "DEL")
                .And(x => x.IsActive == true)
                .List();
            var patientTaxons = this.persistence.QueryOver<Delegation>()
                .Where(x => x.Account == account)
                .And(x => x.IsActive == true)
                .List();
            var existingDelegations = new HashSet<Guid>(patientTaxons.Select(x => x.Taxon.Id));
            var delegationTypes = new List<SelectListItem>();
            var map = new Dictionary<Taxon, IList<Taxon>>();
            foreach (var taxon in taxons)
            {
                if (taxon.Parent.IsNull())
                {
                    delegationTypes.Add(new SelectListItem
                    {
                        Text = taxon.Name,
                        Value = taxon.Id.ToString()
                    });
                    map.Add(taxon, new List<Taxon>());
                }
                else
                {
                    if (map.ContainsKey(taxon.Parent))
                    {
                        map[taxon.Parent].Add(taxon);
                    }
                }
            }
            model.AccountId = id;
            model.DelegationTypes = delegationTypes;
            model.DelegationTemplate = map;
            model.DelegationsTaken = existingDelegations;
            model.PatientItems = p.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            }).ToList();
            return View(model);
        }

        #endregion

        #region Report View.

        /// <summary>
        /// Returns the delegation report view.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="tId">Optional taxon id filter</param>
        /// <param name="sId">Optional schedule settings filter</param>
        /// <param name="startDate">Optional start date - defaults to now</param>
        /// <param name="endDate">Optional end date - defaults to last instant of today</param>
        /// <param name="page">Optional page - defaults to 1</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("DelegationReport/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.ReportValue)]
        public ActionResult DelegationReport(Guid id, Guid? tId, Guid? sId, DateTime? startDate, DateTime? endDate, int? page = 1)
        {
            var account = this.persistence.Get<Account>(id);
            var taxons = this.taxonomyService.List(TaxonomicSchema.Organization);
            startDate = (startDate.HasValue) ? startDate.Value : DateTimeUtilities.Now().AddDays(-DateTimeUtilities.Now().DaysInMonth());
            endDate = (endDate.HasValue) ? endDate.Value.LastInstantOfDay() : DateTimeUtilities.Now().LastInstantOfDay();
            var previousPeriodStart = startDate.GetValueOrDefault().AddDays(-endDate.Value.Subtract(startDate.Value).Days);
            var previousPeriodEnd = startDate.GetValueOrDefault().AddDays(-1);
            return View(new DelegationReportViewModel
            {
                StartDate = startDate.Value.Date,
                EndDate = endDate.Value.Date,
                AccountId = account.Id,
                Account = MapToPatientViewModel(taxons, account),
                Report = this.reports.GetReportData(new ChartDataFilter
                {
                    Account = account.Id,
                    StartDate = startDate.GetValueOrDefault(),
                    EndDate = endDate.GetValueOrDefault(),
                    ScheduleSetting = sId
                }),
                PreviousPeriodReport = this.reports.GetReportData(new ChartDataFilter
                {
                    Account = account.Id,
                    StartDate = previousPeriodStart,
                    EndDate = previousPeriodEnd,
                    ScheduleSetting = sId
                }),
                Tasks = this.tasks.List(new ListTaskModel 
                {
                    StartDate = startDate.GetValueOrDefault(),
                    EndDate = endDate.GetValueOrDefault(),
                    Account = account.Id
                }, page.GetValueOrDefault(1), 30),
                DelegationId = tId,
                Delegations = this.taxonomyService.ListChildren(TaxonomicSchema.Delegation).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                Schedule = sId,
                Schedules = this.persistence.QueryOver<ScheduleSettings>().Where(x => x.IsActive == true).List(),
            });

        }

        /// <summary>
        /// Posted delegation report.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="model">The delegation report model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost]
        [Route("DelegationReport/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.ReportValue)]
        public ActionResult DelegationReport(Guid id, DelegationReportViewModel model)
        {
            return DelegationReport(id, model.DelegationId, model.Schedule, model.StartDate, model.EndDate);
        }

        /// <summary>
        /// Generates JSON chart code for delegations.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="tId">Optional taxon id filter</param>
        /// <param name="sId">Optional schedule settings filter</param>
        /// <param name="startDate">Optional start date - defaults to now</param>
        /// <param name="endDate">Optional end date - defaults to last instant of today</param>
        /// <returns><see cref="ActionResult"/></returns>
        /*[HttpGet]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult Chart(Guid id, Guid? tId, Guid? sId, DateTime startDate, DateTime endDate)
        {
            return Json(ExecuteCommand<List<object[]>>(new CreateChartCommand<DelegationReportFilter>
            {
                StartDate = startDate,
                EndDate = endDate,
                Filter = new DelegationReportFilter
                {
                    AccountId = id,
                    TaxonId = tId,
                    ScheduleSettingsId = sId
                }
            }), JsonRequestBehavior.AllowGet);
        }*/

        /// <summary>
        /// Returns an Excel document with all delegations for this account.
        /// </summary>
        /// <param name="account">The account id</param>
        /// <param name="taxon">Optional taxon id filter</param>
        /// <param name="sId">Optional schedule settings filter</param>
        /// <param name="startDate">Optional start date - defaults to now</param>
        /// <param name="endDate">Optional end date - defaults to last instant of today</param>
        /// <returns><see cref="FileContentResult"/></returns>
        [HttpGet]
        [Route("Excel/{account:guid}")]
        [PermissionsAttribute(Permissions.Delegation.ReportValue)]
        public FileContentResult Excel(Guid account, Guid? taxon, Guid? sId, DateTime startDate, DateTime endDate)
        {
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.IsActive == true)
                .And(x => x.OnNeedBasis == false)
                .And(x => x.UpdatedAt >= startDate)
                .And(x => x.UpdatedAt <= endDate.LastInstantOfDay())
                .Fetch(x => x.Patient).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer())
                .OrderBy(x => x.UpdatedAt).Desc;
            new DelegationReportFilter
            {
                AccountId = account,
                TaxonId = taxon,
                ScheduleSettingsId = sId
            }.Filter(query);
            var tasks = query.List();
            var bytes = ExcelWriter.CreateNew<Task, ExcelTaskModel>(
                PathResolver.ResolveAppRelativePath("\\Templates\\Template.xls"),
                x => new ExcelTaskModel
                {
                    Task = x.Name,
                    TaskCompletedOnDate = x.UpdatedAt.Date,
                    TaskCompletedOnTime = (x.Delayed && x.CompletedBy.IsNull()) ? "Ej signerad" : string.Format("{0} {1:HH:mm}", "kl", x.UpdatedAt),
                    TaskScheduledOnDate = x.Scheduled.Date,
                    TaskScheduledOnTime = string.Format("{0} {1:HH:mm}", "kl", x.Scheduled),
                    MinutesBefore = x.RangeInMinutesBefore,
                    MinutesAfter = x.RangeInMinutesAfter,
                    PatientFullName = x.Patient.FullName,
                    CompletedBy = x.CompletedBy.IsNotNull() ? x.CompletedBy.FullName : "",
                    TaskCompletionStatus = Status(x)
                },
                tasks);
            ITenantIdentity identity;
            this.tenantService.TryIdentifyTenant(out identity);
            return File(bytes, "application/vnd.ms-excel",
                string.Format("Rapport-{0}-{1}.xls", identity.Name.Replace(" ", "-"),
                DateTime.Now.ToFileTimeUtc()));
        }

        /// <summary>
        /// Returns status by <c>Task</c>.
        /// FIXME: REFACTOR AWAY!
        /// </summary>
        /// <param name="task">The task</param>
        /// <returns>A string representation of the task status</returns>
        private string Status(Task task)
        {
            if (task.StatusTaxon != null)
            {
                if (task.Delayed && task.StatusTaxon.Weight < 2)
                {
                    return string.Format("{0} för sent", task.StatusTaxon.Name);
                }
                else
                {
                    return task.StatusTaxon.Name;
                }
            }
            if (task.Status.Equals(1))
            {
                if (task.Delayed)
                {
                    return "Given för sent";
                }
                return "OK";
            }
            else if (task.Status.Equals(2))
            {
                return "Delvis given";
            }
            else if (task.Status.Equals(3))
            {
                return "Ej given";
            }
            else if (task.Status.Equals(4))
            {
                return "Kan ej ta";
            }
            else if (task.Status.Equals(5))
            {
                return "Medskickad";
            }
            else if (task.Status.Equals(6))
            {
                return "Räknad mängd stämmer ej med saldo";
            }
            if (task.Status.Equals(0) || task.Delayed)
            {
                if (task.DelayHandled)
                {
                    return "Larm åtgärdat";
                }
                return "Ej given";
            }
            return string.Empty;
        }

        #endregion

        #region Edit View.

        /// <summary>
        /// Returns the delegation edit view.
        /// </summary>
        /// <param name="id">The delegation id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Edit/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult Edit(Guid id)
        {
            var filterT = this.filtering.GetCurrentFilter();
            var delegation = this.persistence.Get<Delegation>(id);
            var patients = this.persistence.QueryOver<Patient>()
                .Where(p => p.IsActive == true)
                .And(p => p.Deceased == false)
                .OrderBy(x => x.LastName).Asc
                .ThenBy(x => x.FirstName).Asc
                .JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                    .IsLike(filterT.Id.ToString(), MatchMode.Anywhere))
                .List();
            return View(new DelegationEditViewModel
            {
                Id = delegation.Id,
                StartDate = delegation.StartDate,
                EndDate = delegation.EndDate,
                ConnectedPatients = delegation.Patients.ToList(),
                PatientItems = patients.Select(x => new SelectListItem
                {
                    Text = string.Format("{0} {1}", x.FirstName, x.LastName),
                    Value = x.Id.ToString()
                }).ToList(),
                Taxons = TaxonomyHelper.SelectList(delegation.OrganisationTaxon, this.taxonomyService.List(TaxonomicSchema.Organization)),
                Taxon = delegation.OrganisationTaxon.IsNull() ? this.taxonomyService.Roots(TaxonomicSchema.Organization).First().Id.ToString() : delegation.OrganisationTaxon.Id.ToString()
            });
        }

        /// <summary>
        /// Updates a delegation if valid.
        /// </summary>
        /// <param name="id">The delegation id</param>
        /// <param name="model">The delegation model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost]
        [Route("Edit/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult Edit(Guid id, DelegationEditViewModel model)
        {
            var delegation = this.persistence.Get<Delegation>(id);
            var oldPatients = delegation.Patients;
            var rootId = this.taxonomyService.Roots(TaxonomicSchema.Organization).First().Id;
            var orgTaxon = model.Taxon.IsNotNull() ? this.persistence.Get<Taxon>(new Guid(model.Taxon)) : this.persistence.Get<Taxon>(rootId);
            foreach (var patient in delegation.Patients.ToArray<Patient>())
            {
                delegation.Patients.Remove(patient);
            }
            var patients = new List<Patient>();
            if (model.Patients != null && model.Patients.Count() > 0)
            {
                foreach (string guid in model.Patients)
                {
                    Guid patientId;
                    if (Guid.TryParse(guid, out patientId))
                    {
                        Patient patient = this.persistence.Get<Patient>(patientId);
                        if (!patients.Contains(patient))
                        {
                            delegation.Add(patient);
                        }
                    }
                }
                delegation.IsGlobal = false;
            }
            else
            {
                delegation.IsGlobal = true;
            }
            this.persistence.Save(new ChangeSet
            {
                EntityId = delegation.Id,
                Entity = typeof(Delegation).ToString(),
                Revision = delegation.Version,
                ModifiedBy = Identity(),
                Changes = new List<Change> {
                    new Change {
                        Property = "StartDate",
                        OldState = delegation.StartDate.ToShortDateString(),
                        NewState = model.StartDate.ToShortDateString(),
                        TypeOf = typeof(DateTime).ToString()
                    },
                    new Change {
                        Property = "EndDate",
                        OldState = delegation.EndDate.ToShortDateString(),
                        NewState = model.EndDate.ToShortDateString(),
                        TypeOf = typeof(DateTime).ToString()
                    },
                    new Change {
                        Property = "Patients",
                        OldState = string.Join(",", oldPatients),
                        NewState = string.Join(",", patients),
                        TypeOf = typeof(Array).ToString()
                    },
                    new Change {
                        Property = "OrganisationTaxon",
                        OldState = delegation.Taxon.ToString(),
                        NewState = orgTaxon.ToString(),
                        TypeOf = typeof(Taxon).ToString()
                    },
                    new Change {
                        Property = "CreatedBy",
                        OldState = delegation.CreatedBy.ToString(),
                        NewState = Identity().ToString(),
                        TypeOf = typeof(Account).ToString()
                    }
                }
            });
            delegation.StartDate = model.StartDate;
            delegation.EndDate = model.EndDate;
            delegation.UpdatedAt = DateTime.Now;
            delegation.OrganisationTaxon = orgTaxon;
            delegation.CreatedBy = Identity();
            this.persistence.Update(delegation);
            var currentUser = Identity();
            this.auditing.Update(
                "ändrade delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för patient/patienter {4} för användare {5} (REF: {6}).",
                delegation.Name,
                delegation.StartDate, 
                delegation.EndDate,
                delegation.Id,
                delegation.IsGlobal ? "alla" : string.Join(",", delegation.Patients.ToArray().Select(x => x.FullName)),
                delegation.Account.FullName,
                delegation.Account.Id);
            return this.RedirectToAction("List", new { Id = delegation.Account.Id });

        }

        #endregion

        #region Activate.

        /// <summary>
        /// Activates a delegation by id.
        /// </summary>
        /// <param name="id">The delegation id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Activate/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult Activate(Guid id)
        {
            var delegation = this.persistence.Get<Delegation>(id);
            delegation.Pending = false;
            delegation.UpdatedAt = DateTime.Now;
            this.persistence.Update(delegation);
            var currentUser = Identity();
            this.auditing.Update(
                "aktiverade delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för användare {4} (REF: {5}).",
                delegation.Name,
                delegation.StartDate, 
                delegation.EndDate,
                delegation.Id,
                delegation.Account.FullName,
                delegation.Account.Id);
            return this.RedirectToAction("List", new { Id = delegation.Account.Id });
        }

        #endregion

        #region Activate All.

        /// <summary>
        /// Activates all delegations for an account.
        /// </summary>
        /// <param name="accountId">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("ActivateAll/{accountId:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult ActivateAll(Guid accountId)
        {
            var currentUser = Identity();
            var delegations = this.persistence.QueryOver<Delegation>()
                .Where(x => x.IsActive)
                .And(x => x.Pending)
                .JoinQueryOver<Account>(x => x.Account)
                    .Where(x => x.Id == accountId)
                .List();
            foreach (var delegation in delegations)
            {
                delegation.Pending = false;
                delegation.UpdatedAt = DateTime.Now;
                this.persistence.Update(delegation);
                this.auditing.Update(
                    "aktiverade delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för användare {4} (REF: {5}).",
                    delegation.Name,
                    delegation.StartDate, 
                    delegation.EndDate,
                    delegation.Id,
                    delegation.Account.FullName,
                    delegation.Account.Id);
            }
            return this.RedirectToAction("List", new { Id = accountId });
        }

        #endregion

        #region Inactivate.

        /// <summary>
        /// Returns a inactivate delegation view. 
        /// </summary>
        /// <param name="id">The delegation id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("Inactivate/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult Inactivate(Guid id)
        {
            var delegation = this.persistence.Get<Delegation>(id);
            return View(new DelegationInactivateViewModel
            {
                Delegation = delegation
            });
        }

        /// <summary>
        /// Inactivates a delegation by id.
        /// TODO: post parameter is not used - is it just to differentiate?
        /// </summary>
        /// <param name="id">The delegation id</param>
        /// <param name="post">Not used</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost]
        [Route("Inactivate/{id:guid}")]
        public ActionResult Inactivate(Guid id, bool post)
        {
            var delegation = this.persistence.Get<Delegation>(id);
            delegation.IsActive = false;
            delegation.UpdatedAt = DateTime.Now;
            this.persistence.Update(delegation);
            var currentUser = Identity();
            this.auditing.Update(
                "inaktiverade delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för användare {4} (REF: {5}).",
                delegation.Name,
                delegation.StartDate, 
                delegation.EndDate,
                delegation.Id,
                delegation.Account.FullName,
                delegation.Account.Id);
            return this.RedirectToAction("List", new { Id = delegation.Account.Id });
        }

        #endregion

        #region Print

        /// <summary>
        /// Returns the delegation print view.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("DelegationPrint/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.ReadValue)]
        public ActionResult DelegationPrint(Guid id)
        {
            var account = this.persistence.QueryOver<Account>()
                .Where(x => x.Id == id)
                .Fetch(x => x.Roles).Eager.SingleOrDefault();
            var delegations = new List<Delegation>();
            foreach (var delegation in account.Delegations)
            {
                if (delegation.IsActive)
                {
                    delegations.Add(delegation);
                }
            }
            var knowledgeTests = this.persistence.QueryOver<KnowledgeTest>()
                .Where(x => x.IsActive == true)
                .And(x => x.Account == account)
                .List();
            var user = Identity();
            this.auditing.Read(
                "skapade utskrift för delegeringar för användare {0} (REF: {1}).",
                    account.FullName,
                    account.Id);
            return View(new DelegationPrintViewModel
            {
                Account = account,
                AccountId = account.Id,
                Delegations = delegations,
                KnowledgeTests = knowledgeTests,
                Identity = Identity()
            });
        }

        #endregion

        #region Update

        /// <summary>
        /// Returns the update view.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="taxonId">The taxon id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Update/{id:guid}/{taxonId:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult Update(Guid id, Guid taxonId)
        {
            return View(new DelegationDateSpanViewModel
            {
                Id = id,
                TaxonId = taxonId
            });
        }

        /// <summary>
        /// Updates all active delegations with new start/end dates.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="taxonId">The taxon id</param>
        /// <param name="model">The delegation date span model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("Update/{id:guid}/{taxonId:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult Update(Guid id, Guid taxonId, DelegationDateSpanViewModel model)
        {
            if (ModelState.IsValid)
            {
                return this.RedirectToAction("UpdateAllActiveDelegationsWithStartEndDate", new
                {
                    Id = id,
                    TaxonId = taxonId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                });
            }
            return View(model);
        }

        /// <summary>
        /// Updates all active delegations with new start/end dates.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="taxonId">The taxon id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("UpdateAllActiveDelegationsWithStartEndDate/{id:guid}/{taxonId:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult UpdateAllActiveDelegationsWithStartEndDate(Guid id, Guid taxonId, DateTime startDate, DateTime endDate)
        {
            var currentUser = Identity();
            var taxon = this.persistence.Get<Taxon>(taxonId);
            var delegations = this.persistence.QueryOver<Delegation>()
                .Where(x => x.Account.Id == id)
                .JoinQueryOver<Taxon>(x => x.Taxon)
                .Where(x => x.Parent.Id == taxon.Parent.Id)
                .List();
            foreach (var delegation in delegations)
            {
                this.persistence.Save(new ChangeSet
                {
                    EntityId = delegation.Id,
                    Entity = typeof(Delegation).ToString(),
                    Revision = delegation.Version,
                    ModifiedBy = Identity(),
                    Changes = new List<Change> {
                        new Change {
                            Property = "StartDate",
                            OldState = delegation.StartDate.ToShortDateString(),
                            NewState = startDate.ToShortDateString(),
                            TypeOf = typeof(DateTime).ToString()
                        },
                        new Change {
                            Property = "EndDate",
                            OldState = delegation.EndDate.ToShortDateString(),
                            NewState = endDate.ToShortDateString(),
                            TypeOf = typeof(DateTime).ToString()
                        },
                        new Change {
                            Property = "CreatedBy",
                            OldState = delegation.CreatedBy.ToString(),
                            NewState = Identity().ToString(),
                            TypeOf = typeof(Account).ToString()
                        }
                    }
                });
                delegation.StartDate = startDate;
                delegation.EndDate = endDate;
                delegation.UpdatedAt = DateTime.Now;
                delegation.CreatedBy = Identity();
                this.persistence.Update(delegation);
                this.auditing.Update(
                    "förnyade start och slutdatum för delegering {0} ({1:yyyy-MM-dd} - {2:yyyy-MM-dd} REF: {3}) för användare {4} (REF: {5}).",
                    delegation.Name,
                    delegation.StartDate, 
                    delegation.EndDate,
                    delegation.Id,
                    delegation.Account.FullName,
                    delegation.Account.Id);
            }
            return this.RedirectToAction("List", new { Id = id });
        }

        #endregion

        #region Add Knowledge Test.

        /// <summary>
        /// Returns the knowledge test view.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="taxonId">The taxon id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("AddKnowledgeTest/{id:guid}/{taxonId:guid}")]
        [PermissionsAttribute(Permissions.Delegation.CreateValue)]
        public ActionResult AddKnowledgeTest(Guid id, Guid taxonId)
        {
            return View(new KnowledgeTestFormModel
            {
                AccountId = id,
                TaxonId = taxonId
            });
        }

        /// <summary>
        /// Adds a new knowledge test.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="taxonId">The taxon id</param>
        /// <param name="model">The knowledge test model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("AddKnowledgeTest/{id:guid}/{taxonId:guid}")]
        [PermissionsAttribute(Permissions.Delegation.CreateValue)]
        public ActionResult AddKnowledgeTest(Guid id, Guid taxonId, KnowledgeTestFormModel model)
        {
            if (ModelState.IsValid)
            {
                var test = new KnowledgeTest
                {
                    DelegationTaxon = this.persistence.Get<Taxon>(taxonId),
                    Account = this.persistence.Get<Account>(id),
                    Name = model.Name,
                    CompletedDate = model.CompletedDate
                };
                this.persistence.Save(test);
                return this.RedirectToAction("List", new { Id = id });
            }
            return this.RedirectToAction("AddKnowledgeTest", new { Id = id, TaxonId = taxonId });
        }

        #endregion

        #region Delete Knowledge Test.

        /// <summary>
        /// Deletes a knowledge test by id.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="knowledgeTestId">The knowledge test id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("DeleteKnowledgeTest/{id:guid}/{knowledgeTestId:guid}")]
        [PermissionsAttribute(Permissions.Delegation.DeleteValue)]
        public ActionResult DeleteKnowledgeTest(Guid id, Guid knowledgeTestId)
        {
            var knowledgeTest = this.persistence.Get<KnowledgeTest>(knowledgeTestId);
            knowledgeTest.IsActive = false;
            this.persistence.Update(knowledgeTest);
            return this.RedirectToAction("List", new { Id = id });
        }

        #endregion

        #region Issued Delegations

        /// <summary>
        /// TODO: ?
        /// TODO: Lower case parameter History.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="History">Optional is history or not</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Issued/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.IssuedValue)]
        public ActionResult Issued(Guid id, bool History = false)
        {
            var account = this.persistence.Get<Account>(id);
            var delegations = this.persistence.QueryOver<Delegation>()
                .Where(x => x.CreatedBy.Id == id);
            if (History)
            {
                delegations.Where(x => !x.IsActive);
            }
            else
            {
                delegations.Where(x => x.IsActive);
            }

            return View(new IssuedDelegationsViewModel
            {
                AccountId = id,
                Account = this.transformer.ToAccount(account),
                Delegations = delegations.Fetch(x => x.CreatedBy).Eager
                    .TransformUsing(new DistinctRootEntityResultTransformer())
                    .List(),
                History = History
            });
        }

        #endregion

        #region Overview

        /// <summary>
        /// Returns the partial dashboard overview. 
        /// </summary>
        /// <returns><see cref="PartialViewResult"/></returns>
        [Route("overview")]
        [PermissionsAttribute(Permissions.Dashboard.ReadDelegationValue)]
        public PartialViewResult Overview()
        {
            var taxon = this.filtering.GetCurrentFilter();
            var fiftyDaysFromNow = DateTime.Today.AddDays(50);
            var delegations = this.persistence.QueryOver<Delegation>()
                .Where(x => x.IsActive == true && x.Pending == false)
                .And(x => x.EndDate <= fiftyDaysFromNow)
                .OrderBy(o => o.EndDate).Asc
                    .JoinQueryOver<Account>(x => x.Account)
                        .Where(x => x.IsActive == true)
                    .JoinQueryOver<Taxon>(x => x.Taxon)
                        .WhereRestrictionOn(x => x.Path).IsLike(taxon.Id.ToString(), MatchMode.Anywhere)
                .List();
            var delegationsExpired = from x in delegations
                                     where x.EndDate.Subtract(DateTimeUtilities.Now()).Days < 0
                                     group x by x.Account into gr
                                     select new DelegationExpired
                                     {
                                         Id = gr.Key.Id,
                                         FullName = gr.Key.FullName,
                                         DaysLeft = gr.Min(x => x.EndDate.Subtract(DateTimeUtilities.Now()).Days)
                                     };
            var delegationsExpiresWithin50Days = from x in delegations
                                                 where x.EndDate.Subtract(DateTimeUtilities.Now()).Days >= 0
                                                 group x by x.Account into gr
                                                 select new DelegationExpired
                                                 {
                                                     Id = gr.Key.Id,
                                                     FullName = gr.Key.FullName,
                                                     DaysLeft = gr.Min(x => x.EndDate.Subtract(DateTimeUtilities.Now()).Days)
                                                 };
            return PartialView(new DelegationOverviewViewModel
            {
                DelegationsExpired = delegationsExpired,
                DelegationsExpiresWithin50Days = delegationsExpiresWithin50Days
            });
        }

        #endregion

        /// <summary>
        /// Returns the delegation revisions.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="date">Optional date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Revision/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.RevisionValue)]
        public ActionResult Revision(Guid id, DateTime? date)
        {
            var account = this.persistence.Get<Account>(id);
            var delegationIds = account.Delegations.Select(x => x.Id).ToArray();
            var query = this.persistence.QueryOver<ChangeSet>()
                .WhereRestrictionOn(x => x.EntityId).IsIn(delegationIds);
            if (date.HasValue)
            {
                query.Where(x => x.CreatedAt >= date.Value && x.CreatedAt <= date);
            }
            return View(new DelegationRevisionViewModel
            {
                AccountId = id,
                Account = this.transformer.ToAccount(account),
                ChangeSets = query.List(),
                Date = date
            });
        }

        /// <summary>
        /// Returns a specific change set.
        /// </summary>
        /// <param name="id">The change set id</param>
        /// <param name="accountId">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Changeset/{id:guid}/{accountId:guid}")]
        [PermissionsAttribute(Permissions.Delegation.RevisionValue)]
        public ActionResult Changeset(Guid id, Guid accountId)
        {
            var change = this.persistence.Get<ChangeSet>(id);
            var delegation = this.persistence.Get<Delegation>(change.EntityId);
            foreach (var changeset in change.Changes)
            {
            }
            var account = this.persistence.Get<Account>(accountId);
            return View(new DelegationRevisionViewModel
            {
                AccountId = accountId,
                Account = this.transformer.ToAccount(account),
                ChangeSet = change,
                Delegation = delegation
            });
        }

        /// <summary>
        /// Mapper for patient view.
        /// </summary>
        /// <param name="taxons">A list of taxons</param>
        /// <param name="account">The account</param>
        /// <returns><see cref="AccountViewModel"/></returns>
        private AccountViewModel MapToPatientViewModel(IList<ITaxon> taxons, Account account)
        {
            var retval = new List<AccountViewModel>();
            var superiors = GetSuperiors();
            var taxonMap = new Dictionary<string, ITaxon>(taxons.ToDictionary(x => x.Id.ToString(), x => x));
            var taxon = taxonMap[account.Taxon.Id.ToString()];
            var superiorList = superiors.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
            var superior = (superiorList.Count() > 0) ? superiorList.First() : null;
            return new AccountViewModel
            {
                Id = account.Id,
                Active = account.IsActive,
                FullName = account.FullName,
                UniqueIdentifier = account.PersonalIdentityNumber,
                Title = account.Title,
                Superior = (superior.IsNotNull()) ? superior.FullName : "Saknas",
                Account = account
            };
        }

        /// <summary>
        /// Returns the accounts which have a superior role.
        /// </summary>
        /// <returns><see cref="IList{Account}"/></returns>
        private IList<Account> GetSuperiors()
        {
            return this.persistence.QueryOver<Account>()
                .Where(x => x.IsActive == true)
                .JoinQueryOver<Role>(x => x.Roles)
                .Where(x => x.MachineName == "_superioraccount")
                .List();
        }

        
    }
}