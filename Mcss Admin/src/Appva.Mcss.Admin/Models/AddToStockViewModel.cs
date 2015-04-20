using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    
    public class AddToStockViewModel : InventoryTransactionItemViewModel {

        public Sequence Sequence { get; set; }


    }

}