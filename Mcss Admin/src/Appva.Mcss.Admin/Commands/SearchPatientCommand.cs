using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NHibernate.Criterion;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;
using Appva.Core.Extensions;
using Appva.Mcss.Web.Mappers;
using Appva.Cqrs;
using Appva.Persistence;

namespace Appva.Mcss.Web.Controllers {

    public class SearchPatientCommand : IRequest<SearchViewModel<PatientViewModel>>
    {
        public string SearchQuery { get; set; }
        public int? Page { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeceased { get; set; }
    }

    public sealed class SearchPatientHandler : RequestHandler<SearchPatientCommand, SearchViewModel<PatientViewModel>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPatientHandler"/> class.
        /// </summary>
        public SearchPatientHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<PatientQuickSearch, IEnumerable<object>> Overrides.

        /// <inheritdoc /> 
        public override SearchViewModel<PatientViewModel> Handle(SearchPatientCommand message)
        {
            var pageSize = 10;
            var pageIndex = message.Page ?? 1;
            var firstResult = (pageIndex - 1) * pageSize;
            var query = this.persistence.QueryOver<Patient>()
                .Where(x => x.IsActive == message.IsActive)
                .And(x => x.Deceased == message.IsDeceased);
            if (message.SearchQuery.IsNotEmpty()) {
                Expression<Func<Patient, object>> expression = x => x.FullName;
                if (message.SearchQuery.First(2).Is(Char.IsNumber)) {
                    expression = x => x.PersonalIdentityNumber;
                }
                query.Where(Restrictions.On<Patient>(expression).IsLike(message.SearchQuery, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName);
            }
            if (FilterCache.HasCache()) {
                var taxon = FilterCache.Get(this.persistence);
                query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
            }
            var items = query.OrderBy(x => x.HasUnattendedTasks).Desc
                .ThenBy(x => x.LastName).Asc
                .Skip(firstResult).Take(pageSize).Future<Patient>().ToList();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();
            return new SearchViewModel<PatientViewModel>() {
                Items = PatientMapper.ToListOfPatientViewModel(this.persistence, items),
                PageNumber = pageIndex,
                PageSize = pageSize,
                TotalItemCount = totalCount.Value
            };
        }

        #endregion
    }
}