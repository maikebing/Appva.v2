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
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mvc;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;

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
            this.tenantService = tenantService;
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
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Delegation.ReadValue)]
        public ActionResult List(ListDelegation request)
        {
            return View();
        }

        #endregion

        #region Create View.

        /// <summary>
        /// Returns the create delegation view.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Create/{id:guid}")]
        [Dispatch]
        [PermissionsAttribute(Permissions.Delegation.CreateValue)]
        public ActionResult Create(CreateDelegation request)
        {
            return View();
        }

        /// <summary>
        /// Creates a delegation if valid.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="model">The delegation model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost]
        [Route("Create/{id:guid}")]
        [ValidateAntiForgeryToken, Validate, Dispatch("list","delegation")]
        [PermissionsAttribute(Permissions.Delegation.CreateValue)]
        public ActionResult Create(CreateDelegationModel request)
        {
            return View();
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
            var scheduleSettings = TaskService.GetAllRoleScheduleSettingsList(account);
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
                    ScheduleSetting = sId,
                    Organisation = this.filtering.GetCurrentFilter().Id
                }),
                /*PreviousPeriodReport = this.reports.GetReportData(new ChartDataFilter
                {
                    Account = account.Id,
                    StartDate = previousPeriodStart,
                    EndDate = previousPeriodEnd,
                    ScheduleSetting = sId,
                    Organisation = this.filtering.GetCurrentFilter().Id
                }),*/
                Tasks = this.tasks.List(new ListTaskModel 
                {
                    StartDate = startDate.GetValueOrDefault(),
                    EndDate = endDate.GetValueOrDefault(),
                    AccountId = account.Id,
                    ScheduleSettingId = sId,
                    TaxonId = this.filtering.GetCurrentFilter().Id
                }, page.GetValueOrDefault(1), 30),
                DelegationId = tId,
                Delegations = this.taxonomyService.ListChildren(TaxonomicSchema.Delegation).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                Schedule = sId,
                Schedules = scheduleSettings,
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
                PathResolver.ResolveAppRelativePath("Templates\\Template.xls"),
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
        [Route("update/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        [Dispatch]
        public ActionResult Update(Identity<UpdateDelegationModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates a delegation if valid.
        /// </summary>
        /// <param name="id">The delegation id</param>
        /// <param name="model">The delegation model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost]
        [Route("update/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        [Dispatch("list","delegation")]
        public ActionResult Update(UpdateDelegationModel request)
        {
            return this.View();
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
        [Route("print/{id:guid}")]
        [PermissionsAttribute(Permissions.Delegation.ReadValue)]
        [Dispatch]
        public ActionResult Print(Identity<PrintDelegationModel> request)
        {
            return this.View();
        }

        #endregion

        #region Renew

        /// <summary>
        /// Returns the update view.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="taxonId">The taxon id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("renew/{id:guid}/{DelegationCategoryId:guid}")]
        [Dispatch]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult Renew(RenewDelegations request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates all active delegations with new start/end dates.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="taxonId">The taxon id</param>
        /// <param name="model">The delegation date span model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("renew/{accountId:guid}/{delegationCategoryId:guid}")]
        [Dispatch("List", "Delegation")]
        [PermissionsAttribute(Permissions.Delegation.UpdateValue)]
        public ActionResult Renew(RenewDelegationsModel request)
        {
            return this.View();
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
        [Dispatch(typeof(Parameterless<DelegationOverviewModel>))]
        public PartialViewResult Overview()
        {
            return this.PartialView();
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