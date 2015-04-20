using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using NHibernate.Criterion;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using Appva.Mcss.Infrastructure;

namespace Appva.Mcss.Web.Controllers {

    public class QuickSearchCommand<T> : Command<IEnumerable<object>> where T : Person {

        public string Term { get; set; }
        public bool IsActive { get; set; }

        public override void Execute() {
            var query = Session.QueryOver<T>().Where(x => x.Active == IsActive);
            if (Term.IsNotEmpty()) {
                Expression<Func<T, object>> expression = x => x.FullName;
                if (Term.First(2).Is(Char.IsNumber)) {
                    expression = x => x.UniqueIdentifier;
                }
                var result = query.Where(Restrictions.On<T>(expression)
                    .IsLike(Term, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName).Asc
                    .Take(10).List();
                Result = result.Select(x => new { value = x.FullName });
            }
        }

    }

}