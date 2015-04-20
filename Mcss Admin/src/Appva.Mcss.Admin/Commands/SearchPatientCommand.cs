using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NHibernate.Criterion;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Business;
using Appva.Mcss.Web.ViewModels;
using Appva.Core.Extensions;
using Appva.Mcss.Web.Mappers;
using Appva.Mcss.Infrastructure;

namespace Appva.Mcss.Web.Controllers {

    public class SearchPatientCommand : Command<SearchViewModel<PatientViewModel>> {

        public string SearchQuery { get; set; }
        public int? Page { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeceased { get; set; }

        public override void Execute() {
            var pageSize = 10;
            var pageIndex = Page ?? 1;
            var firstResult = (pageIndex - 1) * pageSize;
            var query = Session.QueryOver<Patient>()
                .Where(x => x.Active == IsActive)
                .And(x => x.Deceased == IsDeceased);
            if (SearchQuery.IsNotEmpty()) {
                Expression<Func<Patient, object>> expression = x => x.FullName;
                if (SearchQuery.First(2).Is(Char.IsNumber)) {
                    expression = x => x.UniqueIdentifier;
                }
                query.Where(Restrictions.On<Patient>(expression).IsLike(SearchQuery, MatchMode.Anywhere)).OrderBy(x => x.LastName);
            }
            if (FilterCache.HasCache()) {
                var taxon = FilterCache.Get(Session);
                query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
            }
            var items = query.OrderBy(x => x.HasUnattendedTasks).Desc
                .ThenBy(x => x.LastName).Asc
                .Skip(firstResult).Take(pageSize).Future<Patient>().ToList();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();
            Result = new SearchViewModel<PatientViewModel>() {
                Items = PatientMapper.ToListOfPatientViewModel(Session, items),
                PageNumber = pageIndex,
                PageSize = pageSize,
                TotalItemCount = totalCount.Value
            };
        }

    }

}