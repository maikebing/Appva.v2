using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Web.ViewModels;
using NHibernate;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Application.Services;
using Appva.Persistence;
using Appva.Mcss.Admin.Domain.Repositories;
using Appva.Core.Extensions;
using Appva.Mcss.Admin.Application.Common;

namespace Appva.Mcss.Web.Mappers {
    
    public class AccountMapper {

        public static AccountViewModel ToAccountViewModel(IPersistenceContext session, Account account)
        {
            var superiors = new RoleService(new RoleRepository(session)).MembersOfRole("_superioraccount");
            var taxonMap = new TaxonomyService(PatientMapper.RTC, new TaxonRepository(session)).List(TaxonomicSchema.Organization)
                .ToDictionary(x => x.Id.ToString(), x => x);
            var taxon = taxonMap[account.Taxon.Id.ToString()];
            var superiorList = superiors.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
            var superior = (superiorList.Count() > 0) ? superiorList.First() : null;
            return new AccountViewModel() {
                Id = account.Id,
                Active = account.IsActive,
                FullName = account.FullName,
                UniqueIdentifier = account.PersonalIdentityNumber,
                Title = TitleHelper.GetTitle(account.Roles),
                Superior = (superior.IsNotNull()) ? superior.FullName : "Saknas",
                Account = account
            };
        }

        public static IList<AccountViewModel> ToListOfAccountViewModel(IPersistenceContext session, IList<AccountViewModel> accounts)
        {
            var retval = new List<AccountViewModel>();
            var taxonMap = new TaxonomyService(PatientMapper.RTC, new TaxonRepository(session)).List(TaxonomicSchema.Organization)
                .ToDictionary(x => x.Id.ToString(), x => x);
            var superiors = new RoleService(new RoleRepository(session)).MembersOfRole("_superioraccount");
            foreach (var account in accounts) {
                var taxon = taxonMap[account.Account.Taxon.Id.ToString()];
                var superiorList = superiors.Where(x => taxon.Path.Contains(x.Taxon.Path)).ToList();
                var superior = (superiorList.Count() > 0) ? superiorList.First() : null;
                retval.Add(new AccountViewModel() {
                    Id = account.Id,
                    Active = account.Active,
                    FullName = account.FullName,
                    UniqueIdentifier = account.UniqueIdentifier,
                    Title = account.Title,
                    ShowAlertOnDaysLeft = account.ShowAlertOnDaysLeft,
                    DaysLeft = account.DaysLeft,
                    Superior = (superior.IsNotNull()) ? superior.FullName : "Saknas",
                    IsPaused = account.IsPaused
                });
            }
            return retval;
        }

    }

}