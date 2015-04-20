using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using NHibernate.Criterion;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using Appva.Mcss.Infrastructure;
using System.Web;

namespace Appva.Mcss.Web.Controllers {
    public class PatientQuickSearch : Command<IEnumerable<object>> {

        public Account Identity { get; set; }
        public string Term { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeceased { get; set; }

        public override void Execute() {
            var query = Session.QueryOver<Patient>().Where(x => x.Active == IsActive && x.Deceased == IsDeceased);
            if (Term.IsNotEmpty()) {
                Expression<Func<Patient, object>> expression = x => x.FullName;
                if (Term.First(2).Is(Char.IsNumber)) {
                    expression = x => x.UniqueIdentifier;
                }
                if (FilterCache.HasCache()) {
                    var taxon = FilterCache.GetOrSet(Identity, Session);
                    query.JoinQueryOver<Taxon>(x => x.Taxon)
                        .Where(Restrictions.On<Taxon>(x => x.Path)
                            .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
                }
                var result = query.Where(Restrictions.On<Patient>(expression)
                    .IsLike(Term, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName).Asc
                    .Take(10).List();
                Result = result.Select(x => new { value = x.FullName });
            }
        }

    }
}