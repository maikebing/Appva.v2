using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using NHibernate.Criterion;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using Appva.Cqrs;
using Appva.Persistence;

namespace Appva.Mcss.Web.Controllers {

    public class QuickSearchCommand<T> : IRequest<IEnumerable<object>> where T : Person<T>
    {
        public string Term { get; set; }
        public bool IsActive { get; set; }
    }

    public sealed class QuickSearchHandler<T> : RequestHandler<QuickSearchCommand<T>, IEnumerable<object>>
         where T : Person<T>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickSearchCommand"/> class.
        /// </summary>
        public QuickSearchHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<PatientQuickSearch, IEnumerable<object>> Overrides.

        /// <inheritdoc /> 
        public override IEnumerable<object> Handle(QuickSearchCommand<T> message)
        {
            var query = this.persistence.QueryOver<Account>()
                .Where(x => x.IsActive == message.IsActive);
            if (message.Term.IsNotEmpty()) {
                Expression<Func<T, object>> expression = x => x.FullName;
                if (message.Term.First(2).Is(Char.IsNumber))
                {
                    //expression = x => x.PersonalIdentityNumber;
                }
                var result = query.Where(Restrictions.On<T>(expression)
                    .IsLike(message.Term, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName).Asc
                    .Take(10).List();
                return result.Select(x => new { value = x.FullName });
            }
            return null;
        }

        #endregion
    }

}