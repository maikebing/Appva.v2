using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appva.Mcss.Web.ViewModels
{
    public class ListInventoryViewModel
    {
        public ListInventoryViewModel()
        {
            OperationTranslation = new Dictionary<string, string>() {
                {"withdrawal", "Uttag"},
                {"add", "Insättning"},
                {"recount", "Kontrollräkning"},
                {"readd", "Återförd mängd"}
            };
        }
        public PatientViewModel Patient { get; set; }
        public IList<Inventory> Inventories { get; set; }
        public Inventory Inventory { get; set; }
        public IList<InventoryTransactionItem> Transactions { get; set; }

        public IDictionary<string, string> OperationTranslation { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public IEnumerable<SelectListItem> Months { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public int TotalTransactionCount { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }
    }
}