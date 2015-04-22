using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using NHibernate.Criterion;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using System.Web;
using Appva.Cqrs;
using Appva.Persistence;

namespace Appva.Mcss.Web.Controllers {
    public class PatientQuickSearch : IRequest<IEnumerable<object>>
    {

        public Account Identity { get; set; }
        public string Term { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeceased { get; set; }
    }
    public sealed class IsUniqueIdentifierhandler : RequestHandler<PatientQuickSearch, IEnumerable<object>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateOrActivatehandler"/> class.
        /// </summary>
        public IsUniqueIdentifierhandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<PatientQuickSearch, IEnumerable<object>> Overrides.

        /// <inheritdoc /> 
        public override IEnumerable<object> Handle(PatientQuickSearch message)
        {
            var query = this.persistence.QueryOver<Patient>()
                .Where(x => x.IsActive == message.IsActive && x.Deceased == message.IsDeceased);
            if (message.Term.IsNotEmpty())
            {
                Expression<Func<Patient, object>> expression = x => x.FullName;
                if (message.Term.First(2).Is(Char.IsNumber))
                {
                    expression = x => x.PersonalIdentityNumber;
                }
                if (FilterCache.HasCache()) {
                    var taxon = FilterCache.GetOrSet(message.Identity, this.persistence);
                    query.JoinQueryOver<Taxon>(x => x.Taxon)
                        .Where(Restrictions.On<Taxon>(x => x.Path)
                            .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
                }
                var result = query.Where(Restrictions.On<Patient>(expression)
                    .IsLike(message.Term, MatchMode.Anywhere))
                    .OrderBy(x => x.LastName).Asc
                    .Take(10).List();
                return result.Select(x => new { value = x.FullName }).ToList();
            }
            return new List<object>();
        }

        #endregion
    }
}