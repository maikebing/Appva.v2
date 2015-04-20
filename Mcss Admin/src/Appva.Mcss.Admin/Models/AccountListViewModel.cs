using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;
using System.Web.Mvc;

namespace Appva.Mcss.Web.ViewModels {
    public class AccountListViewModel {
        public IList<SelectListItem> Delegations { get; set; }
        public IList<SelectListItem> Titles { get; set; }
        public SearchViewModel<AccountViewModel> Search { get; set; }
        public Guid? FilterByTitle { get; set; }
        public Guid? FilterByDelegation { get; set; }
        public bool FilterByCreatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaused { get; set; }
    }
}