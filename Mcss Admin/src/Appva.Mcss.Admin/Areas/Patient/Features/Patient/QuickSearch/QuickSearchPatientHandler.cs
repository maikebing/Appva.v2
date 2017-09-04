// <copyright file="QuickSearchPatientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

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
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class QuickSearchPatientHandler : RequestHandler<QuickSearchPatient, IEnumerable<object>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickSearchPatientHandler"/> class.
        /// </summary>
        /// <param name="identity">The <see cref="IIdentityService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public QuickSearchPatientHandler(IIdentityService identity, ITaxonFilterSessionHandler filtering, IPersistenceContext persistence)
        {
            this.identity = identity;
            this.filtering = filtering;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc /> 
        public override IEnumerable<object> Handle(QuickSearchPatient message)
        {
            if (message.Term.IsEmpty())
            {
                return new List<object>();
            }
            var isActive = message.IsActive ?? true;
            var isDeceased = message.IsDeceased ?? false;
            var query = this.persistence.QueryOver<Patient>()
                .Where(x => x.IsActive == isActive).And(y => y.IsArchived == false);
            if (isActive)
            {
                query.Where(x => x.Deceased == isDeceased);
            }
            Expression<Func<Patient, object>> expression = x => x.FullName;
            if (message.Term.First(2).Is(Char.IsNumber))
            {
                expression = x => x.PersonalIdentityNumber.Value;
            }
            if (this.filtering.HasActiveFilter())
            {
                var taxon = this.filtering.GetCurrentFilter();
                query.JoinQueryOver<Taxon>(x => x.Taxon)
                    .Where(Restrictions.On<Taxon>(x => x.Path)
                        .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
            }
            return query.WhereRestrictionOn(expression)
                .IsLike(message.Term, MatchMode.Anywhere)
                .OrderBy(x => x.LastName).Asc
                .Take(10)
                .List()
                .Select(x => new { 
                    value = x.FullName 
                })
                .ToList();
        }

        #endregion
    }
}