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
using System.Web.Mvc;
using Appva.Cqrs;
using Appva.Persistence;
using Appva.Core.Resources;

namespace Appva.Mcss.Web.Controllers {

    public class SearchAccountCommand : IRequest<SearchViewModel<AccountViewModel>>
    {
        public Account Identity
        {
            get;
            set;
        }
        public string SearchQuery
        {
            get;
            set;
        }
        public int? Page
        {
            get;
            set;
        }
        public bool IsActive
        {
            get;
            set;
        }
        public bool IsPaused
        {
            get;
            set;
        }
        public Guid? FilterByDelegation
        {
            get;
            set;
        }
        public Guid? FilterByTitle
        {
            get;
            set;
        }
        public bool FilterByCreatedByIdentity
        {
            get;
            set;
        }
    }

    public sealed class SearchAccountHandler : RequestHandler<SearchAccountCommand, SearchViewModel<AccountViewModel>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchAccountHandler"/> class.
        /// </summary>
        public SearchAccountHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<PatientQuickSearch, IEnumerable<object>> Overrides.

        /// <inheritdoc /> 
        public override SearchViewModel<AccountViewModel> Handle(SearchAccountCommand message)
        {
            /*
            if (FilterCache.HasCache()) {
                var taxon = FilterCache.GetOrSet(message.Identity, this.persistence);
            }*/

            return new SearchViewModel<AccountViewModel>() {
                /*Items = AccountMapper.ToListOfAccountViewModel(this.persistence, items),
                PageNumber = pageIndex,
                PageSize = pageSize,
                TotalItemCount = accountList.Count*/
            };
        }

        #endregion
    }

}