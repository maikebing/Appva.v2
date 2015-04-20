using System;
using System.Collections.Generic;
using System.Linq;
using Appva.Mcss.Web.ViewModels;
using Appva.Mcss.Infrastructure;
using Appva.Mcss.Infrastructure.Mvc;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Business;
using Appva.Core.Extensions;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Appva.Mcss.Web.Controllers {

    public class CreateOverviewCommand : Command<HomeViewModel> {

        [AutoWired]
        public TaxonomyService TaxonomyService { get; set; }

        [AutoWired]
        public SettingService SettingService { get; set; }

        public Account Identity { get; set; }

        public Taxon Taxon { get; set; }

        public override void Execute() {
            //var roleService = new RoleService(Session);
            //var patientService = new PatientService(Session);
            //var sequenceService = new SequenceService(Session);
            //var organizationalStructure = TaxonomyService.Find(HierarchyUtils.Organization);
            //var accountsWithRoles = roleService.Roles("_overseeingaccount");
            //var flattenedStructure = new Dictionary<string, Taxon>(organizationalStructure.ToDictionary(x => x.Id.ToString(), x => x));
            //if (Taxon.IsNull()) {
            //    Taxon = organizationalStructure.First();
            //}
            Taxon = FilterCache.Get(Session);
            if (!FilterCache.HasCache()) {
                Taxon = FilterCache.GetOrSet(Identity, Session);
            }
            //var patients = patientService.FindByTaxon(Taxon, false);
            //var delegations = GetDelegations();
            //var sequences = sequenceService.FindByTaxon(Taxon, patients);

            //int stockControlInterval = 30;
            //Setting setting = Session.QueryOver<Setting>().Where(x => x.MachineName == "System.Inventory.StockControlInterval").SingleOrDefault();
            //if (setting.IsNotNull()) {
            //    stockControlInterval = System.Web.Helpers.Json.Decode<int>(setting.Value);
            //}
            //var stockControlDate = DateTimeExt.Now().AddDays(-stockControlInterval);
            //var stockCount = sequences.Where(x => x.LastStockAmountCalculation.HasValue == true &&
            //    x.LastStockAmountCalculation.Value.Subtract(stockControlDate).Days <= 7
            //    && x.LastStockAmountCalculation.Value.Subtract(stockControlDate).Days >= 0).ToList();
            //var test = stockCount.First();
            //var amount = DateTimeExt.Now().AddDays(-stockControlInterval).Subtract(test.LastStockAmountCalculation.Value).Days;
            //var amount2 = test.LastStockAmountCalculation.Value.Subtract(DateTimeExt.Now().AddDays(-stockControlInterval)).Days;
            //var delayedStockCount = sequences.Where(x => x.LastStockAmountCalculation.HasValue == true &&
            //    x.LastStockAmountCalculation.Value.Subtract(stockControlDate).Days < 0).ToList();
            
            //var delegationsExpired = delegations.Where(d => d.EndDate.Subtract(DateTimeExt.Now()).Days < 0).ToList();
            //var delegationsExpired = from x in delegations
            //          where x.EndDate.Subtract(DateTimeExt.Now()).Days < 0
            //          group x by x.Account into gr
            //          select new DelegationExpired{ Id = gr.Key.Id, FullName = gr.Key.FullName, DaysLeft = gr.Min(x => x.EndDate.Subtract(DateTimeExt.Now()).Days)  };

            //var delegationsExpiresWithin50Days = from x in delegations
            //          where x.EndDate.Subtract(DateTimeExt.Now()).Days >= 0
            //          group x by x.Account into gr
            //          select new DelegationExpired{ Id = gr.Key.Id, FullName = gr.Key.FullName, DaysLeft = gr.Min(x => x.EndDate.Subtract(DateTimeExt.Now()).Days)  };

            //var delegationsExpiresWithin50Days = delegations.Where(d => d.EndDate.Subtract(DateTimeExt.Now()).Days >= 0).GroupBy(x => x.Patients).ToList();
            Result = new HomeViewModel() {
                //RaisedAlertsForPatients = GetPatientTasks(accountsWithRoles, flattenedStructure, patients),
                //Taxon = Taxon,
                //Taxons = TaxonomyHelper.SelectList(Taxon, organizationalStructure),
                //StockCounts = stockCount.OrderBy(x => x.LastStockAmountCalculation).ToList(),
                //DelayedStockCounts = delayedStockCount.OrderBy(x => x.LastStockAmountCalculation).ToList(),
                //StockControlIntervalInDays = stockControlInterval,
                //DelegationsExpiresWithin50Days = delegationsExpiresWithin50Days,
                //DelegationsExpired = delegationsExpired,
                HasCalendarOverview = SettingService.HasCalendarOverview(),
                HasOrderOverview = SettingService.HasOrderRefill()
            };
        }

        //public IList<Delegation> GetDelegations(Account account, IList<Patient> patients) {
        //    Patient patientAlias = null;
        //    return Session.QueryOver<Delegation>()
        //        .Where(d => d.EndDate < DateTime.Today.AddDays(50))
        //        .And(x => x.CreatedBy.Id == account.Id)
        //        .OrderBy(o => o.EndDate).Asc
        //        .JoinQueryOver<Patient>(x => x.Patients, () => patientAlias)
        //        .WhereRestrictionOn(() => patientAlias.Id).IsIn(patients.Select(x => x.Id).ToArray())
        //        .List();
        //}

        public IList<Delegation> GetDelegations() {
            var fiftyDaysFromNow = DateTime.Today.AddDays(50);
            return Session.QueryOver<Delegation>()
                .Where(x => x.Active == true && x.Pending == false)
                .And(x => x.EndDate <= fiftyDaysFromNow)
                .OrderBy(o => o.EndDate).Asc
                    .JoinQueryOver<Account>(x => x.Account)
                        .Where(x => x.Active == true)
                    .JoinQueryOver<Taxon>(x => x.Taxon)
                        .WhereRestrictionOn(x => x.Path).IsLike(Taxon.Id.ToString(), MatchMode.Anywhere)
                .List();
        }

        public IList<PatientViewModel> GetPatientTasks(IList<Account> accounts, Dictionary<string, Taxon> taxonMap, IList<Patient> patients) {
            //return Session.QueryOver<Patient>()
            //    .JoinQueryOver<Task>(x => x.Tasks)
            //    .Where(x => x.Active == true)
            //    .And(x => x.Delayed == true)
            //    .And(x => x.DelayHandled == false)
            //    .JoinQueryOver<Taxon>(x => x.Taxon)
            //    .Where(Restrictions.On<Taxon>(x => x.Path)
            //    .IsLike(taxon.Id.ToString(), MatchMode.Anywhere))
            //    .List();
            var retval = new List<PatientViewModel>();
            var tasks = Session.QueryOver<Task>()
                .Where(x => x.Active == true)
                .And(x => x.Delayed == true)
                .And(x => x.DelayHandled == false)
                .JoinQueryOver<Patient>(x => x.Patient)
                    .Where(x => x.Deceased == false)
                .List();
            var patientIds = new HashSet<Guid>(tasks.Select(x => x.Patient.Id));
            var patientsForTasks = patients.Where(x => patientIds.Contains(x.Id));
            foreach (var account in accounts) {
                account.Taxon = taxonMap[account.Taxon.Id.ToString()];
            }
            foreach (var patient in patientsForTasks) {
                var isFirst = true;
                var address = string.Empty;
                var taxon = taxonMap[patient.Taxon.Id.ToString()];
                var paths = taxon.Path.Split('.');
                foreach (var path in paths) {
                    if (!isFirst) {
                        address += taxonMap[path].Name + " ";
                    }
                    isFirst = false;
                }
                var accountsWithRole = accounts.Where(x => taxon.Path.Contains(x.Taxon.Path));
                var account = (accountsWithRole.Count() > 0) ? accountsWithRole.First() : null;
                retval.Add(new PatientViewModel() {
                    Id = patient.Id,
                    FullName = patient.FullName,
                    UniqueIdentifier = patient.UniqueIdentifier,
                    Address = address,
                    Overseeing = (account.IsNotNull()) ? account.FullName : "Saknas"
                });
            }
            return retval;
        }

    }

    
}