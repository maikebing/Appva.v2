using System;
using Appva.Mcss.Admin.Domain.Entities;
using System.Collections.Generic;

namespace Appva.Mcss.Web.ViewModels {
    public class SearchViewModel<T> where T : class {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItemCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
